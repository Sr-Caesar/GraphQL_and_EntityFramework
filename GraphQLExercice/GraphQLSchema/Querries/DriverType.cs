using ExerciceData.Models;

namespace GraphQL_Exercice.GraphQLSchema.Querries
{
    public class DriverType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int TirId { get; set; }
        public int CompanyId { get; set; }
    }
}
