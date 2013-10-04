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
    public partial class Usersadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
                Response.Redirect("Default.aspx");
        }


        protected void sterge_utilizator(object sender, EventArgs e)
        {
            Button c=(Button)sender;
            int id = Convert.ToInt32(c.CommandArgument.ToString());

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            //deschiderea conexiunii
            conn.Open();
            string cmd = "DELETE FROM [user] WHERE id=@id";
            SqlCommand deletecmd = new SqlCommand(cmd, conn);
            deletecmd.Parameters.AddWithValue("@id", id);

            try
            {
                
                deletecmd.ExecuteNonQuery();
                utilizatori_grid.DataSourceID = "SqlDataSourceUsers";
                utilizatori_grid.DataBind();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Delete Error:";
                msg += ex.Message;
                Response.Write(msg);

            }

            conn.Close();


        }


    }



}