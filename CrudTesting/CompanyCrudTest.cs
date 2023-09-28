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
using GreenDonut;

namespace CrudTesting
{
    public class CompanyCrudTest
    {
        private readonly ICompanyRepository _myRep;
        private readonly Mock<ICompanyRepository> _moqCompanyRepository;

        public CompanyCrudTest()
        {
            _moqCompanyRepository = new Mock<ICompanyRepository>();
            _myRep = _moqCompanyRepository.Object;
        }
        [Fact]
        public async Task CreatingTest()
        {
            CompanyModel model = new CompanyModel
            {
                Id = 1,
                Name = "Test",
                DateOfFundation = DateTime.Now
            };
            _moqCompanyRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<CompanyModel>()))
                .ReturnsAsync(model);

            var result = await _myRep.CreateAsync(model); // testing effettivo del metodo

            // Assert => verifico la correttezza dei dati manipolati
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);

            // Verifico che il metodo CreateAsync sia stato chiamato esattamente una volta
            _moqCompanyRepository.Verify(repo => repo.CreateAsync(It.IsAny<CompanyModel>()), Times.Once);
        }
        [Fact]
        public async Task UpdatingTest()
        {
            CompanyModel noModifiedModel = new CompanyModel
            {
                Id = 1,
                Name = "noModified",
                DateOfFundation = DateTime.Now
            };
            CompanyModel modelModified = new CompanyModel
            {
                Id = 1,
                Name = "TestModified",
                DateOfFundation = DateTime.Now
            };
            _moqCompanyRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<CompanyModel>()))
                .ReturnsAsync(modelModified);

            var result = await _myRep.UpdateAsync(noModifiedModel);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("TestModified", result.Name);
            _moqCompanyRepository.Verify(repo => repo.UpdateAsync(It.IsAny<CompanyModel>()), Times.Once);
        }
        [Fact]
        public async Task DeletingTest()
        {
            CompanyModel existingModel = new CompanyModel
            {
                Id = 1,
                Name = "ExistingCompany",
                DateOfFundation = DateTime.Now
            };

            // Configurazione
            _moqCompanyRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync((int companyId) =>
                {
                    if (companyId == existingModel.Id)
                    {
                        existingModel = null;
                        return existingModel;//simulazione di eliminazione se id corrisponde
                    }
                    return null;
                });

            int companyIdToDelete = 1;
            var result = await _myRep.DeleteAsync(companyIdToDelete);
            Assert.Null(result);
            _moqCompanyRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task InsertingTest()
        {
            //configure
            _moqCompanyRepository
                .Setup(repo => repo.InsertAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) =>
                {
                    var company = new CompanyModel()
                    {
                        Name = name,
                        DateOfFundation = DateTime.Now
                    };
                    return company;
                });
            var result = await _myRep.InsertAsync("miao");

            Assert.NotNull(result);
            Assert.Equal("miao", result.Name);
            _moqCompanyRepository.Verify(repo => repo.InsertAsync(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task GetCompanyByIdTest()
        {
            CompanyModel model = new CompanyModel
            {
                Id = 1,
                Name = "miao",
                DateOfFundation = DateTime.Now
            };

            // Configurazione
            _moqCompanyRepository
                .Setup(repo => repo.GetCompanyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            int companyIdToPick = 1;
            var result = await _myRep.GetCompanyByIdAsync(companyIdToPick);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("miao", result.Name);
            _moqCompanyRepository.Verify(repo => repo.GetCompanyByIdAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task GetAllCompanyTest()
        {
            var companiesData = new List<CompanyModel>
            {
                new CompanyModel { Id = 1, Name = "Company1", DateOfFundation = DateTime.Now },
                new CompanyModel { Id = 2, Name = "Company2", DateOfFundation = DateTime.Now },
                new CompanyModel { Id = 3, Name = "Company3", DateOfFundation = DateTime.Now }
            };
            _moqCompanyRepository
                .Setup(repo => repo.GetAllCompaniesAsync())
                .ReturnsAsync(companiesData);
            var result = await _myRep.GetAllCompaniesAsync();
            var companyArray = result.ToArray();
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            for (int i = 0; i < companiesData.Count; i++)
            {
                Assert.Equal(companiesData[i].Id, companyArray[i].Id);
                Assert.Equal(companiesData[i].Name, companyArray[i].Name);
            }
            _moqCompanyRepository.Verify(repo => repo.GetAllCompaniesAsync(), Times.Once);
        }
    }
}