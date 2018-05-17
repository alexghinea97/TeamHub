using System;
using System.Collections.Generic;
using System.Data;
using Android.OS;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace TeamHub.Fragments
{
    //public static 
    public class Fragment_projects : Android.Support.V4.App.Fragment
    {
        private ListView listViewProjects;
        private List<String> projectList;
        private ArrayAdapter<String> listAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_projects_layout, container, false);
            listViewProjects = view.FindViewById<ListView>(Resource.Id.listViewProiecte);
            projectList = new List<string>();
            DataTable data = new DataTable();
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand getProjects = new MySqlCommand("SELECT TeamName,ProjectName FROM THTeams ", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(getProjects);
                adapter.Fill(data);
                String numeProiect, numeEchipa;
                foreach (DataRow row in data.Rows)
                {
                    numeEchipa = row["TeamName"].ToString();
                    projectList.Add(numeEchipa);
                    numeProiect = row["ProjectName"].ToString();
                    projectList.Add(numeProiect);
                }
                conn.Close();
            }
            listAdapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleExpandableListItem1, projectList);
            listViewProjects.Adapter = listAdapter;
            return view;
        }
    }
}