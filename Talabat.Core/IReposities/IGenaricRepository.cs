using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;
using Talabat.Core.Spacifications;


// genaric Reposatory
namespace Talabat.Core.IReposities
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        // get By Id 
        Task<T?> GetByIdAsync(int Id);

        //Delete
        Task DeleteAsync(T Entity);

        //Add
        Task AddAsync(T Entity);

        //Update
        Task UpdateAsync(T Entity);

        // Get All 
        Task<IEnumerable<T>> GetAllAsync();

        // for using Specification design pattern
        // we send Type of ISpacification and any class that implement this Interface
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpacification<T> Spec);

        // for using Specification design pattern
        Task<T?> GetByIDWithSpecAsync(ISpacification<T> Spec);


    }
}
