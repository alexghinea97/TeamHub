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
        private Button btnCancel;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        { 
            var view = inflater.Inflate(Resource.Layout.DialogSignUpLayout,container,false);

            btnSignUp = view.FindViewById<Button>(Resource.Id.btnSignUpLayout);
            btnCancel = view.FindViewById<Button>(Resource.Id.btnCancelSignUp);
            etEmail = view.FindViewById<EditText>(Resource.Id.txtEmailSup);
            etPassword = view.FindViewById<EditText>(Resource.Id.txtPasswordSup);

            btnSignUp.Click += BtnSignUp_Click;
            btnCancel.Click += BtnCancel_Click;
            view.Focusable = true ;
            return view;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            InsertInfo(etEmail.Text,etPassword.Text);

        }

        void InsertInfo(string userPar, string passPar)
        {
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd2 = new MySqlCommand("SELECT COUNT(*) FROM THMembers WHERE Username LIKE '" + userPar + "' ", conn);
                System.Object var = cmd2.ExecuteScalar();
                if (var != null)
                {
                    int count = System.Convert.ToInt32(cmd2.ExecuteScalar());
                    if (count == 0)
                    {
                        MySqlCommand cmd = new MySqlCommand("insert into THMembers(username,userpass) values(@user,@pass)", conn);
                        cmd.Parameters.AddWithValue("@user", userPar);
                        cmd.Parameters.AddWithValue("@pass", passPar);
                        cmd.ExecuteNonQuery();
                        //tvTips.Text = "Successfully Signup";
                        AlertDialog.Builder alertRegisterSucces = new AlertDialog.Builder(this.Activity);
                        alertRegisterSucces.SetMessage("You have registered successfully !");
                        alertRegisterSucces.Show();
                        this.Dismiss();
                    }
                    else
                    {
                        AlertDialog.Builder alertRegisterSucces = new AlertDialog.Builder(this.Activity);
                        alertRegisterSucces.SetMessage("The email you have entered already exists !");
                        alertRegisterSucces.Show();
                    }

                }
                conn.Close();
                
            }
        } 
            

    }
}