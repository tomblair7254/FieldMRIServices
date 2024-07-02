namespace FieldMRIServices.Entities
{
    public class InventoryPhoto
    {
        public int Id { get; set; }
        public string InventoryName { get; set; }
        public string Image { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
