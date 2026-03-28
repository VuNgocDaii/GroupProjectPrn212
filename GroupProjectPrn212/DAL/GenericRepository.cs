using GroupProjectPrn212.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GroupProjectPrn212.DAL
{
    public class GenericRepository<T> where T : class
    {
        protected readonly QuanLyTrungTamTinHocNgoaiNguContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository()
        {
            _context = new QuanLyTrungTamTinHocNgoaiNguContext();
            _dbSet = _context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual List<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            var entry = _context.Entry(entity);
            var key = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey();

            if (key != null)
            {
                var keyValues = key.Properties
                    .Select(p => typeof(T).GetProperty(p.Name)?.GetValue(entity))
                    .ToArray();

                var localEntity = _dbSet.Local.FirstOrDefault(e =>
                {
                    var localKeyValues = key.Properties
                        .Select(p => typeof(T).GetProperty(p.Name)?.GetValue(e))
                        .ToArray();

                    return localKeyValues.SequenceEqual(keyValues);
                });

                if (localEntity != null)
                {
                    _context.Entry(localEntity).State = EntityState.Detached;
                }
            }

            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
    }
}
