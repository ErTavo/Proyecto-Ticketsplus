using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_BD.Models
{
    public partial class Ticketxcliente
    {
        public int IdCliente { get; set; }
        public int IdTicket { get; set; }
        public int IdTicketxCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Ticket IdTicketNavigation { get; set; }
    }
}
