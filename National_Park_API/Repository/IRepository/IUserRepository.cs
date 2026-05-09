using National_Park_API.Models;

namespace National_Park_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser (string username);
        User Authenticate (string username, string password );
        User Register (string username, string password );
    }
    
}
