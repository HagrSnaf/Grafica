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
    public partial class LectieEditadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
                Response.Redirect("Default.aspx");
            if (Session["id_lectie"] == null)
                Response.Redirect("Lectiiadmin.aspx");

            Int32 id = Convert.ToInt32(Session["id_lectie"]);


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);
            conn.Open();

            SqlDataSource sds = new SqlDataSource();
            sds.ConnectionString = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();
            sds.SelectParameters.Add("id", TypeCode.Int32, id.ToString());

            sds.SelectCommand = "SELECT * FROM [lectie] WHERE [id] = @id";

            DataView dv = (DataView)sds.Select(DataSourceSelectArguments.Empty);
            if (dv.Count > 0)
            {
                string cmds2 = "Select nume from capitol WHERE id ='" + Convert.ToInt16( dv[0].Row[2].ToString()) + "'";
                SqlCommand exista2 = new SqlCommand(cmds2, conn);
                string capitol_nume = exista2.ExecuteScalar().ToString();

                if (!Page.IsPostBack) nume.Text = dv[0].Row[1].ToString().Replace("  ", "");
                if (!Page.IsPostBack) descriere.Text = dv[0].Row[3].ToString();
                if (!Page.IsPostBack) nr_ord.Text = dv[0].Row[5].ToString();
                if (!Page.IsPostBack) capitol.SelectedValue = capitol_nume;
            }
            else
            {
                Response.Redirect("Lectiiadmin.aspx");
            }
        }

        protected void salveaza_Click(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(Session["id_lectie"]);
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            conn.Open();
            string cmds2 = "Select id from capitol WHERE nume ='" + capitol.Text + "'";
            SqlCommand exista2 = new SqlCommand(cmds2, conn);
            int id_capitol = Convert.ToInt32(exista2.ExecuteScalar().ToString());


            string sql = "UPDATE [lectie] "
                        + "SET [descriere] = @descriere, [nume] = @nume  , [id_capitol] = @id_capitol , [nr_ordine] = @nr_ordine " +
                        "WHERE [id] = @id";

            SqlCommand insertUser = new SqlCommand(sql, conn);
            insertUser.Parameters.AddWithValue("@id", id);
            insertUser.Parameters.AddWithValue("@nume", nume.Text);
            insertUser.Parameters.AddWithValue("@descriere", descriere.Text);
            insertUser.Parameters.AddWithValue("@id_capitol", id_capitol);
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
                Response.Redirect("Lectiiadmin.aspx");
            }

        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Session["id_lectie"] = null;
            Response.Redirect("Lectiiadmin.aspx");
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                string FilePath = "uploads/lectie/" + FileName;
                FileUpload1.SaveAs(Server.MapPath(FilePath));
                descriere.Text += string.Format("<img src = '{0}' alt = '{1}' />", FilePath, FileName);
            }
        }


        public string ValidateRequest { get; set; }
    }
}