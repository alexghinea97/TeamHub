using System;
using System.Data;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace TeamHub.Fragments
{
    class Fragment_add_member : DialogFragment
    {
        private EditText etMemberEmail;
        private Button btnAddMember;
        private Button btnCancelAddMember;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Fragment_add_member_layout, container, false);
            etMemberEmail = view.FindViewById<EditText>(Resource.Id.txtEmailMember);
            btnAddMember = view.FindViewById<Button>(Resource.Id.btnAddMember);
            btnCancelAddMember = view.FindViewById<Button>(Resource.Id.btnCancelAddMember);

            btnAddMember.Click += BtnAddMember_Click;
            btnCancelAddMember.Click += BtnCancelAddMember_Click;
            return view;
        }

        private void BtnCancelAddMember_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        private void BtnAddMember_Click(object sender, EventArgs e)
        {
            if (Fragment_projects.idTeamSelected != -1)
            {
                MySqlConnection conn = new MySqlConnection("server=db4free.net;port=3307;database=teamhubunibuc;user id=teamhubunibuc;password=teamhubunibuc;charset=utf8");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand checkIdMember = new MySqlCommand("SELECT id_member FROM THMembers WHERE Username LIKE '" + etMemberEmail.Text + "' ;", conn);
                    System.Object returnedValue = checkIdMember.ExecuteScalar();
                    if (returnedValue != null)
                    {
                        int memberId = System.Convert.ToInt32(returnedValue);
                        int userID = MainActivity.user_id;

                        MySqlCommand checkCombination = new MySqlCommand("SELECT COUNT(*) FROM THMembers_on_teams WHERE id_member = " + memberId + " AND id_team = " + Fragment_projects.idTeamSelected + " ;", conn);
                        System.Object objNoCombinations = checkCombination.ExecuteScalar();
                        int noCombinations = System.Convert.ToInt32(objNoCombinations);

                        if (noCombinations == 0)
                        {
                            MySqlCommand checkCombination1 = new MySqlCommand("SELECT COUNT(*) FROM THMembers_with_managers WHERE id_member = " + memberId + " AND id_manager = " + userID + " ;", conn);
                            System.Object objNoCombinations1 = checkCombination1.ExecuteScalar();
                            int noCombinations1 = System.Convert.ToInt32(objNoCombinations1);
                            if (noCombinations1 == 0)
                            {
                                MySqlCommand addToMembers_with_managers = new MySqlCommand("INSERT INTO THMembers_with_managers(id_member, id_manager) values(@idMember,@idManager);", conn);
                                addToMembers_with_managers.Parameters.AddWithValue("@idMember", memberId);
                                addToMembers_with_managers.Parameters.AddWithValue("@idManager", userID);
                                addToMembers_with_managers.ExecuteNonQuery();
                            }

                            MySqlCommand addToMembers_on_teams = new MySqlCommand("INSERT INTO THMembers_on_teams(id_member, id_team) values(@idMember,@idTeam);", conn);
                            addToMembers_on_teams.Parameters.AddWithValue("@idMember", memberId);
                            addToMembers_on_teams.Parameters.AddWithValue("@idTeam", Fragment_projects.idTeamSelected);
                            addToMembers_on_teams.ExecuteNonQuery();

                            AlertDialog.Builder alertTaskCreateSucces = new AlertDialog.Builder(this.Activity);
                            alertTaskCreateSucces.SetMessage("You have added a member !");
                            alertTaskCreateSucces.Show();
                            this.Dismiss();
                        }
                        else
                        {
                            AlertDialog.Builder alertTaskCreateSucces = new AlertDialog.Builder(this.Activity);
                            alertTaskCreateSucces.SetMessage("You have already added this member !");
                            alertTaskCreateSucces.Show();
                            this.Dismiss();
                        }
                    }
                    else
                    {
                        AlertDialog.Builder alertTaskCreateSucces = new AlertDialog.Builder(this.Activity);
                        alertTaskCreateSucces.SetMessage("The member doesn't exist.");
                        alertTaskCreateSucces.Show();
                        this.Dismiss();
                    }
                    conn.Close();
                }
            }
            else
            {
                AlertDialog.Builder alertTaskCreateSucces = new AlertDialog.Builder(this.Activity);
                alertTaskCreateSucces.SetMessage("Select a team first!");
                alertTaskCreateSucces.Show();
                this.Dismiss();
            }
        }
    }
}