
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace Sample.ConsoleApp
{
	internal class SampleHttpClient
	{
		private readonly string _url;
		private readonly int _port;

		private readonly HttpClient _client;
		public SampleHttpClient(string url, int port)
		{
			_url = url;
			_port = port;
			_client = new HttpClient();
		}

		public async Task<T> Post<T>(string action, object request)
			where T: class
		{
			var builder = new UriBuilder(_url + action);
			builder.Port = _port;
			string url = builder.ToString();

			var serializedRequest = JsonConvert.SerializeObject(request);

			using StringContent jsonContent = new (serializedRequest, Encoding.UTF8, MediaTypeNames.Application.Json);

			using HttpResponseMessage response = await _client.PostAsync(new Uri(url), jsonContent);

			using HttpContent content = response.Content;

			if (content == null || !response.IsSuccessStatusCode)
			{
				throw new Exception($"Response code: {response.StatusCode}");
			}

			var responseResult = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<T>(responseResult);
		}
	}
}
