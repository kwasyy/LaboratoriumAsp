using Microsoft.AspNetCore.Mvc;

namespace RazorWeb.Controllers
{
    public class BookController : Controller
    {
        public string Hello() => "Hello!";
        
        public string HelloUser(string name = "Jan") => $"Hello {name}";
        
        public IActionResult HelloV(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        
        public string Sum(int x = 0, int y = 0)
        {
            return $"{x} + {y} = {x + y}";
        }
        public string Echo(string name, int count = 1)
        {
            var result = new System.Text.StringBuilder();
            for (int i = 0; i < count; i++)
            {
                result.AppendLine(name);
            }
            return result.ToString();
        }
        
        public string Square(int id) => $"{id}^2 = {id * id}";
        
        [HttpPost]
        public string Report()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Method: {Request.Method}");
            sb.AppendLine($"Path: {Request.Path}{Request.QueryString}");
            foreach (var h in Request.Headers)
            {
                sb.AppendLine($"{h.Key}: {h.Value}");
            }
            return sb.ToString();
        }
        
        public string Select(int x, int y)
        {
            int max = Math.Max(x, y);
            return $"Większa liczba to: {max}";
        }
        
        public string UserAgent()
        {
            string ua = Request.Headers["User-Agent"].ToString();
            return $"Twój User-Agent: {ua}";
        }
        
        public IActionResult ResultJSON()
        {
            var data = new { Status = "OK", Name = "RefWiz" };
            return Json(data);
        }
        
    }
}