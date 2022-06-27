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
    public class LotServiceTests
    {
        [Test]
        public async Task LotService_GetAll_ReturnsAllLots()
        {
            //arrange
            var expected = LotModels.ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.LotRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(LotEntities.AsEnumerable());

            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await lotService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(x => x.OrderDetailsIds));
        }

        [Test]
        public async Task LotService_GetAllPhotosAsync_ReturnsAllPhotos()
        {
            //arrange
            var expected = PhotoModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.PhotoRepository.GetAllAsync())
                .ReturnsAsync(PhotoEntities.AsEnumerable());

            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await lotService.GetAllPhotosAsync();

            //assert
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(x => x.Id));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task LotService_GetById_ReturnsLotModel(int id)
        {
            //arrange
            var expected = LotModels.FirstOrDefault(x => x.Id == id);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(LotEntities.FirstOrDefault(x => x.Id == id));

            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await lotService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected, options =>
              options.Excluding(x => x.OrderDetailsIds));
        }

        [Test]
        public async Task LotService_AddAsync_AddsLot()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.AddAsync(It.IsAny<Lot>()));

            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var product = new LotModel
            {
                Id = 1,
                Title = "BMW M5 F90",
                PhotoId = 1,
                CurrentPrice = 63000.99m,
                StartDate = new DateTime(2022, 11, 11),
                EndDate = new DateTime(2022, 11, 16),
            };

            //act
            await lotService.AddAsync(product);

            //assert
            mockUnitOfWork.Verify(x => x.LotRepository.AddAsync(It.Is<Lot>(c => c.Id == product.Id &&
                        c.PhotoId == product.PhotoId && c.CurrentPrice == product.CurrentPrice
                        && c.Title == product.Title)), Times.Once);
        }




        private static IEnumerable<LotModel> LotModels =>
           new List<LotModel>
           {
                new LotModel {
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
                new LotModel {
                    Id=2,
                    Title = "2007 Volkswagen Golf V Plus Tour",
                    Status = LotStatus.Created,
                    MinRate = 110,
                    StartDate = new DateTime(2022, 6, 25, 20, 0, 0),
                    EndDate = new DateTime(2022, 6, 28, 20, 0, 0),
                    StartPrice = 3400,
                    PhotoId = 1,
                    CurrentPrice = 8600.33M
                },
                new LotModel {
                    Id= 3,
                    Title = "2020 Audi RS7 Advanture",
                    Status = LotStatus.Created,
                    MinRate = 500,
                    StartDate = new DateTime(2022, 7, 25, 20, 0, 0),
                    EndDate = new DateTime(2022, 7, 28, 12, 0, 0),
                    StartPrice = 17500,
                    PhotoId = 2,
                    CurrentPrice = 26666.33M
                },
                new LotModel {
                    Id= 4,
                    Title = "2002 BMW M3 SportM",
                    Status = LotStatus.Created,
                    MinRate = 50,
                    StartDate = new DateTime(2022, 7, 12, 20, 0, 0),
                    EndDate = new DateTime(2022, 7, 18, 12, 0, 0),
                    StartPrice = 3200,
                    PhotoId = 2,
                    CurrentPrice = 3500
                },
                new LotModel {
                    Id= 5,
                    Title = "2010 Ford Focus",
                    Status = LotStatus.Created,
                    MinRate = 50,
                    StartDate = new DateTime(2022, 9, 15, 20, 0, 0),
                    EndDate = new DateTime(2022, 9, 22, 12, 0, 0),
                    StartPrice = 1000,
                    PhotoId = 3,
                    CurrentPrice = 1000
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

        private static IEnumerable<Photo> PhotoEntities =>
            new List<Photo>
            {
                new Photo
                {
                    Id = 1, PhotoSrc = "Photo1",GroupOfPhoto = 1
                },
                new Photo
                {
                    Id = 2, PhotoSrc = "Photo2",GroupOfPhoto = 1
                },
                new Photo
                {
                    Id = 3, PhotoSrc = "Photo3",GroupOfPhoto = 2
                },
                new Photo
                {
                    Id = 4, PhotoSrc = "Photo4",GroupOfPhoto = 2
                },
            };

        private static IEnumerable<PhotoModel> PhotoModels =>
            new List<PhotoModel>
            {
                new PhotoModel { Id = 1, PhotoSrc = "Photo1",GroupOfPhoto = 1 },
                new PhotoModel { Id = 2, PhotoSrc = "Photo2",  GroupOfPhoto = 1},
                new PhotoModel { Id = 3, PhotoSrc = "Photo3",GroupOfPhoto = 2},
                new PhotoModel { Id = 4, PhotoSrc = "Photo4", GroupOfPhoto = 2}
            };
    }
}
