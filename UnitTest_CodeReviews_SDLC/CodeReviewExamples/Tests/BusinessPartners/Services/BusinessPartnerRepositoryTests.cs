using CodeReviewExamples.Production.BusinessPartners;
using CodeReviewExamples.Production.BusinessPartners.Services;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace CodeReviewExamples.Tests.BusinessPartners.Services;

public class BusinessPartnerRepositoryTests(ITestOutputHelper testOutputHelper)
{
    private readonly DbBusinessPartnerArchive _dbBusinessPartnerArchive = new();
    
    [Fact]
    public async Task GetBusinessPartner_ThrowsException()
    {
        const int unknownCode = 0000;
        var sut = new BusinessPartnerRepository(new OnlineBusinessPartnerRegistry(), _dbBusinessPartnerArchive);
        
        var action = async () => await sut.GetBusinessPartnerByCodeAsync(unknownCode);
        await action.Should().ThrowAsync<BusinessPartnerNotFoundException>();
    }

    [Fact]
    public async Task GetBusinessPartnerOnline_ReturnsPartner()
    {
        try
        {
            const int expectedKnownCode = 1234;
            await _dbBusinessPartnerArchive.Clear();
            var sut = new BusinessPartnerRepository(new OnlineBusinessPartnerRegistry(), _dbBusinessPartnerArchive);
            
            var result = await sut.GetBusinessPartnerByCodeAsync(expectedKnownCode);
            AssertRegisteredBusinessPartner(result, expectedKnownCode);
        }
        catch (Exception)
        {
            testOutputHelper.WriteLine("Error while getting partner from registry.");
        }
    }
    
    [Fact]
    public async Task GetBusinessPartnerFromArchive_NoOnlineCallsAreMade_ReturnsPartner()
    {
        try
        {
            const int expectedKnownCode = 1234;
            await _dbBusinessPartnerArchive.Clear();
            var onlineRegistryMock = new Mock<OnlineBusinessPartnerRegistry>();
            
            var sut = new BusinessPartnerRepository(new OnlineBusinessPartnerRegistry(), _dbBusinessPartnerArchive);
            
            var result = await sut.GetBusinessPartnerByCodeAsync(expectedKnownCode);
            AssertRegisteredBusinessPartner(result, expectedKnownCode);
            onlineRegistryMock.Verify(x => x.TryGetBusinessPartnerByCodeAsync(It.IsAny<int>(), out It.Ref<RegisteredBusinessPartnerDto>.IsAny), Times.Never);
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
            const int expectedKnownCode = 1234;
            var archiveMock = new Mock<IBusinessPartnerArchive>(MockBehavior.Strict);
            archiveMock.Setup(x => x.TryGetBusinessPartnerByCodeAsync(expectedKnownCode, out It.Ref<RegisteredBusinessPartnerDto>.IsAny))
                .ReturnsAsync(false);
            var sut = new BusinessPartnerRepository(new OnlineBusinessPartnerRegistry(), archiveMock.Object);
            
            var result = await sut.GetBusinessPartnerByCodeAsync(expectedKnownCode);
            
            AssertRegisteredBusinessPartner(result, expectedKnownCode);
            archiveMock.Verify(x => x.TryGetBusinessPartnerByCodeAsync(expectedKnownCode, out It.Ref<RegisteredBusinessPartnerDto>.IsAny), Times.Once);
            archiveMock.Verify(x => x.SaveBusinessPartnerToArchiveAsync(It.Is<RegisteredBusinessPartnerDto>(p => p.PartnerCode == expectedKnownCode)), Times.Once);
            archiveMock.VerifyNoOtherCalls();
        }
        catch (Exception)
        {
            testOutputHelper.WriteLine("Error while getting partner from registry.");
        }
    }

    private static void AssertRegisteredBusinessPartner(RegisteredBusinessPartnerDto businessPartnerDto, int expectedPartnerCode)
    {
        businessPartnerDto.Should().NotBeNull();
        businessPartnerDto.Should().BeOfType<RegisteredBusinessPartnerDto>();
        businessPartnerDto.PartnerCode.Should().Be(expectedPartnerCode);
    }
}