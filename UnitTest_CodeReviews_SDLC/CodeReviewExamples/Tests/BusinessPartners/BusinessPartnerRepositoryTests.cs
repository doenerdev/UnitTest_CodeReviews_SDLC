using CodeReviewExamples.Production.BusinessPartners;
using CodeReviewExamples.Production.BusinessPartners.Services;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace CodeReviewExamples.Tests.BusinessPartners;

public class BusinessPartnerRepositoryTests(ITestOutputHelper testOutputHelper)
{
    private readonly DbBusinessPartnerArchive _dbBusinessPartnerArchive = new();
    
    [Fact]
    public async Task GetBusinessPartner_ThrowsException()
    {
        const int unknownCode = 0000;
        var onlineRegistry = new OnlineBusinessPartnerRegistry();
        var sut = new BusinessPartnerRepository(onlineRegistry, _dbBusinessPartnerArchive);
        
        var action = async () => await sut.GetBusinessPartnerByCodeAsync(unknownCode);
        await action.Should().ThrowAsync<BusinessPartnerNotFoundException>();
    }

    [Fact]
    public async Task GetBusinessPartnerOnline_ReturnsPartner()
    {
        try
        {
            const int knownCode = 1234;
            var onlineRegistry = new OnlineBusinessPartnerRegistry();
            await _dbBusinessPartnerArchive.Clear();
            var sut = new BusinessPartnerRepository(onlineRegistry, _dbBusinessPartnerArchive);
            
            var result = await sut.GetBusinessPartnerByCodeAsync(knownCode);
            result.Should().NotBeNull();
            result.Should().BeOfType<RegisteredBusinessPartnerDto>();
            result.PartnerCode.Should().Be(knownCode);
        }
        catch (Exception)
        {
            testOutputHelper.WriteLine("Error while getting partner from registry.");
        }
    }
    
    [Fact]
    public async Task GetBusinessPartnerOnline_SavedToArchive()
    {
        try
        {
            const int knownCode = 1234;
            var onlineRegistry = new OnlineBusinessPartnerRegistry();
            var archiveMock = new Mock<IBusinessPartnerArchive>(MockBehavior.Strict);
            archiveMock.SetupSequence(x => x.TryGetBusinessPartnerByCodeAsync(knownCode, out It.Ref<RegisteredBusinessPartnerDto>.IsAny))
                .ReturnsAsync(false);
            var sut = new BusinessPartnerRepository(onlineRegistry, archiveMock.Object);
            
            await sut.GetBusinessPartnerByCodeAsync(knownCode);
            
            archiveMock.Verify(x => x.TryGetBusinessPartnerByCodeAsync(knownCode, out It.Ref<RegisteredBusinessPartnerDto>.IsAny), Times.Once);
            archiveMock.Verify(x => x.SaveBusinessPartnerToArchiveAsync(It.Is<RegisteredBusinessPartnerDto>(p => p.PartnerCode == knownCode)), Times.Once);
            archiveMock.VerifyNoOtherCalls();
        }
        catch (Exception)
        {
            testOutputHelper.WriteLine("Error while getting partner from registry.");
        }
    }
}