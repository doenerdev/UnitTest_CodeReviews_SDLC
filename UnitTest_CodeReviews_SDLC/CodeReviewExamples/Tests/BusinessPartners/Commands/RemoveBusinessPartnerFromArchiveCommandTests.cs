using CodeReviewExamples.Production.BusinessPartners.Commands;
using CodeReviewExamples.Production.BusinessPartners.Services;
using Moq;
using Xunit;

namespace CodeReviewExamples.Tests.BusinessPartners.Commands;

public class RemoveBusinessPartnerFromArchiveCommandTests
{
    [Fact]
    public async Task ExecuteRemoval_PartnerIsRemoved_NoMailIsSentInTestContext()
    {
        var mailServiceMock = new Mock<IMailService>();
        var archiveMock = new Mock<IBusinessPartnerArchive>();
        const int partnerCode = 1234;
        var sut = new RemoveBusinessPartnerFromArchiveCommand(true, mailServiceMock.Object, archiveMock.Object);

        await sut.Execute(partnerCode);
        archiveMock.Verify(x => x.RemoveBusinessPartnerFromArchiveAsync(partnerCode), Times.Once);
        mailServiceMock.VerifyNoOtherCalls();
    }
}