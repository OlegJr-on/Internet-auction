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
        [Test]
        public async Task OrderService_GetAll_ReturnsAllOrders()
        {
            //arrange
            var expected = GetTestOrdersModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.OrderRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestOrdersEntities.AsEnumerable());

            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await orderService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task OrderService_GetById_ReturnsOrderModel(int id)
        {
            //arrange
            var expected = GetTestOrdersModels.FirstOrDefault(x => x.Id == id);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestOrdersEntities.FirstOrDefault(x => x.Id == id));

            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await orderService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task OrderService_AddAsync_AddsOrder()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.OrderRepository.AddAsync(It.IsAny<Order>()));

            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var receipt = GetTestOrdersModels.First();

            //act
            await orderService.AddAsync(receipt);

            //assert
            mockUnitOfWork.Verify(x => x.OrderRepository.AddAsync(It.Is<Order>(c => c.Id == receipt.Id)), Times.Once);
        }

        [Test]
        public async Task OrderService_UpdateAsync_UpdatesOrder()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.OrderRepository.Update(It.IsAny<Order>()));

            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var order = GetTestOrdersModels.First();

            //act
            await orderService.UpdateAsync(order);

            //assert
            mockUnitOfWork.Verify(x => x.OrderRepository.Update(It.Is<Order>(order => order.Id == order.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public async Task OrderService_GetOrderDetailsAsync_ReturnsDetailsByOrderId(int receiptId)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestOrdersEntities.FirstOrDefault(x => x.Id == receiptId));
            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await orderService.GetOrderDetailsAsync(receiptId);

            //assert
            var expected = GetTestOrdersEntities.FirstOrDefault(x => x.Id == receiptId)?.OrderDetails;

            actual.Should().BeEquivalentTo(expected, options => options.Excluding(x => x.Lot).Excluding(x => x.Order));
        }

        [TestCase("2022-8-1", "2022-9-15", new[] { 2, 3 })]
        [TestCase("2022-10-1", "2022-12-29", new[] { 1 })]
        [TestCase("2022-7-1", "2022-8-20", new[] { 2, 4 })]
        public async Task OrderService_GetOrdersByPeriodAsync_ReturnsOrdersInPeriod(DateTime startDate, DateTime endDate, IEnumerable<int> expectedReceiptIds)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepository.GetAllWithDetailsAsync()).ReturnsAsync(GetTestOrdersEntities.AsEnumerable());
            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await orderService.GetOrdersByPeriodAsync(startDate, endDate);

            //assert
            var expected = GetTestOrdersModels.Where(x => expectedReceiptIds.Contains(x.Id));

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase]
        public async Task OrderService_AddLotAsync_CreatesOrderDetailIfLotWasNotAddedBefore()
        {
            //arrange 
            var productId = 4;

            var order = new Order
            {
                Id = 1,
                User = new User { Id = 1 },
                OrderDetails = new List<OrderDetail> {
                new OrderDetail { Id = 1, LotId = 1,OrderId = 1 },
                new OrderDetail { Id = 2, LotId = 2, OrderId = 1 },
                new OrderDetail { Id = 3, LotId = 3, OrderId = 1 }
            }
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdWithDetailsAsync(It.IsAny<int>())).ReturnsAsync(order);
            mockUnitOfWork.Setup(x => x.LotRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(LotEntities.FirstOrDefault(x => x.Id == productId));
            mockUnitOfWork.Setup(x => x.OrderDetailRepository.AddAsync(It.IsAny<OrderDetail>()));
            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await orderService.AddLotAsync(productId, 1);

            //assert
            mockUnitOfWork.Verify(x => x.OrderDetailRepository.AddAsync(It.Is<OrderDetail>(receiptDetail =>
                receiptDetail.OrderId == order.Id && receiptDetail.LotId == productId)), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(11)]
        public async Task OrderService_AddProduct_ThrowsAuctionExceptionIfLotDoesNotExist(int lotId)
        {
            //arrange
            var order = new Order { Id = 1, UserId = 1 };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdWithDetailsAsync(It.IsAny<int>())).ReturnsAsync(order);
            mockUnitOfWork.Setup(x => x.LotRepository.GetByIdAsync(It.IsAny<int>()));

            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            Func<Task> act = async () => await orderService.AddLotAsync(lotId, 1);

            await act.Should().ThrowAsync<AuctionException>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public async Task OrderService_AddLot_ThrowsAuctionExceptionIfOrderDoesNotExist(int orderId)
        {
            //arrange

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdWithDetailsAsync(It.IsAny<int>()));
            mockUnitOfWork.Setup(x => x.LotRepository.GetByIdAsync(It.IsAny<int>()));

            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            Func<Task> act = async () => await orderService.AddLotAsync(1, orderId);

            await act.Should().ThrowAsync<AuctionException>();
        }



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
