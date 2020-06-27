using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS.DAL;

namespace TPS {
    public partial class Users : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
			FormsAuthenticationEx.RedirectIfUserNotInRole("~/Default.aspx", "Manager");
			if (!IsPostBack) {
				// Data-binding on a postback interferes with GridView updates, so don't do it!
				UsersGridView.DataBind();
			}
        }

		protected void UsersDataSource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e) {
			// This is required to make the newer Entity Framework work with the old ASP.NET WebForms controls
			e.Context = new TpsDbContext().ToObjectContext();
		}

		protected void NewUserDetailsView_ItemInserting(object sender, DetailsViewInsertEventArgs e) {
			/*
			 * Since the SecuritySalt and SecurityKey fields aren't editable in the DetailsView (nor should they be),
			 * we have to intercept the insert event and populate them ourselves.
			 */
			byte[] salt, key;
			TextBox passwordTextBox = (TextBox)NewUserDetailsView.FindControl("NewUserPasswordTextBox");
			UserInfo.GetDataForPassword(passwordTextBox.Text, out salt, out key);
			e.Values["SecuritySalt"] = salt;
			e.Values["SecurityKey"] = key;
		}

		protected void NewUserDetailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e) {
			// Refresh the GridView to display the new record
			UsersGridView.DataBind();
		}

		protected void RoleFilterDropDownList_SelectedIndexChanged(object sender, EventArgs e) {
			UsersDataSource.Where = "it.SecurityRole = @Role";

			/*
			 * The parameters from the previous selection will persist between requests, so we need
			 * to clear them, otherwise we'll get an exception about duplicates.
			 */
			UsersDataSource.WhereParameters.Clear();

			switch (RoleFilterDropDownList.SelectedValue) {
				case "Managers":
					UsersDataSource.WhereParameters.Add("Role", DbType.String,"Manager");
					break;
				case "Clients":
					UsersDataSource.WhereParameters.Add("Role", DbType.String, "Client");
					break;
				case "Staff":
					UsersDataSource.WhereParameters.Add("Role", DbType.String, "Staff");
					break;
				case "All":
				default:
					UsersDataSource.Where = "";
					return;
			}

			// Update the grid view with the filter
			UsersGridView.DataBind();
		}

		protected void UsersDataSource_Deleting(object sender, EntityDataSourceChangingEventArgs e) {
			// Delete the user's contracts
			using (TpsDbContext tps = new TpsDbContext()) {
				UserInfo user = tps.Users.Where(u => u.UserID == ((UserInfo)e.Entity).UserID).Single();
				user.ManagedContracts.ToList().ForEach(c => tps.Contracts.Remove(c));
				user.HeldContracts.ToList().ForEach(c => tps.Contracts.Remove(c));
				tps.SaveChanges();
			}
		}
    }
}