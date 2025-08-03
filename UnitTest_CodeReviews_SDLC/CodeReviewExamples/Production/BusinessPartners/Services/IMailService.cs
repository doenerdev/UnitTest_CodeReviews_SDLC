namespace CodeReviewExamples.Production.BusinessPartners.Services;

public interface IMailService
{
    void SendMail(string subject, string message, string recipient);
}