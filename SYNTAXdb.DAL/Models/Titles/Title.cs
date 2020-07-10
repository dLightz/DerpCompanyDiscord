namespace SYNTAXdb.DAL.Models.Titles
{
    public class Title : Entity
    {
      
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tier { get; set; }
        public string Redeemed { get; set; }
        public int Price { get; set; }
    }
}
