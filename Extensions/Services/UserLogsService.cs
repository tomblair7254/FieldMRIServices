using AutoMapper;
using FieldMRIServices.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class UserLogsService : IUserLogsService
{
    private readonly FMIventoryDbContext _context;
    private readonly IMapper _mapper;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<UserLogsService> _logger;

    public UserLogsService(FMIventoryDbContext context, IMapper mapper, SignInManager<IdentityUser> signInManager, ILogger<UserLogsService> logger)
    {
        _context = context;
        _mapper = mapper;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task LogActivityAsync(UserLogsModel log)
    {
        var logEntity = await _context.UserLogs.FirstOrDefaultAsync(l => l.UserId == log.UserId && l.LogoutTimestamp == null);
        if (logEntity != null)
        {
            //_logger.LogInformation("Existing log found for UserId: {UserId}. Updating PagesVisited and CrudOperation.", log.UserId);
            logEntity.PagesVisited = string.IsNullOrEmpty(logEntity.PagesVisited)
                ? log.PagesVisited
                : $"{logEntity.PagesVisited},{log.PagesVisited}";
            logEntity.CrudOperation = string.IsNullOrEmpty(logEntity.CrudOperation)
                ? log.CrudOperation
                : $"{logEntity.CrudOperation},{log.CrudOperation}";
            logEntity.DataAccessed = string.IsNullOrEmpty(logEntity.DataAccessed)
                ? log.DataAccessed
                : $"{logEntity.DataAccessed},{log.DataAccessed}";
            //_logger.LogInformation("Updated PagesVisited: {PagesVisited}, CrudOperation: {CrudOperation}, DataAccessed: {DataAccessed}", logEntity.PagesVisited, logEntity.CrudOperation, logEntity.DataAccessed);
        }
        else
        {
            // _logger.LogInformation("No existing log found for UserId: {UserId}. Creating new log entry.", log.UserId);
            logEntity = _mapper.Map<UserLogs>(log);
            _context.UserLogs.Add(logEntity);
            // _logger.LogInformation("New log entry created with PagesVisited: {PagesVisited}, CrudOperation: {CrudOperation}, DataAccessed: {DataAccessed}", logEntity.PagesVisited, logEntity.CrudOperation, logEntity.DataAccessed);
        }
        await _context.SaveChangesAsync();
        // _logger.LogInformation("Log activity saved for UserId: {UserId}", log.UserId);
    }

    public async Task<List<UserLogsModel>> GetLogsAsync()
    {
        var logEntities = await _context.UserLogs.ToListAsync();
        return _mapper.Map<List<UserLogsModel>>(logEntities);
    }

    public async Task LogLoginAsync(string userId, string userName)
    {
        var existingLog = await _context.UserLogs
            .FirstOrDefaultAsync(l => l.UserId == userId && l.LogoutTimestamp == null);

        if (existingLog != null)
        {
            existingLog.LogoutTimestamp = DateTime.UtcNow;
            _context.UserLogs.Update(existingLog);
            await _context.SaveChangesAsync();
        }

        var sessionId = Guid.NewGuid().ToString();
        var logModel = new UserLogsModel
        {
            UserId = userId,
            UserName = userName,
            LoginTimestamp = DateTime.UtcNow,
            SessionId = sessionId
        };

        var logEntity = _mapper.Map<UserLogs>(logModel);
        _context.UserLogs.Add(logEntity);
        await _context.SaveChangesAsync();

        // Store the sessionId in the user's claims
        var user = await _context.Users.FindAsync(userId);
        var claims = new List<Claim>
        {
            new Claim("SessionId", sessionId)
        };
        var identity = new ClaimsIdentity(claims);
        await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, identity.Claims);
    }

    public async Task LogLogoutAsync(string userId, string sessionId)
    {
        var log = await _context.UserLogs
            .FirstOrDefaultAsync(l => l.UserId == userId && l.SessionId == sessionId && l.LogoutTimestamp == null);

        if (log != null)
        {
            log.LogoutTimestamp = DateTime.UtcNow;
            _context.UserLogs.Update(log);
            await _context.SaveChangesAsync();
        }
    }

    public async Task LogCrudOperationAsync(string userId, string userName, string crudOperation)
    {
        var log = new UserLogsModel
        {
            UserId = userId,
            UserName = userName,
            CrudOperation = crudOperation,
            SessionId = Guid.NewGuid().ToString() // Assuming a new session for each CRUD operation
        };

        var logEntity = _mapper.Map<UserLogs>(log);
        _context.UserLogs.Add(logEntity);
        await _context.SaveChangesAsync();
    }
}