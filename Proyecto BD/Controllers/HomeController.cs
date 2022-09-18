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
using QRCoder;
using System.Drawing;
using static QRCoder.PayloadGenerator;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting.Server;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.IO;

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

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Generador de QR
        private void GenerarQR(int id)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("Tu numero de Ticket es: 142", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            //qrCodeImage.Save("C:\\Users\\gusjr\\Documents\\GitHub\\Backup Miercoles 14\\Proyecto BD\\wwwroot\\images\\QRs\\QRba.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        



        //Boton Reclamar boletos

        private void SendEmail(string EmailDestino, string nombre, string telefono, string evento)
        {





            int numeroticket=0;

            //GenerarQR(numeroticket);
            string EmailOrigen = "proyectosudeo321@gmail.com";
            string Contrase単a = "ibljpoybidoaiwio";
            
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Boletos",
                "<h2> Ticket+ </h2>" +
                "<h3> ¡Gracias por confiar en nosotros! Tenemos tus necesidades como la prioridad número 1. Eres parte esencial de lo que hacemos en Ticket+, esperamos que tu experiencia con nosotros fuera extraordinaria. </h3>" +
                "<h3> A continuación adjuntamos el código QR que será la llave a toda la información sobre tu evento. </h3>" +
                "<li> Nombre </li>" +
                "<li> Teléfono </li>" +
                "<li> Correo </li>" +
                "<li> No. Ticket </li>" +
                "<li> Evento </li>" +
                "<li> Fecha </li>" +
                "<h3> Asegúrate de escanear la imagen de tu código QR completamente, incluyendo los bordes en blanco. </h3>" +
                "<h3> Muchas gracias por tu preferencia  </h3>" +
                "<a><img src=https://www.ukapp.org.uk/wp-content/uploads/2021/08/tickets.png </a><br>");
            //oMailMessage.Attachments.Add(new Attachment("C:\\Users\\gusjr\\Documents\\GitHub\\Backup Miercoles 14\\Proyecto BD\\wwwroot\\images\\QRs\\QRba.png"));

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
        private void GuardarEntrada()
        {

        }



        [HttpGet]
        public IActionResult Recuperar()
        {
            RecuperarViewModel model = new RecuperarViewModel();
            return View(model);
        }


        [HttpPost]
        public int Recuperar(string model, string nombre, string telefono, string evento)
        {
            if (model !="")
            {
                SendEmail(model, nombre, telefono, evento);
                return 1;
            }
            else
            {
                return 0;
            }
                
            

        }

    }
}
