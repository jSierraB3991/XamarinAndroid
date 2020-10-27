namespace FingerPrintXam
{
    using Android;
    using Android.App;
    using Android.Hardware.Fingerprints;
    using Android.OS;
    using Android.Runtime;
    using Android.Security.Keystore;
    using Android.Support.V4.App;
    using Android.Support.V7.App;
    using Android.Widget;
    using Java.Security;
    using Javax.Crypto;
    using System;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private KeyStore keyStore;
        private Cipher cipher;
        private string KEY_NAME = "JDSB";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            KeyguardManager keyguardManager = (KeyguardManager)GetSystemService(KeyguardService);
            FingerprintManager fingerprintManager = (FingerprintManager)GetSystemService(FingerprintService);
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.UseFingerprint) != (int)Android.Content.PM.Permission.Granted)
                return;
            if (!fingerprintManager.IsHardwareDetected)
            {
                Toast.MakeText(this, GetString(Resource.String.fingerPrintPermissionDisabled), ToastLength.Short).Show();
                return;
            }
            if (!fingerprintManager.HasEnrolledFingerprints)
            {
                Toast.MakeText(this, GetString(Resource.String.fingerPrintNotRegister), ToastLength.Short).Show();
                return;
            }
            if (!keyguardManager.IsKeyguardSecure)
            {
                Toast.MakeText(this, GetString(Resource.String.lockscreenDisabled), ToastLength.Short).Show();
                return;
            }
            GetKey();
            if (CipherInit())
            {
                FingerprintManager.CryptoObject cryptoObject = new FingerprintManager.CryptoObject(cipher);
                FingerPrintHandler helper = new FingerPrintHandler(this);
                helper.StartAuthentication(fingerprintManager, cryptoObject);
            }

        }

        private bool CipherInit()
        {
            try
            {
                cipher = Cipher.GetInstance(KeyProperties.KeyAlgorithmAes + "/" +
                                            KeyProperties.BlockModeCbc + "/" +
                                            KeyProperties.EncryptionPaddingPkcs7);
                keyStore.Load(null);
                IKey key = (IKey)keyStore.GetKey(KEY_NAME, null);
                cipher.Init(CipherMode.EncryptMode, key);
                return true;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                return false;
            }
        }

        private void GetKey()
        {
            string androidKeyStore = "AndroidKeyStore";

            keyStore = KeyStore.GetInstance(androidKeyStore);
            KeyGenerator keyGenerator = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes,
                androidKeyStore);
            keyStore.Load(null);
            keyGenerator.Init(new KeyGenParameterSpec.Builder(KEY_NAME,
                    KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                .SetBlockModes(KeyProperties.BlockModeCbc)
                .SetUserAuthenticationRequired(true)
                .SetEncryptionPaddings(KeyProperties.EncryptionPaddingPkcs7)
                .Build());
            keyGenerator.GenerateKey();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}