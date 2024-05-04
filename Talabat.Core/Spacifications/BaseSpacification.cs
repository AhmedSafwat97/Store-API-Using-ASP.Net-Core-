using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;

namespace Talabat.Core.Spacifications
{
    public class BaseSpacification<T> : ISpacification<T> where T : BaseEntity  // BaseEntity => The Parant Class For All Entites
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();


        public BaseSpacification()
        {
        }


        public BaseSpacification(Expression<Func<T, bool>> CriteriaEx)
        {
            Criteria = CriteriaEx;
        }


        // we call the methods of the
        // spacifications in the Repostory Project in Class
        // that Named SpacificationsEvaluator

    }
}
