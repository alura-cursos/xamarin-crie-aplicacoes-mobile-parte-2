using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;

namespace TestDrive.Data
{
    class AgendamentoDAO
    {
        readonly SQLiteConnection conexao;

        public AgendamentoDAO(SQLiteConnection conexao)
        {
            this.conexao = conexao;
            this.conexao.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            conexao.Insert(agendamento);
        }
    }
}
