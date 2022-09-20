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
using Microsoft.SqlServer.Server;
using static System.Net.Mime.MediaTypeNames;

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
        private void GenerarQR(int idticket, int ideven, int idcli)


        {
            


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("Tu numero de Ticket es: "+idticket, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            qrCodeImage.Save("C:\\Users\\gusjr\\Documents\\GitHub\\Proyecto-BD\\Proyecto BD\\wwwroot\\images\\QRs\\QRNo" + idticket+".png", System.Drawing.Imaging.ImageFormat.Png);

            byte[] i = imagentobyte(qrCodeImage);
                
            

            
            
            try
            {
                var _db = new TicketsplusDBContext();
                //_db.Tickets.Add(new Ticket {IdEvento = ideven, IdCliente= idcli ,Qr = "C:\\Users\\gusjr\\Documents\\GitHub\\Proyecto-BD\\Proyecto BD\\wwwroot\\images\\QRs\\QRNo" + idticket+".png" });
                _db.Tickets.Add(new Ticket {IdEvento = ideven, IdCliente= idcli ,Qr = i });
                _db.SaveChanges();

            }
            catch (Exception )
            {

                return;
            }

        }

        public static byte[] imagentobyte(Bitmap qrCodeImage)
        {
            ImageConverter convertidor = new ImageConverter();
            return (byte[])convertidor.ConvertTo(qrCodeImage, typeof(byte[]));
        }





        //Boton Reclamar boletos

        private void SendEmail(string[] datos)
        {
            // Guardar cliente en db
            try
            {
                var _db = new TicketsplusDBContext();
                _db.Clientes.Add(new Cliente { Nombre = datos[1], Telefono = Int32.Parse(datos[2]), Correo = datos[0] });
                _db.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }

            // numero de ticket
            int noti;
            int nocliente;
            List<Ticket> tickets = new List<Ticket>();
            List<Evento> Eventos = new List<Evento>();
            List<Cliente> clietnes = new List<Cliente>();
            int noevento;

            if (datos[3] != "Feria de Ingenieria")
            {
                noevento = 4;

                if (datos[3] != "Concierto")
                {
                    noevento = 5;
                }
            }
            else
            {
                noevento = 1;
            }

            try
            {
                using (var _db = new TicketsplusDBContext())
                {
                    tickets = _db.Tickets.Where(x => x.IdTicket != 0).ToList();

                    clietnes = _db.Clientes.Where(x => x.Nombre != "").ToList();

                    Eventos = _db.Eventos.Where(x => x.Nombre == datos[3]).ToList();

                }
                noti = tickets.Count();
                nocliente = clietnes.Count();


            }
            catch (Exception )
            {

                return;
            }

            noti = noti + 1;

            



            


            GenerarQR(noti, noevento, nocliente);

            try
            {
                var _db = new TicketsplusDBContext();
                _db.Ticketxclientes.Add(new Ticketxcliente { IdCliente = nocliente, IdTicket = noti });
                _db.SaveChanges();

            }
            catch (Exception)
            {

                return;
            }


            string EmailOrigen = "proyectosudeo321@gmail.com";
            string Contrase単a = "ibljpoybidoaiwio";
            
            MailMessage oMailMessage = new MailMessage(EmailOrigen, datos[0], "Boletos",
                "<h2> Ticket+ </h2>" +
                "<h3> ¡Gracias por confiar en nosotros! Tenemos tus necesidades como la prioridad número 1. Eres parte esencial de lo que hacemos en Ticket+, esperamos que tu experiencia con nosotros fuera extraordinaria. </h3>" +
                "<h3> A continuación adjuntamos el código QR que será la llave a toda la información sobre tu evento. </h3>" +
                "<li> Nombre: " + datos[1]+" </li>"  +
                "<li> Teléfono: " + datos[2] + " </li>" +
                "<li> Correo: " + datos[0] + " </li>" +
                "<li> No. Ticket: "+ noti +"</li>" +
                "<li> Evento: " + datos[3] + " </li>" +
                "<li> Fecha: " + Eventos[0].Fecha +" </li>" + 
                "<h3> Asegúrate de escanear la imagen de tu código QR completamente, incluyendo los bordes en blanco. </h3>" +
                "<h3> Muchas gracias por tu preferencia  </h3>" +
                "<a><img src=https://www.ukapp.org.uk/wp-content/uploads/2021/08/tickets.png </a><br>");
            oMailMessage.Attachments.Add(new Attachment("C:\\Users\\gusjr\\Documents\\GitHub\\Proyecto-BD\\Proyecto BD\\wwwroot\\images\\QRs\\QRNo" + noti+".png"));            

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
            RecuperarViewModel nombre = new RecuperarViewModel();
            RecuperarViewModel telefono = new RecuperarViewModel();
            RecuperarViewModel evento = new RecuperarViewModel();
            List<RecuperarViewModel> datos = new List<RecuperarViewModel>();
            datos.Add(model);
            datos.Add(nombre);
            datos.Add(telefono);
            datos.Add(evento);

            
            return View(datos);
        }


        [HttpPost]
        public int Recuperar(string[] datos)
        {
            
            if (datos[0] !="")
            {
                SendEmail(datos);
                return 1;
            }
            else
            {
                return 0;
            }
                
            

        }

    }
}
