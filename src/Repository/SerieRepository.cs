
using System;
using System.Collections.Generic;
using DIO.Series.Entity;
using DIO.Series.Interfaces;

namespace DIO.Series.Repository
{
    public class SerieRepository : IRepository<Serie>
    {
        private int _ultimoId = 5;
        private List<Serie> _listaSerie = new List<Serie>()
        {
            new Serie(1, "Alf o eteimoso", "Um alienigena que curte gatos", 1980, Enums.Genero.Comedia),
            new Serie(2, "The office", "Um escritorio muito louco", 2005, Enums.Genero.Comedia),
            new Serie(3, "Parks and recreations", "Um orgao do governo muito louco", 2005, Enums.Genero.Comedia),
            new Serie(4, "Rick and Morty", "Um garoto e seu avo em multiversos", 2015, Enums.Genero.Comedia),
            new Serie(5, "Invencivel", "Um adolescente com codinome invencivel mas que é bem 'vencivel'", 2021, Enums.Genero.Acao)
        };

        public void Atualizar(int id, Serie entidade)
        {
            var index = _listaSerie.FindIndex(s => s.Id.Equals(entidade.Id));
            _listaSerie[index] = entidade;
        }

        public void Excluir(int id)
        {
            var item = _listaSerie.Find(s => s.Id.Equals(id));
            _listaSerie.Remove(item);
        }

        public void Inserir(Serie entidade)
        {
            _listaSerie.Add(entidade);
        }

        public List<Serie> Listar()
        {
            return _listaSerie;
        }
        public Serie RetornaPorId(int id)
        {
            return _listaSerie.Find(s => s.Id.Equals(id));
        }

        public int ProximoId()
        {
            _ultimoId += 1;
            return _ultimoId;
        }
    }
}