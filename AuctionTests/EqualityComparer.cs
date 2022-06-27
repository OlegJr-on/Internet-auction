using DAL.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BLL.Models;

namespace AuctionTests
{
    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Password == y.Password;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class OrderEqualityComparer : IEqualityComparer<Order>
    {
        public bool Equals([AllowNull] Order x, [AllowNull] Order y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.UserId == y.UserId
                && x.OperationDate == y.OperationDate;
        }

        public int GetHashCode([DisallowNull] Order obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class LotEqualityComparer : IEqualityComparer<Lot>
    {
        public bool Equals([AllowNull] Lot x, [AllowNull] Lot y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.PhotoId == y.PhotoId
                && x.Title == y.Title
                && x.StartPrice == y.StartPrice
                && x.StartDate == y.StartDate
                && x.EndDate == y.EndDate;
        }

        public int GetHashCode([DisallowNull] Lot obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class OrderDetailEqualityComparer : IEqualityComparer<OrderDetail>
    {
        public bool Equals([AllowNull] OrderDetail x, [AllowNull] OrderDetail y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.OrderId == y.OrderId
                && x.LotId == y.LotId
                && x.Status == y.Status;
        }

        public int GetHashCode([DisallowNull] OrderDetail obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class CustomerModelEqualityComparer : IEqualityComparer<UserModel>
    {
        public bool Equals([AllowNull] UserModel x, [AllowNull] UserModel y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.Name == y.Name &&
                x.Surname == y.Surname &&
                x.PhoneNumber == y.PhoneNumber &&
                x.Password == y.Password &&
                x.Email == y.Email &&
                x.Location == y.Location;
        }

        public int GetHashCode([DisallowNull] UserModel obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class LotModelEqualityComparer : IEqualityComparer<LotModel>
    {
        public bool Equals([AllowNull] LotModel x, [AllowNull] LotModel y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.PhotoId == y.PhotoId &&
                string.Equals(x.Title, y.Title) &&
                string.Equals(x.StartPrice, y.StartPrice) &&
                x.CurrentPrice == y.CurrentPrice &&
                x.MinRate == y.MinRate &&
                x.StartDate == y.StartDate &&
                x.EndDate == y.EndDate;
        }

        public int GetHashCode([DisallowNull] LotModel obj)
        {
            return obj.GetHashCode();
        }
    }
}
