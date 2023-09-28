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
        private readonly ICompanyRepository _companyRep;
        private readonly IDriverRepository _driverRep;
        private readonly ITirRepository _tirRep;
        private readonly Mock<ICompanyRepository> _moqCompanyRepository;
        private readonly Mock<IDriverRepository> _moqDriverRepository;
        private readonly Mock<ITirRepository> _moqTirRepository;

        public CompanyCrudTest()
        {
            _moqCompanyRepository = new Mock<ICompanyRepository>();
            _moqDriverRepository = new Mock<IDriverRepository>();
            _moqTirRepository = new Mock<ITirRepository>();
            _companyRep = _moqCompanyRepository.Object;
            _driverRep = _moqDriverRepository.Object;
            _tirRep = _moqTirRepository.Object;
        }
        [Fact]
        public async Task ShouldCreateCompany()
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

            var result = await _companyRep.CreateAsync(model); // testing effettivo del metodo

            // Assert => verifico la correttezza dei dati manipolati
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);

            // Verifico che il metodo CreateAsync sia stato chiamato esattamente una volta
            _moqCompanyRepository.Verify(repo => repo.CreateAsync(It.IsAny<CompanyModel>()), Times.Once);
        }
        [Fact]
        public async Task ShouldUpdateCompany()
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

            var result = await _companyRep.UpdateAsync(noModifiedModel);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("TestModified", result.Name);
            _moqCompanyRepository.Verify(repo => repo.UpdateAsync(It.IsAny<CompanyModel>()), Times.Once);
        }
        [Fact]
        public async Task ShouldDeleteCompany()
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
            var result = await _companyRep.DeleteAsync(companyIdToDelete);
            Assert.Null(result);
            _moqCompanyRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task ShouldInsertCompany()
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
            var result = await _companyRep.InsertAsync("miao");

            Assert.NotNull(result);
            Assert.Equal("miao", result.Name);
            _moqCompanyRepository.Verify(repo => repo.InsertAsync(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task ShouldGetCompanyById()
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
            var result = await _companyRep.GetCompanyByIdAsync(companyIdToPick);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("miao", result.Name);
            _moqCompanyRepository.Verify(repo => repo.GetCompanyByIdAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task ShouldGetAllCompany()
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
            var result = await _companyRep.GetAllCompaniesAsync();
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
        [Fact]
        public async Task ShouldGetDriverById()
        {
            DriverModel driverModel = new DriverModel
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Age = 30,
            };
            _moqDriverRepository
                .Setup(repo => repo.GetDriverByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(driverModel);
            int driverIdToPick = 1;
            var result = await _driverRep.GetDriverByIdAsync(driverIdToPick);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John", result.Name); 
            _moqDriverRepository.Verify(repo => repo.GetDriverByIdAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task ShouldGetDriverByTirId()
        {
            var driver = new DriverModel
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Age = 30,
            };
            TirModel tir = new TirModel
            {
                Id = 1,
                Model = "TirModel1",
                Company = new CompanyModel
                {
                    Id = 1,
                    Name = "Company1",
                    DateOfFundation = DateTime.Now, 
                },
                CompanyId = 1, 
                DriverId = 1,  
                Driver = new DriverModel
                {
                    Id = 1,
                    Name = "John",
                    Surname = "Doe",
                    Age = 30,
                }
            };
            _moqDriverRepository
                .Setup(repo => repo.GetDriverByTirIdAsync(It.IsAny<int>()))
                .ReturnsAsync(driver);

            var result = await _driverRep.GetDriverByTirIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John", result.Name);
            _moqDriverRepository.Verify(repo => repo.GetDriverByTirIdAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task ShouldGetAllDrivers()
        {
            var driversData = new List<DriverModel>
            {
                new DriverModel { Id = 1, Name = "John", Surname = "Doe", Age = 30 },
                new DriverModel { Id = 2, Name = "Alice", Surname = "Smith", Age = 25 },
                new DriverModel { Id = 3, Name = "Bob", Surname = "Johnson", Age = 35 }
            };
            _moqDriverRepository
                .Setup(repo => repo.GetAllDriversAsync())
                .ReturnsAsync(driversData);

            var result = await _driverRep.GetAllDriversAsync();
            Assert.NotNull(result);
            Assert.Equal(driversData.Count, result.Count());
            for (int i = 0; i < driversData.Count; i++)
            {
                Assert.Equal(driversData[i].Id, result.ElementAt(i).Id);
                Assert.Equal(driversData[i].Name, result.ElementAt(i).Name);
                Assert.Equal(driversData[i].Surname, result.ElementAt(i).Surname);
                Assert.Equal(driversData[i].Age, result.ElementAt(i).Age);
            }
            _moqDriverRepository.Verify(repo => repo.GetAllDriversAsync(), Times.Once);
        }
        [Fact]
        public async Task ShouldUpdateDriver()
        {
            DriverModel unmodifiedDriver = new DriverModel
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Age = 30,
            };
            DriverModel modifiedDriver = new DriverModel
            {
                Id = 1,
                Name = "UpdatedName",
                Surname = "UpdatedSurname",
                Age = 35,
            };
            _moqDriverRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<DriverModel>()))
                .ReturnsAsync(modifiedDriver);

            var result = await _driverRep.UpdateAsync(modifiedDriver);
            Assert.NotNull(result);
            Assert.Equal(modifiedDriver.Id, result.Id);
            Assert.Equal(modifiedDriver.Name, result.Name);
            Assert.Equal(modifiedDriver.Surname, result.Surname);
            Assert.Equal(modifiedDriver.Age, result.Age);
            _moqDriverRepository.Verify(repo => repo.UpdateAsync(It.IsAny<DriverModel>()), Times.Once);
        }
        [Fact]
        public async Task ShouldInsertDriver()
        {
            string name = "John";
            string surname = "Doe";
            int age = 30;
            int tirId = 1;
            int companyId = 1;

            _moqDriverRepository
                .Setup(repo => repo.InsertAsync(name, surname, age, tirId, companyId))
                .ReturnsAsync((string driverName, string driverSurname, int driverAge, int driverTirId, int driverCompanyId) =>
                {
                    var driver = new DriverModel()
                    {
                        Name = driverName,
                        Surname = driverSurname,
                        Age = driverAge,
                        TirId = driverTirId,
                        CompanyId = driverCompanyId,
                        Id = 1, 
                    };
                    return driver;
                });
            var result = await _driverRep.InsertAsync(name, surname, age, tirId, companyId);

            Assert.NotNull(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(age, result.Age);
            Assert.Equal(tirId, result.TirId);
            Assert.Equal(companyId, result.CompanyId);
            _moqDriverRepository.Verify(repo => repo.InsertAsync(name, surname, age, tirId, companyId), Times.Once);
        }
    }
}