using Business.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APItest.Controllers
{



    [ApiController]
    [Route("[controller]")]
    public class HabitacionController : ControllerBase
    {
        [HttpGet]
        [Route("obtenerHabitacionesTemporada")]
        public string obtenerHabitacionesTemporada()

        {
            ;
            return new HabitacionBusiness().obtenerHabitacionesTemporada();
        }// fin m



    }// fin clase

}