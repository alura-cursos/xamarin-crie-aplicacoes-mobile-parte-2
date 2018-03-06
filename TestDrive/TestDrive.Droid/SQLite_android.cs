using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using TestDrive.Data;
using TestDrive.Droid;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_android))]
namespace TestDrive.Droid
{
    class SQLite_android : ISQLite
    {
        private const string nomeArquivoDB = "Agendamento.db3";

        public SQLiteConnection PegarConexao()
        {
            var caminhoDB = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path,
                nomeArquivoDB);

            return new SQLite.SQLiteConnection(caminhoDB);
        }
    }
}