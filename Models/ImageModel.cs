namespace ImagePratik.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string ImgName { get; set; }
        public IFormFile? Img { get; set; }
        public string? ImgPath { get; set; }
    }
}
