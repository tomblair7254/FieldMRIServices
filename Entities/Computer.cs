namespace FieldMRIServices.Entities

{
    public class Computer
    {
        public int Id { get; set; }
        public string ComputerName { get; set; } = string.Empty;
        public string? InventoryNumber { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public string HardDrive { get; set; } = string.Empty;
        public string Memory { get; set; } = string.Empty;
        public string Player { get; set; } = string.Empty;
        public string Network { get; set; } = string.Empty;
        public string SCSI { get; set; } = string.Empty;
        public string MultipleCom { get; set; } = string.Empty;
        public string Video { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string Qty { get; set; } = string.Empty;
        public string BarCodes { get; set; } = string.Empty;
        public string Pin { get; set; }
        public string Fiber { get; set; }
        public string IEEE { get; set; }
        public string Modisk { get; set; }
        public string SASRaid { get; set; }
        public string Mrits { get; set; }

    }
}



