using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.AcessoDados.Servicos;
using WebProjVet.Controllers;

namespace WebProjVet.Models
{
    public class ProprietarioViewModel
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Nome { get; set; }


        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Endereco { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Uf { get; set; }
    }
}
