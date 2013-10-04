using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class LectiiAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
                Response.Redirect("Default.aspx");
        }
        protected void sterge_lectie(object sender, EventArgs e)
        {
            Button c = (Button)sender;
            int id = Convert.ToInt32(c.CommandArgument.ToString());

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            //deschiderea conexiunii
            conn.Open();
            string cmd = "DELETE FROM [lectie] WHERE id=@id";
            SqlCommand deletecmd = new SqlCommand(cmd, conn);
            deletecmd.Parameters.AddWithValue("@id", id);

            try
            {

                deletecmd.ExecuteNonQuery();
                lectii_grid.DataSourceID = "SqlDataSource1";
                lectii_grid.DataBind();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Delete Error:";
                msg += ex.Message;
                Response.Write(msg);

            }
            conn.Close();
        }
        protected void edit_lectie(object sender, EventArgs e)
        {
            Button c = (Button)sender;
            Session["id_lectie"] = Convert.ToInt32(c.CommandArgument.ToString());
            Response.Redirect("LectieEditadmin.aspx");
        }
        
    }
}