namespace FieldMRIServices.Model
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public string InventoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Qty { get; set; }
        public string Pin { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string DateRemoved { get; set; } = string.Empty;
        public string RemovedBy { get; set; } = string.Empty;
        public string DateReturned { get; set; } = string.Empty;
        public string ReturnedBy { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string GeneralImage { get; set; } = string.Empty;
        public string BarCodes { get; set; }
        public List<PhotoModel> Photos { get; set; } = new();
    }
}
