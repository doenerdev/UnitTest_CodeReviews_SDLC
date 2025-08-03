using CodeReviewExamples.Production.BusinessPartners.Services;

namespace CodeReviewExamples.Production.BusinessPartners.Commands;

public class RemoveBusinessPartnerFromArchiveCommand(
    bool isTestEnvironment,
    IMailService mailService,
    IBusinessPartnerArchive businessPartnerArchive)
{

    public async Task Execute(int partnerCode)
    {
        await businessPartnerArchive.RemoveBusinessPartnerFromArchiveAsync(partnerCode);

        if (!isTestEnvironment)
        {
            mailService.SendMail("Partner Removed", $"The partner with code {partnerCode} was removed from the archive", "someoneimportant@business.com");
        }
    }
}