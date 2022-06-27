using DAL.Data;
using DAL.Entities;
using BLL;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;

namespace AuctionTests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<AuctionDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new AuctionDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new Automapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        public static void SeedData(AuctionDbContext context)
        {
            context.Users.AddRange(
                new User
                {
                    Name = "Oleh",
                    Surname = "Mandra",
                    Location = "Ukraine,Kyiv",
                    Email = "mandra@gmail.com",
                    Password = "123",
                    AccessLevel = Role.Admin,
                    PhoneNumber = "095-937-3031"
                },
                new User
                {
                    Name = "Fedor",
                    Surname = "Lokovskiy",
                    Location = "Ukraine,Lviv",
                    Email = "feLo@gmail.com",
                    Password = "12345",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "096-837-3031",
                });

            context.Photos.AddRange(
                new Photo
                {
                    PhotoSrc = "Photo1",
                    GroupOfPhoto = 1
                },
                new Photo
                {
                    PhotoSrc = "Photo2",
                    GroupOfPhoto = 1
                },
                new Photo
                {
                    PhotoSrc = "Photo3",
                    GroupOfPhoto = 2
                });

            context.Lots.AddRange(
                new Lot
                {
                    Title = "2016 BMW 1er 116i Advantage",
                    Status = LotStatus.Created,
                    MinRate = 50,
                    StartDate = new DateTime(2022, 6, 20, 20, 0, 0),
                    EndDate = new DateTime(2022, 6, 22, 20, 0, 0),
                    StartPrice = 1600,
                    PhotoId = 1,
                    CurrentPrice = 2100.33M
                },
                new Lot
                {
                    Title = "2007 Volkswagen Golf V Plus Tour",
                    Status = LotStatus.Created,
                    MinRate = 110,
                    StartDate = new DateTime(2022, 6, 25, 20, 0, 0),
                    EndDate = new DateTime(2022, 6, 28, 20, 0, 0),
                    StartPrice = 3400,
                    PhotoId = 3,
                    CurrentPrice = 8600.33M
                });

            context.Orders.AddRange(
                new Order
                {
                    OperationDate = new DateTime(2022, 12, 02),
                    UserId = 1
                },
                new Order
                {
                    OperationDate = new DateTime(2022, 8, 10),
                    UserId = 1
                },
                new Order
                {
                    OperationDate = new DateTime(2022, 10, 10),
                    UserId = 2
                });

            context.OrderDetails.AddRange(
                new OrderDetail
                {
                    LotId = 1,
                    OrderId = 1,
                    Status = LotDetailStatus.Bid_placed
                },
                new OrderDetail
                {
                    LotId = 2,
                    OrderId = 1,
                    Status = LotDetailStatus.Bid_placed
                },
                new OrderDetail
                {
                    LotId = 2,
                    OrderId = 2,
                    Status = LotDetailStatus.Bid_placed
                },
                new OrderDetail
                {
                    LotId = 1,
                    OrderId = 3,
                    Status = LotDetailStatus.Bid_placed
                },
                new OrderDetail
                {
                    LotId = 2,
                    OrderId = 3,
                    Status = LotDetailStatus.Bid_placed
                });

            context.SaveChanges();
        }
    }

}
