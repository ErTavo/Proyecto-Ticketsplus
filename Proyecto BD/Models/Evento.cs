using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_BD.Models
{
    public partial class Evento
    {
        public Evento()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int IdEvento { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
