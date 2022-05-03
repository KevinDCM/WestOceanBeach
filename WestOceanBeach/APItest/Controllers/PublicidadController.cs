using Business.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicidadController : ControllerBase
    {
        [HttpGet]
        [Route("getPublicidadActiva")]
        public List<Publicidad> getPublicidadActiva()
        {
           return new PublicidadBusiness().getPublicidadActiva();
        }

    }
}