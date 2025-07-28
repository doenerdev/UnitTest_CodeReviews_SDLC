namespace CodeReviewExamples.Production.BusinessPartners.Services;

public class DbBusinessPartnerArchive : IBusinessPartnerArchive
{
    public Task<bool> TryGetBusinessPartnerByCodeAsync(int partnerCode, out RegisteredBusinessPartnerDto businessPartnerDto)
    {
        //sample implementation
        businessPartnerDto = null;
        return Task.FromResult(false);
    }

    public Task SaveBusinessPartnerToArchiveAsync(RegisteredBusinessPartnerDto registeredBusinessPartner)
    {
        //sample implementation
        return Task.CompletedTask;
    }

    public Task Clear()
    {
        //sample implementation
        return Task.CompletedTask;
    }
}