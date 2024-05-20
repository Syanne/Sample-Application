using System.Net;

namespace Sample.Auth.Api.Exceptions
{
	public class SampleException : Exception
	{
		public SampleException(HttpStatusCode code, string message)
		{
			StatusCode = code;
			Message = message;
		}

		public HttpStatusCode StatusCode { get; set; }
		public string Message { get; set; }
	}
}
