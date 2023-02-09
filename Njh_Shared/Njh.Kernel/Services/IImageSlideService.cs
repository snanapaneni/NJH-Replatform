using Njh.Kernel.Models.Dto;

namespace Njh.Kernel.Services
{
    public interface IImageSlideService
    {
        IEnumerable<ImageSlide> GetSlides(string path);
    }
}
