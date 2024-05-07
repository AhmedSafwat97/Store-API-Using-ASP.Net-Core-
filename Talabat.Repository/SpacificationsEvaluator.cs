using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;
using Talabat.Core.Spacifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Talabat.Repository
{

    // i do not want to create object from this class
    // to have the functions That create the Spec

    internal static class SpacificationsEvaluator<T> where T : BaseEntity
    {

        // Query => this the DbSet
        public static IQueryable<T> GetQuery (IQueryable<T> BaseQuery , ISpacification<T> Spec) {

           var Query = BaseQuery;

            if (Spec.Criteria is not null)
            Query = BaseQuery.Where(Spec.Criteria); //DbContext.Set<>(

           /// To _dbContext.Set<Product>() + .Where(P => P.Id == Id) + .Include(P => P.Brand)
           /// Aggregate fun take the Query that i want to add to it The includes functions
                                                
            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExprition) => CurrentQuery.Include(IncludeExprition));


            return Query;

        }



    }
}
