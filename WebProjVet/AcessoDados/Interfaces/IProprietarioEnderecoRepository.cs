﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IProprietarioEnderecoRepository
    {
        void Editar(ProprietarioEndereco proprietarioEndereco);
        void Remover(ProprietarioEndereco proprietarioEndereco);
    }
}
