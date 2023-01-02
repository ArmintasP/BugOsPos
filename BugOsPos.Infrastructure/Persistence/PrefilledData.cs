using BugOsPos.Domain.CategoryAggregate;
using BugOsPos.Domain.CategoryAggregate.ValueObjects;
using BugOsPos.Domain.CustomerAggregate;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.DiscountAggregate;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;
using BugOsPos.Domain.LocationAggregate;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyCardAggregate;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyDiscountAggregate;
using BugOsPos.Domain.LoyaltyDiscountAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate;
using BugOsPos.Domain.ProductAggregate.ValueObjects;
using BugOsPos.Domain.ShiftAggregate;
using BugOsPos.Domain.ShiftAggregate.ValueObjects;
using System;
using System.Collections.Generic;

namespace BugOsPos.Infrastructure.Persistence;

public static class PrefilledData
{
    private const string Password = "Test12345";
    private const string HashedPassword = "7A6F9E8C215A9701075C0E1FE2652F5BDA8AF235FC18C2ED96F7C7FA120509C1C5CA34721A102A35E197F8B8B637273A23BBF879AB4B541C61C56C9FF36CD430";
    private static byte[] Salt = Enumerable.Repeat((byte)0, 64).ToArray();

    public static List<Customer> SampleCustomers()
    {
        return new List<Customer>()
        {
            Customer.New(CustomerId.New(1), "sarah", HashedPassword, Salt, "sarah@gmail.com", "Sarah", "Evans", null, 1),
            Customer.New(CustomerId.New(2), "john", HashedPassword, Salt, "john@gmail.com", "John", "Smith", "Evergreen 11, 12345 New York", 1),
            Customer.New(CustomerId.New(3), "jane", HashedPassword, Salt, "hastings@gmail.com", "Jane", "Hastings", "35 Hendford Hill, HR84UN Much Marcle", 1)
        };
    }

    public static List<Employee> SampleEmployees()
    {
        return new List<Employee>()
        {
            Employee.New(EmployeeId.New(1), "11", HashedPassword, Salt, FranchiseId.New(1), GroupId.New(1), 0, "aa@gmail.com", "Adam", "Smith", "+1223457855", "6 Nottingham Rd, Selby", "GB24BKEN10000031510604", 1, new List<EmployeeRole>() { EmployeeRole.Manager } , new DateOnly(1999, 10, 10)),
            Employee.New(EmployeeId.New(2), "12", HashedPassword, Salt, FranchiseId.New(1), GroupId.New(1), 0, "bb@gmail.com", "Babam", "Smith", "+555555566", "999 Nottingham Rd, Selby", "GB24BKEN99990031510604", 0.8m, new List<EmployeeRole>() { EmployeeRole.Cashier, EmployeeRole.Specialist } , new DateOnly(2000, 02, 02)),
            Employee.New(EmployeeId.New(3), "13", HashedPassword, Salt, FranchiseId.New(1), groupId: null, 0, "zz@gmail.com", "Zen", "Smith", "+1111111111", "000 Nottingham Rd, Selby", "HK912FFF98890011540321", 1, new List<EmployeeRole>() { EmployeeRole.Cashier } , new DateOnly(2001, 01, 01)),
            Employee.New(EmployeeId.New(4), "14", HashedPassword, Salt, FranchiseId.New(1), groupId: null, 0, "zz@gmail.com", "Zen", "Smith", "+1111111111", "000 Nottingham Rd, Selby", "HK912FFF98890011540321", 1, new List<EmployeeRole>() { EmployeeRole.Courier } , new DateOnly(2001, 01, 01)),
        };
    }

    public static List<Group> SampleGroups()
    {
        return new List<Group>()
        {
            Group.New(GroupId.New(1), FranchiseId.New(1), "test1group", "group1 fran1"),
            Group.New(GroupId.New(2), FranchiseId.New(1), "test2group", "group2 fran1"),
            Group.New(GroupId.New(3), FranchiseId.New(1), "test3group", "group3 fran2"),
        };
    }

    public static List<Location> SampleLocations()
    {
        return new List<Location>()
        {
            Location.New(LocationId.New(1), "Shop96", "1 Evergreen St, New York"),
            Location.New(LocationId.New(2), "MakuDonardo", "2 Evergreen St, New York"),
            Location.New(LocationId.New(3), "CFK", "3 Evergreen St, New York"),
        };
    }

    public static List<Shift> SampleShifts()
    {
        return new List<Shift>()
        {
            Shift.New(ShiftId.New(1), EmployeeId.New(1), new DateTime(2020, 10, 10, 10, 10, 10), new DateTime(2020, 11, 11, 10, 10, 10), LocationId.New(1), GroupId.New(1)),
            Shift.New(ShiftId.New(2), EmployeeId.New(2), new DateTime(2021, 10, 10, 10, 10, 10), new DateTime(2021, 11, 11, 10, 10, 10), LocationId.New(2), GroupId.New(2)),
            Shift.New(ShiftId.New(3), EmployeeId.New(3), new DateTime(2022, 10, 10, 10, 10, 10), new DateTime(2022, 11, 11, 10, 10, 10), LocationId.New(3), GroupId.New(3)),
        };
    }

