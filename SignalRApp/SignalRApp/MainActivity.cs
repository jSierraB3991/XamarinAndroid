namespace SignalRApp
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Views;
    using Android.Widget;
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using static Android.Views.View;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnTouchListener
    {
        private View viewMove;
        private Button btnStart;
        private HubConnection hubConnection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btnStart = FindViewById<Button>(Resource.Id.btnMove);
            viewMove = FindViewById<View>(Resource.Id.viewMode);

            string url = GetString(Resource.String.urlHub);
            try {
                hubConnection = new HubConnectionBuilder()
                                .WithUrl(url)
                                .Build();
                hubConnection.On<float, float>("ReceiveNewPosition", (x, y) => {
                    viewMove.SetX(x);
                    viewMove.SetY(y);
                });
            }
            catch (Exception ex) { Toast.MakeText(this, ex.Message, ToastLength.Short).Show(); }

            btnStart.Click += async delegate {
                if(btnStart.Text.ToLower().Equals("start")) {
                    if(hubConnection.State == HubConnectionState.Disconnected)
                    {
                        try {
                            await hubConnection.StartAsync();
                            btnStart.Text = "Stop";
                        }
                        catch (Exception ex) { Toast.MakeText(this, ex.Message, ToastLength.Short).Show(); }
                    }
                }
                else if(btnStart.Text.ToLower().Equals("stop")) {
                    if (hubConnection.State == HubConnectionState.Connected)
                    {
                        try
                        {
                            await hubConnection.StopAsync();
                            btnStart.Text = "Start";
                        }
                        catch (Exception ex) { Toast.MakeText(this, ex.Message, ToastLength.Short).Show(); }
                    }
                }
            };

            viewMove.SetOnTouchListener(this);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            viewMove.SetX(e.RawX);
            viewMove.SetY(e.RawY);

            if (hubConnection.State == HubConnectionState.Connected)
                hubConnection.SendAsync("MoveViewFromServerAsync", e.RawX, e.RawY);

            return true;
        }
    }
}