using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TPS.DAL {
	[Table("UserInfo")] // Prevent EF from naming the table "UserInfoes"
	public class UserInfo {
		[Key]
		public int UserID { get; set; }

		public string Organization { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string SecurityRole { get; set; }
		public byte[] SecuritySalt { get; set; }
		public byte[] SecurityKey { get; set; }

		// Staff-specific fields
		public string EducationLevel { get; set; }
		public decimal DesiredSalary { get; set; }
		public bool CanRelocate { get; set; }
		public string DesiredJobCategory { get; set; }

		public virtual ICollection<ContractInfo> ManagedContracts { get; set; }
		public virtual ICollection<ContractInfo> HeldContracts { get; set; }

		[NotMapped]
		public string Password {
			set {
				byte[] salt, key;
				GetDataForPassword(value, out salt, out key);
				SecuritySalt = salt;
				SecurityKey = key;
			}
		}

		public UserInfo() {
			ManagedContracts = new List<ContractInfo>();
			HeldContracts = new List<ContractInfo>();

			// Must have a default value
			EducationLevel = "High School";
			DesiredJobCategory = "Administrative";
		}

		public override string ToString() {
			StringBuilder str = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(Organization)) {
				str.Append(Organization + " - ");
			}
			str.AppendFormat("{0}, {1}", LastName, FirstName);
			return str.ToString();
		}

		public static void GetDataForPassword(string password, out byte[] salt, out byte[] key) {
			using (var pbkdf2 = new Rfc2898DeriveBytes(password, 20)) {
				salt = pbkdf2.Salt;
				key = pbkdf2.GetBytes(20);
			}
		}

		public static UserInfo Authenticate(string email, string password) {
			TpsDbContext tps = new TpsDbContext();
			UserInfo user = (from u in tps.Users where u.Email == email select u).SingleOrDefault();
			if (user != null) {
				using (var pbkdf2 = new Rfc2898DeriveBytes(password, user.SecuritySalt)) {
					byte[] key = pbkdf2.GetBytes(20);
					if (!key.SequenceEqual(user.SecurityKey)) {
						return null;
					}
				}
			}
			return user;
		}
	}
}