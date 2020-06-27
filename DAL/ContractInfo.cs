using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TPS.DAL {
	[Table("ContractInfo")]
	public class ContractInfo {
		[Key]
		public int ContractID { get; set; }

		public string Position { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public virtual UserInfo Client { get; set; }
		public virtual UserInfo Manager { get; set; }
		public virtual UserInfo Staff { get; set; }
	}
}