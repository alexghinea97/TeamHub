using System;
using System.Collections.Generic;
using System.Data;
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
        public static int idTeamSelected = -1 ;
        private int option;

        public void AfiseazaEchipe()
        {
            projectList = new List<string>();
            DataTable data = new DataTable();
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand getProjects = new MySqlCommand("SELECT TeamName,ProjectName FROM THTeams where id_manager = " + MainActivity.user_id + " ;", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(getProjects);
                adapter.Fill(data);
                String numeProiect, numeEchipa;
                foreach (DataRow row in data.Rows)
                {
                    numeEchipa = row["TeamName"].ToString();
                    numeProiect = row["ProjectName"].ToString();
                    projectList.Add("Project : " + numeProiect + " - Team : " + numeEchipa);
                }
                conn.Close();
            }
            listAdapter = new ArrayAdapter<string>(this.Context, Android.Resource.Layout.SimpleExpandableListItem1, projectList);
            listViewProjects.Adapter = listAdapter;
            listViewProjects.ItemClick += ListViewProjects_ItemClick;
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
                        MySqlCommand getProjects = new MySqlCommand("SELECT id_team FROM THTeams where id_manager = " + MainActivity.user_id + " ;", conn);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(getProjects);
                        adapter.Fill(data);
                        foreach (DataRow row in data.Rows)
                        {
                            if (rownum == 0)
                            {
                                idTeamSelected = System.Convert.ToInt32(row["id_team"].ToString());
                                break;
                            }
                            else
                                rownum--;
                        }
                        conn.Close();
                    }
                    break;
                case Resource.Id.deleteTeam:
                    int rownumDel = option;
                    DataTable dataDel = new DataTable();
                    MySqlConnection connDel = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
                    if (connDel.State == ConnectionState.Closed)
                    {
                        connDel.Open();
                        MySqlCommand getProjectid = new MySqlCommand("SELECT id_team FROM THTeams where id_manager = " + MainActivity.user_id + " ;", connDel);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(getProjectid);
                        adapter.Fill(dataDel);
                        foreach (DataRow row in dataDel.Rows)
                        {
                            if (rownumDel == 0)
                            {
                                idTeamSelected = System.Convert.ToInt32(row["id_team"].ToString());
                                break;
                            }
                            else
                                rownumDel--;
                        }
                        MySqlCommand delProject = new MySqlCommand("DELETE FROM THTeams WHERE id_team="+idTeamSelected+";", connDel);
                        MySqlCommand delProjectMemberOnTeam = new MySqlCommand("DELETE FROM THMembers_on_teams WHERE id_team=" + idTeamSelected + ";",connDel);
                        MySqlCommand delProjectTasks = new MySqlCommand("DELETE FROM THTasks WHERE id_team=" + idTeamSelected + ";", connDel);
                        delProject.ExecuteNonQuery();
                        delProjectMemberOnTeam.ExecuteNonQuery();
                        delProjectTasks.ExecuteNonQuery();
                        idTeamSelected = -1;
                        connDel.Close();
                    }
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
                MySqlCommand getProjects = new MySqlCommand("SELECT TeamName,ProjectName FROM THTeams where id_manager = " + MainActivity.user_id + " ;", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(getProjects);
                adapter.Fill(data);
                String numeProiect, numeEchipa;
                foreach (DataRow row in data.Rows)
                {
                    numeEchipa = row["TeamName"].ToString();
                    numeProiect = row["ProjectName"].ToString();
                    projectList.Add("Project : " + numeProiect + " - Team : " + numeEchipa);
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