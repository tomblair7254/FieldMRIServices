namespace FieldMRIServices.Model
{
    public class ModiskPhotoModel
    {
        public int Id { get; set; }

        public string ComputerName { get; set; }

        public string Serial { get; set; }
        public string Status { get; set; }

        public string ModiskId { get; set; }
        public ModiskModel Modisk { get; set; }
    }
}
