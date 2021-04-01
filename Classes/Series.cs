using System;

namespace CadastroDeSeries{

    public class Series : EntidadeBase
    {

        private Genero Genero { get; set; }
        private string Título { get; set; }
        private string Descrição { get; set; }
        private int Ano { get; set; }
        private bool Excluído { get; set; }

        public Series(int id, Genero genero, string título, string descrição,
                     int ano, bool excluído)
        {

            this.Id = id;
            this.Genero = genero;
            this.Título = título;
            this.Descrição = descrição;
            this.Ano = ano;
            this.Excluído = false;

        }


        public override string ToString()
        {

            string retorno = "|";
            retorno += "Genero: " + this.Genero + "|" + Environment.NewLine;
            retorno += "Título: " + this.Título + "|" + Environment.NewLine;
            retorno += "Descrição: " + this.Descrição + "|" + Environment.NewLine;
            retorno += "Ano de Início: " + this.Ano + "|" + Environment.NewLine;
            retorno += "Situação: " + this.Excluído + "|" + Environment.NewLine;
            return retorno;

        }

        public string ToOpera()
        {

            string retorno = "|";
            retorno += this.Genero + "|";
            retorno += this.Título + "|" ;
            retorno += this.Descrição + "|";
            retorno += this.Ano + "|" ;
            retorno += this.Excluído + "|" ;
            return retorno;

        }

        public string RetornarTítulo()
        {

            return this.Título;

        }

        public int RetornarId()
        {

            return this.Id;

        }

        public bool RetornarSituacao()
        {
            return this.Excluído;
        }

        public int RetornarAno()
        {

            return this.Ano;

        }


        public void ExcluirSerie()
        {

            this.Excluído = true;

        }


    }
}
