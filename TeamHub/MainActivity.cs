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
        private EditText etusername;
        private EditText etpass;
        private Button btninsert;
        private TextView tvTips;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            etusername = FindViewById<EditText>(Resource.Id.etusername);
            etpass = FindViewById<EditText>(Resource.Id.etPass);
            btninsert = FindViewById<Button>(Resource.Id.btninsert);
            tvTips = FindViewById<TextView>(Resource.Id.tvTips);

            btninsert.Click += Btninsert_Click;
        }

        private void Btninsert_Click(object sender, System.EventArgs e)
        {
            InsertInfo(etusername.Text, etpass.Text);
        }

        void InsertInfo(string userPar, string passPar)
        {
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into THMembers(username,userpass) values(@user,@pass)", conn);
                    cmd.Parameters.AddWithValue("@user", userPar);
                    cmd.Parameters.AddWithValue("@pass", passPar);
                    cmd.ExecuteNonQuery();
                    tvTips.Text = "Successfully Signup";
                }

            }
            catch (MySqlException ex)
            {
                tvTips.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

