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
    public class UserServiceTests
    {
        [Test]
        public async Task UserService_GetAll_ReturnsAllUsers()
        {
            //arrange
            var expected = GetTestUserModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.UserRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestUserEntities.AsEnumerable());

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await userService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserService_GetById_ReturnsUserModel()
        {
            //arrange
            var expected = GetTestUserModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.UserRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestUserEntities.First());

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await userService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));

            var UserService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var customer = GetTestUserModels.First();

            //act
            await UserService.AddAsync(customer);

            //assert
            mockUnitOfWork.Verify(x => x.UserRepository.AddAsync(It.Is<User>(x =>
                            x.Id == customer.Id && x.Password == customer.Password &&
                            x.Surname == customer.Surname && x.Name == customer.Name &&
                            x.PhoneNumber == customer.PhoneNumber && x.Email == customer.Email)), Times.Once);
        }


        [Test]
        public async Task UserService_AddAsync_ThrowsAuctionExceptionWithEmptyName()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var user = GetTestUserModels.First();
            user.Name = string.Empty;

            //act
            Func<Task> act = async () => await userService.AddAsync(user);

            //assert
            await act.Should().ThrowAsync<AuctionException>();
        }

        [Test]
        public async Task UserService_AddAsync_ThrowsAuctionExceptionWithNullObject()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            Func<Task> act = async () => await userService.AddAsync(null);

            //assert
            await act.Should().ThrowAsync<AuctionException>();
        }



        public List<UserModel> GetTestUserModels =>
           new List<UserModel>()
           {
                 new UserModel {
                    Id = 1,
                    Name = "Oleh",
                    Surname = "Mandra",
                    Location = "Ukraine,Kyiv",
                    Email = "mandra@gmail.com",
                    Password = "123",
                    AccessLevel = Role.Admin,
                    PhoneNumber = "095-937-3031",
                    OrdersIds = new List<int>(){ 1 }
                    },
                new UserModel {
                    Id = 2,
                    Name = "Nassim",
                    Surname = "Vakulenko",
                    Location = "Ukraine,Lviv",
                    Email = "vaN@gmail.com",
                    Password = "12345",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "095-937-3044",
                    OrdersIds = new List<int>(){ 2 }
                    },
                new UserModel {
                    Id = 3,
                    Name = "Mykola",
                    Surname = "Iaremchuk",
                    Location = "Ukraine,Kharkiv",
                    Email = "vaN@gmail.com",
                    Password = "1234445",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "095-737-3044",
                    OrdersIds = new List<int>(){ 3 }
                    },
                new UserModel {
                    Id = 4,
                    Name = "Andriy",
                    Surname = "Orenko",
                    Location = "Ukraine,Kharkiv",
                    Email = "vaN@gmail.com",
                    Password = "1234104445",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "095-747-3044",
                    OrdersIds = new List<int>(){ 4 }
                    }
           };


        public List<User> GetTestUserEntities =>
            new List<User>()
            {
                 new User {
                    Id = 1,
                    Name = "Oleh",
                    Surname = "Mandra",
                    Location = "Ukraine,Kyiv",
                    Email = "mandra@gmail.com",
                    Password = "123",
                    AccessLevel = Role.Admin,
                    PhoneNumber = "095-937-3031",
                    Orders = new List<Order> { GetTestOrdersEntitiesWithOrderDetails[0] }
                    },
                  new User {
                    Id = 2,
                    Name = "Nassim",
                    Surname = "Vakulenko",
                    Location = "Ukraine,Lviv",
                    Email = "vaN@gmail.com",
                    Password = "12345",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "095-937-3044",
                    Orders = new List<Order> { GetTestOrdersEntitiesWithOrderDetails[1] }
                    },
                new User {
                    Id = 3,
                    Name = "Mykola",
                    Surname = "Iaremchuk",
                    Location = "Ukraine,Kharkiv",
                    Email = "vaN@gmail.com",
                    Password = "1234445",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "095-737-3044",
                    Orders = new List<Order> { GetTestOrdersEntitiesWithOrderDetails[2] }
                    },
                new User {
                    Id = 4,
                    Name = "Andriy",
                    Surname = "Orenko",
                    Location = "Ukraine,Kharkiv",
                    Email = "vaN@gmail.com",
                    Password = "1234104445",
                    AccessLevel = Role.RegUser,
                    PhoneNumber = "095-747-3044",
                    Orders = new List<Order> { GetTestOrdersEntitiesWithOrderDetails[3] }
                    }

            };

        public List<Order> GetTestOrdersEntitiesWithOrderDetails =>
             new List<Order>()
             {
                new Order
                {
                    Id = 1,
                    OperationDate = new DateTime(2022,12,02),
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
                    OperationDate = new DateTime(2022, 8, 10),
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
                    OperationDate = new DateTime(2022, 10, 10),
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
                    OperationDate = new DateTime(2022, 12, 10),
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
                },
             };
    }

}
