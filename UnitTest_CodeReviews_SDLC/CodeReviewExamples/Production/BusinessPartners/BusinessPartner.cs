namespace CodeReviewExamples.Production.BusinessPartners;

public class BusinessPartner(int partnerCode)
{
    private VerificationStatus _status = VerificationStatus.Unverified;
    public int PartnerCode { get; private set; } = partnerCode;

    public bool IsEligibleForLeaseContracts() => _status switch
    {
        VerificationStatus.Verified => true,
        _ => false
    };

    public void Verify()
    {
        _status = VerificationStatus.Verified;
    }
    
    public void Reject()
    {
        _status = VerificationStatus.Rejected;
    }
}