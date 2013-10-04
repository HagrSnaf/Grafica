using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;

namespace WebApplication1
{

    public class pereche {
       
        private float nota;
        private string tip;
        private int continut;
       


       public pereche(int continut, float nota, string tip)
       {
           // TODO: Complete member initialization
           this.continut = continut;
           this.nota = nota;
           this.tip = tip;
       }


       public float get_nota()
       {

           return nota;
       }

       public int get_id()
       {
           return continut;
       }

       public string get_tip()
       {
           return tip;
       }
    
    
    }



    public partial class View1 : System.Web.UI.Page
    {
        List<string> lista_capitol;
        List<string> lista_lectii;
        List<pereche> lista_l;
        List<pereche> lista_c;  

        private int are_test(string id, int tip)
        {

          
            if (tip == 1)
            {


                foreach (string idlectie in lista_lectii)
                {
                    if (id == idlectie) return 1;
                }


                return 0;

            }


            if (tip == 2)
            {

                foreach (string idcapitol in lista_capitol)
                {
                    if (id == idcapitol) return 1;
                }

                return 0;

            }
            return 0;

        }


        private void get_perechi()
        {
            string id_user = Session["id_user"].ToString();
           
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

           

            conn.Open();

            string command = "Select nota,tip,id_continut from [test] where id_user="+id_user+" and tip='capitol'";
            SqlCommand teste = new SqlCommand(command, conn);
            SqlDataReader reader2 = teste.ExecuteReader();

           

            while (reader2.Read())
            {
                int continut=int.Parse(reader2[2].ToString());
                float nota=float.Parse(reader2[0].ToString());
                String tip=reader2[1].ToString();

                pereche p = new pereche(continut,nota,tip);
               
                    lista_c.Add(p);
            
            }
            conn.Close();
        }


        private void get_perechi_l()
        {
            string id_user = Session["id_user"].ToString();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);



            conn.Open();

            string command = "Select nota,tip,id_continut from [test] where id_user=" + id_user +" and tip='lectie'" ;
            SqlCommand teste = new SqlCommand(command, conn);
            SqlDataReader reader2 = teste.ExecuteReader();



            while (reader2.Read())
            {
                int continut = int.Parse(reader2[2].ToString());
                float nota = float.Parse(reader2[0].ToString());
                String tip = reader2[1].ToString();

                pereche p = new pereche(continut, nota, tip);
                
                    lista_l.Add(p);
               
            }
            conn.Close();
        }





        private float are_nivel_l(int id)
        {
           foreach (pereche j in lista_l)
            {
                int id_continut =j.get_id();
                float nota = j.get_nota();
                string tip =j.get_tip();
               
                if (id_continut == id ) 
                {
                    
                    return nota;
                    
                }
            }

           
            return -1;
        }


        private float are_nivel_c(int id)
        {
            foreach (pereche j in lista_c)
            {
                int id_continut = j.get_id();
                float nota = j.get_nota();
                string tip = j.get_tip();

                if (id_continut == id)
                {

                    return nota;

                }
            }


            return -1;
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["utilizator"] == null)
                Response.Redirect("Default.aspx");

            lista_l = new List<pereche>();

            lista_c = new List<pereche>();
            

            get_perechi();
            get_perechi_l();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            conn.Open();

            string command = "Select id_continut from item where tip='lectie'";
            SqlCommand teste = new SqlCommand(command, conn);
            SqlDataReader reader2 = teste.ExecuteReader();

            lista_lectii = new List<string>();
            while (reader2.Read())
            {
                lista_lectii.Add(reader2[0].ToString());

            }

            reader2.Close();


            command = "Select id_continut from item where tip='capitol'";
            teste = new SqlCommand(command, conn);
            reader2 = teste.ExecuteReader();

            lista_capitol = new List<string>();
            while (reader2.Read())
            {
                lista_capitol.Add(reader2[0].ToString());

            }

            reader2.Close();

