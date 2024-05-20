using Sample.Auth.Models.Requests;
using Sample.Auth.Models.Responses;
using Sample.ConsoleApp;
using System.Text.RegularExpressions;
using System.Configuration;

internal class Program
{
	const string emailRegex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
	const string passwordRegex = "^[\\w\\.\\,\\-\\!\\@]{5,20}$";

	private static async Task Main(string[] args)
	{
		Console.WriteLine("Please enter your email");

		string? password = null;
		string? email = ReadValue(emailRegex);
		if (string.IsNullOrWhiteSpace(email))
		{
			Console.WriteLine("\nIncorrect email!");
		}
		else
		{
			Console.WriteLine("\nPlease enter your password");
			password = ReadValue(passwordRegex);
			if (string.IsNullOrWhiteSpace(password))
			{
				Console.WriteLine("\nIncorrect password!");
			}
			else
			{
				Console.WriteLine("Authorizing...");
				var authModel = new AunthenticateUserModel()
				{
					Email = email,
					Password = password
				};

				string host = ConfigurationManager.AppSettings["host"];
				int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
				var client = new SampleHttpClient(host, port);

				var response = await client.Post<UserAuthenticatedResponse>("api/Account", authModel);
				Console.WriteLine("\nAuthentication completed successfully. Data: " +
					$"\nName = {response.UserName ?? "-"}," +
					$"\nEmail = {response.UserEmail ?? "-"}" +
					$"\nPhone = {response.PhoneNumber ?? "-"}");
			}
		}

		Console.WriteLine("\nProgram execution has finished...");
		Console.ReadLine();
	}

	private static string ReadValue(string pattern)
	{
		var value = string.Empty;
		for (int i = 0; i < 3; i++)
		{
			value = Console.ReadLine();

			if (!string.IsNullOrWhiteSpace(value))
			{
				if (Regex.Match(value, emailRegex).Success)
				{
					break;
				}
			}

			Console.WriteLine("\nWrong email format! please enter a correct email address");

			value = string.Empty;
		}

		return value;
	}
}