using DiplomaWebService.Common.Enum;

namespace DiplomaWebService.Common.Results
{
    public class Result
    {
        public int ErrorCode { get; set; } = (int)ErrorCodes.Success;
        public string ErrorMessage { get; set; }
    }

    public class Result<T>
    {
        public int ErrorCode { get; set; } = (int)ErrorCodes.Success;
        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}
