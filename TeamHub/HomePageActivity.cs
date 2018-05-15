using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace TeamHub
{
    [Activity(Label = "HomePageActivity", Theme = "@style/MyTheme")]
    public class HomePageActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar hToolbar;
        private HomePageActionBarDrawerToggle hDrawerToggle;
        private DrawerLayout hDrawerLayout;
        private ListView hLeftDrawer;
        private ListView hRightDrawer;
        private ArrayAdapter leftMenuAdapter;
        private ArrayAdapter rightMenuAdapter;
        private List<string> leftMenuDataSet;
        private List<string> rightMenuDataSet;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomePageLayout);

            int userId = Intent.GetIntExtra("idMember", -1);
            hToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            hDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            hRightDrawer = FindViewById<ListView>(Resource.Id.right_drawer);
            hLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            SetSupportActionBar(hToolbar);

            rightMenuDataSet = new List<string>();
            rightMenuDataSet.Add("Create Project and Team");
            rightMenuDataSet.Add("Add member");
            rightMenuDataSet.Add("Add task");
            rightMenuDataSet.Add("Delete project");
            rightMenuDataSet.Add("Delete member");
            rightMenuDataSet.Add("Delete task");
            rightMenuAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, rightMenuDataSet);
            hRightDrawer.Adapter = rightMenuAdapter;

            leftMenuDataSet = new List<string>();
            leftMenuDataSet.Add("Backlog");
            leftMenuDataSet.Add("In Development");
            leftMenuDataSet.Add("In Review");
            leftMenuDataSet.Add("In Testing");
            leftMenuDataSet.Add("Done");
            leftMenuAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, leftMenuDataSet);
            hLeftDrawer.Adapter = leftMenuAdapter;

            hDrawerToggle = new HomePageActionBarDrawerToggle(
                this,
                hDrawerLayout,
                Resource.String.openedDrawer,
                Resource.String.closedDrawer
            );
            hDrawerLayout.AddDrawerListener(hDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            hDrawerToggle.SyncState();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    hDrawerLayout.CloseDrawer(hRightDrawer);
                    hDrawerToggle.OnOptionsItemSelected(item);
                    return true;

                case Resource.Id.add_menu:
                    if (hDrawerLayout.IsDrawerOpen(hRightDrawer))
                    {
                        hDrawerLayout.CloseDrawer(hRightDrawer);
                        return true;
                    }
                    else
                    {
                        hDrawerLayout.OpenDrawer(hRightDrawer);
                        hDrawerLayout.CloseDrawer(hLeftDrawer);
                        return true;
                    }
                default: return base.OnOptionsItemSelected(item);
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_add_items, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}    


