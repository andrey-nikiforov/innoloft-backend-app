using Innoloft_Application.DBContext;
using Innoloft_Application.Dto;

namespace Innoloft_Application.Services
{
    public class UserService
    {

        private readonly EventsDbContext _dbContext;

        public UserService(EventsDbContext dbContext) {  _dbContext = dbContext; }

        public async Task<UserInfoDto> GetUserInfo()
        {
            // usage of hardcoded user in db with id = 1
            User? user =  _dbContext.Users.Find(1);

            return UserInfoDto.FromEntity(user);         
        }
    }
}
