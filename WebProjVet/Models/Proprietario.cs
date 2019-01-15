using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Proprietario : Pessoa
    {
        [Key]
        public int Id { get; set; }        

        [Display(Name = "RAZÃO SOCIAL"), MaxLength(100)]
        public string RazaoSocial { get; set; }

        [Display(Name = "INSCRIÇÃO ESTADUAL"), MaxLength(20)]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "TELEFONE"), MaxLength(20)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Telefone { get; set; }

        [Display(Name = "EMAIL"), MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido")]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Email { get; set; }


        [NotMapped]
        public string TabelaItensEnderecoJson { get; set; }

        [NotMapped]
        [Display(Name = "TIPO")]
        [EnumDataType(typeof(EnderecoTipo))]
        public EnderecoTipo EnderecoTipo { get; set; }

        public virtual ICollection<ProprietarioEndereco> ProprietarioEnderecos { get; set; }

        public virtual ICollection<DoadoraProprietario> DoadoraProprietarios { get; set; }

        public virtual ICollection<GaranhaoProprietario> GaranhaoProprietarios { get; set; }

        public virtual ICollection<AnimaisProprietario> AnimaisProprietarios { get; set; }

        public virtual ICollection<Faturamento> Faturamentos { get; set; }

        public Proprietario()
        {
            ProprietarioEnderecos = new List<ProprietarioEndereco>();
            DoadoraProprietarios = new List<DoadoraProprietario>();
            AnimaisProprietarios = new List<AnimaisProprietario>();
        }
        
    }
}
