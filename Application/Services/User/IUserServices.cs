

using Application.Interface.Context;
using Common.ResultDto;

namespace Application.Services.User
{
    public interface IUserServices
    {
        ResultDto<GetUserDto> GetUser();
        ResultDto EditUser(string userName, string password);
        ResultDto<GetUserDto> LoginUser(string userName, string password);
    }

    public class UserServices : IUserServices
    {
        private IDataBaseContext _context;
        public UserServices(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetUserDto> GetUser()
        {
            var res = _context.Users.FirstOrDefault(x => x.Id == 1);

            return new ResultDto<GetUserDto>()
            {
                Data = new GetUserDto()
                {
                    UserName = res.Username,
                    Password = res.Password,
                    Id = res.Id
                },
                Message = "",
                IsSuccess = true
            };
        }

        public ResultDto EditUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(password))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام کاربری یا کلمه عبور را وارد کنید"
                };
            }

            var res = _context.Users.FirstOrDefault(x => x.Id == 1);

            res.Username = userName;
            res.Password = password;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ویرایش شد"
            };

        }

        public ResultDto<GetUserDto> LoginUser(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(userName))
            {
                return new ResultDto<GetUserDto>()
                {
                    IsSuccess = false,
                    Message = "نام کاربری یا رمز عبور را وارد کنید"
                };
            }

            var user = _context.Users.FirstOrDefault(x => x.Password == password && x.Username == userName.Trim().ToLower());

            if (user == null) { return new ResultDto<GetUserDto> { IsSuccess = false, Message = "کاربری با این مشخصات یافت نشد" }; }

            return new ResultDto<GetUserDto>()
            {
                Data = new GetUserDto() { Id = user.Id, Password = user.Password, UserName = user.Username },
                IsSuccess = true,
                Message = "ورود با موفقیت انجام شد"
            };

        }
    }

    public class GetUserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
