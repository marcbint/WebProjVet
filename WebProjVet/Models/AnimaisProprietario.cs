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

        public DateTime Data { get; set; }

        

    }
}
