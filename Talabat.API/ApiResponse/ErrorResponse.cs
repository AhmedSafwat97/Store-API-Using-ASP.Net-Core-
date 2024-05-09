
namespace Talabat.API.ApiResponse
{
    public class ObjResponse
    {

        public int statusCode { get; set; }

        public string? message { get; set; }


        public ObjResponse(int Code , string? Mesaage = null)
        {
            statusCode = Code;
            message = Mesaage ?? GetMessageFromStatusCode(Code);
        }

        private string? GetMessageFromStatusCode(int Code)
        {

            return Code switch
            {
                200 => "Success",
                401 => "Not Found",
                404 => "Bad Request",
                _ => null
            };

        }
    }
}
