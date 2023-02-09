using Njh.Kernel.Models.Dto;

namespace Njh.Mvc.Models
{
    public class HomePageHeroViewModel
    {
        public int PlaySpeed { get; set; }
        public IEnumerable<ImageSlide> ImageSlides { get; set; }
    }
}
