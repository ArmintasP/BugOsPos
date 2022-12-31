﻿using BugOsPos.Domain.CustomerAggregate;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

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
            Customer.New(CustomerId.New(3), "jane", HashedPassword, Salt, "hastings@gmail.com", "Jane", "Hastings", "35 Hendford Hill, HR84UN Much Marcle", 2)
        };
    }

    public static List<Employee> SampleEmployees()
    {
        return new List<Employee>()
        {
            Employee.New(EmployeeId.New(1), "11", HashedPassword, Salt, FranchiseId.New(1), groupId: null, 0, "aa@gmail.com", "Adam", "Smith", "+1223457855", "6 Nottingham Rd, Selby", "GB24BKEN10000031510604", 1, new List<EmployeeRole>() { EmployeeRole.Manager } , new DateOnly(1999, 10, 10)),
            Employee.New(EmployeeId.New(2), "12", HashedPassword, Salt, FranchiseId.New(1), GroupId.New(1), 0, "bb@gmail.com", "Babam", "Smith", "+555555566", "999 Nottingham Rd, Selby", "GB24BKEN99990031510604", 0.8m, new List<EmployeeRole>() { EmployeeRole.Cashier, EmployeeRole.Specialist } , new DateOnly(2000, 02, 02)),
            Employee.New(EmployeeId.New(3), "23", HashedPassword, Salt, FranchiseId.New(2), groupId: null, 0, "zz@gmail.com", "Zen", "Smith", "+1111111111", "000 Nottingham Rd, Selby", "HK912FFF98890011540321", 1, new List<EmployeeRole>() { EmployeeRole.Cashier } , new DateOnly(2001, 01, 01)),
        };
    }

    public static List<Group> SampleGroups()
    {
        return new List<Group>()
        {
            Group.New(GroupId.New(1), FranchiseId.New(1), "test1group", "group1 fran1"),
            Group.New(GroupId.New(2), FranchiseId.New(1), "test2group", "group2 fran1"),
            Group.New(GroupId.New(3), FranchiseId.New(2), "test3group", "group3 fran2"),
        };
    }

}