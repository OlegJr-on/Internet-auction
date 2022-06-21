using DAL.Data;
using DAL.Entities;
using DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionTests.DAL.Tests
{
    [TestFixture]
    public class LotRepositoryTests {

        [TestCase(1)]
        [TestCase(2)]
        public async Task LotRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var LotRepository = new LotRepository(context);

            var lot = await LotRepository.GetByIdAsync(id);

            var expected = ExpectedLots.FirstOrDefault(x => x.Id == id);

            Assert.That(lot, Is.EqualTo(expected).Using(new LotEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task LotRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var LotRepository = new LotRepository(context);

            var lots = await LotRepository.GetAllAsync();

            Assert.That(lots, Is.EqualTo(ExpectedLots).Using(new LotEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task LotRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var LotRepository = new LotRepository(context);
            var lot = new Lot { Id = 3 };

            await LotRepository.AddAsync(lot);
            await context.SaveChangesAsync();

            Assert.That(context.Lots.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task LotRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var LotRepository = new LotRepository(context);

            await LotRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Lots.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task LotRepository_Update_UpdatesEntity()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var LotRepository = new LotRepository(context);
            var lot = new Lot
            {
                Id = 1,
                Title = "2016 BMW 1er 116i Advantage",
                Status = LotStatus.Created,
                MinRate = 50,
                StartDate = new DateTime(2022, 6, 20, 20, 0, 0),
                EndDate = new DateTime(2022, 6, 22, 20, 0, 0),
                StartPrice = 1600,
                PhotoId = 1,
                CurrentPrice = 2100.33M
            };

            LotRepository.Update(lot);
            await context.SaveChangesAsync();

            Assert.That(lot, Is.EqualTo(new Lot
            {
                Id = 1,
                Title = "2016 BMW 1er 116i Advantage",
                Status = LotStatus.Created,
                MinRate = 50,
                StartDate = new DateTime(2022, 6, 20, 20, 0, 0),
                EndDate = new DateTime(2022, 6, 22, 20, 0, 0),
                StartPrice = 1600,
                PhotoId = 1,
                CurrentPrice = 2100.33M
            }).Using(new LotEqualityComparer()), message: "Update method works incorrect");
        }

        private static IEnumerable<Lot> ExpectedLots =>
            new[]
            {
                new Lot {
                    Id = 1,
                    Title = "2016 BMW 1er 116i Advantage",
                    Status = LotStatus.Created,
                    MinRate = 50,
                    StartDate = new DateTime(2022, 6, 20, 20, 0, 0),
                    EndDate = new DateTime(2022, 6, 22, 20, 0, 0),
                    StartPrice = 1600,
                    PhotoId = 1,
                    CurrentPrice = 2100.33M,
                },
                new Lot {
                    Id=2,
                    Title = "2007 Volkswagen Golf V Plus Tour",
                    Status = LotStatus.Created,
                    MinRate = 110,
                    StartDate = new DateTime(2022, 6, 25, 20, 0, 0),
                    EndDate = new DateTime(2022, 6, 28, 20, 0, 0),
                    StartPrice = 3400,
                    PhotoId = 3,
                    CurrentPrice = 8600.33M
                }
            };
    }
}
