using System.Collections.Generic;
using CadastroDeSeries.Interfaces;

namespace CadastroDeSeries{
    public class SerieRepositorio : IRepositorio<Series>
    {

        private List<Series> listaSeries = new List<Series>();

        
        public void AtualizaSerie(int id, Series objeto)
        {
            listaSeries[id] = objeto;
        }

        public void ExcluiSerie(int id)
        {
            listaSeries[id].ExcluirSerie();
        }

        public void InsereSerie(Series objeto)
        {
            listaSeries.Add(objeto);
        }


        public List<Series> ListaDeSeries()
        {
            return listaSeries;
        }
       
        public int ProximoId()
        {
            return listaSeries.Count;
        }

        public Series RetornaPorId(int id)
        {
            return listaSeries[id];
        }

    }
}