using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TPS.DAL {
	[Table("StaffRequest")]
	public class StaffRequest {
		[Key]
		public int RequestID { get; set; }

		public int ContractID { get; set; }
		public string Status { get; set; }

		[ForeignKey("ContractID")]
		public virtual ContractInfo Contract { get; set; }

		public virtual UserInfo Staff1 { get; set; }
		public virtual UserInfo Staff2 { get; set; }
		public virtual UserInfo Staff3 { get; set; }

		public StaffRequest() {
			Status = "In Review";
		}
	}
}