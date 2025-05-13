namespace MyAppDemo.Models
{
    public class User : UserMV
    {
        public Guid Id { get; set; }
    }
    public class  UserMV
    {
        public string Username {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        
    }
}
