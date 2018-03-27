using Android.App;
using Android.Widget;
using Android.OS;
using System.Data;
using MySql.Data.MySqlClient;

namespace TeamHub
{
    [Activity(Label = "TeamHub", MainLauncher = true)]
    public class MainActivity : Activity
    {

        private Button btnSignUp;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            btnSignUp = FindViewById<Button>(Resource.Id.btnSignup);
            btnSignUp.Click += BtnSignUp_Click;

        }

        private void BtnSignUp_Click(object sender, System.EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogSignUp dialogSup = new DialogSignUp();
            dialogSup.Show(transaction, "dialog fragment");
        }

    }
}

