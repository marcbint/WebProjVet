﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class TratamentoServico
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
   
        public int TratamentoId { get; set; }
        
        [ForeignKey("TratamentoId")]
        public virtual Tratamento Tratamento { get; set; }
        
        public int ServicoId { get; set; }
       
        [ForeignKey("ServicoId")]
        public virtual Servico Servico { get; set; }
       
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
            
        public DateTime Data { get; set; }

        [DataType(DataType.Currency)]
        public decimal ValorOriginal { get; set; }

        //public DateTime? DataEstadiaFim { get; set; }

        //public TratamentoServico()
        //{
        //Servico = new Servico();
        //Tratamento = new Tratamento();
        //}
    }
}
