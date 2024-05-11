using Talabat.API.Dtos;

namespace Talabat.API.ApiResponse
{
    public class PaginationResponse<T>
    {

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; } = 0;

        public IEnumerable<T> Data { get; set; }


        public PaginationResponse(int pageindex, int pageSize, int count ,IEnumerable<T> data)
        {
            PageIndex = pageindex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

    }
}
