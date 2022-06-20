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
    public class UserRepositoryTests
    {



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
