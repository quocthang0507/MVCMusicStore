using System.Web.Security;

namespace MvcMusicStore.Services
{
	public interface IMembershipService
	{
		bool ValidateUser(string userName, string password);
		MembershipCreateStatus CreateUser(string userName, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerKey);
		bool ChangePassword(string name, bool isOnline, string oldPassword, string newPassword);

	}
}
