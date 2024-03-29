﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_BD.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Ticketxclientes = new HashSet<Ticketxcliente>();
        }

        public int IdTicket { get; set; }
        public int IdEvento { get; set; }
        public int IdCliente { get; set; }
        public byte[] Qr { get; set; }

        public virtual Evento IdEventoNavigation { get; set; }
        public virtual ICollection<Ticketxcliente> Ticketxclientes { get; set; }
    }
}
