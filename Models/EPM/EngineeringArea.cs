using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpmDashboard.Models.EPM
{
    [Table("EngineeringAreas")]
    public partial class EngineeringArea
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        public string area { get; set; }
        
        public string School { get; set; }

        public IEnumerable<ProblemSolver> ProblemSolvers { get; set; }

    }
}