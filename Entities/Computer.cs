namespace FieldMRIServices.Entities

{
    public class Computer
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Location { get; set; }
        public string Serial { get; set; }
        public string HardDrive { get; set; }
        public string Memory { get; set; }
        public string Player { get; set; }
        public string Network { get; set; }
        public string SCSI { get; set; }
        public string MultipleCom { get; set; }
        public string Video { get; set; }
        public string GeneralImage { get; set; }
        public string Status { get; set; }
        public string Tag { get; set; }
        public string Qty { get; set; }
        public string BarCodes { get; set; }
        public string Model { get; set; }


        public List<ComputerPhoto> ComputerPhotos { get; set; }
    }
}
