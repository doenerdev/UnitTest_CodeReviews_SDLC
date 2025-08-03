namespace CodeReviewExamples.Production.BusinessPartners.Services;

/// <summary>
///  Online business partner registry connected to the external OBP service.
/// </summary>
public class OnlineBusinessPartnerRegistry : IBusinessPartnerRegistry
{
    //sample implementation...
    private readonly Dictionary<int, RegisteredBusinessPartnerDto> _businessPartnerOnlineRegistry =
        new()
        {
            { 1234, new RegisteredBusinessPartnerDto { PartnerCode = 1234, Origin = RegistryOrigin.Online } }
        };
    
    public virtual Task<bool> TryGetBusinessPartnerByCodeAsync(int partnerCode, out RegisteredBusinessPartnerDto registeredBusinessPartner)
    {
        if(!_businessPartnerOnlineRegistry.TryGetValue(partnerCode, out registeredBusinessPartner))
            return Task.FromResult(false);
        
        registeredBusinessPartner = new RegisteredBusinessPartnerDto();
        return Task.FromResult(true);
    }
}