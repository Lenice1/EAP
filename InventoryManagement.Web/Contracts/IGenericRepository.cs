namespace EmployeeAttendancePortal.Web.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task AddRangeAsync(List<T> entities);

        Task<List<T>> GetAllAsync();
       // Task<int> GetCountAsync();
        Task<bool> Exists(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task<T> AddAsync(T entity);

    }
}
