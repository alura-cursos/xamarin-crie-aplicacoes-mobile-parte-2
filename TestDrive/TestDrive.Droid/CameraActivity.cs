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
using TestDrive.Droid;
using TestDrive.Media;
using Android.Provider;
using Java.IO;
using Android.Graphics;
using Xamarin.Forms;
using Android.Content.PM;

[assembly: Xamarin.Forms.Dependency(typeof(CameraActivity))]
namespace TestDrive.Droid
{
    [Activity(Label = "CameraActivity")]
    public class CameraActivity : Activity
        , ICamera
    {
        public static class ImageData
        {
            public static File Arquivo;
            public static File Diretorio;
            public static Bitmap Imagem;
        }

        public void TirarFoto()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            ImageData.Arquivo = new File(ImageData.Diretorio,
                String.Format("MinhaFoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(ImageData.Arquivo));
            ((Activity)Forms.Context).StartActivityForResult(intent, 0);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (AppPodeTirarFotos())
                CriaDiretorioParaImagens();
        }

        private void CriaDiretorioParaImagens()
        {
            ImageData.Diretorio = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "Imagens");
            if (!ImageData.Diretorio.Exists())
            {
                ImageData.Diretorio.Mkdirs();
            }
        }

        private bool AppPodeTirarFotos()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        protected override void OnActivityResult(int requestCode, 
            [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Deixa a imagem disponível na galeria de imagens
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(ImageData.Arquivo);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            //Define o tamanho da imagem
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = Resources.DisplayMetrics.WidthPixels;

            //Obtendo dados da imagem
            var imgFile = new Java.IO.File(ImageData.Arquivo.Path);
            var stream = new Java.IO.FileInputStream(imgFile);
            var bytes = new byte[imgFile.Length()];
            stream.Read(bytes);
            MessagingCenter.Send<byte[]>(bytes, "TirarFoto");

            // Fazendo dispose da imagem no Java
            GC.Collect();
        }
    }
}