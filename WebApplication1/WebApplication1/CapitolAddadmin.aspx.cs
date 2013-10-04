using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WebApplication1
{
    public partial class CapitolAddadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
                Response.Redirect("Default.aspx");
        }

        protected void salveaza_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            //deschiderea conexiunii
            conn.Open();
            int idbd=0;
            
                string cmds = "Select max(id),count(id) from [capitol]";
                SqlCommand exista = new SqlCommand(cmds, conn);
                SqlDataReader reader = exista.ExecuteReader();
                reader.Read();
                if (int.Parse(reader[1].ToString()) != 0)
                    idbd = int.Parse(reader[0].ToString())+1;
                else
                    idbd++;
                reader.Close();

                string sql = "Insert into [capitol] (id,nume,descriere,nr_ordine)"
                    + "values (@id,@nume,@descriere,@nr_ordine)";
            SqlCommand insertUser = new SqlCommand(sql, conn);
            insertUser.Parameters.AddWithValue("@id", idbd + 1);
            insertUser.Parameters.AddWithValue("@nume", nume.Text);
            insertUser.Parameters.AddWithValue("@descriere", descriere.Text);
            insertUser.Parameters.AddWithValue("@nr_ordine", nr_ord.Text);

            try
            {
                insertUser.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex_msg)
            {
                string msg = "Error occured while updating";
                msg += ex_msg.Message;
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
                Response.Redirect("Capitoadmin.aspx");
            }

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                string FilePath = "uploads/capitol/" + FileName;
                FileUpload1.SaveAs(Server.MapPath(FilePath));
                descriere.Text += string.Format("<img src = '{0}' alt = '{1}' />", FilePath, FileName);
            }
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Capitoadmin.aspx");
        }

    }
}