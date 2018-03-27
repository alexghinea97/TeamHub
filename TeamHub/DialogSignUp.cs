using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data;
using MySql.Data.MySqlClient;

namespace TeamHub
{
    class DialogSignUp : DialogFragment
    {
        private EditText etEmail;
        private EditText etPassword;
        private Button btnSignUp;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        { 
            var view = inflater.Inflate(Resource.Layout.DialogSignUpLayout,container,false);

            btnSignUp = view.FindViewById<Button>(Resource.Id.btnSignUpLayout);
            etEmail = view.FindViewById<EditText>(Resource.Id.txtEmailSup);
            etPassword = view.FindViewById<EditText>(Resource.Id.txtPasswordSup);

            btnSignUp.Click += BtnSignUp_Click;
            view.Focusable = true ;
            return view;
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            InsertInfo(etEmail.Text,etPassword.Text);
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
                    //tvTips.Text = "Successfully Signup";
                }

            }
            catch (MySqlException ex)
            {
                //tvTips.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }

    }
}