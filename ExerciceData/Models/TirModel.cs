namespace ExerciceData.Models
{
    public class TirModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public CompanyModel Company { get; set; }
        public int CompanyId { get; set; }
        public int DriverId { get; set; }
        public DriverModel Driver { get; set; }
    }
}
