namespace FingerPrintXam
{
    using Android;
    using Android.Content;
    using Android.Hardware.Fingerprints;
    using Android.OS;
    using Android.Support.V4.App;
    using Android.Widget;

    internal class FingerPrintHandler: FingerprintManager.AuthenticationCallback
    {
        private readonly Context _context;

        public FingerPrintHandler(Context context)
        {
            this._context = context;
        }

        internal void StartAuthentication(FingerprintManager fingerprintManager, FingerprintManager.CryptoObject cryptoObject)
        {
            CancellationSignal cancellationSignal = new CancellationSignal();
            if (ActivityCompat.CheckSelfPermission(_context, Manifest.Permission.UseFingerprint) != (int)Android.Content.PM.Permission.Granted)
                return;
            fingerprintManager.Authenticate(cryptoObject, cancellationSignal, 0, this, null);
        }

        public override void OnAuthenticationFailed()
        {
            Toast.MakeText(_context, _context.GetString(Resource.String.fingerprintFailed), ToastLength.Short).Show();
        }

        public override void OnAuthenticationSucceeded(FingerprintManager.AuthenticationResult result)
        {
            Toast.MakeText(_context, _context.GetString(Resource.String.fingerprintSuccess), ToastLength.Short).Show();
            _context.StartActivity(new Intent(_context, typeof(HomeActivity)));
        }
    }
}