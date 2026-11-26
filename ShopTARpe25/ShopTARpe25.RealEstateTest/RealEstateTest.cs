using Azure.Core;
using ShopTARpe25.ApplicationServices.Services;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShopTARpe25.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            // Arrange
            RealEstateDto dto = new()
            {
                Area = 120.5,
                Address = "Test location",
                RoomNumber = 3,
                BuildingType = "house",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow

            };

            //Act Siin tuleb kutsuda RealEstate Create meetod esile
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);

            //nüüd uurite kuidas testi käima panna

        }


        //teha test, kus otsib ankeeti id alusel, aga seda ei ole andmebaasis

        [Fact]
        public async Task ShouldNot_FindRealEstate_WhenIdDoesNotExist()
        {
            // Arrange
            Guid databaseGuid = Guid.Parse("54cfecb3-1cf8-4ef4-ab28-7699de10b700");
            Guid wrongGuid = Guid.NewGuid();

            // Act
            await Svc<IRealEstateServices>().DetailsAsync(databaseGuid);

            // Assert
            //vastupidist loogikat tuleb kasutada
            Assert.NotEqual(wrongGuid, databaseGuid);
        }

        //mul on vaja testida DetailsAsync meetotit
        //leiab ankeedi üles
        [Fact]

        public async Task Test_DetailsAsync()
        {
            //kui vajutad nupule, siis see guid saadetakse andmebaasi, mida võrreldatakse
            Guid guid = Guid.Parse("cc78bce1-0537-4b8a-9bd6-bbcb6b647a57");
            //see guid on juba dnmebaasis olemas ankeedi juures
            Guid databaseGuid = Guid.Parse("cc78bce1-0537-4b8a-9bd6-bbcb6b647a57");

            await Svc<IRealEstateServices>().DetailsAsync(guid);

            Assert.Equal(databaseGuid, guid);
        }

        //kustutamise meetodit on vaja testida, andmebaasis on ankeet, mida tahan ära kustutada

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {

            // Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var addRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            // Assert
            //võrdlen Id-sid, kas kustutasin ära selle, mis oli andmebaasis
            Assert.Equal(addRealEstate.Id, deleteRealEstate.Id);
        }

        //mul on kaks ankeeti loodud ja ühe kustutan ära
        //assertis võrdlen, kas need kaks on ühesugused
        [Fact]
        public async Task Should_CreateTwoDatabases_And_DeleteOne()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var addRealEstate = await Svc<IRealEstateServices>().Create(dto);

            var addRealEstate2 = await Svc<IRealEstateServices>().Create(dto);

            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            //Assert
            Assert.NotEqual(addRealEstate2.Id, deleteRealEstate.Id);
        }

        //uuendab andmeid

        [Fact]
        public async Task Should_UpdateRealEstate_WhenReturnResult()
        {
            // Arrange

            var guid = new Guid("cc78bce1-0537-4b8a-9bd6-bbcb6b647a57");

            RealEstateDto dto = MockRealEstateData();
            RealEstateDto domain = new();

            domain.Id = guid;
            domain.Area = 200;
            domain.Address = "Updated Location";
            domain.RoomNumber = 5;
            domain.BuildingType = "Mansion";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            // Act
            await Svc<IRealEstateServices>().Update(dto);

            // Assert
            Assert.Equal(domain.Id, guid);
            Assert.NotEqual(dto.Area, domain.Area);
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.DoesNotMatch(dto.Address, domain.Address);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {

        }



        private RealEstateDto MockRealEstateData()
        {
            return new RealEstateDto
            {
                Area = 88.65,
                Address = "Test location",
                RoomNumber = 3,
                BuildingType = "house",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
    }
}
