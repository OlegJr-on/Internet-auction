﻿using BLL.Models;
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
