using ExerciceData.Models;

namespace GraphQL_Exercice.GraphQLSchema.Querries
{
    public class TirType
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int CompanyId { get; set; }
        public int DriverId { get; set; }
    }
}
