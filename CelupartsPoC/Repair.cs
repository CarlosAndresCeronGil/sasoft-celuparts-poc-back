﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class Repair
    {
        [Key]
        public int IdRepair { get; set; }

        public int IdRequest { get; set; }
        [ForeignKey("IdRequest")]
        [JsonIgnore]
        public virtual RequestWithEquipments? Request { get; set; }

        public int? IdTechnician { get; set; }
        [ForeignKey("IdTechnician")]
        public virtual Technician? Technician { get; set; }

        public DateTime? RepairDate { get; set; }
        public DateTime? RepairStartDate { get; set; }
        public TimeSpan? RepairTime { get; set; }
        public string DeviceDiagnostic { get; set; } = string.Empty;
        public string RepairDiagnostic { get; set; } = string.Empty;
        public string RepairQuote { get; set; } = string.Empty;
        public bool PriceReviewedByAdmin { get; set; } = false;
        public virtual List<RepairPayment> RepairPayments { get; set; } = new List<RepairPayment>();
        public virtual List<PartsToRepair> PartsToRepair { get; set; } = new List<PartsToRepair>();
    }
}
