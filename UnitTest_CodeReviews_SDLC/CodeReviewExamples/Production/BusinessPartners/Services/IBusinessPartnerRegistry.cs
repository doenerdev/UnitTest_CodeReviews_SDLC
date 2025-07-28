namespace CodeReviewExamples.Production.BusinessPartners.Services;

/// <summary>
///  Interface for different types of business partner registries, where business partner information is stored.
/// </summary>
public interface IBusinessPartnerRegistry
{
    Task<bool> TryGetBusinessPartnerByCodeAsync(int partnerCode, out RegisteredBusinessPartnerDto registeredBusinessPartner);
}