using System.Data.Common;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public interface ISqlRepository
    {
        Task<int> ExecuteNonQueryAsync(string spName, params object[] parameters);
        Task<T> ExecuteScalarAsync<T>(string spName, params object[] parameters);
        Task<List<T>> ExecuteListAsync<T>(string spName, params object[] parameters) where T : class, new();
        Task<T> ExecuteSingleAsync<T>(string spName, params object[] parameters) where T : class, new();
        Task<(List<T1>, List<T2>)> ExecuteMultiResultAsync<T1, T2>(string spName, params object[] parameters)
            where T1 : class, new()
            where T2 : class, new();
    }
}

