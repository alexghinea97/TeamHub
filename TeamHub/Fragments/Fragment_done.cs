using System;
using System.Collections.Generic;
using System.Data;
using Android.OS;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace TeamHub.Fragments
{
    public class Fragment_done : Android.Support.V4.App.Fragment
    {
        private ListView listViewTasks;
        private List<String> tasksList;
        private ArrayAdapter<String> listAdapter;
        public static int option;
        public static int idSelectedTask = -1;

        public void AfiseazaTask()
        {
            tasksList = new List<string>();
            DataTable data = new DataTable();
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                if (Fragments.Fragment_projects.idTeamSelected != -1)
                {
                    MySqlCommand getTasks = new MySqlCommand("SELECT task_name FROM THTasks where id_team = " + Fragment_projects.idTeamSelected + " AND status = 4;", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(getTasks);
                    adapter.Fill(data);
                    String numeTask;
                    foreach (DataRow row in data.Rows)
                    {
                        numeTask = row["task_name"].ToString();
                        tasksList.Add(numeTask);
                    }
                }
                else
                {
                    MySqlCommand getTasks = new MySqlCommand("SELECT task_name FROM THTasks where id_member = " + MainActivity.user_id + " AND status = 4;", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(getTasks);
                    adapter.Fill(data);
                    String numeTask;
                    foreach (DataRow row in data.Rows)
                    {
                        numeTask = row["task_name"].ToString();
                        tasksList.Add(numeTask);
                    }
                }
                conn.Close();
            }
            listAdapter = new ArrayAdapter<string>(this.Context, Android.Resource.Layout.SimpleExpandableListItem1, tasksList);
            listViewTasks.Adapter = listAdapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_done_layout,container,false);
            listViewTasks = view.FindViewById<ListView>(Resource.Id.listViewTask);
            tasksList = new List<string>();
            DataTable data = new DataTable();
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                if (Fragments.Fragment_projects.idTeamSelected != -1)
                {
                    MySqlCommand getTasks = new MySqlCommand("SELECT task_name FROM THTasks where id_team = " + Fragment_projects.idTeamSelected + " AND status=4;", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(getTasks);
                    adapter.Fill(data);
                    String numeTask;
                    foreach (DataRow row in data.Rows)
                    {
                        numeTask = row["task_name"].ToString();
                        tasksList.Add(numeTask);
                    } 
                }
                else
                {
                    MySqlCommand getTasks = new MySqlCommand("SELECT task_name FROM THTasks where id_member = " + MainActivity.user_id + " AND status=4;", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(getTasks);
                    adapter.Fill(data);
                    String numeTask;
                    foreach (DataRow row in data.Rows)
                    {
                        numeTask = row["task_name"].ToString();
                        tasksList.Add(numeTask);
                    }
                }
                conn.Close();
            }
            listAdapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleExpandableListItem1, tasksList);
            listViewTasks.Adapter = listAdapter;
            return view;
        }
    }
}