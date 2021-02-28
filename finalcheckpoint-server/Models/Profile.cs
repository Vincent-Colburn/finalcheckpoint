namespace finalcheckpoint_server.Models
{
    public class Profile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
    }

    public class ProfileKeepViewModel : Profile
    {
        public int ProfileKeepId { get; set; }
    }
}