﻿using DAL.Data;
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
    public class OrderRepositoryTests
    {
        [TestCase(1)]
        [TestCase(3)]
        public async Task OrderRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var order = await orderRepository.GetByIdAsync(id);

            var expected = ExpectedOrders.FirstOrDefault(x => x.Id == id);

            Assert.That(order, Is.EqualTo(expected).Using(new OrderEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task OrderRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var orders = await orderRepository.GetAllAsync();

            Assert.That(orders, Is.EqualTo(ExpectedOrders).Using(new OrderEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task OrderRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var order = new Order { Id = 4 };

            await orderRepository.AddAsync(order);
            await context.SaveChangesAsync();

            Assert.That(context.Orders.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task OrderRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);

            await orderRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Orders.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task OrderRepository_Update_UpdatesEntity()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var order = new Order
            {
                Id = 1,
                OperationDate = new DateTime(2021, 9, 10),
                UserId = 1
            };

            orderRepository.Update(order);
            await context.SaveChangesAsync();

            Assert.That(order, Is.EqualTo(new Order
            {
                Id = 1,
                OperationDate = new DateTime(2021, 9, 10),
                UserId = 1
            }).Using(new OrderEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task OrderRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);

            var order = await orderRepository.GetByIdWithDetailsAsync(1);

            var expected = ExpectedOrders.FirstOrDefault(x => x.Id == 1);

            Assert.That(order,
                Is.EqualTo(expected).Using(new OrderEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(order.OrderDetails,
                Is.EqualTo(ExpectedOrdersDetails.Where(i => i.OrderId == expected.Id).OrderBy(i => i.Id)).Using(new OrderDetailEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(order.OrderDetails.Select(i => i.Lot).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedLots.Where(i => i.Id == 1 || i.Id == 2)).Using(new LotEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(order.User,
                Is.EqualTo(ExpectedUsers.FirstOrDefault(i => i.Id == expected.UserId)).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        [Test]
        public async Task OrderRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new AuctionDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);

            var orders = await orderRepository.GetAllWithDetailsAsync();

            Assert.That(orders,
                Is.EqualTo(ExpectedOrders).Using(new OrderEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(orders.SelectMany(i => i.OrderDetails).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedOrdersDetails).Using(new OrderDetailEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(orders.Select(i => i.User).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");
        }



        private static IEnumerable<Order> ExpectedOrders =>
            new[]
            {
                new Order { Id = 1,
                    OperationDate = new DateTime(2022,12,02),
                    UserId = 1
                },
                new Order { Id = 2,
                    OperationDate = new DateTime(2022, 8, 10),
                    UserId = 1
                },
                new Order { Id = 3,
                    OperationDate = new DateTime(2022, 10, 10),
                    UserId = 2
                }

            };

        private static IEnumerable<Lot> ExpectedLots =>
            new[]
            {
                new Lot
                {   Id = 1,
                    Title = "2016 BMW 1er 116i Advantage",
                    Status = LotStatus.Created,
                    MinRate = 50,
                    StartDate = new DateTime(2022, 6, 20, 20, 0, 0),
                    EndDate = new DateTime(2022, 6, 22, 20, 0, 0),
                    StartPrice = 1600,
                    PhotoId = 1,
                    CurrentPrice = 2100.33M,
                },
                new Lot
                {
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

        private static IEnumerable<Photo> ExpectedPhotos =>
            new[]
            {
                new Photo
                {
                    Id = 1,
                    PhotoSrc = "Photo1",
                    GroupOfPhoto = 1
                },
                new Photo
                {
                    Id = 2,
                    PhotoSrc = "Photo2",
                    GroupOfPhoto = 1
                }
            };

        private static IEnumerable<OrderDetail> ExpectedOrdersDetails =>
            new[]
            {
                 new OrderDetail
                {
                     Id = 1,
                    LotId = 1,
                    OrderId = 1,
                    Status = LotDetailStatus.Bid_placed
                },
                new OrderDetail
                {
                    Id = 2,
                    LotId = 2,
                    OrderId = 1,
                    Status = LotDetailStatus.Bid_placed
                },
                new OrderDetail
                {
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
                    Id=2,
                    Name = "Fedor",
                    Surname = "Lokovskiy",
                    Location = "Ukraine,Lviv",
                    Email = "feLo@gmail.com",
                    Password = "12345",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "096-837-3031",
                }

            };
    }
}
