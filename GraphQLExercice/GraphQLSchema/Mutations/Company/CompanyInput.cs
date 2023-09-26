using ExerciceData.Models;

namespace GraphQL_Exercice.GraphQLSchema.Mutations.Company
{
        public class CompanyInputType
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public DateTime? DateOfFundation { get; set; }

        }
}
