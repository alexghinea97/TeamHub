using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Views;

namespace TeamHub
{
    class HomePageActionBarDrawerToggle : ActionBarDrawerToggle
    {
        private AppCompatActivity mHostActivity;
        private int mOpenedResource;
        private int mClosedResource;
        public HomePageActionBarDrawerToggle(AppCompatActivity host, DrawerLayout drawerLayout, int openedResource, int closedResource) 
            : base(host,drawerLayout,openedResource,closedResource)
        {
            mHostActivity = host;
            mOpenedResource = openedResource;
            mClosedResource = closedResource;
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            mHostActivity.SupportActionBar.SetTitle(mClosedResource);
        }

        public override void OnDrawerOpened(View drawerView)
        {
            mHostActivity.SupportActionBar.SetTitle(mOpenedResource);
            base.OnDrawerOpened(drawerView);
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}