using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable

namespace Proyecto_BD.Models
{
    public partial class Ticket
    {
        public int IdTicket { get; set; }
        public int IdEvento { get; set; }
        public int IdCliente { get; set; }
        public Bitmap Qr { get; set; }

        public virtual Evento IdEventoNavigation { get; set; }
    }
}
