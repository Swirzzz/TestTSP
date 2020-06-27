using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Common;

namespace TPS.DAL {
	public class TpsDbContext : DbContext {
		public DbSet<UserInfo> Users { get; set; }
		public DbSet<ContractInfo> Contracts { get; set; }
		public DbSet<StaffRequest> Requests { get; set; }

		public TpsDbContext() : base() { }
		public TpsDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
			: base(objectContext, dbContextOwnsObjectContext) { }

		public ObjectContext ToObjectContext() {
			return (this as IObjectContextAdapter).ObjectContext;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Entity<UserInfo>()
				.HasMany(e => e.ManagedContracts)
				.WithOptional(e => e.Manager);

			modelBuilder.Entity<UserInfo>()
				.HasMany(e => e.HeldContracts)
				.WithOptional(e => e.Client);
		}
	}
}