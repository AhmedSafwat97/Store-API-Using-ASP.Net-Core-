using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;

namespace Talabat.Core.Spacifications
{

    // we make this Class To Have the commone codee and to be inherited by the Child Classes
    // and to give the ineshial value in the constractor to chain on it from the child Class
    public class BaseSpacification<T> : ISpacification<T> where T : BaseEntity  // BaseEntity => The Parant Class For All Entites
    {

        // Where codition
        public Expression<Func<T, bool>> Criteria { get; set; } = null;

        // Includes Conditons
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        // OrderBy Conditon
        public Expression<Func<T, object>> OrderBy { get; set; } = null;

        // OrderbyDesc
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

        // Page Size For Pagination
        public int Skip { get; set; }

        // Page Index For Pagination
        public int Take { get; set; }


        // To check If the Pagination Is Enable or Not
        public bool IsPagination { get; set; }


        // we have to chain to this constractor from the child class to set the includes values
        // and set the Criteria (Where Codition = Nul)
        public BaseSpacification()
        {

        }

        // we have to chain to this constractor from the child class to set the Criteria (Where Condition) values
        public BaseSpacification(Expression<Func<T, bool>> CriteriaEx)
        {
            Criteria = CriteriaEx;
        }

        // For Pagination
        public void ApplyPagination(int skip, int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }


        // we call the methods of the
        // spacifications in the Repostory Project in Class
        // that Named SpacificationsEvaluator


    }
}
