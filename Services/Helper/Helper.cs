namespace TaskManagement.Services.Helper
{
    public class Helper : IHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Helper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetActiveUser() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public void SetHttpErrorResponse<T>(ServiceResponse<T> response, string message)
        {
            response.Success = false;
            response.Message = message;
        }
    }

}
