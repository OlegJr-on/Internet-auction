using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using FluentAssertions;
using BLL;

namespace AuctionTests.BLL.Tests
{
    public class OrderServiceTests
    {


        private static IEnumerable<Order> GetTestOrdersEntities =>
          new List<Order>()
          {
                new Order
                {
                    Id = 1,
                    OperationDate = new DateTime(2022, 12, 2),
                    UserId = 1,
                    OrderDetails = new List<OrderDetail>()
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
                            LotId = 3,
                            OrderId = 1,
                            Status = LotDetailStatus.Bid_placed
                        },
                    }
                },

                new Order
                {
                    Id=2,
                    OperationDate = new DateTime(2022, 8, 15),
                    UserId = 2,
                    OrderDetails = new List<OrderDetail>()
                    {
                         new OrderDetail
                        {
                             Id = 4,
                            LotId = 1,
                            OrderId = 2,
                            Status = LotDetailStatus.Bid_placed
                        },
                         new OrderDetail
                        {
                            Id = 5,
                            LotId = 3,
                            OrderId = 2,
                            Status = LotDetailStatus.Bid_placed
                        },
                    }
                },

                new Order
                {
                    Id= 3,
                    OperationDate = new DateTime(2022, 9, 14),
                    UserId = 3,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail
                        {
                             Id = 6,
                            LotId = 1,
                            OrderId = 3,
                            Status = LotDetailStatus.Bid_placed
                        },
                        new OrderDetail
                        {
                            Id = 7,
                            LotId = 2,
                            OrderId = 3,
                            Status = LotDetailStatus.Bid_placed
                        },
                    }
                },

                new Order
                {
                    Id= 4,
                    OperationDate = new DateTime(2022, 7,7),
                    UserId = 4,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail
                        {
                             Id = 8,
                            LotId = 5,
                            OrderId = 4,
                            Status = LotDetailStatus.Bid_placed
                        },
                        new OrderDetail
                        {
                            Id = 9,
                            LotId = 6,
                            OrderId = 4,
                            Status = LotDetailStatus.Bid_placed
                        },
                    }
                }
          };

        private static IEnumerable<OrderModel> GetTestOrdersModels =>
         new List<OrderModel>()
         {
                new OrderModel
                {
                    Id = 1, UserId = 1,  OperationDate = new DateTime(2022, 12, 2),
                    OrderDetailsIds = new List<int>()
                    {
                       1,
                       2,
                       3,
                    }
                },
                new OrderModel
                {
                    Id = 2, UserId = 2,   OperationDate = new DateTime(2022, 8, 15),
                    OrderDetailsIds = new List<int>()
                    {
                        4,
                        5
                    }
                },
                new OrderModel
                {
                    Id = 3, UserId = 3, OperationDate = new DateTime(2022, 9, 14),
                    OrderDetailsIds = new List<int>()
                    {
                        6,
                        7
                    }
                },
                  new OrderModel
                {
                    Id = 4, UserId = 4, OperationDate = new DateTime(2022, 7,7),
                    OrderDetailsIds = new List<int>()
                    {
                        8,
                        9
                    }
                }
         };


        private static IEnumerable<Lot> LotEntities =>
            new List<Lot>
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
                    Photo = new Photo { Id = 1, PhotoSrc = "Photo1", GroupOfPhoto = 1}
                },
                new Lot {
                    Id=2,
                    Title = "2007 Volkswagen Golf V Plus Tour",
                    Status = LotStatus.Created,
                    MinRate = 110,
                    StartDate = new DateTime(2022, 6, 25, 20, 0, 0),
                    EndDate = new DateTime(2022, 6, 28, 20, 0, 0),
                    StartPrice = 3400,
                    PhotoId = 1,
                    CurrentPrice = 8600.33M,
                    Photo = new Photo { Id = 1, PhotoSrc = "Photo1",GroupOfPhoto = 1 }
                },
                new Lot {
                    Id= 3,
                    Title = "2020 Audi RS7 Advanture",
                    Status = LotStatus.Created,
                    MinRate = 500,
                    StartDate = new DateTime(2022, 7, 25, 20, 0, 0),
                    EndDate = new DateTime(2022, 7, 28, 12, 0, 0),
                    StartPrice = 17500,
                    PhotoId = 2,
                    CurrentPrice = 26666.33M,
                    Photo = new Photo { Id = 2, PhotoSrc = "Photo2",GroupOfPhoto = 2 }
                },
                new Lot {
                    Id= 4,
                    Title = "2002 BMW M3 SportM",
                    Status = LotStatus.Created,
                    MinRate = 50,
                    StartDate = new DateTime(2022, 7, 12, 20, 0, 0),
                    EndDate = new DateTime(2022, 7, 18, 12, 0, 0),
                    StartPrice = 3200,
                    PhotoId = 2,
                    CurrentPrice = 3500,
                     Photo = new Photo { Id = 2, PhotoSrc = "Photo2",GroupOfPhoto = 2 }
                },
                new Lot {
                    Id= 5,
                    Title = "2010 Ford Focus",
                    Status = LotStatus.Created,
                    MinRate = 50,
                    StartDate = new DateTime(2022, 9, 15, 20, 0, 0),
                    EndDate = new DateTime(2022, 9, 22, 12, 0, 0),
                    StartPrice = 1000,
                    PhotoId = 3,
                    CurrentPrice = 1000,
                    Photo = new Photo { Id = 3, PhotoSrc = "Photo3",GroupOfPhoto = 3 }
                }
            };
    }
}
