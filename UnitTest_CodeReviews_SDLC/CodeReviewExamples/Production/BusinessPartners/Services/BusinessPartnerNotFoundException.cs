namespace CodeReviewExamples.Production.BusinessPartners.Services;

public class BusinessPartnerNotFoundException(int partnerCode)
    : Exception($"Business Partner with Code {partnerCode} was not found.");