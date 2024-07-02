namespace FieldMRIServices.Model
{
    public class ModiskModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Serial { get; set; }
        public string GeneralImage { get; set; } = string.Empty;
        public string Status { get; set; }
        public string Location { get; set; }
        public string Tag { get; set; }
        public string Qty { get; set; }
        public string BarCodes { get; set; }
        public string Model { get; set; }

        public List<ModiskPhotoModel> ModiskPhotoModels { get; set; }


    }
}
