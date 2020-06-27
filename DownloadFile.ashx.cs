using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace TPS {
	public class DownloadFile : IHttpHandler {
		public void ProcessRequest(HttpContext context) {
			int id;
			string type = context.Request.QueryString["type"];

			if (string.IsNullOrWhiteSpace(type) || !int.TryParse(context.Request.QueryString["id"], out id)) {
				context.Response.StatusCode = 404;
				context.Response.End();
				return;
			}

			string path;
			switch (type.ToLower()) {
				case "photo":
					context.Response.ContentType = "image/jpeg";
					path = context.Server.MapPath(Path.Combine(
						ConfigurationManager.AppSettings["PhotoDirectory"], id + ".jpg"));
					if (!File.Exists(path)) {
						path = context.Server.MapPath(ConfigurationManager.AppSettings["DefaultPhotoPath"]);
					}
					break;

				case "resume":
					context.Response.ContentType = "application/pdf";
					path = context.Server.MapPath(Path.Combine(
						ConfigurationManager.AppSettings["ResumeDirectory"], id + ".pdf"));
					break;

				default:
					context.Response.StatusCode = 404;
					context.Response.End();
					return;
			}

			if (!File.Exists(path)) {
				context.Response.StatusCode = 404;
				context.Response.End();
				return;
			}

			context.Response.WriteFile(path);
		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}