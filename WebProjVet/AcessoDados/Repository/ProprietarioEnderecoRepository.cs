using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Repository
{
    public class ProprietarioEnderecoRepository : IProprietarioEnderecoRepository
    {
        private readonly WebProjVetContext _context;


        public ProprietarioEnderecoRepository(WebProjVetContext context)
        {
            _context = context;
        }

        public void Editar(ProprietarioEndereco proprietarioEndereco)
        {
            _context.Entry(proprietarioEndereco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;           
            _context.SaveChanges();

        }

        public void Remover(ProprietarioEndereco proprietarioEndereco)
        {
            _context.ProprietarioEnderecos.Remove(proprietarioEndereco);
            _context.SaveChanges();
        }
    }
}
