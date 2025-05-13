namespace Challenge2.DTOs.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
    public class UserWithAddressResponse : UserResponse
    {
        public List<AddressResponse> Addresses { get; set; }
    }
}
