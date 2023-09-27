using ExerciceData.Context;
using ExerciceData.Models;
using GraphQL_Exercice.GraphQLResolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Moq;
using Serilog.Events;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GraphQL_Exercice.Repository;

namespace CrudTesting
{
    public class CompanyCrudTest
    {
        public CompanyCrudTest()
        {
        }

        //[Fact]
        //public async Task InsertingTest()
        //{
        //    var moqCompanyRepository = new Mock<CompanyRepository>().Object;
        //    CompanyModel model = new()
        //    {
        //        Id = 1,
        //        Name = "Test",
        //        DateOfFundation = DateTime.Now
        //    };
        //    await myRep.CreateAsync(model);

        //    moqCompanyRepository.Verify();
        //}

        [Fact]
        public async Task InsertingTest()
        {
            var moqCompanyRepository = new Mock<CompanyRepository>();
            CompanyModel model = new CompanyModel
            {
                Id = 1,
                Name = "Test",
                DateOfFundation = DateTime.Now
            };
            moqCompanyRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<CompanyModel>()))
                .ReturnsAsync(model);

            
            CompanyRepository myRep = moqCompanyRepository.Object; //creo un obj reale

            var result = await myRep.CreateAsync(model); // testing effettivo del metodo

            // Assert => verifico la correttezza dei dati manipolati
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);

            // Verifico che il metodo CreateAsync sia stato chiamato esattamente una volta
            moqCompanyRepository.Verify(repo => repo.CreateAsync(It.IsAny<CompanyModel>()), Times.Once);
        }
    }
}