using System.Linq.Expressions;
using Talabat.Core.Enities;
using Talabat.Core.Spacifications;
using Talabat.Core.Spacifications.EntityParams;

namespace Talabat.API.ApiResponse
{
    public class PaginationCountResponse<T> : BaseSpacification<T> where T : BaseEntity   
    {

        public PaginationCountResponse(Expression<Func<T , bool>> Criteria) :base(Criteria)
        {
            
        }





    }
}
