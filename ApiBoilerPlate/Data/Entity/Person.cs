using System;

namespace ApiBoilerPlate.Data.Entity
{
    public class Person : EntityBase
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
