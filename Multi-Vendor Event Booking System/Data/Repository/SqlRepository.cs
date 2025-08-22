using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public class SqlRepository : ISqlRepository
    {
        private readonly EventDBContext _context;

        public SqlRepository(EventDBContext context)
        {
            _context = context;
        }

        public async Task<int> ExecuteNonQueryAsync(string spName, params object[] parameters)
        {
            // Build a parameter string like "@Id"
            var paramNames = parameters
                .OfType<SqlParameter>()
                .Select(p => p.ParameterName)
                .ToArray();

            var paramPlaceholders = string.Join(", ", paramNames);

            return await _context.Database.ExecuteSqlRawAsync(
                $"EXEC {spName} {paramPlaceholders}", parameters
            );
        }


        public async Task<T> ExecuteScalarAsync<T>(string spName, params object[] parameters)
        {
            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            var result = await cmd.ExecuteScalarAsync();

            if (result == null || result == DBNull.Value)
                return default!;

            return (T)Convert.ChangeType(result, typeof(T));
        }


        public async Task<List<T>> ExecuteListAsync<T>(string spName, params object[] parameters) where T : class, new()
        {
            return await _context.Database
                .SqlQueryRaw<T>($"EXEC {spName}", parameters)
                .ToListAsync();
        }

        public async Task<T> ExecuteSingleAsync<T>(string spName, params object[] parameters) where T : class, new()
        {
            return await _context.Database
                .SqlQueryRaw<T>($"EXEC {spName}", parameters)
                .FirstOrDefaultAsync();
        }

        public async Task<(List<T1>, List<T2>)> ExecuteMultiResultAsync<T1, T2>(string spName, params object[] parameters)
    where T1 : class, new()
    where T2 : class, new()
        {
            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = spName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (var param in parameters)
                cmd.Parameters.Add(param);

            using var reader = await cmd.ExecuteReaderAsync();

            var result1 = new List<T1>();
            var result2 = new List<T2>();

            // Map first result set
            var t1Props = typeof(T1).GetProperties();
            var t1Columns = Enumerable.Range(0, reader.FieldCount)
                                      .Select(reader.GetName)
                                      .ToHashSet(StringComparer.OrdinalIgnoreCase);

            while (await reader.ReadAsync())
            {
                var obj = new T1();
                foreach (var prop in t1Props)
                {
                    if (t1Columns.Contains(prop.Name) && !reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                    {
                        prop.SetValue(obj, reader.GetValue(reader.GetOrdinal(prop.Name)));
                    }
                }
                result1.Add(obj);
            }

            // Move to second result set
            await reader.NextResultAsync();

            var t2Props = typeof(T2).GetProperties();
            var t2Columns = Enumerable.Range(0, reader.FieldCount)
                                      .Select(reader.GetName)
                                      .ToHashSet(StringComparer.OrdinalIgnoreCase);

            while (await reader.ReadAsync())
            {
                var obj = new T2();
                foreach (var prop in t2Props)
                {
                    if (t2Columns.Contains(prop.Name) && !reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                    {
                        prop.SetValue(obj, reader.GetValue(reader.GetOrdinal(prop.Name)));
                    }
                }
                result2.Add(obj);
            }

            return (result1, result2);
        }

    }
}
