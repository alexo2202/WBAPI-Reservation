using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TblVehicles")]
    public class Vehicle : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public int VehicleTypeId { get; set; }
        public int BookingValuePerDay { get; set; }
        public VehicleType VehicleType { get; set; }

    }
}
