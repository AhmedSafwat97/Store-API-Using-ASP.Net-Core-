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


        public async Task<T?> GetAsync(int Id)
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

        // we make helper method to have the common Code
        private IQueryable<T> ApplyQuery(ISpacification<T>  Spec)
        {
            return SpacificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }



    }

}
