using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Academia.Application.DTOs.Student
{
    public class CreateStudent
    {
        [MaxLength(250)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage ="Invalid e-mail")]
        public string Email { get;  set; }
        [Required(ErrorMessage = "Cpf is required")]
        
        public string CPF { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }


    }
}
