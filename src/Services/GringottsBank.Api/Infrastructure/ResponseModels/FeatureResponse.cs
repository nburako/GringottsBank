using System.Net;

namespace GringottsBank.Api.Infrastructure.ResponseModels
{
    public record FeatureResponse<T>
    {
        public T ResponseDto { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static FeatureResponse<T> Ok(T response)
        {
            return new FeatureResponse<T>(response);
        }

        public static FeatureResponse<T> Fail(string errorMessage)
        {
            return new FeatureResponse<T>(errorMessage);
        }

        private FeatureResponse(T responseDto) : base()
        {
            StatusCode = HttpStatusCode.OK;
            IsSuccess = true;
            ResponseDto = responseDto;
        }

        private FeatureResponse(string errorMessage)
        {
            StatusCode = HttpStatusCode.BadRequest;
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}