            string cmd = "Select * from capitol order by nr_ordine , id";
            SqlCommand capitole = new SqlCommand(cmd, conn);
            SqlDataReader reader = capitole.ExecuteReader();
            int k = 1;
            while (reader.Read())
            {
                string s = reader[0].ToString();
                int id=int.Parse(reader[0].ToString());
                float are_niv=are_nivel_c(id);
                

                string cmd1 = "Select * from lectie where id_capitol=" + s + "  order by nr_ordine,id";


                SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

                conn1.Open();

                SqlCommand lectii = new SqlCommand(cmd1, conn1);
                SqlDataReader reader1 = lectii.ExecuteReader();

                Panel titlu_capitol = new Panel();
                titlu_capitol.CssClass = "tit";
                titlu_capitol.Attributes.Add("runat", "server");

                LinkButton c = new LinkButton();
                c.Text = reader[1].ToString();
                c.ForeColor = Color.White;
                c.ID = "c+" + reader[0].ToString();
                c.BackColor = Color.Black;
                if (are_niv >= 0)
                {
                  
                    Color col = new Color();
                    col = Color.Crimson;

                    if (are_niv < 5)
                        c.BackColor = col;
                    col = Color.Gold;
                    if (are_niv >= 5 && are_niv < 8)
                        c.BackColor = col;
                    col = Color.ForestGreen;
                    if (are_niv >= 8)
                        c.BackColor = col;

                }

                titlu_capitol.Controls.Add(c);
                c.Click += new EventHandler(afiseaza_capitole);
               
                lista_cap.Controls.Add(titlu_capitol);
                Panel panou_lectii = new Panel();
                panou_lectii.CssClass = "lect";
                panou_lectii.ID = "lect" + k;
                lista_cap.Controls.Add(panou_lectii);


                int x = 0;
                while (reader1.Read())
                {
                    int id_lect=int.Parse(reader1[0].ToString());
                    float are_niv_l = are_nivel_l(id_lect);
                    LinkButton l = new LinkButton();
                    l.ID = "l+" + reader1[0].ToString();
                    l.ForeColor = Color.White;
                    l.BackColor = Color.Black;
                    l.CssClass = "ancora";

                    if (are_niv_l >= 0)
                    {
  
                        Color col = new Color();
                        col = Color.Crimson;

                        if (are_niv_l < 5)
                            l.BackColor = col;
                        col = Color.Gold;
                        if (are_niv_l >= 5 && are_niv_l < 8)
                            l.BackColor = col;
                        col = Color.ForestGreen;
                        if (are_niv_l >= 8)
                            l.BackColor = col;
                    }


                    l.Text = reader1[1].ToString();
                    l.Attributes.Add("runat", "server");
                    panou_lectii.Controls.Add(l);
                    panou_lectii.Controls.Add(new LiteralControl("<br>"));
                    l.Click += new EventHandler(afiseaza_lectii);

                    if (are_test(reader1[0].ToString(), 1) == 1)
                    {
                        
                        LinkButton test = new LinkButton();
                        test.Text = "Test!!!";
                        test.CssClass = "tes";
                        test.ID = "t+" + k + reader1[0];
                        test.Click+=new EventHandler(test_Click);
                        panou_lectii.Controls.Add(test);
                        panou_lectii.Controls.Add(new LiteralControl("<br>"));
 

                    }

                   

                }

                if (are_test(reader[0].ToString(), 2) == 1)
                {
                    panou_lectii.Controls.Add(new LiteralControl("<br>"));
                    LinkButton test = new LinkButton();
                    test.Text = "Test!!!";
                    test.CssClass = "tes_c";
                    test.ID = "tc+" + k + reader[0];
                    test.Click += new EventHandler(testc_Click);
                    panou_lectii.Controls.Add(test);
                    panou_lectii.Controls.Add(new LiteralControl("<br>"));




                } 

                k++;
                reader1.Close();
                conn1.Close();


            }
            reader.Close();
            conn.Close();
          
        }


        protected void test_Click(object sender, EventArgs e)
        {
            LinkButton l = (LinkButton)sender;
            String s = l.ID;
            string id = string.Join(string.Empty, s.Skip(3));
            Session["id"] = id;
            Session["tip"] = "lectie";
            Response.Redirect("Test.aspx");


        }

        protected void testc_Click(object sender, EventArgs e)
        {
            LinkButton l = (LinkButton)sender;
            String s = l.ID;
            string id = string.Join(string.Empty, s.Skip(4));
            Session["id"] = id;
            Session["tip"] = "capitol";
            Response.Redirect("Test.aspx");


        }

        protected void afiseaza_capitole(object sender, EventArgs e)
        {

            LinkButton l = (LinkButton)sender;
            String s = l.ID;
            string id = string.Join(string.Empty, s.Skip(2));
            string cmd1 = "Select * from capitol where id=" + id + "";
            
            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            conn1.Open();
            SqlCommand continut = new SqlCommand(cmd1, conn1);

            SqlDataReader reader1 = continut.ExecuteReader();

            reader1.Read();
            continut_lect.Controls.Add(new LiteralControl(reader1[2].ToString()));
            reader1.Close();

            string cmd2 = "Select count ( id ) from test where tip= 'capitol' and  id_continut=" + id + " and id_user="+Session["id_user"]+"";
            SqlCommand continut2 = new SqlCommand(cmd2, conn1);
            SqlDataReader reader2 = continut2.ExecuteReader();
            reader2.Read();
            int nr = Convert.ToInt16(reader2[0].ToString());
            reader2.Close();

            if (nr == 0)
            {
                string cmd = "Select max(id),count(id) from [test]";

                //rularea comenzii cmd pe conexiunea conn
                SqlCommand exista = new SqlCommand(cmd, conn1);

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
                SqlCommand insertTestRez = new SqlCommand(inscmd, conn1);
                insertTestRez.Parameters.AddWithValue("@id", idbd + 1);

                insertTestRez.Parameters.AddWithValue("@id_user", Session["id_user"]);
                insertTestRez.Parameters.AddWithValue("@nota", 10);
                insertTestRez.Parameters.AddWithValue("@data", databd);
                insertTestRez.Parameters.AddWithValue("@tip", "capitol");
                insertTestRez.Parameters.AddWithValue("@id_continut", id);

                try
                {
                    insertTestRez.ExecuteNonQuery();
                    Session["id"] = id;
                    Session["tip"] = "capitol";
                    fa_nivelul();
                    conn1.Close();
                }
                catch (Exception err)
                {
                    throw err;
                    Response.Write("A avut loc o eroare la afisarea capitolului");

                }
            }
            conn1.Close();

        }

        protected void afiseaza_lectii(object sender, EventArgs e)
        {

            LinkButton l = (LinkButton)sender;
            String s = l.ID;
            string id = string.Join(string.Empty, s.Skip(2));
            string cmd1 = "Select * from lectie where id=" + id + "";
            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            conn1.Open();
            SqlCommand continut = new SqlCommand(cmd1, conn1);
            SqlDataReader reader1 = continut.ExecuteReader();
            reader1.Read();
            continut_lect.Controls.Add(new LiteralControl(reader1[3].ToString()));
            reader1.Close();

            string cmd2 = "Select count ( id ) from test where tip= 'lectie' and  id_continut=" + id + " and id_user="+Session["id_user"]+"";
            SqlCommand continut2 = new SqlCommand(cmd2, conn1);
            SqlDataReader reader2 = continut2.ExecuteReader();
            reader2.Read();
            int nr = Convert.ToInt16(reader2[0].ToString());
            reader2.Close();
            
            if (nr == 0)
            {
                string cmd = "Select max(id),count(id) from [test]";

                //rularea comenzii cmd pe conexiunea conn
                SqlCommand exista = new SqlCommand(cmd, conn1);

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
                SqlCommand insertTestRez = new SqlCommand(inscmd, conn1);
                insertTestRez.Parameters.AddWithValue("@id", idbd + 1);

                insertTestRez.Parameters.AddWithValue("@id_user", Session["id_user"]);
                insertTestRez.Parameters.AddWithValue("@nota", 10);
                insertTestRez.Parameters.AddWithValue("@data", databd);
                insertTestRez.Parameters.AddWithValue("@tip", "lectie");
                insertTestRez.Parameters.AddWithValue("@id_continut", id);

                try
                {
                    insertTestRez.ExecuteNonQuery();
                    Session["id"] = id;
                    Session["tip"] = "lectie";
                    fa_nivelul();
                    conn1.Close();
                   
                }
                catch (Exception err)
                {
                    Response.Write("A avut loc o eroare la afisarea lectiei");

                }
            }

            conn1.Close();
        }


        protected void afiseaza_test(object sender, EventArgs e)
        {
           
           

        }


        protected void fa_nivelul()
        {
            int id_utilizator = int.Parse(Session["id_user"].ToString());
            int id_cont = int.Parse(Session["id"].ToString());
            int id_cap = id_cont;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            //deschiderea conexiunii
            conn.Open();

            if (Session["tip"].ToString() == "lectie")
            {
                string cmd = "Select id_capitol from [lectie] where id=" + id_cont + "";
                SqlCommand exista = new SqlCommand(cmd, conn);

                int raspuns = Convert.ToInt32(exista.ExecuteScalar().ToString());

                if (raspuns == null)
                {
                    Response.Write("A avut loc o eroare la updatarea nivelului. Va rugam sa reincercati");
                }
                else
                {
                    id_cap = raspuns;
                }
            }

            string cmd2 = "Select count(id) from [nivel] where id_user=" + id_utilizator + " and id_capitol=" + id_cap + "";
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
                cmd2 = "Select id from [nivel] where id_capitol=" + id_cap + " and id_user=" + id_utilizator + "";

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
            double suma = 0;
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
            suma = (suma / nr) * 0.3;
            medie = suma;
            string cmd2 = "Select nota from [test] where id_user=" + id_utilizator + " and tip='capitol' and id_continut =" + id_capitol + " ";
            SqlCommand exista2 = new SqlCommand(cmd2, conn);
            int raspuns2;
            if (exista2.ExecuteScalar() != null)
                raspuns2 = Convert.ToInt32(exista2.ExecuteScalar().ToString());
            else
                raspuns2 = 0;

            medie += raspuns2 * 0.7;
            return medie;
        }


    }
}