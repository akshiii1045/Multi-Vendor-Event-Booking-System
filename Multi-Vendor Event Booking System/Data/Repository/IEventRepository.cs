using System.Linq.Expressions;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public interface IEventRepository<T>
    {

        Task<T> CreateAsync(T dbRecord);

        Task<T> UpdateAsync(T dbRecord);

        Task<bool> DeleteAsync(T dbRecord);

        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);
    }
}
