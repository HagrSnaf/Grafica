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
    public partial class ItemAddadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
                Response.Redirect("Default.aspx");
        }

        protected void tip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tip.SelectedValue == "Lectie") {
                iduri.DataSourceID = "SqlDataSource2";
            }
            else {
                iduri.DataSourceID = "SqlDataSource1";
            }
        }

        protected void salveaza_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            conn.Open();
            int idbd = 0;
            string cmds = "Select max(id),count(id) from [item]";
            SqlCommand exista = new SqlCommand(cmds, conn);
            SqlDataReader reader = exista.ExecuteReader();
            reader.Read();
            if (int.Parse(reader[1].ToString()) != 0)
                idbd = int.Parse(reader[0].ToString()) + 1;
            else
                idbd++;
            reader.Close();

            string tipul;
            int id_continut;

            if (tip.SelectedValue == "Lectie") {
                string cmds2 = "Select id from lectie WHERE nume ='" + iduri.Text + "'";
                SqlCommand exista2 = new SqlCommand(cmds2, conn);
                id_continut = Convert.ToInt32(exista2.ExecuteScalar().ToString());

                tipul = "lectie";
            }
            else {
                string cmds2 = "Select id from capitol WHERE nume ='" + iduri.Text + "'";
                SqlCommand exista2 = new SqlCommand(cmds2, conn);
                id_continut = Convert.ToInt32(exista2.ExecuteScalar().ToString());

                tipul = "capitol";
            }
            

            string sql = "Insert into [item] (id,enunt,a,b,c,d,raspuns,tip,id_continut)"
                    + "values (@id,@enunt,@a,@b,@c,@d,@raspuns,@tip,@id_continut)";
            SqlCommand insertUser = new SqlCommand(sql, conn);
            insertUser.Parameters.AddWithValue("@id", idbd + 1);
            insertUser.Parameters.AddWithValue("@enunt", enunt.Text);
            insertUser.Parameters.AddWithValue("@a", var_a.Text);
            insertUser.Parameters.AddWithValue("@b", var_b.Text);
            insertUser.Parameters.AddWithValue("@c", var_c.Text);
            insertUser.Parameters.AddWithValue("@d", var_d.Text);
            insertUser.Parameters.AddWithValue("@raspuns", raspuns.Text);
            insertUser.Parameters.AddWithValue("@tip", tipul);
            insertUser.Parameters.AddWithValue("@id_continut", id_continut);

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
                Response.Redirect("Itemadmin.aspx");
            }

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                string FilePath = "uploads/test/enunt/" + FileName;
                FileUpload1.SaveAs(Server.MapPath(FilePath));
                enunt.Text += string.Format("<img src = '{0}' alt = '{1}' />", FilePath, FileName);
            }
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Lectiiadmin.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload2.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
                string FilePath = "uploads/test/a/" + FileName;
                FileUpload2.SaveAs(Server.MapPath(FilePath));
                var_a.Text += string.Format("<img src = '{0}' alt = '{1}' />", FilePath, FileName);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (FileUpload3.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload3.PostedFile.FileName);
                string FilePath = "uploads/test/b/" + FileName;
                FileUpload3.SaveAs(Server.MapPath(FilePath));
                var_b.Text += string.Format("<img src = '{0}' alt = '{1}' />", FilePath, FileName);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (FileUpload4.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload4.PostedFile.FileName);
                string FilePath = "uploads/test/c/" + FileName;
                FileUpload4.SaveAs(Server.MapPath(FilePath));
                var_c.Text += string.Format("<img src = '{0}' alt = '{1}' />", FilePath, FileName);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (FileUpload5.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload5.PostedFile.FileName);
                string FilePath = "uploads/test/d/" + FileName;
                FileUpload5.SaveAs(Server.MapPath(FilePath));
                var_d.Text += string.Format("<img src = '{0}' alt = '{1}' />", FilePath, FileName);
            }
        }

    }
}