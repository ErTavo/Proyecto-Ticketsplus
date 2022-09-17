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
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("Turip ip ip", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            qrCodeImage.Save("C:\\Users\\gusjr\\Documents\\GitHub\\Backup Miercoles 14\\Proyecto BD\\wwwroot\\images\\QRs\\QRba.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        



        //Boton Reclamar boletos

        private void SendEmail(string EmailDestino, string nombre, string telefono, string evento)
        {





            int numeroticket=0;

            //GenerarQR(numeroticket);
            string EmailOrigen = "proyectosudeo321@gmail.com";
            string Contrase単a = "ibljpoybidoaiwio";
            
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Boletos",
                "<h1> Muchas Gracias por tu preferencia </h1>" +
                "<p>Adjuntamos los boletos para su evento</p>" +
                "<a><img src=https://images.squarespace-cdn.com/content/v1/56be46a6b6aa60dbb45e41a5/1580423021730-66FL6RSLNEJJAKBGDU2I/RaffleTicket_iStock-114267095.jpg?format=1000w></a><br>");
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
