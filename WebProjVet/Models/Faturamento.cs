using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Faturamento
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProprietarioId { get; set; }

        [ForeignKey("ProprietarioId")]
        public Proprietario Proprietario { get; set; }



    }
}
