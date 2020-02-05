using System.Collections.Generic;

namespace Snow.Blog.Web.Admin.Middleware.Wrappers
{
    public class ApiException : System.Exception
    {
        public int StatusCode { get; set; }
        public bool IsModelValidatonError { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }
        public string ReferenceErrorCode { get; set; }
        public string ReferenceDocumentLink { get; set; }

        public ApiException(string message,
                            int statusCode = 500,
                            string errorCode = "",
                            string refLink = "") :
            base(message)
        {
            IsModelValidatonError = false;
            StatusCode = statusCode;
            ReferenceErrorCode = errorCode;
            ReferenceDocumentLink = refLink;
        }

        public ApiException(IEnumerable<ValidationError> errors, int statusCode = 400)
        {
            IsModelValidatonError = true;
            StatusCode = statusCode;
            Errors = errors;
        }

        public ApiException(System.Exception ex, int statusCode = 500) : base(ex.Message)
        {
            IsModelValidatonError = false;
            StatusCode = statusCode;
        }
    }
}
