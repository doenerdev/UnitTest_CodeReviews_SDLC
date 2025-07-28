namespace CodeReviewExamples.Production.BusinessPartners.Services;

public interface IBusinessPartnerArchive
{
    Task<bool> TryGetBusinessPartnerByCodeAsync(int partnerCode, out RegisteredBusinessPartnerDto businessPartnerDto);
    Task SaveBusinessPartnerToArchiveAsync(RegisteredBusinessPartnerDto registeredBusinessPartner);
    Task Clear();
}