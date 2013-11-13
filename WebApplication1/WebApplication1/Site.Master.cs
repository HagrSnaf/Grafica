using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace WebApplication1
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["utilizator"]!=null)
            {
                nelogat.Visible = false;
                bunaziua.Text = "Bine ai venit, " + Session["utilizator"].ToString();
                logat.Visible = true;
            }

            for (int day = 1; day < 32; day++)
            {
                ListItem li = new ListItem();
                li.Text = day.ToString();
                if (day <= 9)
                li.Value = "0"+day.ToString();
                else
                    li.Value = day.ToString();
                zi.Items.Add(li);
            }
            // populate the days drop down
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            for (int month = 1; month < 13; month++)
            {
                ListItem li = new ListItem();
                li.Text = dtfi.GetMonthName(month);
                if(month<=9)
                li.Value ="0"+month.ToString();
                else
                    li.Value =  month.ToString(); 
                luna.Items.Add(li);
            }
            // populate the years drop down
            int thisYear = System.DateTime.Now.Year;
            int startYear = thisYear - 14;
            for (int year = startYear; year > startYear - 100; year--)
            {
                ListItem li = new ListItem();
                li.Text = year.ToString();
                li.Value = year.ToString();
                an.Items.Add(li);
            }
        }
        protected void Verifica_date(object sender, EventArgs e)
        {
            
            String username = login_un.Text;
            String password = login_pass.Text;
            //conectarea la baza de date
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            //deschiderea conexiunii
            conn.Open();
            string cmd = "Select count(*) from [user] where username='" + username + "' and parola='"+password+"'";


            string cmdadmin = "Select count(*) from [user] where username='" + username + "' and parola='" + password + "' and admin is not null";


            //rularea comenzii cmd pe conexiunea conn
            SqlCommand exista = new SqlCommand(cmd, conn);

            SqlCommand este_admin = new SqlCommand(cmdadmin, conn);


            //obtinerea continutului primei linii de pe prima coloana a rezultatului rularii comenzii cmd
            int raspuns = Convert.ToInt32(exista.ExecuteScalar().ToString());

           int rasp_admin = Convert.ToInt32(este_admin.ExecuteScalar().ToString()); 

           

            if (raspuns == 1 )
                if(rasp_admin == 0)
               {
                Session["utilizator"] = username;
                cmd = "Select id from [user] where username='" + username + "' and parola='" + password + "'";
                exista = new SqlCommand(cmd, conn);
                raspuns = Convert.ToInt32(exista.ExecuteScalar().ToString());
                Session["id_user"] = raspuns;
                Response.Redirect("Galerie.aspx");
               }
            
                else
               {
                Session["admin"] = username;
                Response.Redirect("Homeadmin.aspx");
               }

            conn.Close();
        }

        protected void Inregistrare(object sender, EventArgs e)
        {
            String numebd = nume.Text;
            String prenumebd = prenume.Text;
            String utilizatorbd = utilizator.Text;
            String parolabd = parola.Text;
            String zibd = zi.SelectedValue;
            String lunabd = luna.SelectedValue;
            String anbd = an.SelectedValue;
            String emailbd = e_mail.Text;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            //deschiderea conexiunii
            conn.Open();
            string cmd = "Select max(id) from [user]";

            //rularea comenzii cmd pe conexiunea conn
            SqlCommand exista = new SqlCommand(cmd, conn);
             int id_util = Convert.ToInt32(exista.ExecuteScalar().ToString());

             cmd = "Select count(id) from [user] where username='"+utilizatorbd+"'";

             //rularea comenzii cmd pe conexiunea conn
             exista = new SqlCommand(cmd, conn);
             int idbd = Convert.ToInt32(exista.ExecuteScalar().ToString());
             if (idbd == 0)
             {
                 DateTime databd = new DateTime();
                 databd = Convert.ToDateTime(zibd + " " + lunabd + " " + anbd);
                 string inscmd = "Insert into [user] (id,nume,prenume,username,parola,data_nastere,email)"
                    + "values (@id,@nume,@prenume,@username,@parola,@data,@mail)";
                 SqlCommand insertUser = new SqlCommand(inscmd, conn);
                 insertUser.Parameters.AddWithValue("@id", id_util + 1);
                 insertUser.Parameters.AddWithValue("@nume", numebd);
                 insertUser.Parameters.AddWithValue("@prenume", prenumebd);
                 insertUser.Parameters.AddWithValue("@username", utilizatorbd);
                 insertUser.Parameters.AddWithValue("@parola", parolabd);
                 insertUser.Parameters.AddWithValue("@data", databd);
                 insertUser.Parameters.AddWithValue("@mail", emailbd);

                 try
                 {
                     insertUser.ExecuteNonQuery();

                     Session["utilizator"] = utilizatorbd;
                     nume.Text = "";
                     prenume.Text = "";
                     zi.SelectedValue = "*";
                     luna.SelectedValue = "*";
                     an.SelectedValue = "*";
                     utilizator.Text = "";
                     e_mail.Text = "";

                     cmd = "Select id from [user] where username='" + utilizatorbd + "' and parola='" + parolabd + "'";

                     //rularea comenzii cmd pe conexiunea conn
                     exista = new SqlCommand(cmd, conn);
                     idbd = Convert.ToInt32(exista.ExecuteScalar().ToString());
                     Session["id_user"] = idbd;


                     conn.Close();
                     Response.Redirect("Galerie.aspx");

                 }
                 catch (Exception err)
                 {
                     Response.Write(err.Message);

                 }

             }
             else
             {
                 msg.Visible = true;
                 msg.Text = "Numele de utilizator este deja folosit";
             }

               




             
        }

        protected void aplicatie_Click(object sender, EventArgs e)
        {
            Response.Redirect("View.aspx");
        }
        protected void logout_Click(object sender, EventArgs e)
        {   Session.Clear();
            Response.Redirect("Default.aspx");
        }

    }
}
