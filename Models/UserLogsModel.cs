public class UserLogsModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string PagesVisited { get; set; }
    public string DataAccessed { get; set; }
    public string CrudOperation { get; set; }
    public DateTime CrudTimestamp { get; set; }
    public DateTime? LoginTimestamp { get; set; }
    public DateTime? LogoutTimestamp { get; set; }
    public string SessionId { get; set; }

    public string FormattedPagesVisited
    {
        get
        {
            if (string.IsNullOrEmpty(PagesVisited))
                return string.Empty;

            var pages = PagesVisited.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(", ", pages.Select(p => p == "/" ? "HomePage" : p));
        }
    }
}