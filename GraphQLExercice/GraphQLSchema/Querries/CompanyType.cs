using ExerciceData.Models;

namespace GraphQL_Exercice.GraphQLSchema.Querries
{
    public class CompanyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfFundation { get; set; }
        public List<DriverType>? Drivers { get; set; }
        public List<TirType>? Tirs { get; set; }
    }
}
