using System.Web;

namespace MvcMusicStore.Services
{
	public interface IAuthenticationService
	{
		void SetAuthCookie(string userName, bool rememberMe, HttpResponseBase response);
		void SignOut();
	}
}
