using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public class EventRepository<T> : IEventRepository<T> where T : class
    {
        private readonly EventDBContext _dbContext;
        private DbSet<T> _dbSet;

        public EventRepository(EventDBContext dBContext)
        {
            _dbContext = dBContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T dbRecord)
        {
            _dbSet.Add(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<T> UpdateAsync(T dbRecord)
        {
            _dbContext.Update(dbRecord);
            await _dbContext.SaveChangesAsync();

            return dbRecord;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>filter,bool useNoTracking = false)
        {
            if(useNoTracking)
            {
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            }
            else
            {
                return await _dbSet.Where(filter).FirstOrDefaultAsync();
            }
        }
    }
}
