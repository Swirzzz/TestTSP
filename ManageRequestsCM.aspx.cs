using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS.DAL;

namespace TPS {
    public partial class ManageRequestsCM : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
			FormsAuthenticationEx.RedirectIfUserNotInRole("~/Default.aspx", "Manager");

			// Only show requests for contracts managed by the currently logged-in manager
			RequestDataSource.Where = "it.Contract.Manager.Email = @email";
			RequestDataSource.WhereParameters.Clear();
			RequestDataSource.WhereParameters.Add("email", DbType.String, User.Identity.Name);

			if (!IsPostBack) {
				RequestsGridView.DataBind();
			}
        }

		protected void RequestDataSource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e) {
			e.Context = new TpsDbContext().ToObjectContext();
		}

		protected void RequestsGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
			if (e.Row.DataItem != null) {
				StaffRequest request = (StaffRequest)e.Row.DataItem;

				if (request.Status != "In Review") {
					e.Row.Cells[5].Visible = false;
				}

				DropDownList staffDropDownList = (DropDownList)e.Row.FindControl("StaffDropDownList");
				if (staffDropDownList != null) {
					staffDropDownList.Items.Add(new ListItem("Denied", "0"));

					if (request.Staff1 != null) {
						staffDropDownList.Items.Add(new ListItem(string.Format("Approved: {0}, {1}",
							request.Staff1.LastName, request.Staff1.FirstName), request.Staff1.UserID.ToString()));
					}

					if (request.Staff2 != null) {
						staffDropDownList.Items.Add(new ListItem(string.Format("Approved: {0}, {1}",
							request.Staff2.LastName, request.Staff2.FirstName), request.Staff2.UserID.ToString()));
					}

					if (request.Staff3 != null) {
						staffDropDownList.Items.Add(new ListItem(string.Format("Approved: {0}, {1}",
							request.Staff3.LastName, request.Staff3.FirstName), request.Staff3.UserID.ToString()));
					}
				}
			}
		}

		protected void RequestsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e) {
			DropDownList staffDropDownList = (DropDownList)RequestsGridView.Rows[e.RowIndex].FindControl("StaffDropDownList");

			using (TpsDbContext tps = new TpsDbContext()) {
				int requestId = (int)e.Keys[0];
				StaffRequest request = tps.Requests.Where(r => r.RequestID == requestId).Single();

				if (staffDropDownList.SelectedValue == "0") {
					request.Status = "Denied";
				} else {
					int staffId = int.Parse(staffDropDownList.SelectedValue);
					request.Status = "Approved";
					request.Contract.Staff = tps.Users.Where(u => u.UserID == staffId).Single();
				}

				tps.SaveChanges();
			}
		}
    }
}