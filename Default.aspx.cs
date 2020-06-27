using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPS {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (User.IsInRole("Manager")) Response.Redirect("~/Contracts.aspx");
			else if (User.IsInRole("Client")) Response.Redirect("~/MyStaffingRequests.aspx");
			else if (User.IsInRole("Staff")) Response.Redirect("~/StaffInfo.aspx");
		}
	}
}