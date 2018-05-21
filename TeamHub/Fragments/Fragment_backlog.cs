using System;
using System.Collections.Generic;
using System.Data;
using Android.OS;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace TeamHub.Fragments
{
    public class Fragment_backlog : Android.Support.V4.App.Fragment
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
                MySqlCommand getTasks = new MySqlCommand("SELECT task_name FROM THTasks where id_team = " + Fragment_projects.idTeamSelected + " ;", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(getTasks);
                adapter.Fill(data);
                String numeTask;
                foreach (DataRow row in data.Rows)
                {
                    numeTask = row["task_name"].ToString();
                    tasksList.Add(numeTask);
                }
                conn.Close();
            }
            listAdapter = new ArrayAdapter<string>(this.Context, Android.Resource.Layout.SimpleExpandableListItem1, tasksList);
            listViewTasks.Adapter = listAdapter;
            listViewTasks.ItemClick += ListViewTasks_ItemClick;
        }

        private void ListViewTasks_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            option = e.Position;
            PopupMenu popup = new PopupMenu(this.Activity, listViewTasks.GetChildAt(option));
            popup.Inflate(Resource.Menu.popup_tasks);
            popup.MenuItemClick += Popup_MenuItemClick;
            popup.Show();

        }

        private void Popup_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.asignTask:
                    MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        MySqlCommand assignTask = new MySqlCommand("UPDATE THTasks SET id_member = " + Fragment_members.idMemberSelected + " WHERE id_team = " + Fragment_projects.idTeamSelected + " ;", conn);
                        assignTask.ExecuteNonQuery();
                        conn.Close();
                    }
                    break;
                case Resource.Id.deleteTask:
                    int rownumDel = option;
                    DataTable dataDel = new DataTable();
                    MySqlConnection connDel = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
                    if (connDel.State == ConnectionState.Closed)
                    {
                        connDel.Open();
                        MySqlCommand getTasks = new MySqlCommand("SELECT id_task FROM THTasks where id_team = " + Fragment_projects.idTeamSelected + " ;", connDel);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(getTasks);
                        adapter.Fill(dataDel);
                        String numeTask;
                        foreach (DataRow row in dataDel.Rows)
                        {
                            if (option != 0)
                                option--;
                            else
                            {
                                idSelectedTask = System.Convert.ToInt32(row["id_task"].ToString());
                                break;
                            }   
                        }
                        MySqlCommand delTask = new MySqlCommand("DELETE FROM THTasks where id_task ="+idSelectedTask+";", connDel);
                        delTask.ExecuteNonQuery();
                        connDel.Close();
                    }
                    break;
                default: throw new NotImplementedException();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_backlog_layout, container, false);
            listViewTasks = view.FindViewById<ListView>(Resource.Id.listViewTask);
            tasksList = new List<string>();
            DataTable data = new DataTable();
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand getTasks = new MySqlCommand("SELECT task_name FROM THTasks where id_team = " + Fragment_projects.idTeamSelected + " ;", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(getTasks);
                adapter.Fill(data);
                String numeTask;
                foreach (DataRow row in data.Rows)
                {
                    numeTask = row["task_name"].ToString();
                    tasksList.Add(numeTask);
                }
                conn.Close();
            }
            listAdapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleExpandableListItem1, tasksList);
            listViewTasks.Adapter = listAdapter;
            listViewTasks.ItemClick += ListViewTasks_ItemClick;
            return view;
        }
    }
}