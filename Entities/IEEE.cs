using Microsoft.EntityFrameworkCore.Query;

namespace FieldMRIServices.Entities
{
    public class IEEE
    {
        public int Id { get; set; }
        public string Qunity { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public string ComputerName { get; set; }
        public string GeneralImage { get; set; }
        public string Location { get; set; }
        public string Tag { get; set; }
        public string BarCodes { get; set; }
        public string Serial { get; set; }

        public IEEEPhoto IEEEPhoto { get; set; }
    }
}
