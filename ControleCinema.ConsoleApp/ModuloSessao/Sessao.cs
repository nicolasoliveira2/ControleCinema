using ControleCinema.ConsoleApp.Compartilhado;
using System;

namespace ControleCinema.ConsoleApp.ModuloGenero
{
    public class Sessao : EntidadeBase
    {
        public string Descricao { get; set; }

        public Sessao(string descricao)
        {
            Descricao = descricao;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Descrição da Sessao: " + Descricao + Environment.NewLine;
        }
    }
}
