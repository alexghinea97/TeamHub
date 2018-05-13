using Android.App;
using Android.Widget;
using Android.OS;
using System.Data;
using MySql.Data.MySqlClient;
using System;

namespace TeamHub
{
    [Activity(Label = "TeamHub", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button btnSignUp;
        private Button btnLogIn;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            btnSignUp = FindViewById<Button>(Resource.Id.btnSignup);
            btnLogIn = FindViewById<Button>(Resource.Id.btnLogin);
            btnSignUp.Click += BtnSignUp_Click;
            btnLogIn.Click += BtnLogIn_Click;
        }

        private void BtnLogIn_Click(object sender, System.EventArgs e)
        {
            try
            {
                EditText emailField = FindViewById<EditText>(Resource.Id.txtEmail);
                EditText passwordField = FindViewById<EditText>(Resource.Id.txtPassword);
                const string ConnectionString = "Server=db4free.net;Port=3307;database=teamhubunibuc;User id=teamhubunibuc;Password=teamhubunibuc;charset=utf8";
                MySqlConnection conn = new MySqlConnection(ConnectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand checkUser = new MySqlCommand("SELECT COUNT(*) FROM THMembers WHERE Username LIKE '" + emailField.Text + "' AND " +
                        "Userpass LIKE '" + passwordField.Text + "'", conn);
                    System.Object returnedValue = checkUser.ExecuteScalar();
                    if (returnedValue != null)
                    {
                        int count = System.Convert.ToInt32(checkUser.ExecuteScalar());
                        if (count > 0)
                        {
                            AlertDialog.Builder alertLoginSucces = new AlertDialog.Builder(this);
                            alertLoginSucces.SetMessage("Welcome !");
                            alertLoginSucces.Show();
                            StartActivity(typeof(HomePageActivity));
                        }
                        else
                        {
                            AlertDialog.Builder alertLoginSucces = new AlertDialog.Builder(this);
                            alertLoginSucces.SetMessage("The username or the password you have entered is not valid !");
                            alertLoginSucces.Show();
                        }
                        checkUser.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BtnSignUp_Click(object sender, System.EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogSignUp dialogSup = new DialogSignUp();
            dialogSup.Show(transaction, "dialog fragment");
        }
    }
}
