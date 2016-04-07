using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using AD419.Core;
using AD419.Core.IdentityStores;
using AD419.Core.Models;

namespace AD_419_DataHelperWebApp
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var userstore = new CatbertUserStore(context.Get<CatbertDataContext>());
            var manager = new ApplicationUserManager(userstore);

            return manager;
        }
    }
}
