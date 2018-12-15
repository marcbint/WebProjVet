using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class DoadoraProprietario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DoadoraId { get; set; }

        [ForeignKey("DoadoraId")]
        public virtual Doadora Doadora { get; set; }

        public int ProprietarioId { get; set; }

        [ForeignKey("ProprietarioId")]
        public virtual Proprietario Proprietario { get; set; }

        public DateTime Data { get; set; }

        

    }
}
