﻿namespace StoryProgressBar
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Views;
    using Android.Widget;
    using JP.Shts.Android.Storiesprogressview;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, StoriesProgressView.IStoriesListener
    {
        public StoriesProgressView _stories;
        private ImageView _image;
        private int _counter = 0;

        private readonly int[] _resource = new int[] {
            Resource.Drawable.onepieceuniverse,
            Resource.Drawable.roronoazoro,
            Resource.Drawable.koala,
            Resource.Drawable.nami,
            Resource.Drawable.mihawk,
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            this._stories = FindViewById<StoriesProgressView>(Resource.Id.stories);
            this._stories.SetStoriesCount(_resource.Length);
            this._stories.SetStoryDuration(5000L);
            this._stories.SetStoriesListener(this);

            this._image = FindViewById<ImageView>(Resource.Id.imageView);
            _image.Click += delegate { _stories.Skip(); };
            _image.SetImageResource(_resource[_counter]);
            this._stories.StartStories();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            _stories.Destroy();
            base.OnDestroy();
        }

        public void OnComplete() { Toast.MakeText(this, Resource.String.story_complete, ToastLength.Short).Show(); }

        public void OnNext()
        {
            _counter++;
            _image.SetImageResource(_resource[_counter]);
            Toast.MakeText(this, "onNext", ToastLength.Short).Show();
        }

        public void OnPrev()
        {
            _counter--;
            _image.SetImageResource(_resource[_counter]);
            Toast.MakeText(this, "onPrev", ToastLength.Short).Show();
        }
    }
}