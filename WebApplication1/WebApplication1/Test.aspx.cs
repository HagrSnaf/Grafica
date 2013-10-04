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
    public partial class Test : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["utilizator"] == null)
                Response.Redirect("Default.aspx");
            
                String id = Session["id"].ToString();

                string cmd1 = "Select * from item where id_continut=" + id + "";
                SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

                conn1.Open();
                SqlCommand continut = new SqlCommand(cmd1, conn1);
                SqlDataReader reader1 = continut.ExecuteReader();
                int k = 0;
                while (reader1.Read())
                {
                    RadioButtonList item = new RadioButtonList();
                    item.CssClass = "radios";
                    ListItem ita = new ListItem();
                    ListItem itb = new ListItem();
                    ListItem itc = new ListItem();
                    ListItem itd = new ListItem();
                    item.Items.Add(ita);
                    item.Items.Add(itb);
                    item.Items.Add(itc);
                    item.Items.Add(itd);
                    k++;
                    continut_test.Controls.Add(new LiteralControl("<br /><br />"));
                    continut_test.Controls.Add(new LiteralControl( "<span style=\"float:left;\"> "+k +" . </span>" + reader1[1].ToString()));
                    ita.Attributes.Add("class", "Redbutton");
                    itb.Attributes.Add("class", "Redbutton");
                    itc.Attributes.Add("class", "Redbutton");
                    itd.Attributes.Add("class", "Redbutton");

                    ita.Text = reader1[2].ToString();
                    itb.Text = reader1[3].ToString();
                    itc.Text = reader1[4].ToString();
                    itd.Text = reader1[5].ToString();
                    continut_test.Controls.Add(new LiteralControl("<br>"));
                    if (reader1[6].ToString().ToCharArray()[0] == 'a')
                    {
                        ita.Value = "corect";
                        itb.Value = "gresit";
                        itc.Value = "gresit";
                        itd.Value = "gresit";
                    }

                    if (reader1[6].ToString().ToCharArray()[0] == 'b')
                    {
                        itb.Value = "corect";
                        ita.Value = "gresit";
                        itd.Value = "gresit";
                        itc.Value = "gresit";
                    }

                    if (reader1[6].ToString().ToCharArray()[0] == 'c')
                    {
                        itc.Value = "corect";
                        itb.Value = "gresit";
                        ita.Value = "gresit";
                        itd.Value = "gresit";
                    }

                    if (reader1[6].ToString().ToCharArray()[0] == 'd')
                    {
                        itd.Value = "corect";
                        itb.Value = "gresit";
                        itc.Value = "gresit";
                        ita.Value = "gresit";
                    }
                    continut_test.Controls.Add(item);
                    continut_test.Controls.Add(new LiteralControl("<br> <br>"));
                }
                reader1.Close();
                conn1.Close();

                Button b = new Button();
                b.Attributes.Add("runat", "server");
                b.Text = "Gata!";
                b.Click += new EventHandler(corecteaza);
                
                b.Attributes.Add("align", "center");
                continut_test.Controls.Add(b);
        }

        protected void corecteaza(object sender, EventArgs e)
        {
            int corecte = 0;
            int gresite = 0;
            int total = 0;
            foreach (Control r in continut_test.Controls)
            {
                if (r.GetType() == typeof(RadioButtonList))
                {
                   
                    RadioButtonList item = (RadioButtonList)r;
                    if (item.SelectedValue == "corect")
                        corecte++;
                    else
                        gresite++;
                   
                }
            }

               
                total = corecte + gresite;

               float nivel = corecte * 10 / total;

              int id_utilizator=int.Parse(Session["id_user"].ToString());
              int id_cont=int.Parse(Session["id"].ToString());

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

                //deschiderea conexiunii
                conn.Open();

                string cmd = "Select count(id) from [test] where id_user=" + id_utilizator + " and tip='" + Session["tip"].ToString() + "' and id_continut=" + id_cont + "";

                SqlCommand exista = new SqlCommand(cmd, conn);

                  int raspuns = Convert.ToInt32(exista.ExecuteScalar().ToString());

                  if (raspuns == 0)
                  {

                      cmd = "Select max(id),count(id) from [test]";

                      //rularea comenzii cmd pe conexiunea conn
                      exista = new SqlCommand(cmd, conn);

                      SqlDataReader reader = exista.ExecuteReader();
                      reader.Read();
                      int idbd = 0;
                      if (int.Parse(reader[1].ToString()) != 0)
                          idbd = int.Parse(reader[0].ToString()) + 1;
                      else
                          idbd++;
                      reader.Close();

                      DateTime databd = new DateTime();
                      databd = Convert.ToDateTime(DateTime.Now);
                      string inscmd = "Insert into [test] (id,id_user,nota,data,tip,id_continut)"
                         + "values (@id,@id_user,@nota,@data,@tip,@id_continut)";
                      SqlCommand insertTestRez = new SqlCommand(inscmd, conn);
                      insertTestRez.Parameters.AddWithValue("@id", idbd + 1);

                      insertTestRez.Parameters.AddWithValue("@id_user", id_utilizator);
                      insertTestRez.Parameters.AddWithValue("@nota", nivel);
                      insertTestRez.Parameters.AddWithValue("@data", databd);
                      insertTestRez.Parameters.AddWithValue("@tip", Session["tip"].ToString());
                      insertTestRez.Parameters.AddWithValue("@id_continut", Session["id"]);

                      try
                      {
                          insertTestRez.ExecuteNonQuery();
                          conn.Close();
                          fa_nivelul();
                      }
                      catch (Exception err)
                      {
                          Response.Write("A avut loc o eroare la corectarea testului");

                      }
                  }

                  else
                  {
                      cmd = "Select id from [test] where id_user=" + id_utilizator + " and tip='" + Session["tip"].ToString() + "' and id_continut=" + id_cont + "";

                      exista = new SqlCommand(cmd, conn);
                     
                      raspuns = Convert.ToInt32(exista.ExecuteScalar().ToString());
                      DateTime databd = new DateTime();
                      databd = Convert.ToDateTime(DateTime.Now);
                      cmd = "Update [test] set nota =@niv , data=@dat where id = " + raspuns + "";
                      SqlCommand exista1 = new SqlCommand(cmd, conn);
                      exista1.Parameters.AddWithValue("@niv", nivel);
                      exista1.Parameters.AddWithValue("@dat", databd);
                      try
                      {
                          exista1.ExecuteNonQuery();
                          conn.Close();
                          fa_nivelul();
                      }
                      catch (Exception err)
                      {
                          Response.Write("A avut loc o eroare la corectarea testului");

                      }
                  }


                  Label1.Text = "Ai raspuns corect la " + corecte + "!";
                  //Response.Write("Ai raspuns corect la "+corecte+"!");

        }

        protected void fa_nivelul()
        {
                int id_utilizator=int.Parse(Session["id_user"].ToString());
                int id_cont = int.Parse(Session["id"].ToString());
                int id_cap = id_cont;
            
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

                //deschiderea conexiunii
                conn.Open();

                if (Session["tip"].ToString() == "lectie")
                { 
                    string cmd = "Select id_capitol from [lectie] where id=" + id_cont +"";
                    SqlCommand exista = new SqlCommand(cmd, conn);

                    int raspuns = Convert.ToInt32(exista.ExecuteScalar().ToString());

                    if (raspuns == null)
                    {
                        Response.Write("A avut loc o eroare la updatarea nivelului. Va rugam sa reincercati");
                    }
                    else
                    { 
                        id_cap=raspuns;
                    }
                }

                string cmd2 = "Select count(id) from [nivel] where id_user=" + id_utilizator + " and id_capitol=" + id_cap +"";
                SqlCommand exista2 = new SqlCommand(cmd2, conn);

                  int raspuns2 = Convert.ToInt32(exista2.ExecuteScalar().ToString());

                  if (raspuns2 == 0)
                  {

                      cmd2 = "Select max(id) from [nivel]";

                      //rularea comenzii cmd pe conexiunea conn
                      exista2 = new SqlCommand(cmd2, conn);
                      int idbd = Convert.ToInt32(exista2.ExecuteScalar().ToString());
                      double valoare = fa_media(id_utilizator, id_cap);

                      string inscmd = "Insert into [nivel] (id,id_capitol,id_user,valoare)"
                         + "values (@id,@id_capitol,@id_user,@valoare)";
                      SqlCommand insertTestRez = new SqlCommand(inscmd, conn);
                      insertTestRez.Parameters.AddWithValue("@id", idbd + 1);
                      insertTestRez.Parameters.AddWithValue("@id_capitol", id_cap);
                      insertTestRez.Parameters.AddWithValue("@id_user", id_utilizator);
                      insertTestRez.Parameters.AddWithValue("@valoare", valoare);
                      

                      try
                      {
                          insertTestRez.ExecuteNonQuery();
                          conn.Close();
                      }
                      catch (Exception err)
                      {
                          Response.Write("A avut loc o eroare la corectarea testului");

                      }
                  }
                  else
                  {
                      cmd2 = "Select id from [nivel] where id_capitol=" +id_cap +" and id_user="+id_utilizator+"";

                      //rularea comenzii cmd pe conexiunea conn
                      exista2 = new SqlCommand(cmd2, conn);
                      int idbd = Convert.ToInt32(exista2.ExecuteScalar().ToString());
                      double valoare = fa_media(id_utilizator, id_cap);

                      string inscmd = "Update [nivel] set valoare =@valoare  where id = " + idbd + "";
                      SqlCommand insertTestRez = new SqlCommand(inscmd, conn);
                      insertTestRez.Parameters.AddWithValue("@valoare", valoare);

                      try
                      {
                          insertTestRez.ExecuteNonQuery();
                          conn.Close();
                      }
                      catch (Exception err)
                      {
                          Response.Write("A avut loc o eroare la corectarea testului");

                      }
                  }
        }

        protected double fa_media(int id_utilizator, int id_capitol)
        {
            double medie = 0;

            SqlDataSource sds = new SqlDataSource();
            sds.ConnectionString = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();
            sds.SelectParameters.Add("id_capitol", TypeCode.Int32, id_capitol.ToString());

            sds.SelectCommand = "SELECT * FROM [lectie] WHERE [id_capitol] = @id_capitol";
            double suma=0;
            DataView dv = (DataView)sds.Select(DataSourceSelectArguments.Empty);
            int nr = dv.Count;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);
            conn.Open();
            if (dv.Count > 0)
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    string cmd = "Select nota from [test] where id_user=" + id_utilizator + " and tip='lectie' and id_continut =" + dv[i].Row[0].ToString() + " ";
                    SqlCommand exista = new SqlCommand(cmd, conn);
                    int raspuns;
                    if (exista.ExecuteScalar() != null)
                        raspuns = Convert.ToInt32(exista.ExecuteScalar().ToString());
                    else
                        raspuns = 0;
                    suma += raspuns;
                }
             }
            else
            {
                Response.Write("A avut loc o eroare la corectarea testului");
            }
            suma = (suma / nr)* 0.3;
            medie = suma;
            string cmd2 = "Select nota from [test] where id_user=" + id_utilizator + " and tip='capitol' and id_continut =" + id_capitol + " ";
            SqlCommand exista2 = new SqlCommand(cmd2, conn);
            int raspuns2;
            if (exista2.ExecuteScalar() != null)
                raspuns2 = Convert.ToInt32(exista2.ExecuteScalar().ToString());
            else
                raspuns2 = 0;

            medie += raspuns2 *0.7;
            return medie;
        }
    }
}