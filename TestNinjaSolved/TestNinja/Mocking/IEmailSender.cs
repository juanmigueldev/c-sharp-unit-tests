namespace TestNinja.Mocking
{
    public interface IEmailSender
    {
        void SendEmailFile(string emailAddress, string emailBody, string filename, string subject);
    }
}