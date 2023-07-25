using Microsoft.AspNetCore.Authentication;

namespace ADOTest.ViewModels
{
    public class LoginViewModel
    {
        public int email { get; set; }
        public int password { get; set; }
        public int rememberMe { get; set; }
        public int returnUrl { get; set; }
        public IList<AuthenticationScheme> externalLogins { get; set; }
    }
}
