using CodeReviewExamples.Production.BusinessPartners.Services;

namespace CodeReviewExamples.Production.BusinessPartners;

public class RegisteredBusinessPartnerDto
{
    public int PartnerCode { get; set; }
    public RegistryOrigin Origin { get; set; }
}