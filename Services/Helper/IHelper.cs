namespace TaskManagement.Services.Helper
{
    public interface IHelper
    {
        int GetActiveUser();
        void SetHttpErrorResponse<T>(ServiceResponse<T> response, string message);
    }
}
