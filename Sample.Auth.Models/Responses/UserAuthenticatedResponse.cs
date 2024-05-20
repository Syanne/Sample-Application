namespace Sample.Auth.Models.Responses
{
	public class UserAuthenticatedResponse
	{
		public UserAuthenticatedResponse(string userName, string userEmail, string phoneNumber)
		{
			UserName = userName;
			UserEmail = userEmail;
			PhoneNumber = phoneNumber;
		}

		public string? UserName { get; }
		
		public string? UserEmail { get; }

		public string? PhoneNumber { get; }
	}
}
