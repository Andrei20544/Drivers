namespace Zadanie1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Licences
    {
        public int id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? LicenceDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        [StringLength(255)]
        public string Categories { get; set; }

        [StringLength(255)]
        public string LicenceSeries { get; set; }

        public double? LicenceNumber { get; set; }

        [StringLength(255)]
        public string VIN { get; set; }

        [StringLength(255)]
        public string Manufacturer { get; set; }

        [StringLength(255)]
        public string Model { get; set; }

        public double? Year { get; set; }

        public double? Weight { get; set; }

        public double? Color { get; set; }

        public double? EngineType { get; set; }

        [StringLength(255)]
        public string TypeOfDrive { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public virtual Drivers Drivers { get; set; }
    }
}
