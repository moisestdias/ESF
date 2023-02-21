using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpmDashboard.Models.EPM
{
    [Table("ProblemMaker")]
    public partial class ProblemMaker
    {
        [Key] [Required] public int id { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public string name { get; set; }

        public string about { get; set; }

        [Column("create_time")] public DateTime? createtime { get; set; }

        [Column("update_time")] public DateTime? updatetime { get; set; }

        public IEnumerable<Problem> Problems { get; set; }
        [ForeignKey("user_id")] public ApplicationUser ApplicationUser { get; set; }
    }
}