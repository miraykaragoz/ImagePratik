using Dapper;
using ImagePratik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ImagePratik.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ImageModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MessageCssClass = "alert-danger";
                ViewBag.Message = "Fotoðraf ekleme iþlemi baþarýsýz oldu.";
                return View("Message");
            }

            var ImageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Img.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", ImageName);

            using var stream = new FileStream(path, FileMode.Create);
            model.Img.CopyTo(stream);

            model.ImgPath = ImageName;

            var connectionString = "";

            using var connection = new SqlConnection(connectionString);

            var sql = "INSERT INTO images (ImgName, ImgPath) VALUES (@ImgName, @ImgPath)";

            var data = new
            {
                model.ImgName,
                model.ImgPath,
            };

            var rowsAffected = connection.Execute(sql, data);

            ViewBag.MessageCssClass = "alert-success";
            ViewBag.Message = "Fotoðraf ekleme iþlemi baþarýyla gerçekleþti.";
            return View("Message");
        }
    }
}