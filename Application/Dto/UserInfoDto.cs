using Innoloft_Application.DBContext;

namespace Innoloft_Application.Dto
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public static UserInfoDto? FromEntity(User entity)
        {

            if (entity is null)
            {
                return null;
            }
            else
            {
                return new UserInfoDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Surname = entity.Surname
                };
            }
        }
    }
}
