using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using TPS.DAL;

namespace TPS {
	public class Global : System.Web.HttpApplication {
		protected void Application_Start(object sender, EventArgs e) {
			// Configure the Entity Framework
			Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
			Database.SetInitializer<TpsDbContext>(new TpsDbInitializer());

			// Create data directories
			string photoDir = Server.MapPath(ConfigurationManager.AppSettings["PhotoDirectory"]);
			if (!Directory.Exists(photoDir)) {
				Directory.CreateDirectory(photoDir);
			}

			string resumeDir = Server.MapPath(ConfigurationManager.AppSettings["ResumeDirectory"]);
			if (!Directory.Exists(resumeDir)) {
				Directory.CreateDirectory(resumeDir);
			}
		}

		protected void Session_Start(object sender, EventArgs e) {

		}

		protected void Application_BeginRequest(object sender, EventArgs e) {

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {
			FormsAuthenticationEx.AuthenticateRequest();
		}

		protected void Application_Error(object sender, EventArgs e) {

		}

		protected void Session_End(object sender, EventArgs e) {

		}

		protected void Application_End(object sender, EventArgs e) {

		}
	}
}