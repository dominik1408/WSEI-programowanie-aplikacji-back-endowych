using System.ComponentModel.DataAnnotations;

namespace ExerciseLAB3
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}
