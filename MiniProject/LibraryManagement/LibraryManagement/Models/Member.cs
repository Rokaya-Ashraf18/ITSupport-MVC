namespace LibraryBooksSystem.Models
{
    public class Member : Person
    {
        public string Email { get; set; }

        public Member(int id, string name, string email)
            : base(id, name)
        {
            Email = email;
        }
    }
}