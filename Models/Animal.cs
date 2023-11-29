namespace PetAdopt.Models
{
    public class Animal
    {
        public int id { get; set; }
        public AnimalSpecies animal_speacies { get; set; }
        public string animal_breed { get; set; }
        public int animal_age { get; set; }
        public string animal_name { get; set; }
    }

    public enum AnimalSpecies
    {
        Dog,
        Cat
    }
}
