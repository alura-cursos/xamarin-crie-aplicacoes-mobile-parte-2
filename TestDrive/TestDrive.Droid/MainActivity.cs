using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TestDrive;
using TestDrive.Media;
using TestDrive.Droid;
using Android.Provider;
using Android.Content;
using Xamarin.Forms;
using Android.Graphics;
using System.Collections.Generic;
using TestDrive.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MainActivity))]
namespace TestDrive.Droid
{
    [Activity(Label = "TestDrive", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity :
        global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
        , ICamera
    {
        static Java.IO.File arquivoImagem;

        public void TirarFoto()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            arquivoImagem = PegarArquivoImagem();

            intent.PutExtra(MediaStore.ExtraOutput,
                Android.Net.Uri.FromFile(arquivoImagem));

            var activity = Forms.Context as Activity;
            activity.StartActivityForResult(intent, 0);
        }

        private static Java.IO.File PegarArquivoImagem()
        {
            Java.IO.File arquivoImagem;
            Java.IO.File diretorio = new Java.IO.File(
Android.OS.Environment.GetExternalStoragePublicDirectory(
Android.OS.Environment.DirectoryPictures), "Imagens");

            if (!diretorio.Exists())
                diretorio.Mkdirs();

            arquivoImagem =
                new Java.IO.File(diretorio, "MinhaFoto.jpg");
            return arquivoImagem;
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                byte[] bytes;
                using (var stream = new Java.IO.FileInputStream(arquivoImagem))
                {
                    bytes = new byte[arquivoImagem.Length()];
                    stream.Read(bytes);
                }
                MessagingCenter.Send<byte[]>(bytes, "FotoTirada");
            }
        }
    }
}

