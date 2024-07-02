namespace FieldMRIServices.Entities
{
    public class MonitorPhoto
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int MonitorsId { get; set; }
        public Monitors Monitors { get; set; }
    }
}
