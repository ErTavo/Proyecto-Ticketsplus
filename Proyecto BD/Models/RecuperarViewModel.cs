using System.ComponentModel.DataAnnotations;

namespace Proyecto_BD.Models
{   
       
    
        public class RecuperarViewModel
        {
        public string Nombre { get; set; }
         public string Telefono { get; set; }
            public Evento Eventos { get; set; }
        [Required]
            [EmailAddress]
            public string Email { get; set; }
           

        
        }

    }


