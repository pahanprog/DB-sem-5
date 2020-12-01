namespace WpfApp1.Models
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public bool Blocked { get; set; }
        public int StatusId { get; set; }

        public bool Changed { get; set; }
    }

    public enum Status
    {
        Admin,User,
    }

}
