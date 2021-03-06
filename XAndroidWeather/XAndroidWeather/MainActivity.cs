namespace XAndroidWeather
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Widget;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    [Activity(Label = "@string/app_name", 
        Theme = "@style/AppTheme", 
        ScreenOrientation = ScreenOrientation.Portrait)]

    public class MainActivity : AppCompatActivity
    {
        Button btnBuscar;
        TextView txtxCitySearch;
        TextView txtTemperature;
        TextView txtDescripcion;
        TextView txtCityName;
        ImageView imgWheter;
        ProgressDialogFragment progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            this.btnBuscar = (Button)FindViewById(Resource.Id.btnCheck);
            this.txtxCitySearch = (TextView)FindViewById(Resource.Id.CityName);
            this.txtTemperature = (TextView)FindViewById(Resource.Id.temperatureTextView);
            this.txtDescripcion = (TextView)FindViewById(Resource.Id.weatherDescriptionText);
            this.txtCityName = (TextView)FindViewById(Resource.Id.placeText);
            this.imgWheter = (ImageView)FindViewById(Resource.Id.weatherImage);
            this.GetData("Bogota");
            btnBuscar.Click += BtnBuscar_Click;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.GetData(txtxCitySearch.Text);
            txtxCitySearch.Text = string.Empty;
        }

        async void GetData(string city)
        {
            string key = "a75a2e3279fd4174c1958f3cfacc23ab";
            string apibase = $"http://api.openweathermap.org/data/2.5/weather?q=";
            string unit = "metric";

            if (string.IsNullOrEmpty(city))
            {
                Toast.MakeText(this, "Please enter a valid city name", ToastLength.Short).Show();
                return;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                Toast.MakeText(this, "This App required intenet", ToastLength.Short).Show();
                return;
            }

            this.ShowProgressDialog("Fetching Weather...");

            try
            {
                string url = $"{apibase}{city}&appid={key}&units={unit}&lang=es";
                var handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                string result = await client.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<RootObject>(result);
                txtDescripcion.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data.Weather[0].Description);
                txtTemperature.Text = data.Main.Temp.ToString();
                txtCityName.Text = $"{data.Name}, {data.Sys.Country}";
                this.CloseProgressDialog();
            }
            catch (Exception)
            {
                this.CloseProgressDialog();
                Toast.MakeText(this, "Danger, city not found", ToastLength.Short).Show();
            }
        }

        private void ShowProgressDialog(string status)
        {
            this.progressDialog = new ProgressDialogFragment(status);
            var tran = SupportFragmentManager.BeginTransaction();
            progressDialog.Cancelable = true;
            progressDialog.Show(tran, "progress");
        }

        private void CloseProgressDialog()
        {
            if(progressDialog != null)
            {
                progressDialog.Dismiss();
                progressDialog = null;
            }
        }
    }
}