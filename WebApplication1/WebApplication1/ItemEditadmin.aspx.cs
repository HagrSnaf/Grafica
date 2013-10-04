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
    public partial class ItemEditadmin : System.Web.UI.Page
    {
        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
                Response.Redirect("Default.aspx");
            if (Session["id_item"] == null)
                Response.Redirect("Itemadmin.aspx");
            //incarc din baza de date in cele 2 textbox-uri

            Int32 id = Convert.ToInt32(Session["id_item"]);

            SqlDataSource sds = new SqlDataSource();
            sds.ConnectionString = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();
            sds.SelectParameters.Add("id", TypeCode.Int32, id.ToString());

            sds.SelectCommand = "SELECT * FROM [item] WHERE [id] = @id";

            DataView dv = (DataView)sds.Select(DataSourceSelectArguments.Empty);
            if (dv.Count > 0)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);
                conn.Open();
                string cmds2 = "Select nume from " + dv[0].Row[7].ToString() +" WHERE id ='" + Convert.ToInt16(dv[0].Row[8].ToString()) + "'";
                SqlCommand exista2 = new SqlCommand(cmds2, conn);
                string nume = exista2.ExecuteScalar().ToString();

                if (!Page.IsPostBack)
                {
                    tip.SelectedValue = UppercaseFirst(dv[0].Row[7].ToString().Replace(" ",""));
                    if (dv[0].Row[7].ToString().Replace(" ","")=="lectie")
                    {
                        iduri.DataSourceID = "SqlDataSource2";
                    }
                    else
                    {
                        iduri.DataSourceID = "SqlDataSource1";
                    }

                    iduri.DataBind();
                    iduri.SelectedValue = nume;
                    enunt.Text = dv[0].Row[1].ToString();
                    var_a.Text = dv[0].Row[2].ToString();
                    var_b.Text = dv[0].Row[3].ToString();
                    var_c.Text = dv[0].Row[4].ToString();
                    var_d.Text = dv[0].Row[5].ToString();
                    raspuns.SelectedValue = dv[0].Row[6].ToString().Replace(" ","");
                }
            }
            else
            {
                Response.Redirect("Itemadmin.aspx");
            }
        }

        protected void salveaza_Click(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(Session["id_item"]);
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            conn.Open();
            string cmds2 = "Select id from " + tip.Text.ToLower() + " WHERE nume ='" + iduri.Text + "'";
            SqlCommand exista2 = new SqlCommand(cmds2, conn);
            int id_continut = Convert.ToInt32(exista2.ExecuteScalar().ToString());


            string sql = "UPDATE [item] "
                        + "SET [enunt] = @enunt , [a] = @a  , [b] = @b  , [c] = @c , [d] = @d, [raspuns] = @raspuns, [tip] = @tip , [id_continut] = @id_continut " +
                        "WHERE [id] = @id";

            SqlCommand insertUser = new SqlCommand(sql, conn);
            insertUser.Parameters.AddWithValue("@id", id);
            insertUser.Parameters.AddWithValue("@enunt", enunt.Text);
            insertUser.Parameters.AddWithValue("@a", var_a.Text);
            insertUser.Parameters.AddWithValue("@b", var_b.Text);
            insertUser.Parameters.AddWithValue("@c", var_c.Text);
            insertUser.Parameters.AddWithValue("@d", var_d.Text);
            insertUser.Parameters.AddWithValue("@raspuns", raspuns.Text);
            insertUser.Parameters.AddWithValue("@tip", tip.Text.ToLower());
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

        protected void cancel_Click(object sender, EventArgs e)
        {
            Session["id_item"] = null;
            Response.Redirect("Itemadmin.aspx");
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

        protected void tip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tip.SelectedValue == "Lectie") {
                iduri.DataSourceID = "SqlDataSource2";
            }
            else {
                iduri.DataSourceID = "SqlDataSource1";
            }
        }

    }
}