using System;
using System.Collections.Generic;

namespace RealEstate.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public Client(string firstName, string lastName, string middleName, string phone, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Phone = phone;
        Email = email;
    }
}
