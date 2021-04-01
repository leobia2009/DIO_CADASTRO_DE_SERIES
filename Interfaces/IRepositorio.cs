using System.Collections.Generic;


namespace CadastroDeSeries.Interfaces{
    public interface IRepositorio <T> {

        List<T> ListaDeSeries();

        T RetornaPorId(int id);
        void InsereSerie(T entidade);

        void ExcluiSerie(int id);

        void AtualizaSerie(int id, T entidade);

        public int ProximoId();
         
    }
}