﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public abstract class Animal : Entity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "NOME"), MaxLength(100)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Nome { get; set; }

        [Display(Name = "REGISTRO ABQM"), MaxLength(20)]
        public string Abqm { get; set; }


    }
}
