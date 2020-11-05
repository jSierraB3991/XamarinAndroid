namespace DownloadFileProgress
{
    using Android.App;
    using Android.Content;
    using Android.Graphics.Drawables;
    using Android.OS;
    using Android.Widget;
    using Java.IO;
    using Java.Net;
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class DownloadImageFromUrl : AsyncTask<string, string, string>
    {
        private readonly Context context;
        private readonly ImageView imageView;
        private ProgressDialog progressDialog;
        private string fileName;

        public DownloadImageFromUrl(Context context, ImageView imageView)
        {
            this.context = context;
            this.imageView = imageView;
            this.fileName = Guid.NewGuid().ToString();
        }

        protected override void OnPreExecute()
        {
            progressDialog = new ProgressDialog(context);
            progressDialog.SetMessage("Download File. Please Wait...");
            progressDialog.Indeterminate = false;
            progressDialog.Max = 100;
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.SetCancelable(true);
            progressDialog.Show();
            base.OnPreExecute();
        }

        protected override void OnProgressUpdate(params string[] values)
        {
            base.OnProgressUpdate(values);
            progressDialog.SetProgressNumberFormat(values[0]);
            progressDialog.Progress = int.Parse(values[0]);
        }

        protected override void OnPostExecute([AllowNull] string result)
        {
            var storagePath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = System.IO.Path.Combine(storagePath, $"{fileName}.jpg");
            progressDialog.Dismiss();
            imageView.SetImageDrawable(Drawable.CreateFromPath(filePath));
        }

        protected override string RunInBackground(params string[] @params)
        {
            var storagePath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = System.IO.Path.Combine(storagePath, $"{fileName}.jpg");
            int count = 0;
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