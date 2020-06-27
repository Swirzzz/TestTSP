using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS.DAL;

namespace TPS {
    public partial class MyStaffingRequests : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
			FormsAuthenticationEx.RedirectIfUserNotInRole("~/Default.aspx", "Client");

			// Only display requests for the currently logged-in client
			RequestDataSource.Where = "it.Contract.Client.Email = @email";
			RequestDataSource.WhereParameters.Clear();
			RequestDataSource.WhereParameters.Add("email", DbType.String, User.Identity.Name);

			if (!IsPostBack) {
				RequestGridView.DataBind();
			}
        }

		protected void RequestDataSource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e) {
			e.Context = new TpsDbContext().ToObjectContext();
		}
    }
}