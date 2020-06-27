using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS.DAL;

namespace TPS {
    public partial class Staff : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
			FormsAuthenticationEx.RedirectIfUserNotInRole("~/Default.aspx", "Manager");
			if (!IsPostBack) {
				StaffGridView.DataBind();
			}
        }

		protected void StaffDataSource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e) {
			e.Context = new TpsDbContext().ToObjectContext();
		}
    }
}