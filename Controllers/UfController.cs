using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UfController : Controller
    {

        // GET api/uf/dd-MM-yyyy
        [HttpGet("{fecha}")]
        public JsonResult Get(string fecha)
        {

            DateTime date;
            if (DateTime.TryParseExact(fecha, "dd-MM-yyyy",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out date))
            {
                HttpClient http = new HttpClient();
                var data = http.GetAsync("https://www.mindicador.cl/api/uf/" + fecha).Result.Content.ReadAsStringAsync().Result;
                var data_json = JsonConvert.DeserializeObject(data);
                return Json(data_json);
            }
            else
            {
                var data_json = JsonConvert.DeserializeObject("{ 'error': 'fecha invalida' }");
                return Json(data_json);
            }
        }
    }
}
