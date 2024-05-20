using IdentityServer4.Events;
using IdentityServer4.Services;
using Sample.Auth.Api.Repositories.Interfaces;

namespace Sample.Auth.Api.Sinks
{
	public class EventLogSink : IEventSink
	{
		private readonly ILogRepository _logRepository;

		public EventLogSink(ILogRepository logRepository)
		{
			_logRepository = logRepository;
		}

		public async Task PersistAsync(Event evt)
		{
			var logMessage = GetLogMessage(evt);
			await _logRepository.WriteLog(logMessage, evt.EventType.ToString());
		}

		private string GetLogMessage(Event evt)
		{
			switch (evt.Id)
			{
				case EventIds.UserLoginSuccess:
					{
						var successUserName = (evt as UserLoginSuccessEvent)?.Username;
						return $"User successfully logged in. Username = {successUserName}";
					}
				case EventIds.UserLoginFailure:
					{
						var failureUserName = (evt as UserLoginFailureEvent)?.Username;
						return $"There was an error during login. UserName = {failureUserName}";
					}
				default: 
					return $"An action with Id = {evt.Id} was raised";
			}
		}
	}
}
