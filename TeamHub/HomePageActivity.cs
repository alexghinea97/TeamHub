using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using SupportFragment = Android.Support.V4.App.Fragment;

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
        private ArrayAdapter<string> leftMenuAdapter;
        private ArrayAdapter<string> rightMenuAdapter;
        private List<string> leftMenuDataSet;
        private List<string> rightMenuDataSet;
        private SupportFragment currentFragment;
        private Fragments.Fragment_projects fragmentProjects;
        private Fragments.Fragment_backlog fragmentBacklog;
        private Fragments.Fragment_members fragmentMembers;
        private Fragments.Fragment_in_development fragmentInDevelopment;
        private Fragments.Fragment_in_testing fragmentInTesting;
        private Fragments.Fragment_in_review fragmentInReview;
        private Fragments.Fragment_done fragmentDone;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomePageLayout);

            fragmentProjects = new Fragments.Fragment_projects();
            fragmentMembers = new Fragments.Fragment_members();
            fragmentBacklog = new Fragments.Fragment_backlog();
            fragmentInDevelopment = new Fragments.Fragment_in_development();
            fragmentInReview = new Fragments.Fragment_in_review();
            fragmentInTesting = new Fragments.Fragment_in_testing();
            fragmentDone = new Fragments.Fragment_done();

            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.fragmentContainer, fragmentDone, "Done");
            transaction.Hide(fragmentDone);
            transaction.Add(Resource.Id.fragmentContainer, fragmentInTesting, "In testing");
            transaction.Hide(fragmentInTesting);
            transaction.Add(Resource.Id.fragmentContainer, fragmentInReview, "In review");
            transaction.Hide(fragmentInReview);
            transaction.Add(Resource.Id.fragmentContainer, fragmentInDevelopment, "In dev");
            transaction.Hide(fragmentInDevelopment);
            transaction.Add(Resource.Id.fragmentContainer, fragmentBacklog, "Backlog");
            transaction.Hide(fragmentBacklog);
            transaction.Add(Resource.Id.fragmentContainer, fragmentMembers, "Members");
            transaction.Hide(fragmentMembers);
            transaction.Add(Resource.Id.fragmentContainer, fragmentProjects, "Projects");
            currentFragment = fragmentProjects;
            transaction.Commit();
           
            hToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            hDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            hRightDrawer = FindViewById<ListView>(Resource.Id.right_drawer);
            hLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            SetSupportActionBar(hToolbar);

            rightMenuDataSet = new List<string>();
            rightMenuDataSet.Add("Create Project and Team");
            rightMenuDataSet.Add("Add member");
            rightMenuDataSet.Add("Create task");
            rightMenuAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, rightMenuDataSet);
            hRightDrawer.Adapter = rightMenuAdapter;

            leftMenuDataSet = new List<string>();
            leftMenuDataSet.Add("Projects and Teams");
            leftMenuDataSet.Add("Members");
            leftMenuDataSet.Add("Backlog");
            leftMenuDataSet.Add("In Development");
            leftMenuDataSet.Add("In Review");
            leftMenuDataSet.Add("In Testing");
            leftMenuDataSet.Add("Done");
            leftMenuAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, leftMenuDataSet);
            hLeftDrawer.Adapter = leftMenuAdapter;

            hLeftDrawer.ItemClick += HLeftDrawer_ItemClick;
            hRightDrawer.ItemClick += HRightDrawer_ItemClick;

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

        private void ShowFragment(SupportFragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Hide(currentFragment);
            transaction.Show(fragment);
            transaction.AddToBackStack(null);
            transaction.Commit();
            currentFragment = fragment;
        }

        private void HLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int option = e.Position;
            switch(option)
            {
                case 0:
                    hDrawerLayout.CloseDrawer(hLeftDrawer);
                    ShowFragment(fragmentProjects);
                    fragmentProjects.OnCreate(null);
                    break;
                case 1:
                    hDrawerLayout.CloseDrawer(hLeftDrawer);
                    ShowFragment(fragmentMembers);
                    break;
                case 2:
                    hDrawerLayout.CloseDrawer(hLeftDrawer);
                    ShowFragment(fragmentBacklog);
                    fragmentBacklog.OnCreate(null);
                    break;
                case 3:
                    hDrawerLayout.CloseDrawer(hLeftDrawer);
                    ShowFragment(fragmentInDevelopment);
                    break;
                case 4:
                    hDrawerLayout.CloseDrawer(hLeftDrawer);
                    ShowFragment(fragmentInReview);
                    break;
                case 5:
                    hDrawerLayout.CloseDrawer(hLeftDrawer);
                    ShowFragment(fragmentInTesting);
                    break;
                case 6:
                    hDrawerLayout.CloseDrawer(hLeftDrawer);
                    ShowFragment(fragmentDone);
                    break;
                default: throw new System.NotImplementedException();
            }
        }

        private void HRightDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int option = e.Position;
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            switch (option)
            {
                case 0:
                    hDrawerLayout.CloseDrawer(hRightDrawer);
                    Fragments.Fragment_create_project dialogCreateProject = new Fragments.Fragment_create_project();
                    dialogCreateProject.Show(transaction, "dialog fragment");
                    break;
                case 1:
                    hDrawerLayout.CloseDrawer(hRightDrawer);
                    Fragments.Fragment_add_member dialogAddMember = new Fragments.Fragment_add_member();
                    dialogAddMember.Show(transaction, "dialog fragment");
                    break;
                case 2:
                    hDrawerLayout.CloseDrawer(hRightDrawer);
                    Fragments.Fragment_create_task dialogCreateTask = new Fragments.Fragment_create_task();
                    dialogCreateTask.Show(transaction, "dialog fragment");
                    break;
                default: throw new System.NotImplementedException();
            }
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


