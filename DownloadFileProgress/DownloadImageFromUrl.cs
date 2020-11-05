namespace DownloadFileProgress
{
    using Android.Graphics.Drawables;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Widget;
    using DownloadFileProgress.Resources.Fragments;
    using Java.IO;
    using Java.Net;
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class DownloadImageFromUrl : AsyncTask<string, string, string>
    {
        private readonly AppCompatActivity context;
        private readonly ImageView imageView;
        private ProgressDialogFragment progressDialog;
        private string fileName;

        public DownloadImageFromUrl(AppCompatActivity context, ImageView imageView)
        {
            this.context = context;
            this.imageView = imageView;
            this.fileName = Guid.NewGuid().ToString();
        }

        protected override void OnPreExecute()
        {
            progressDialog = new ProgressDialogFragment(context.GetString(Resource.String.text_download_file));
            var tran = context.SupportFragmentManager.BeginTransaction();
            progressDialog.Cancelable = false;
            progressDialog.Show(tran, context.GetString(Resource.String.text_name_fragment_progress));
            base.OnPreExecute();
        }

        protected override void OnProgressUpdate(params string[] values)
        {
            base.OnProgressUpdate(values);
        }

        protected override void OnPostExecute([AllowNull] string result)
        {
            var storagePath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = System.IO.Path.Combine(storagePath, $"{fileName}.jpg");
            if (progressDialog != null) {
                progressDialog.Dismiss();
                progressDialog = null;
            }
            imageView.SetImageDrawable(Drawable.CreateFromPath(filePath));
        }

        protected override string RunInBackground(params string[] @params)
        {
            var storagePath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = System.IO.Path.Combine(storagePath, $"{fileName}.jpg");
            try
            {
                URL url = new URL(@params[0]);
                URLConnection connection = url.OpenConnection();
                connection.Connect();
                int lengthOfFile = connection.ContentLength;
                InputStream input = new BufferedInputStream(url.OpenStream(), lengthOfFile);
                OutputStream output = new FileOutputStream(filePath);

                byte[] data = new byte[1024];
                long total = 0;
                int count = 0;
                while ((count = input.Read(data)) != -1)
                {
                    total += count;
                    PublishProgress($"{(int)(total / 100) / lengthOfFile}");
                    output.Write(data, 0, count);
                }
                output.Flush();
                output.Close();
                input.Close();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}