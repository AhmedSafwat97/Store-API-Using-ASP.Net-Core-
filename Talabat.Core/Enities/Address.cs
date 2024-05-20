namespace Talabat.Core.Enities
{
    public class Address : BaseEntity
    {

       public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string AppUserId { get; set; } // Forigen key For User



    }
}