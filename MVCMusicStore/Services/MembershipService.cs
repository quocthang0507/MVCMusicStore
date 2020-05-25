using System.Web.Security;

namespace MvcMusicStore.Services
{
	public class MembershipService : IMembershipService
	{

		public bool ValidateUser(string userName, string password)
		{
			return Membership.ValidateUser(userName, password);
		}

		public MembershipCreateStatus CreateUser(string userName, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerKey)
		{
			MembershipCreateStatus createStatus;
			Membership.CreateUser(userName, password, email, passwordQuestion, passwordAnswer, isApproved, providerKey, out createStatus);
			return createStatus;
		}

		public bool ChangePassword(string name, bool isOnline, string oldPassword, string newPassword)
		{
			MembershipUser currentUser = Membership.GetUser(name, isOnline);
			return currentUser.ChangePassword(oldPassword, newPassword);
		}
	}

}