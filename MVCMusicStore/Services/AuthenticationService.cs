using System.Web;
using System.Web.Security;

namespace MvcMusicStore.Services
{
	public class AuthenticationService : IAuthenticationService
	{

		public void SetAuthCookie(string userName, bool rememberMe, HttpResponseBase response)
		{
			//FormsAuthentication.SetAuthCookie(userName, rememberMe);
			response.Cookies.Add(FormsAuthentication.GetAuthCookie(userName, rememberMe));
		}

		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}
	}
}