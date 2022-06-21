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
    internal class OrderDetailRepositoryTests
    {



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
    }
}
