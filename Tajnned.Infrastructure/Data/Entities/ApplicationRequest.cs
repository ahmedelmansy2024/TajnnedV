using System;
using System.Collections.Generic;

namespace Tajnned.Infrastructure.Data.Entities;

public partial class ApplicationRequest
{
    public Guid Id { get; set; }

    public string? Idnumber { get; set; }

    public Guid? ApplicationId { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? ThirdName { get; set; }

    public string? FourtName { get; set; }

    public string? FifthName { get; set; }

    public string? LastName { get; set; }

    public string? Familylhigh { get; set; }

    public string? Tribe { get; set; }

    public int? SocialStatus { get; set; }

    public int? Nationality { get; set; }

    public string? Career { get; set; }

    public int? CityToServesIn { get; set; }

    public string? Region { get; set; }

    public string? EmailAddress { get; set; }

    public string? PhoneNumber { get; set; }

    public string? FatherName { get; set; }

    public string? FatherIdnumber { get; set; }

    public string? MotherNationality { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreationTime { get; set; }

    public Guid? CreatorUserId { get; set; }

    public string? ApplicationDate { get; set; }

    public int? Gender { get; set; }

    public int? ApplicationType { get; set; }
}
