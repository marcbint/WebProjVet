using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class AnimaisServicos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AnimaisId { get; set; }

        [ForeignKey("AnimaisId")]
        public virtual Animais Animais { get; set; }

        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public virtual Servico Servico { get; set; }

        //[DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        //[RegularExpression(@"[0-9]{1,4}(\.[0-9]{1,2})?")]
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        [DataType(DataType.Currency)]
        public decimal ValorOriginal { get; set; }

        [NotMapped]
        public string AnimaisServicosJson { get; set; }

        



    }

}
