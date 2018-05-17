using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace TeamHub.Fragments
{
    class Fragment_create_task : DialogFragment
    {
        private EditText etTaskName;
        private Button btnCreateTask;
        private Button btnCancelCreateTask;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Fragment_create_task_layout, container, false);
            etTaskName = view.FindViewById<EditText>(Resource.Id.txtTaskName);
            btnCreateTask = view.FindViewById<Button>(Resource.Id.btnCreateTask);
            btnCancelCreateTask = view.FindViewById<Button>(Resource.Id.btnCancelCreateTask);

            btnCreateTask.Click += BtnCreateTask_Click;
            btnCancelCreateTask.Click += BtnCancelCreateTask_Click;
            return view;
        }

        private void BtnCancelCreateTask_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        private void BtnCreateTask_Click(object sender, EventArgs e)
        {
            InsertInfo(etTaskName.Text);
        }

        private void InsertInfo(String numeTask)
        {
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand insertTask = new MySqlCommand("insert into THTasks(task_name) values(@taskName);", conn);
                insertTask.Parameters.AddWithValue("@taskName", numeTask);
                insertTask.ExecuteNonQuery();
                AlertDialog.Builder alertTaskCreateSucces = new AlertDialog.Builder(this.Activity);
                alertTaskCreateSucces.SetMessage("You have created a Task successfully !");
                alertTaskCreateSucces.Show();
                this.Dismiss();
                conn.Close();
            }
        }
    }
}