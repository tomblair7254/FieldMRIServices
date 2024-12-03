public class UserLogs
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string PagesVisited { get; set; }
    public string DataAccessed { get; set; }
    public string CrudOperation { get; set; }
    public DateTime CrudTimestamp { get; set; }
    public DateTime? LoginTimestamp { get; set; }
    public string SessionId { get; set; }
    public DateTime? LogoutTimestamp { get; set; }
}