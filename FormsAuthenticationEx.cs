using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace TPS {
	/// <summary>
	/// Extends ASP.NET Forms Authentication to include support for role-based authorization
	/// </summary>
	public static class FormsAuthenticationEx {
		/// <summary>
		/// Perfoms essentially the same operations as the FormsAuthentication.SetAuthCookie
		/// method, but also saves the user's role.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="role"></param>
		/// <param name="createPersistentCookie"></param>
		public static void SetAuthCookie(string userName, string role, bool createPersistentCookie) {
			// Create a new authentication ticket with the role stored in the UserData property
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now,
				DateTime.Now.Add(FormsAuthentication.Timeout), createPersistentCookie, role);

			// Encrypt the ticket for storage
			string encryptedTicket = FormsAuthentication.Encrypt(ticket);

			if (!FormsAuthentication.CookiesSupported) {
				/*
				 * This may look a little confusing. When cookies aren't enabled, we need to store the
				 * authentication ticket in the URL. The easiest way to do that is to make the
				 * FormsAuthentication class do the work for us. So we allow it to create a new ticket
				 * with our own encrypted ticket as the user name.
				 */
				FormsAuthentication.SetAuthCookie(encryptedTicket, false);
			} else {
				// Cookies are enabled, so create a new one with our ticket data
				HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
				authCookie.Expires = ticket.Expiration;
				HttpContext.Current.Response.Cookies.Add(authCookie);
			}
		}

		/// <summary>
		/// Performs the same operations as the FormsAuthentication.RedirectFromLoginPage method,
		/// but uses our extended SetAuthCookie method to save the user's role.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="role"></param>
		/// <param name="createPersistentCookie"></param>
		public static void RedirectFromLoginPage(string name, string role, bool createPersistentCookie) {
			// Create and save an authentication ticket for the user
			SetAuthCookie(name, role, createPersistentCookie);

			// Get the URL that the user was originally trying to visit
			string returnUrl = HttpContext.Current.Request.QueryString["ReturnUrl"];
			if (string.IsNullOrEmpty(returnUrl)) {
				returnUrl = FormsAuthentication.DefaultUrl;
			}

			// Redirect to the return URL
			HttpContext.Current.Response.Redirect(returnUrl);
		}

		/// <summary>
		/// Redirects to a URL if the current user is not a member of the given role.
		/// </summary>
		/// <param name="destination"></param>
		/// <param name="role"></param>
		public static void RedirectIfUserNotInRole(string destination, string role) {
			if (!HttpContext.Current.User.IsInRole(role)) {
				HttpContext.Current.Response.Redirect(destination);
			}
		}

		/// <summary>
		/// This should be called from the Application_AuthenticateRequest method in Global.asax.
		/// It adds the authenticated user's role to their identity.
		/// </summary>
		public static void AuthenticateRequest() {
			IPrincipal user = HttpContext.Current.User;
			if (user != null && user.Identity.IsAuthenticated && user.Identity is FormsIdentity) {
				FormsIdentity identity = (FormsIdentity)user.Identity;
				FormsAuthenticationTicket ticket = identity.Ticket;

				if (!FormsAuthentication.CookiesSupported) {
					// Decrypt our custom ticket from the one ASP.NET stored in the URL
					ticket = FormsAuthentication.Decrypt(ticket.Name);
				}

				// Extend the current identity with the user's role
				HttpContext.Current.User = new GenericPrincipal(identity, new[] { ticket.UserData });
			}
		}
	}
}