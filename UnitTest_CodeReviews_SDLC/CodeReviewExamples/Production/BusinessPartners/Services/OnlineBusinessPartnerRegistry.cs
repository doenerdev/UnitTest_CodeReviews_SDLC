namespace CodeReviewExamples.Production.BusinessPartners.Services;

/// <summary>
///  Online business partner registry connected to the external OBP service.
/// </summary>
public class OnlineBusinessPartnerRegistry : IBusinessPartnerRegistry
{
    public Task<bool> TryGetBusinessPartnerByCodeAsync(int partnerCode, out RegisteredBusinessPartnerDto registeredBusinessPartner)
    {
        //sample implementation...
        registeredBusinessPartner = new RegisteredBusinessPartnerDto();
        return Task.FromResult(true);
    }
}