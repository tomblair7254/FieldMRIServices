#nullable enable
using System.ComponentModel.DataAnnotations;

namespace FieldMRIServices.Entities
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        public string? InventoryNumber { get; set; }

        public string? Qty { get; set; }

        public string? Pin { get; set; }

        public string? Location { get; set; }

        public string? Tag { get; set; }

        public string? BarCode { get; set; }
        public string? Status { get; set; }
        public string? MRITS { get; set; }
        public string? Images { get; set; }
    }
}