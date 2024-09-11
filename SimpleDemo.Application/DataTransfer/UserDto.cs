namespace SimpleDemo.Application.DataTransfer
{
    public class UserDto
    {
        public string UserName { get; set; } = null!;

        public string? NickName { get; set; }

        public string? Role { get; set; }

        public string? Token { get; set; }
    }
}