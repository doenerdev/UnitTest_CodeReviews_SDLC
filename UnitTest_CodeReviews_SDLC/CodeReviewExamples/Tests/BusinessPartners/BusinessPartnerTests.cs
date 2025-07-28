using System.Reflection;
using Xunit;
using CodeReviewExamples.Production.BusinessPartners;
using FluentAssertions;

namespace CodeReviewExamples.Tests.BusinessPartners;

public class BusinessPartnerTests
{
    private const int DefaultPartnerCode = 1234;
    private static BusinessPartner _sut = new(DefaultPartnerCode);
    
    [Fact]
    public void NotYetVerified_AsExpected()
    {
        _sut = new BusinessPartner(DefaultPartnerCode);
        var status = typeof(BusinessPartner).GetField("_status", BindingFlags.NonPublic | BindingFlags.Instance);
        status!.GetValue(_sut).Should().Be(VerificationStatus.Unverified);
    }
    
    [Theory]
    [InlineData(false, VerificationStatus.Rejected)]
    [InlineData(true, VerificationStatus.Verified)]
    public void Verify_AsExpected(bool verify, VerificationStatus expected)
    {
        if(verify)
            _sut.Verify();
        else
            _sut.Reject();
        
        var status = typeof(BusinessPartner).GetField("_status", BindingFlags.NonPublic | BindingFlags.Instance);
        status!.GetValue(_sut).Should().Be(expected);
    }

    [Fact]
    public void IsEligibleForLeaseContracts_ReturnsTrue()
    {
        var status = typeof(BusinessPartner).GetField("_status", BindingFlags.NonPublic | BindingFlags.Instance);
        status!.SetValue(_sut, VerificationStatus.Verified);
        
        _sut.IsEligibleForLeaseContracts().Should().BeFalse();
    }
    
    [Fact]
    public void IsEligibleForLeaseContracts_ReturnsFalse()
    {
        var random = new Random();
        var notEligibleStatuses = Enum.GetValues<VerificationStatus>().Where(x => x != VerificationStatus.Verified).ToList();
        var notEligibleStatus = (VerificationStatus)notEligibleStatuses[random.Next(notEligibleStatuses.Count)];
        
        var status = typeof(BusinessPartner).GetField("_status", BindingFlags.NonPublic | BindingFlags.Instance);
        status!.SetValue(_sut, notEligibleStatus);
        
        _sut.IsEligibleForLeaseContracts().Should().BeFalse();
    }
    
    
    
    // [Fact]
    // public void IsEligibleForLeaseContracts_NewBusinessPartner_ReturnsFalse()
    // {
    //     //Arrange
    //     var partner = new BusinessPartner();
    //
    //     //Act
    //     var actual = partner.IsEligibleForLeaseContracts();
    //
    //     //Assert
    //     Assert.False(actual);
    // }
    //
    // [Fact]
    // public void IsEligibleForLeaseContracts_VerifiedBusinessPartner_ReturnsFalse()
    // {
    //     //Arrange
    //     var partner = new BusinessPartner();
    //     partner.Verify();
    //
    //     //Act
    //     var actual = partner.IsEligibleForLeaseContracts();
    //
    //     //Assert
    //     Assert.True(actual);
    // }
}