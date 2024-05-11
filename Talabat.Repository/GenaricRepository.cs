using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;
using Talabat.Core.IReposities;
using Talabat.Core.Spacifications;
using Talabat.Repository.Data;

namespace Talabat.Repository

    // Genaric Reposatory Class
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;
         
        // to create object from The Db Context Class That in the Data Folder Emplicitly
        // Dependancy injection
       public GenaricRepository(StoreContext DbContext)
        {
            _dbContext = DbContext;
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int Id)
        {
            ///if(typeof(T) == typeof(Product))
            ///return await _dbContext.Set<Product>().Where(P => P.Id == Id)
            ///                                     .Include(P => P.Brand)
            ///                                      .Include(P => P.Category)
            ///                                      .FirstOrDefaultAsync() as T;

            return await _dbContext.Set<T>().FindAsync(Id);
        }

         
        // For specification Methods (to include the Navigation Property)
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpacification<T> Spec)
        {
            return await ApplyQuery(Spec).ToListAsync();
        }


        // For specification Methods (to include the Navigation Property)
        public async Task<T?> GetByIDWithSpecAsync(ISpacification<T> Spec)
        {
            return await ApplyQuery(Spec).FirstOrDefaultAsync();

        }

        // For Add And Update And Delete

        public async Task DeleteAsync(T Entity)
        {
            _dbContext.Set<T>().Remove(Entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task AddAsync(T Entity)
        {
            _dbContext.Set<T>().Add(Entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T Entity)
        {
            _dbContext.Set<T>().Update(Entity);
            await _dbContext.SaveChangesAsync();

        }


        //public async Task<T?> GetPaginationCountAsync(ISpacification<T> Spec)
        //{
        //    return await ApplyQuery(Spec).CountAsync();
        //}

        async Task<int> IGenaricRepository<T>.GetPaginationCountAsync(ISpacification<T> Spec)
        {
            return await ApplyQuery(Spec).CountAsync();
        }


        // we make helper method to have the common Code
        private IQueryable<T> ApplyQuery(ISpacification<T> Spec)
        {
            return SpacificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }


    }

}
