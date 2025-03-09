namespace Portal.DAL.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Path { get; set; }
        public int? GenreId { get; set; }     
        public Genre? Genre { get; set; }
    }
}
