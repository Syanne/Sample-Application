using System.ComponentModel.DataAnnotations;  namespace Sample.Auth.Models.Requests {
    public class AunthenticateUserModel
    {         [Required]         public string Email { get; set; }          [Required]         public string Password { get; set; }          public string? ReturnUrl { get; set; }     } } 