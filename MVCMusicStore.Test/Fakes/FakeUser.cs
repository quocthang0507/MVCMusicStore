﻿using System.Security.Principal;

namespace MVCMusicStore.Test.Fakes
{
	public class FakeUser : IPrincipal
	{
		private IIdentity _identity;

		public FakeUser() : this(new FakeIdentity()) { }

		public FakeUser(IIdentity identity)
		{
			_identity = identity;
		}

		public IIdentity Identity
		{
			get { return _identity; }
		}

		public bool IsInRole(string role)
		{
			return false;
		}
	}

	public class FakeIdentity : IIdentity
	{
		private string _name, _authType;
		private bool _isAuthed;

		public FakeIdentity() : this("", "", false) { }

		public FakeIdentity(string name, string authType, bool isAuthed)
		{
			_name = name;
			_authType = authType;
			_isAuthed = isAuthed;
		}

		public string AuthenticationType
		{
			get { return _authType; }
		}

		public bool IsAuthenticated
		{
			get { return _isAuthed; }
		}

		public string Name
		{
			get { return _name; }
		}
	}
}
