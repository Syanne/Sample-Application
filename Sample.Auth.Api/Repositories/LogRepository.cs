using Sample.Auth.Api.Repositories.Interfaces;
using Sample.Auth.DataAccess.MsSql.Contexts;
using Sample.Auth.DataAccess.MsSql.Entities;

namespace Sample.Auth.Api.Repositories
{
    public class LogRepository: ILogRepository
	{
        private readonly ReadWriteDbContext _context;

        public LogRepository(ReadWriteDbContext context)
        {
            _context = context;
        }

        public async Task WriteLog(string logMessage, string eventType)
        {
            var entity = new Log()
            {
                EventType = eventType,
                TimeStamp = DateTime.UtcNow,
                LogMessage = logMessage
            };

            _context.Logs.Add(entity);

            await _context.SaveChangesAsync();
        }
    }
}
