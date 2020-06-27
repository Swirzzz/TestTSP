using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS.DAL;

namespace TPS {
    public partial class NewStaffingRequest : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
			FormsAuthenticationEx.RedirectIfUserNotInRole("~/Default.aspx", "Client");

			if (!IsPostBack) {
				ViewState["ChosenStaff"] = new int[] { 0, 0, 0 };

				using (TpsDbContext tps = new TpsDbContext()) {
					foreach (var contract in tps.Contracts.Where(c => c.Client.Email == User.Identity.Name)) {
						ContractDropDownList.Items.Add(new ListItem(string.Format("{0} ({1} - {2})",
							contract.Position, contract.StartDate.ToShortDateString(), contract.EndDate.ToShortDateString()),
							contract.ContractID.ToString()));
					}

					if (ContractDropDownList.Items.Count == 0) {
						NoContractsLabel.Visible = true;
						FormPanel.Visible = false;
					}
				}
			}
        }

		protected void SearchButton_Click(object sender, EventArgs e) {
			decimal maxSalary = 0;
			decimal.TryParse(SalaryTextBox.Text, out maxSalary);

			using (TpsDbContext tps = new TpsDbContext()) {
				var staff = tps.Users.Where(u => u.SecurityRole == "Staff");

				if (JobCategoryDropDownList.SelectedValue != "Any") {
					staff = staff.Where(u => u.DesiredJobCategory == JobCategoryDropDownList.SelectedValue);
				}

				if (maxSalary > 0) {
					staff = staff.Where(u => u.DesiredSalary <= maxSalary);
				}

				if (!string.IsNullOrWhiteSpace(CityTextBox.Text)) {
					staff = staff.Where(u => u.City.ToUpper() == CityTextBox.Text.ToUpper());
				}

				if (!string.IsNullOrWhiteSpace(StateTextBox.Text)) {
					staff = staff.Where(u => u.State.ToUpper() == StateTextBox.Text.ToUpper());
				}

				if (RelocateCheckBox.Checked) {
					staff = staff.Where(u => u.CanRelocate == true);
				}

				var staffList = staff.ToList();

				staffList.RemoveAll(s => {
					// This is so horrible, but just go with it
					if (EducationLevelDropDownList.SelectedValue == "High School") return false;
					if (EducationLevelDropDownList.SelectedValue == "Some College" && s.EducationLevel != "High School") return false;
					if (EducationLevelDropDownList.SelectedValue == "Undergraduate" &&
						(s.EducationLevel != "Some College" && s.EducationLevel != "High School")) return false;
					if (EducationLevelDropDownList.SelectedValue == "Graduate" && s.EducationLevel == "Graduate") return false;
					return true;
				});

				SearchResultsGridView.DataSource = staffList;
				SearchResultsGridView.DataBind();
			}
		}

		protected void SearchResultsGridView_SelectedIndexChanged(object sender, EventArgs e) {
			int[] chosenStaff = (int[])ViewState["ChosenStaff"];
			for (int i = 0; i < chosenStaff.Length; i++) {
				if (chosenStaff[i] == 0) {
					chosenStaff[i] = (int)SearchResultsGridView.SelectedDataKey.Value;
					break;
				}
			}

			RefreshChosenStaffGridView();
		}

		protected void ChosenStaffGridView_RowDeleting(object sender, GridViewDeleteEventArgs e) {
			int[] chosenStaff = (int[])ViewState["ChosenStaff"];
			for (int i = 0; i < chosenStaff.Length; i++) {
				if (chosenStaff[i] == (int)e.Keys[0]) chosenStaff[i] = 0;
			}

			RefreshChosenStaffGridView();
		}

		private void RefreshChosenStaffGridView() {
			int[] chosenStaff = (int[])ViewState["ChosenStaff"];

			using (TpsDbContext tps = new TpsDbContext()) {
				bool atLeastOne = false; // Indicates whether we have at least one staffer chosen
				List<UserInfo> chosenStaffList = new List<UserInfo>();

				for (int i = 0; i < chosenStaff.Length; i++) {
					int id = chosenStaff[i];
					if (chosenStaff[i] > 0) {
						atLeastOne = true;
						chosenStaffList.Add(tps.Users.Where(u => u.UserID == id).Single());
					}
				}

				if (atLeastOne) {
					InstructionLabel.Visible = false;
					SubmitButton.Enabled = true;
				} else {
					InstructionLabel.Visible = true;
					SubmitButton.Enabled = false;
				}

				ChosenStaffGridView.DataSource = chosenStaffList;
				ChosenStaffGridView.DataBind();
			}
		}

		protected void CancelButton_Click(object sender, EventArgs e) {
			Response.Redirect("~/Default.aspx");
		}

		protected void SubmitButton_Click(object sender, EventArgs e) {
			using (TpsDbContext tps = new TpsDbContext()) {
				StaffRequest request = new StaffRequest();
				request.ContractID = int.Parse(ContractDropDownList.SelectedValue);

				int[] chosenStaff = (int[])ViewState["ChosenStaff"];
				for (int i = 0; i < chosenStaff.Length; i++) {
					int id = chosenStaff[i];
					if (id == 0) continue;
					if (i == 0) request.Staff1 = tps.Users.Where(u => u.UserID == id).Single();
					else if (i == 1) request.Staff2 = tps.Users.Where(u => u.UserID == id).Single();
					else if (i == 2) request.Staff3 = tps.Users.Where(u => u.UserID == id).Single();
				}

				tps.Requests.Add(request);
				tps.SaveChanges();
			}

			Response.Redirect("~/MyStaffingRequests.aspx");
		}
    }
}