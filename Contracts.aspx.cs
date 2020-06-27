using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS.DAL;

namespace TPS {
    public partial class Contracts : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
			FormsAuthenticationEx.RedirectIfUserNotInRole("~/Default.aspx", "Manager");
			if (!IsPostBack) {
				ContractGridView.DataBind();
			}
        }

		protected void EntityDataSource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e) {
			e.Context = new TpsDbContext().ToObjectContext();
		}

		protected void NewContractDetailsView_DataBound(object sender, EventArgs e) {
			// Populate the client list
			DropDownList clientDropDownList = (DropDownList)NewContractDetailsView.FindControl("NewContractClientDropDownList");
			TpsDbContext tps = new TpsDbContext();
			foreach (UserInfo client in (from u in tps.Users where u.SecurityRole == "Client" select u)) {
				clientDropDownList.Items.Add(new ListItem(client.ToString(), client.UserID.ToString()));
			}
		}

		protected void NewContractDetailsView_ItemInserting(object sender, DetailsViewInsertEventArgs e) {
			// Set the contract's manager to the logged-in user
			TpsDbContext tps = new TpsDbContext();
			UserInfo manager = (from u in tps.Users where u.Email == User.Identity.Name select u).Single();
			e.Values["Manager.UserID"] = manager.UserID;
		}

		protected void NewContractDetailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e) {
			// Update the GridView to display the new record
			ContractGridView.DataBind();
		}
    }
}