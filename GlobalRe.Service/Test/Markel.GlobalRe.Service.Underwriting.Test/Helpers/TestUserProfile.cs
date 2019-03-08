using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalReUnderwriting.Service.Test.Helpers
{
	public class TestUserProfile
	{
		private CacheStoreManager _cacheStoreManager = null;
		private UserManager _userManager = null;

		public UserManager UserManager { get { return _userManager; } }
		public CacheStoreManager CacheStoreManager { get { return _cacheStoreManager; } }
		public string RequestUri { get { return AppSettings.BASEURL; } }

		public TestUserProfile()
		{
			_cacheStoreManager = new CacheStoreManager();
			_userManager = new UserManager(_cacheStoreManager);

			string userName = Environment.UserName;
			InitializeUser(userName);
		}
		public TestUserProfile(string userLoginName)
		{
			_cacheStoreManager = new CacheStoreManager();
			_userManager = new UserManager(_cacheStoreManager);

			string userName = GetUserNamefromLoginID(userLoginName);
			InitializeUser(userLoginName);
		}

		public TestUserProfile(Enum role)
		{
			_cacheStoreManager = new CacheStoreManager();
			_userManager = new UserManager(_cacheStoreManager);

			string userName = GetTestUserNameByRole(role);
			InitializeUser(userName);
		}
		private void InitializeUser(string userName)
		{
			string domainName = ConfigurationManager.AppSettings.Get("DomainName");
			string userLogin = string.Format("{0:S}@{1:S}", userName, domainName);
			_userManager.Initialize(userLogin);
		}

		private string GetUserNamefromLoginID(string userLoginName)
		{
			throw new NotImplementedException();
		}
		private string GetTestUserNameByRole(Enum role)
		{
			throw new NotImplementedException();
		}
	}
}
