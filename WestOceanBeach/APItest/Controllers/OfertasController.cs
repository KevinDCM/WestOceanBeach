using Business.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
        [ApiController]
        [Route("[controller]")]
    public class OfertasController : ControllerBase
    {
        [HttpGet]
        [Route("OfertasActuales")]
            public Ofertas OfertasActuales()

            {
                return new OfertasBusiness().OfertasActuales();
            }// fin m

        }// fin clase
    
}
