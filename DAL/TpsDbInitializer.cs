using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TPS.DAL {
	public class TpsDbInitializer : DropCreateDatabaseIfModelChanges<TpsDbContext> {
		protected override void Seed(TpsDbContext context) {
			var peterJackson = new UserInfo {
				Organization = "Taylor's Professional Services",
				FirstName = "Peter",
				LastName = "Jackson",
				Email = "jacksonp@tps.com",
				Password = "foobar",
				Phone = "701-555-2645",
				AddressLine1 = "135 Lazy Lane",
				City = "Fargo",
				State = "ND",
				ZipCode = "58102",
				SecurityRole = "Manager"
			};
			context.Users.Add(peterJackson);

			context.Users.Add(new UserInfo {
				Organization = "Hewes & Associates",
				FirstName = "Patricia",
				LastName = "Hewes",
				Email = "phewes@heweslaw.com",
				Password = "foobar",
				Phone = "800-543-0987",
				AddressLine1 = "506 East Broadway",
				AddressLine2 = "5th Floor",
				City = "New York",
				State = "NY",
				ZipCode = "10002",
				SecurityRole = "Client"
			});

			var arthurFrobisher = new UserInfo {
				Organization = "Orion Pharmaceuticals",
				FirstName = "Arthur",
				LastName = "Frobisher",
				Email = "ceo@orion.net",
				Password = "foobar",
				Phone = "234-876-4589 x1000",
				AddressLine1 = "P.O. Box 874",
				City = "Atlanta",
				State = "GA",
				ZipCode = "30301",
				SecurityRole = "Client"
			};
			context.Users.Add(arthurFrobisher);

			context.Users.Add(new UserInfo {
				FirstName = "Ellen",
				LastName = "Parsons",
				Email = "ellen92@gmail.com",
				Password = "foobar",
				Phone = "471-964-2895",
				AddressLine1 = "409 Cardinal Street",
				AddressLine2 = "Apartment 12",
				City = "New York",
				State = "NY",
				ZipCode = "10005",
				SecurityRole = "Staff"
			});

			context.Users.Add(new UserInfo {
				FirstName = "David",
				LastName = "Connor",
				Email = "dconnor100@yahoo.com",
				Password = "foobar",
				Phone = "987-145-3920",
				AddressLine1 = "101 North Pine",
				City = "Denver",
				State = "CO",
				ZipCode = "89476",
				SecurityRole = "Staff",
				EducationLevel = "Undergraduate"
			});

			context.Contracts.Add(new ContractInfo {
				Position = "Lab Assistant",
				StartDate = DateTime.Parse("12/13/2012"),
				EndDate = DateTime.Parse("10/22/2013"),
				Client = arthurFrobisher,
				Manager = peterJackson
			});

			context.SaveChanges();
		}
	}
}