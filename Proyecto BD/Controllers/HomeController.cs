using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Reflection;

namespace Proyecto_BD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Eventos()
        {
            return View();
        }
        public IActionResult Carrito()
        {
            return View();
        }

        public IActionResult Confirmacion(string EmailDestino)
        {
            SendEmail(EmailDestino);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Boton Reclamar boletos

        private void SendEmail(string EmailDestino)
        {
            string EmailOrigen = "proyectosudeo321@gmail.com";
            string Contrase単a = "ibljpoybidoaiwio";
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Boletos",
                "<p>Adjuntamos los boletos para su evento</p><br>" );

            oMailMessage.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailOrigen, Contrase単a),
                Timeout = 30000
            };
            smtp.Send(oMailMessage);
            smtp.Dispose();
        }

        [HttpGet]
        public IActionResult Recuperar()
        {
            RecuperarViewModel model = new RecuperarViewModel();
            return View(model);
        }


        [HttpPost]
        public int Recuperar(string model)
        {
            if (model !="")
            {
                SendEmail(model);
                return 1;
            }
            else
            {
                return 0;
            }
                
            

        }

    }
}
