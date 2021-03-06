﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Receptora : Entity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "CÓDIGO"), MaxLength(10)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Codigo { get; set; }

        [Display(Name = "NOME"), MaxLength(100)]
        public string Nome { get; set; }


    }
}
