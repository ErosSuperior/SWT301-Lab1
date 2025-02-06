namespace ecomerce.IService
{
    public interface IMailKitService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
