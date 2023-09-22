namespace RealEstate.Models;

public partial class Address
{
    public string FullAddress => City + " " + Street;

   
}