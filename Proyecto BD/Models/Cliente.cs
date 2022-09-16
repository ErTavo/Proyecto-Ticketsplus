using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_BD.Models
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
    }
}
