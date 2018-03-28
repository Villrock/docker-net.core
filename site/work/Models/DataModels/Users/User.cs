namespace QFlow.Models.DataModels.Users
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Return Manager Full name
        /// </summary>
        /// <returns>Full Name</returns>
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
