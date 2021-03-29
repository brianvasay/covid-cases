namespace Application.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GenerateDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        private string GenerateDefaultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request has been made.",
                401 => "An unauthorized request has been made.",
                404 => "A requested resource cannot be found.",
                500 => "An error has occurred.",
                _ => ""
            };
        }
    }
}
