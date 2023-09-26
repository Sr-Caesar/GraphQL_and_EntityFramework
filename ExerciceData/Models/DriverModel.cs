using System.Net.Cache;
using System.Reflection;

namespace ExerciceData.Models
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public TirModel Tir { get; set; }
        public int TirId { get; set; }
        public CompanyModel Company { get; set; }
        public int CompanyId { get; set; }
    }
}
