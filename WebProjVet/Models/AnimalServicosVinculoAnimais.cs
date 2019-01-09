using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class AnimalServicosVinculoAnimais
    {
        [Key]
        public int Id { get; set; }

        public int AnimaisServicosId { get; set; }

        [ForeignKey("AnimaisServicosId")]
        public virtual AnimaisServicos AnimaisServicos { get; set; }

        public int AnimaisId { get; set; }

        [ForeignKey("AnimaisId")]
        public virtual Animais Animais { get; set; }
    }
}
