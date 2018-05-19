using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
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
        public static String returnedTaskName;
        public static bool valuesChanged = false;
        public static int option;

        public override void OnCreate(Bundle savedInstanceState)
        {
            if (valuesChanged)
            {
                listAdapter.Add(returnedTaskName);
                listAdapter.NotifyDataSetChanged();
                listViewTasks.ItemClick += ListViewTasks_ItemClick;
            }
            valuesChanged = false;
            base.OnCreate(savedInstanceState);

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
                    break;
                case Resource.Id.deleteTask:
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
                MySqlCommand getTasks = new MySqlCommand("SELECT task_name FROM THTasks", conn);
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