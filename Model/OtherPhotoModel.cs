namespace FieldMRIServices.Entities
{
    public class OtherPhotoModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int OtherId { get; set; }
        public Other Other { get; set; }
    }

}
