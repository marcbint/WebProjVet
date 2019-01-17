using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class AnimaisProprietario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int Id { get; set; }

        public int AnimaisId { get; set; }

        [ForeignKey("AnimaisId")]
        public virtual Animais Animais { get; set; }

        public int ProprietarioId { get; set; }

        [ForeignKey("ProprietarioId")]
        public virtual Proprietario Proprietario { get; set; }

        [Display(Name = "DATA AQUISIÇÃO")]
        [DataType(DataType.Date)]
        public DateTime DataAquisicao { get; set; }

        [Display(Name = "DATA DESASSOCIAÇÃO")]
        [DataType(DataType.Date)]
        public DateTime? DataDesassociacao { get; set; }

        [Display(Name = "DATA INCLUSAO")]
        [DataType(DataType.Date)]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "MOTIVO")]
        [MaxLength(300), StringLength(300)]
        public string Motivo { get; set; }

        public DateTime DataValidade { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataUltimaApuracao { get; set; }


    }
}
