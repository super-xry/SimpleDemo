namespace SimpleDemo.Security.Model
{
    public class TokenValidateResult
    {
        public string? UserName { get; set; }

        public string Role { get; set; }

        public List<string> Permissions { get; set; }
    }
}