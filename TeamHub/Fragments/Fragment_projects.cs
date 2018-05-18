using System;
using System.Collections.Generic;
using System.Data;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace TeamHub.Fragments
{
    public class Fragment_projects : Android.Support.V4.App.Fragment
    {
        private ListView listViewProjects;
        private List<String> projectList;
        private ArrayAdapter<String> listAdapter;
        public static String returnedTeamName;
        public static String returnedProjectName;
        public static bool valuesChanged = false;
        public static int idTeamSelected = -1 ;
        private int option;

        public override void OnCreate(Bundle savedInstanceState)
        {
            System.Diagnostics.Debug.WriteLine(returnedProjectName);
            if (valuesChanged)
            {
                listAdapter.Add("Project : " + returnedProjectName + " by team " + returnedTeamName);
                listAdapter.NotifyDataSetChanged();
                listViewProjects.ItemClick += ListViewProjects_ItemClick;
            }
            valuesChanged = false;
            base.OnCreate(savedInstanceState);
        }

        private void ListViewProjects_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            option = e.Position;
            PopupMenu popup = new PopupMenu(this.Activity, listViewProjects.GetChildAt(option));
            popup.Inflate(Resource.Menu.popup_menu_items);
            popup.MenuItemClick += Popup_MenuItemClick;
            popup.Show();
            
        }

        private void Popup_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            switch(e.Item.ItemId)
            {
                case Resource.Id.selectTeam:
                    int rownum = option;
                    DataTable data = new DataTable();
                    MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        MySqlCommand getProjects = new MySqlCommand("SELECT id_team FROM THTeams", conn);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(getProjects);
                        adapter.Fill(data);
                        foreach (DataRow row in data.Rows)
                        {
                            if (option == 0)
                            {
                                idTeamSelected = System.Convert.ToInt32(row["id_team"].ToString());
                                break;
                            }
                            else
                                option--;
                        }
                        conn.Close();
                    }
                    break;
                case Resource.Id.deleteTeam:
                    AlertDialog.Builder alertRegisterSucce = new AlertDialog.Builder(this.Activity);
                    alertRegisterSucce.SetMessage(idTeamSelected.ToString());
                    alertRegisterSucce.Show();
                    break;
                default: throw new NotImplementedException();
            }
            
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
                MySqlCommand getProjects = new MySqlCommand("SELECT TeamName,ProjectName FROM THTeams", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(getProjects);
                adapter.Fill(data);
                String numeProiect, numeEchipa;
                foreach (DataRow row in data.Rows)
                {
                    numeEchipa = row["TeamName"].ToString();
                    numeProiect = row["ProjectName"].ToString();
                    projectList.Add("Project :" + numeProiect + " - team " + numeEchipa);
                }
                conn.Close();
            }
            listAdapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleExpandableListItem1, projectList);
            listViewProjects.Adapter = listAdapter;
            listViewProjects.ItemClick += ListViewProjects_ItemClick;
            return view;
        }
    }
}