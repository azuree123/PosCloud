namespace POSApp.Services
{
    public interface IUserInfoService
    {
        string UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }

    }
}