using System.ComponentModel.DataAnnotations;

namespace Proyecto_BD.Models
{   
       
    
        public class RecuperarViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }


