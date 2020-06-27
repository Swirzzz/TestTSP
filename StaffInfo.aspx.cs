using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS.DAL;

namespace TPS {
    public partial class StaffInfo : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
			FormsAuthenticationEx.RedirectIfUserNotInRole("~/Default.aspx", "Staff");

			// Only show the data for the currently logged-in staff member
			StaffDataSource.Where = "it.Email = @Email";
			StaffDataSource.WhereParameters.Clear();
			StaffDataSource.WhereParameters.Add("Email", DbType.String, User.Identity.Name);

			if (!IsPostBack) {
				StaffDetailsView.DataBind();
			}
        }

		protected void StaffDataSource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e) {
			e.Context = new TpsDbContext().ToObjectContext();
		}

		protected void StaffDetailsView_ItemUpdating(object sender, DetailsViewUpdateEventArgs e) {
			FileUpload photoUpload = (FileUpload)StaffDetailsView.FindControl("PhotoFileUpload");
			FileUpload resumeUpload = (FileUpload)StaffDetailsView.FindControl("ResumeFileUpload");

			// Clear any previous error message
			ErrorMessage.Text = "";

			if (photoUpload.HasFile) {
				try {
					// Verify the image and then convert it to a JPEG
					System.Drawing.Image img = System.Drawing.Image.FromStream(photoUpload.FileContent, false, true);
					img.Save(Server.MapPath(Path.Combine(
							ConfigurationManager.AppSettings["PhotoDirectory"], e.Keys[0] + ".jpg")),
						System.Drawing.Imaging.ImageFormat.Jpeg);
					img.Dispose();
				} catch (Exception) {
					ErrorMessage.Text = "Invalid image format.";
					e.Cancel = true;
					return;
				}
			}

			if (resumeUpload.HasFile) {
				if (Path.GetExtension(resumeUpload.FileName) != ".pdf") {
					ErrorMessage.Text = "Résumé must be in PDF format.";
					e.Cancel = true;
					return;
				}

				resumeUpload.SaveAs(Server.MapPath(Path.Combine(
					ConfigurationManager.AppSettings["ResumeDirectory"], e.Keys[0] + ".pdf")));
			}
		}
    }
}