namespace FidelityHub.Application.Models.Account
{
    public class ResetForgottenPasswordModel
    {
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
    }
}
