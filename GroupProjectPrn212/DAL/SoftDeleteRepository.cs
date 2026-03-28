using GroupProjectPrn212.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProjectPrn212.DAL
{
    public class SoftDeleteRepository<T> : GenericRepository<T> where T : class
    {
        public virtual void SoftDelete(T entity, string deletedBy = "admin")
        {
            var type = typeof(T);

            type.GetProperty("IsDeleted")?.SetValue(entity, true);
            type.GetProperty("DeletedAt")?.SetValue(entity, DateTime.Now);
            type.GetProperty("DeletedBy")?.SetValue(entity, deletedBy);

            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
