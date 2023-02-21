using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpmDashboard.Models.EPM
{
    [Table("Problem")]
    public partial class Problem
    {
        [Key]
        [Required]
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        [Column("create_time")]
        public DateTime? createtime { get; set; }

        [Column("update_time")]
        public DateTime? updatetime { get; set; }

        public decimal? funding { get; set; }


        public ProblemMaker ProblemMaker { get; set; }

        public ProblemSolver? ProblemSolver { get; set; }

    }
}