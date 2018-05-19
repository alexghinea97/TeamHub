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
    public class Fragment_members : Android.Support.V4.App.Fragment
    {
        private ListView listViewMembers;
        private List<String> memberList;
        private ArrayAdapter<String> listMembersAdapter;
        public static String returnedMemberName;
        public static bool valuesChanged = false;
        public static int idMemberSelected = -1;
        private int option;

        public override void OnCreate(Bundle savedInstanceState)
        {
            System.Diagnostics.Debug.WriteLine(returnedMemberName);
            if (valuesChanged)
            {
                listMembersAdapter.Add(returnedMemberName);
                listMembersAdapter.NotifyDataSetChanged();
                listViewMembers.ItemClick += ListViewMembers_ItemClick;
            }
            valuesChanged = false;
            base.OnCreate(savedInstanceState);
        }

        private void ListViewMembers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            option = e.Position;
            PopupMenu popup = new PopupMenu(this.Activity, listViewMembers.GetChildAt(option));
            popup.Inflate(Resource.Menu.popup_members);
            popup.MenuItemClick += Popup_MenuItemClick;
            popup.Show();
        }

        private void Popup_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            switch(e.Item.ItemId)
            {
                case Resource.Id.selectMember:
                    int rownum = option;
                    DataTable data = new DataTable();
                    MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        MySqlCommand getMember = new MySqlCommand("SELECT id_member FROM THMembers;", conn);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(getMember);
                        adapter.Fill(data);
                        foreach (DataRow row in data.Rows)
                        {
                            if (option == 0)
                            {
                                idMemberSelected = System.Convert.ToInt32(row["id_member"].ToString());
                                break;
                            }
                            else
                                option--;
                        }
                        conn.Close();
                    }
                    break;
                case Resource.Id.deleteMember:
                    AlertDialog.Builder alertMemberID = new AlertDialog.Builder(this.Activity);
                    alertMemberID.SetMessage(idMemberSelected.ToString());
                    alertMemberID.Show();
                    break;
                default: throw new NotImplementedException();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_members_layout, container, false);
            listViewMembers = view.FindViewById<ListView>(Resource.Id.listViewMembers);
            memberList = new List<string>();
            DataTable data = new DataTable();
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand getMembers = new MySqlCommand("SELECT Username FROM THMembers WHERE id_member IN (SELECT id_member FROM THMembers_on_teams WHERE id_team = " + Fragment_projects.idTeamSelected + " );", conn);
                //MySqlCommand getMembers = new MySqlCommand("SELECT Username FROM THMembers;", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(getMembers);
                adapter.Fill(data);
                String numeMember;
                foreach (DataRow row in data.Rows)
                {
                    numeMember = row["Username"].ToString();
                    memberList.Add(numeMember);
                }
                conn.Close();
            }
            listMembersAdapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleExpandableListItem1, memberList);
            listViewMembers.Adapter = listMembersAdapter;
            listViewMembers.ItemClick += ListViewMembers_ItemClick;
            return view;
        }
    }
}