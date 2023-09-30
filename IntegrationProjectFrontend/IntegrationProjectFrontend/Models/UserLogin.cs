namespace IntegrationProjectFrontend.Models
{


    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public T Data { get; set; }
    }
    public class UserLogin
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

    }
}
