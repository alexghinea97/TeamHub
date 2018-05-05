using Android.App;
using Android.OS;

namespace TeamHub
{
    [Activity(Label = "HomePageActivity")]
    public class HomePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomePageLayout);
            // Create your application here
        }
    }
}
