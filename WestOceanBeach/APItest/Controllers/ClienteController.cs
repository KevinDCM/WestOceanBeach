using Business.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APItest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        [HttpPost]
        [Route("insertarCliente")]
        public string insertarCliente(Cliente datosCliente)
        {
           return new ClienteBusiness().insertarCliente(datosCliente);
        }

    }
}
