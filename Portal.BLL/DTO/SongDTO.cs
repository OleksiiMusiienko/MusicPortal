using Portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Path { get; set; }
        public int? GenreId { get; set; }
        public string? Genre { get; set; }
        public bool moderation { get; set; }
    }
}
