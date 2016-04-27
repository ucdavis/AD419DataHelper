using AD419.Core;
using AD419.Core.IdentityStores;
using AD419.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AD419.DataHelper.Web
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
