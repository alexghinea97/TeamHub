using System;
using System.Data;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace TeamHub.Fragments
{
    class Fragment_create_project : DialogFragment
    {
        private EditText etTeamName;
        private EditText etProjectName;
        private Button btnAddTeamAndProject;
        private Button btnCancelAddTeam;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Fragment_create_project_layout, container, false);
            etTeamName = view.FindViewById<EditText>(Resource.Id.txtTeamName);
            etProjectName = view.FindViewById<EditText>(Resource.Id.txtProjectName);
            btnAddTeamAndProject = view.FindViewById<Button>(Resource.Id.btnAddTeam);
            btnCancelAddTeam = view.FindViewById<Button>(Resource.Id.btnCancelAddTeam);

            btnAddTeamAndProject.Click += BtnAddTeamAndProject_Click;
            btnCancelAddTeam.Click += BtnCancelAddTeam_Click;
            return view;
        }

        private void BtnCancelAddTeam_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        private void BtnAddTeamAndProject_Click(object sender, EventArgs e)
        {
            InsertInfo(etTeamName.Text, etProjectName.Text);
        }

        private void InsertInfo(string numeEchipa, string numeProiect)
        {
            MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand checkTeam = new MySqlCommand("SELECT COUNT(*) FROM THTeams WHERE TeamName LIKE '" + numeEchipa + "' AND ProjectName LIKE '" + numeProiect + "' ", conn);
                System.Object returnedValue = checkTeam.ExecuteScalar();
                if (returnedValue != null)
                {
                    int nr = System.Convert.ToInt32(returnedValue);
                    if (nr ==0)
                    {
                        MySqlCommand insertData = new MySqlCommand("insert into THTeams(TeamName,ProjectName,id_manager) values(@teamName,@projectName,@idmanager);", conn);
                        insertData.Parameters.AddWithValue("@teamName", numeEchipa);
                        insertData.Parameters.AddWithValue("@projectName", numeProiect);
                        insertData.Parameters.AddWithValue("@idmanager", MainActivity.user_id);
                        insertData.ExecuteNonQuery();

                        MySqlCommand getTeamId = new MySqlCommand("SELECT id_team FROM THTeams WHERE TeamName LIKE '" + numeEchipa + "' ;", conn);
                        System.Object checkTeamId = getTeamId.ExecuteScalar();
                        if (checkTeamId != null)
                        {
                            int TeamId = System.Convert.ToInt32(checkTeamId);
                            MySqlCommand updateMember = new MySqlCommand("UPDATE THMembers SET id_team = " + TeamId + " WHERE id_member = " + MainActivity.user_id +" ;", conn);
                            updateMember.ExecuteNonQuery();
                        }
                        AlertDialog.Builder alertRegisterSucces = new AlertDialog.Builder(this.Activity);
                        alertRegisterSucces.SetMessage("You have added a Team and a Project successfully !");
                        alertRegisterSucces.Show();
                        Fragment_projects.returnedTeamName = numeEchipa;
                        Fragment_projects.returnedProjectName = numeProiect;
                        Fragment_projects.valuesChanged = true;
                        this.Dismiss();
                    }
                    else
                    {
                        AlertDialog.Builder alertAddTeamFail = new AlertDialog.Builder(this.Activity);
                        alertAddTeamFail.SetMessage("The team name you have entered already exists !");
                        alertAddTeamFail.Show();
                    }
                }
                conn.Close();
            }
        }
    }
}