    internal static List<Category> SampleCategories()
    {
        return new List<Category>()
        {
           Category.New(CategoryId.New(1),"Sports","Extreme sports",FranchiseId.New(1)),
           Category.New(CategoryId.New(2),"Hair","Extreme hairs",FranchiseId.New(2)),
           Category.New(CategoryId.New(3),"Food","Extreme food",FranchiseId.New(3)),
        };
    }

    internal static List<Discount> SampleDiscounts()
    {
        return new List<Discount>()
        {
            Discount.New(DiscountId.New(1), 0.05m, DiscountType.Percentage, new DateTime(2021, 10, 10), new DateTime(2021, 10, 10)),
            Discount.New(DiscountId.New(2), 1m, DiscountType.Percentage, new DateTime(2022, 9, 20), new DateTime(2050, 9, 20)),
            Discount.New(DiscountId.New(3), 100, DiscountType.Amount, new DateTime(2021, 10, 10), new DateTime(2100, 10, 10)),
            Discount.New(DiscountId.New(4),10,DiscountType.Percentage, new DateTime(2021, 10, 10, 10, 10, 10), new DateTime(2021, 11, 10, 10, 10, 10)),
            Discount.New(DiscountId.New(5),10,DiscountType.Percentage, new DateTime(2021, 10, 10, 10, 10, 10), new DateTime(2021, 11, 10, 10, 10, 10)),
            Discount.New(DiscountId.New(6),10,DiscountType.Percentage, new DateTime(2021, 10, 10, 10, 10, 10), new DateTime(2021, 11, 10, 10, 10, 10)),
        };
    }

    internal static List<Franchise> SampleFranchises()
    {
        return new List<Franchise>()
        {
            Franchise.New(FranchiseId.New(1),"tom.tomson@gmail.com","Maxima","+370000000"),
            Franchise.New(FranchiseId.New(1),"tom.tomson@gmail.com","IKI","+370000000"),
            Franchise.New(FranchiseId.New(1),"tom.tomson@gmail.com","Lidl","+370000000"),
        };
    }

    internal static List<LoyaltyCard> SampleLoyaltyCards()
    {
        return new List<LoyaltyCard>()
        {
            LoyaltyCard.New(LoyaltyCardId.New(1), CustomerId.New(1), "TESTCARD1"),
            LoyaltyCard.New(LoyaltyCardId.New(2), CustomerId.New(2), "13CC46E66AA8FF900A1"),
            LoyaltyCard.New(LoyaltyCardId.New(3), CustomerId.New(3), "13CC46E66AA8FF900A2")
            LoyaltyCard.New(LoyaltyCardId.New(1),CustomerId.New(4),"discount1"),
            LoyaltyCard.New(LoyaltyCardId.New(2),CustomerId.New(5),"discount2"),
            LoyaltyCard.New(LoyaltyCardId.New(3),CustomerId.New(6),"discount3"),
        };
    }

    internal static List<LoyaltyDiscount> SampleLoyaltyDiscounts()
    {
        return new List<LoyaltyDiscount>()
        {
            LoyaltyDiscount.New(LoyaltyDiscountId.New(1), LoyaltyCardId.New(1), ProductId.New(1), DiscountId.New(1)),
            LoyaltyDiscount.New(LoyaltyDiscountId.New(2), LoyaltyCardId.New(1), ProductId.New(1), DiscountId.New(2)),
            LoyaltyDiscount.New(LoyaltyDiscountId.New(3), LoyaltyCardId.New(1), ProductId.New(1), DiscountId.New(3)),
            LoyaltyDiscount.New(LoyaltyDiscountId.New(4), LoyaltyCardId.New(2), ProductId.New(2), DiscountId.New(2))
            LoyaltyDiscount.New(LoyaltyDiscountId.New(5),LoyaltyCardId.New(1),ProductId.New(1),DiscountId.New(1)),
            LoyaltyDiscount.New(LoyaltyDiscountId.New(6),LoyaltyCardId.New(2),ProductId.New(2),DiscountId.New(2)),
            LoyaltyDiscount.New(LoyaltyDiscountId.New(7),LoyaltyCardId.New(3),ProductId.New(3),DiscountId.New(3)),
        };
    }

    internal static List<Order> SampleOrders()
    {
        return new List<Order>()
        {
            Order.New(OrderId.New(1),CustomerId.New(1),EmployeeId.New(1),EmployeeId.New(1),LocationId.New(1),true),
            Order.New(OrderId.New(2),CustomerId.New(2),EmployeeId.New(2),EmployeeId.New(2),LocationId.New(2),true),
            Order.New(OrderId.New(3),CustomerId.New(3),EmployeeId.New(3),EmployeeId.New(3),LocationId.New(3),false),
            Order.New(OrderId.New(4),CustomerId.New(3),EmployeeId.New(3),EmployeeId.New(4),LocationId.New(3), true),
        };
    }

    internal static List<Product> SampleProducts()
    {
        return new List<Product>()
        {
            Product.NewProduct(ProductId.New(1), FranchiseId.New(1),EmployeeId.New(1),DiscountId.New(1),CategoryId.New(1),"Banana",12,1,10),
            Product.NewProduct(ProductId.New(2), FranchiseId.New(1),EmployeeId.New(2),DiscountId.New(2),CategoryId.New(2),"Apples",5,1,10),
            Product.NewProduct(ProductId.New(3), FranchiseId.New(1),EmployeeId.New(3),DiscountId.New(3),CategoryId.New(3),"Pineapple",6,1,10),
        };
    }
}