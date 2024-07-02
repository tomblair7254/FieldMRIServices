namespace FieldMRIServices.Entities
{
    public class Other
    {
        public int Id { get; set; }
        public string Qty { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ComputerName { get; set; } = string.Empty;
        public string GeneralImage { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string BarCodes { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public OtherPhoto OtherPhoto { get; set; }

    }
}
