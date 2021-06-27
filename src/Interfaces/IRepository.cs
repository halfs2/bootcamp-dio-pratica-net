using System;
using System.Collections.Generic;

namespace DIO.Series.Interfaces
{
    public interface IRepository<T>
    {
        List<T> Listar();
        T RetornaPorId(int id);        
        void Inserir(T entidade);        
        void Excluir(int id);        
        void Atualizar(int id, T entidade);

        int ProximoId();
    }
}