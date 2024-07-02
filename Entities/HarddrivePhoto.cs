namespace FieldMRIServices.Entities
{
    public class HarddrivePhoto
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string InventoryName { get; set; }
        public string Image { get; set; }
        public int harddriveId { get; set; }
        public Harddrive Harddrive { get; set; }
    }
}
