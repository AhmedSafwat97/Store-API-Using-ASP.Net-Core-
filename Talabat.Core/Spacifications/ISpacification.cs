using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;

namespace Talabat.Core.Spacifications
{

    // to make Spacification Design Pattern
    public interface ISpacification<T> where T : BaseEntity
    {
        // Make Property signature for every spec

        // The Method (Delegate (Lamda Expretion0) that used in where
        public Expression<Func<T, bool>> Criteria { get; set; } // P => P.Id == 1

        // The Method (Delegate (Lamda Expretion0) that used in Include
        public List<Expression<Func<T, object>>> Includes { get; set; }


    }
}



