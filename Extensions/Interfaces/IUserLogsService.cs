public interface IUserLogsService
{
    Task LogActivityAsync(UserLogsModel log);
    Task<List<UserLogsModel>> GetLogsAsync();
    Task LogLoginAsync(string userId, string userName);
    Task LogLogoutAsync(string userId, string sessionId);
    Task LogCrudOperationAsync(string userId, string userName, string crudOperation); // Add this line
}