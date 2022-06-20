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
    public class UserRepositoryTests
    {

        [TestCase(1)]
        [TestCase(2)]
        public async Task UserRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);

            var user = await userRepository.GetByIdAsync(id);

            var expected = ExpectedUsers.FirstOrDefault(x => x.Id == id);

            Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);

            var users = await userRepository.GetAllAsync();

            Assert.That(users, Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);
            var user = new User { Id = 3 };

            await userRepository.AddAsync(user);
            await context.SaveChangesAsync();

            Assert.That(context.Users.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);

            await userRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Users.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task UserRepository_Update_UpdatesEntity()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);
            var user = new User
            {
                Id = 1,
                Name = "Oleh",
                Surname = "Mandra",
                Location = "Ukraine,Kyiv",
                Email = "mandra@gmail.com",
                Password = "123",
                AccessLevel = Role.Admin,
                PhoneNumber = "095-937-3031"
            };

            userRepository.Update(user);
            await context.SaveChangesAsync();

            Assert.That(user, Is.EqualTo(new User
            {
                Id = 1,
                Name = "Oleh",
                Surname = "Mandra",
                Location = "Ukraine,Kyiv",
                Email = "mandra@gmail.com",
                Password = "123",
                AccessLevel = Role.Admin,
                PhoneNumber = "095-937-3031"
            }).Using(new UserEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task UserRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);

            var user = await userRepository.GetByIdWithDetailsAsync(1);

            var expected = ExpectedUsers.FirstOrDefault(x => x.Id == 1);

            Assert.That(user,
                Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(user.Orders.ToList(),
                Is.EqualTo(ExpectedOrders.Where(i => i.UserId == expected.Id)).Using(new OrderEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(user.Orders.SelectMany(i => i.OrderDetails).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedOrdersDetails.Where(i => i.OrderId == 1 || i.OrderId == 2)).Using(new OrderDetailEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }




        private static IEnumerable<User> ExpectedUsers =>
            new[]
            {
                 new User {
                    Id = 1,
                    Name = "Oleh",
                    Surname = "Mandra",
                    Location = "Ukraine,Kyiv",
                    Email = "mandra@gmail.com",
                    Password = "123",
                    AccessLevel = Role.Admin,
                    PhoneNumber = "095-937-3031"
                },
                new User {
                    Id = 2,
                    Name = "Fedor",
                    Surname = "Lokovskiy",
                    Location = "Ukraine,Lviv",
                    Email = "feLo@gmail.com",
                    Password = "12345",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "096-837-3031",
                }
            };

        private static IEnumerable<Order> ExpectedOrders =>
            new[]
            {
                    new Order {
                        Id = 1,
                        OperationDate = new DateTime(2022,12,02),
                        UserId = 1
                    },
                    new Order {
                        Id = 2,
                        OperationDate = new DateTime(2022, 8, 10),
                        UserId = 1
                    },
                    new Order {
                        Id = 3,
                        OperationDate = new DateTime(2022, 10, 10),
                        UserId = 2
                    }
            };

        private static IEnumerable<OrderDetail> ExpectedOrdersDetails =>
            new[]
            {
                  new OrderDetail {
                       Id = 1,
                       LotId = 1,
                       OrderId = 1,
                       Status = LotDetailStatus.Bid_placed
                  },
                  new OrderDetail {
                       Id = 2,
                       LotId = 2,
                       OrderId = 1,
                       Status = LotDetailStatus.Bid_placed
                  },
                  new OrderDetail {
                       Id = 3,
                       LotId = 2,
                       OrderId = 2,
                       Status = LotDetailStatus.Bid_placed
                  },
                  new OrderDetail {
                       Id = 4,
                       LotId = 1,
                       OrderId = 3,
                       Status = LotDetailStatus.Bid_placed
                  },
                  new OrderDetail {
                       Id = 5,
                       LotId = 2,
                       OrderId = 3,
                       Status = LotDetailStatus.Bid_placed
                  }
            };
    }

}
