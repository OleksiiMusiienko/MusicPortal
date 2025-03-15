using Portal.BLL.DTO;

namespace MusicPortal.Models
{
    public class IndexViewModel
    {
        public IEnumerable<SongDTO> Songs { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }

        public IndexViewModel(IEnumerable<SongDTO> songs, PageViewModel pageViewModel, SortViewModel sortViewModel, FilterViewModel filterViewModel)
        {
            Songs = songs;
            PageViewModel = pageViewModel;
            SortViewModel = sortViewModel;
            FilterViewModel = filterViewModel;
        }
    }
}
