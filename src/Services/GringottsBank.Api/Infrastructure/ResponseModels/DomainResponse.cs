namespace GringottsBank.Api.Infrastructure.ResponseModels
{
    public record DomainResponse<T>
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static DomainResponse<T> Ok(T result)
        {
            return new DomainResponse<T>(result);
        }

        public static DomainResponse<T> Fail(string errorMessage)
        {
            return new DomainResponse<T>(errorMessage);
        }

        private DomainResponse(T result) : base()
        {
            IsSuccess = true;
            Result = result;
        }

        private DomainResponse(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}
