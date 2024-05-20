using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sample.Auth.DataAccess.MsSql.Entities
{
	public class Log
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string EventType { get; set; }

		[Required]
		public string LogMessage { get; set; }
		
		[Required]
		public DateTime TimeStamp { get; set; }
	}
}
