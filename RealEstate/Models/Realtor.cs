using System;
using System.Collections.Generic;

namespace RealEstate.Models;

public partial class Realtor
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public int Share { get; set; }
    
    public Realtor(string firstName, string lastName, string middleName, int share)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Share = share;
    }
}
