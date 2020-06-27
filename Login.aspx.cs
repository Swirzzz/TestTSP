using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS.DAL;

namespace TPS {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
			
        }

		protected void HandleAuthenticate(object sender, AuthenticateEventArgs e) {
			TpsDbContext tps = new TpsDbContext();
			UserInfo user = UserInfo.Authenticate(UserLogin.UserName, UserLogin.Password);
			if (user != null) {
				e.Authenticated = true;
				FormsAuthenticationEx.RedirectFromLoginPage(UserLogin.UserName, user.SecurityRole, UserLogin.RememberMeSet);
			} else {
				e.Authenticated = false;
			}
		}
    }
}