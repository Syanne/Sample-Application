namespace Sample.Auth.Api.Repositories.Interfaces
{
	public interface ILogRepository
	{
		Task WriteLog(string logMessage, string eventType);
	}
}
