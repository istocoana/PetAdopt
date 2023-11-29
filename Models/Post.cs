namespace PetAdopt.Models
{
    public class Post
    {
        public int id { get; set; }
        public string title{ get; set; }
        public string description { get; set; }
        public PostType Type { get; set; }
        public int? animalID { get; set; }
        public Animal? Animal { get; set; }
    }

    public enum PostType
    {
        donation,
        lost,
        found
    }
}
