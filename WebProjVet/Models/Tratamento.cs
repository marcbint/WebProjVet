﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models.ViewModels;

namespace WebProjVet.Models
{
    public class Tratamento
    {
        [Display(Name = "NÚMERO"), MaxLength(20)]
        public int Id { get; set; }

        [Display(Name = "DATA INÍCIO")]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public DateTime DataInicio { get; set; }

        //[Display(Name = "DATA ATUALIZAÇÃO")]
        //public DateTime DataAtualizacao { get; set; }

        [Display(Name = "DATA FINALIZAÇÃO")]
        public DateTime? DataFim { get; set; }

        [Display(Name = "DOADORA")]
        public int DoadoraId { get; set; }

        [Display(Name = "GARANHÃO")]
        public int GaranhaoId { get; set; }

        [Display(Name = "RECEPTORA")]
        public int ReceptoraId { get; set; }

        [Display(Name = "DOADORA")]
        [ForeignKey("DoadoraId")]
        public virtual Doadora Doadora { get; set; }
        //public IEnumerable<AnimalDoadora> Doadoras { get; set; }

        [Display(Name = "GARANHÃO")]
        [ForeignKey("GaranhaoId")]
        public virtual Garanhao Garanhao { get; set; }
        //public IEnumerable<AnimalGaranhao> Garanhoes { get; set; }

        [Display(Name = "RECEPTORA")]
        [ForeignKey("ReceptoraId")]
        public virtual Receptora Receptora { get; set; }
        //public IEnumerable<AnimalReceptora> Receptoras { get; set; }

        [Display(Name = "SITUAÇÃO")]
        [EnumDataType(typeof(TratamentoSituacao))]
        public TratamentoSituacao TratamentoSituacao { get; set; }

        public virtual ICollection<TratamentoServico> TratamentoServicos { get; set; }

        public virtual ICollection<TratamentoDiaria> TratamentoDiarias { get; set; }

        [NotMapped]
        public string TratamentoServicosJson { get; set; }

        [NotMapped]
        public string TratamentoDiariaJson { get; set; }

        /*
        [Display(Name = "SERVIÇO")]
        public int ServicoId { get; set; }

        [Display(Name = "SERVIÇO")]
        [ForeignKey("ServicoId")]
        public virtual List<Servico> Servico { get; set; }
        */

        public Tratamento()
        {
            TratamentoServicos = new List<TratamentoServico>();
        }
    }
}
