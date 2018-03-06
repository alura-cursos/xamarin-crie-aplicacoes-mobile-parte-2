using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;

namespace TestDrive.Data
{
    public class AgendamentoDAO
    {
        private readonly SQLiteConnection conexao;

        private List<Agendamento> lista;

        public List<Agendamento> Lista
        {
            get
            {
                if (lista == null)
                {
                    lista = new List<Agendamento>(conexao.Table<Agendamento>());
                }
                return lista;
            }
            private set
            {
                lista = value;
            }
        }


        public AgendamentoDAO(SQLiteConnection con)
        {
            conexao = con;
            conexao.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            if (conexao.Find<Agendamento>(agendamento.ID) == null)
            {
                conexao.Insert(agendamento);
            }
            else
            {
                conexao.Update(agendamento);
            }
        }
    }
}
