﻿namespace Persons.API.Dtos.Persons
{
    public class PersonDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Gender { get; set; }
        public string CountryId { get; set; }

        public List<FamilyMemberCreateDto> FamilyGroup { get; set; }
    }
}
