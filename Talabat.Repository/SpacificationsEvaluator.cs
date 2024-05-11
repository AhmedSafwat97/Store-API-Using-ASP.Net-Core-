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
        // Spec => ISpacification is sending from the controller As Object of class That Implement I Spacification
        // Query => this the DbSet
        public static IQueryable<T> GetQuery (IQueryable<T> BaseQuery , ISpacification<T> Spec) {

           var Query = BaseQuery;

            //Where Condition
            if (Spec.Criteria is not null)
            Query = BaseQuery.Where(Spec.Criteria); //DbContext.Set<>().where(P= => P.Id)

            // Order By
            if (Spec.OrderBy != null)
            Query = Query.OrderBy(Spec.OrderBy); //DbContext.Set<>().where(P= => P.Id).OrderBy(P => P.Price)

            if (Spec.OrderByDesc != null)
                Query = Query.OrderByDescending(Spec.OrderByDesc); //DbContext.Set<>().where(P= => P.Id).OrderByDescending(P => P.Price)


            // If Pagination Enable 
            if (Spec.IsPagination)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            /// To _dbContext.Set<Product>() + .Where(P => P.Id == Id) + .Include(P => P.Brand)
            /// Aggregate fun take the Query that i want to add to it The includes functions


            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExprition) => CurrentQuery.Include(IncludeExprition));


            return Query;

        }



    }
}
