namespace CodeReviewExamples.Production.BusinessPartners.Services;

public class BusinessPartnerRepository(IBusinessPartnerRegistry businessPartnerRegistry, IBusinessPartnerArchive businessPartnerArchive)
{
    public async Task<RegisteredBusinessPartnerDto> GetBusinessPartnerByCodeAsync(int partnerCode)
    {
        if (await businessPartnerArchive.TryGetBusinessPartnerByCodeAsync(partnerCode, out var registeredBusinessPartner))
            return registeredBusinessPartner;

        if (!await businessPartnerRegistry.TryGetBusinessPartnerByCodeAsync(partnerCode, out registeredBusinessPartner))
            throw new BusinessPartnerNotFoundException(partnerCode);
        
        await businessPartnerArchive.SaveBusinessPartnerToArchiveAsync(registeredBusinessPartner);
        return registeredBusinessPartner;
    }
}