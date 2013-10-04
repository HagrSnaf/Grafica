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
    public partial class View : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["utilizator"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            bunaziua.Text = "Bine ai venit, " + Session["utilizator"];
        }

        protected void delogare(object sender, EventArgs e)
        {
            Session["utilizator"] = null;
            Response.Redirect("Default.aspx");
        }


        protected void trimite_la_lectii(object sender, EventArgs e)
        {
            Response.Redirect("View.aspx");
        }

        protected void trimite_la_profil(object sender, EventArgs e)
        {
            Response.Redirect("Profil.aspx");
        }



    }
}