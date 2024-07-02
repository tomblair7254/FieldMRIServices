namespace FieldMRIServices.Entities
{
    public class IEEEPhoto
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int MemoryId { get; set; }
        public Memory Memory { get; set; }
        public int IEEEId { get; set; }
        public IEEE IEEE { get; set; }
    }
}