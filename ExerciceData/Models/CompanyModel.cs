namespace ExerciceData.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfFundation { get; set; }
        public List<DriverModel>? Drivers { get; set; }
        public List<TirModel>? Tirs { get; set; }
    }
}
