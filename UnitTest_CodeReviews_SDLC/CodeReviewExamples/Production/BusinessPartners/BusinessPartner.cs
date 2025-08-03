namespace CodeReviewExamples.Production.BusinessPartners;

public class BusinessPartner(int partnerCode)
{
    private VerificationStatus _status = VerificationStatus.Unverified;
    public int PartnerCode { get; private set; } = partnerCode;
}