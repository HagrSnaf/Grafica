using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;

namespace WebApplication1
{

    public class nivel_pe_cap
    {

        private float valoare;
        private int id_capitol;



        public nivel_pe_cap(int id, float val)
        {
            // TODO: Complete member initialization
            this.valoare = val;
            this.id_capitol = id;
            
        }


        public float get_nivel()
        {

            return valoare;
        }

        public int get_id()
        {
            return id_capitol;
        }


    }

    public class note
    {

        private float nota;
        private string tip;
        private int continut;
        private string nume_lec;
        private string nume_cap;


        public note(int continut, float nota, string tip,string nc,string nl)
        {
            // TODO: Complete member initialization
            this.continut = continut;
            this.nota = nota;
            this.tip = tip;
            this.nume_cap = nc;
            this.nume_lec = nl;
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

        public string get_numec()
        {
            return nume_cap;
        }

        public string get_numel()
        {
            return nume_lec;
        }


    }

    public class capitol
    {

        private string nume;
        private int id;



        public capitol(int id, string nume)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.nume = nume;
        }


        public int get_id()
        {
            return id;
        }

        public string get_nume()
        {
            return nume;
        }

    }

    public class lectie
    {

        private string nume;
        private int id;
        private int id_cap;


        public lectie(int id, string nume,int id_c)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.nume = nume;
            this.id_cap = id_c;
        }


        public int get_id()
        {
            return id;
        }

        public int get_idc()
        {
            return id_cap;
        }

        public string get_nume()
        {
            return nume;
        }

    }



    public partial class Profil : System.Web.UI.Page
    {
        List<nivel_pe_cap> lista_nivele;  

        private void get_nivel()
        {
            string id_user = Session["id_user"].ToString();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            string command = "Select valoare,id_capitol from [nivel] where id_user=" + id_user + "";
            SqlCommand nivele = new SqlCommand(command, conn);
            


            conn.Open();

            SqlDataReader reader2 = nivele.ExecuteReader();

            while (reader2.Read())
            {
                int id_cap = int.Parse(reader2[1].ToString());
                float valoare = float.Parse(reader2[0].ToString());

                nivel_pe_cap niv = new nivel_pe_cap(id_cap,valoare);

                lista_nivele.Add(niv);

            }
            conn.Close();

        
        }

        private void recomandare_azi()
        {
            string id_user = Session["id_user"].ToString();
            List<string> titluri = new List<string>(); ;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            conn.Open();
            for (int i = 0; i < lista_nivele.Count; i++)
            {
                string command = "Select nume from [capitol] where id=" + lista_nivele[i].get_id() + "";
                SqlCommand nume = new SqlCommand(command, conn);
                string s = nume.ExecuteScalar().ToString();

                titluri.Add(s);

            }
            conn.Close();
            bool ok = true;
            string recomandare = "";
            recomandari.Controls.Add(new LiteralControl("<br>"));
            recomandari.Controls.Add(new LiteralControl("<h1>RECOMANDARE AZI</h1>"));
            recomandari.Controls.Add(new LiteralControl("<br>"));
            for (int i = 0; i < lista_nivele.Count; i++)
            {
                if (lista_nivele[i].get_nivel() < 5 && ok==true)
                {
                    ok = false;
                    recomandare = " - Iti recomandam sa reparcurgi capitolul " + titluri[i] + ".";
                }
                else
                {
                    if (lista_nivele[i].get_nivel() < 10 && ok==true)
                    {
                        conn.Open();

                        List<lectie> lectii = new List<lectie>();
                        string cmd2 = "Select l.nume,l.id_capitol, t.nota, t.data from lectie l, test t where t.id_continut=l.id and tip='lectie' and l.id_capitol="
                            + lista_nivele[i].get_id() + " and t.id_user=" + Session["id_user"] + " order by l.nr_ordine, t.nota";
                        SqlCommand continut2 = new SqlCommand(cmd2, conn);
                        SqlDataReader reader2 = continut2.ExecuteReader();

                        while (reader2.Read() && ok==true)
                        {
                            string nume = reader2[0].ToString();
                            int nota = int.Parse(reader2[2].ToString());

                            if (nota < 9 && nota >= 0)
                            {
                                if (nota < 5)
                                {
                                    ok = false;
                                    recomandare = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Lectia " + nume + " de la capitolul " + titluri[i] + " trebuie recitita. <br />";
                                }
                                else
                                    if (nota < 7)
                                    {
                                        ok = false;
                                        recomandare = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp La lectia " + nume + " de la capitolul " + titluri[i] + " s-a obtinut nota " + nota + ". Aceasta ar mai trebui citita o data <br/>";
                                    }
                            }
                        }
                        reader2.Close();

                        cmd2 = "Select c.nume,c.id, t.nota, t.data from capitol c, test t where t.id_continut=c.id and tip='capitol' and c.id="
                            + lista_nivele[i].get_id() + " and t.id_user=" + Session["id_user"] + " order by t.nota";
                        continut2 = new SqlCommand(cmd2, conn);
                        reader2 = continut2.ExecuteReader();

                        while (reader2.Read() && ok==true)
                        {
                            string nume = reader2[0].ToString();
                            int nota = int.Parse(reader2[2].ToString());

                            if (nota < 9 && nota >= 0)
                            {
                                if (nota < 5)
                                {
                                    ok = false;
                                    recomandare = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Capitolul " + nume + " trebuie recitit deoarece nu s-a obtinut o nota de trecere la testul final. <br />";
                                }
                                else
                                    if (nota < 7)
                                    {
                                        ok = false;
                                        recomandare = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp La testul de la capitolul " + nume + " s-a obtinut nota " + nota + ". Acesta ar mai trebui citit o data <br/>";
                                    }
                            }
                        }
                        reader2.Close();

                        cmd2 = "Select nume from lectie where id not in (Select t.id_continut from lectie l, test t where t.id_continut=l.id and tip='lectie' and l.id_capitol="
                            + lista_nivele[i].get_id() + " and t.id_user=" + Session["id_user"] + ") and id_capitol= " + lista_nivele[i].get_id() + " order by nr_ordine";
                        continut2 = new SqlCommand(cmd2, conn);
                        reader2 = continut2.ExecuteReader();

                        while (reader2.Read())
                        {
                            string nume = reader2[0].ToString();
                            if (ok==true) { ok = false; recomandare = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Iti recomandam sa citesti lectia " + nume + " de la capitolul "+titluri[i]; }
                        }
                        reader2.Close();

                        conn.Close();
                    }    
                }

                
            }
            if (ok == true)
            {
                conn.Open();
                string cmd2 = "Select nume from capitol where id not in (Select id_capitol from nivel where id_user=" + Session["id_user"] + ") order by nr_ordine,id";
                SqlCommand continut2 = new SqlCommand(cmd2, conn);
                SqlDataReader reader2 = continut2.ExecuteReader();

                if (reader2.Read())
                {
                    string nume = reader2[0].ToString();
                    recomandare = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Iti recomandam sa incepi capitolul "+nume+" <br/>";
                }
                reader2.Close();
                conn.Close();
            }
            recomandari.Controls.Add(new LiteralControl("<p>"));
            recomandari.Controls.Add(new LiteralControl(recomandare));
            recomandari.Controls.Add(new LiteralControl("</p>"));
            recomandari.Controls.Add(new LiteralControl("<br>"));
            recomandari.Controls.Add(new LiteralControl("<br>"));
            recomandari.Controls.Add(new LiteralControl("<hr>"));
        }

        private void arata_nivel()
        {
            string id_user = Session["id_user"].ToString();
            List<string> titluri = new List<string>(); ; 
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            conn.Open();
            for (int i = 0; i < lista_nivele.Count ; i++)
            {
                string command = "Select nume from [capitol] where id=" + lista_nivele[i].get_id() + "";
                SqlCommand nume = new SqlCommand(command, conn);
                string s= nume.ExecuteScalar().ToString();
                
                titluri.Add(s);
               
            }
            conn.Close();
            string afisare = "";
            string recomandare = "";
            recomandari.Controls.Add(new LiteralControl("<br>"));
            recomandari.Controls.Add(new LiteralControl("<h1>RECOMANDARI PE CAPITOLE</h1>"));
            recomandari.Controls.Add(new LiteralControl("<br>"));
            for (int i = 0; i < lista_nivele.Count ; i++)
            {
                afisare += "Pe capitolul <b>" + titluri[i] + "</b> ai nota " + lista_nivele[i].get_nivel()+"";
                if (lista_nivele[i].get_nivel() < 5)
                {
                    recomandare += " - Iti recomandam sa mai parcurgi capitolul o data .";
                }
                else
                {
                    if (lista_nivele[i].get_nivel() < 10)
                    {
                        recomandare += " - Iti recomandam sa mai aprofundezi lectiile din acest capitol. <br />";
                        conn.Open();

                        List<lectie> lectii = new List<lectie>();
                        string cmd2 = "Select l.nume,l.id_capitol, t.nota, t.data from lectie l, test t where t.id_continut=l.id and tip='lectie' and l.id_capitol=" 
                            + lista_nivele[i].get_id() + " and t.id_user="+ Session["id_user"]+" order by l.nr_ordine, t.nota";
                        SqlCommand continut2 = new SqlCommand(cmd2, conn);
                        SqlDataReader reader2 = continut2.ExecuteReader();

                        while (reader2.Read())
                        {
                            string nume = reader2[0].ToString();
                            int nota = int.Parse(reader2[2].ToString());
                            
                            if(nota<9 && nota>=0)
                            {
                                if (nota<5)
                                    recomandare += "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Lectia " + nume + " trebuie recitita. <br />";
                                else
                                    if (nota <7)
                                        recomandare += "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp La lectia " + nume + " s-a obtinut nota " + nota + ". Aceasta ar mai trebui citita o data <br/>";
                                    else
                                        recomandare += "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Lectia " + nume + " ar trebui aprofundata </br>";
                            }
                        }
                        reader2.Close();

                        cmd2 = "Select c.nume,c.id, t.nota, t.data from capitol c, test t where t.id_continut=c.id and tip='capitol' and c.id=" 
                            + lista_nivele[i].get_id() + " and t.id_user=" + Session["id_user"] + " order by t.nota";
                        continut2 = new SqlCommand(cmd2, conn);
                        reader2 = continut2.ExecuteReader();

                        while (reader2.Read())
                        {
                            string nume = reader2[0].ToString();
                            int nota = int.Parse(reader2[2].ToString());

                            if (nota < 9 && nota >= 0)
                            {
                                if (nota < 5)
                                    recomandare += "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Capitolul " + nume + " trebuie recitit deoarece nu s-a obtinut o nota de trecere. <br />";
                                else
                                    if (nota < 7)
                                        recomandare += "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp La testul de la capitolul " + nume + " s-a obtinut nota " + nota + ". Acesta ar mai trebui citit o data <br/>";
                                    else
                                        recomandare += "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Capitolul " + nume + " ar trebui aprofundat </br>";
                            }
                        }
                        reader2.Close();

                        cmd2 = "Select nume from lectie where id not in (Select t.id_continut from lectie l, test t where t.id_continut=l.id and tip='lectie' and l.id_capitol="
                            + lista_nivele[i].get_id() + " and t.id_user=" + Session["id_user"] + ") and id_capitol= " + lista_nivele[i].get_id() + " order by nr_ordine";
                        continut2 = new SqlCommand(cmd2, conn);
                        reader2 = continut2.ExecuteReader();
                        bool ok = true; 

                        while (reader2.Read())
                        {
                            if (ok) { ok = false; recomandare += "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp De asemenea iti recomandam sa citesti urmatoarele lectii in aceasta ordine: "; }
                            string nume = reader2[0].ToString();
                            recomandare += " <b>"+nume+"</b>";
                           
                        }
                        reader2.Close();

                        conn.Close();
                    }
                    else
                        recomandare += " - Consideram ca stii tot ce trebuie.";
                }
                
                recomandari.Controls.Add(new LiteralControl("<p>"));
                recomandari.Controls.Add(new LiteralControl(afisare));
                recomandari.Controls.Add(new LiteralControl("</p>"));
                recomandari.Controls.Add(new LiteralControl("<p>"));
                recomandari.Controls.Add(new LiteralControl("</p>"));
                recomandari.Controls.Add(new LiteralControl("<p>"));
                recomandari.Controls.Add(new LiteralControl(recomandare));
                recomandari.Controls.Add(new LiteralControl("</p>"));
                recomandari.Controls.Add(new LiteralControl("<p>"));
                recomandari.Controls.Add(new LiteralControl("</p>"));

                afisare = "";
                recomandare = "";
            }
            recomandari.Controls.Add(new LiteralControl("<br>"));
            recomandari.Controls.Add(new LiteralControl("<br>"));
        }

        private void afiseaza_note()
        {
            string id_user = Session["id_user"].ToString();
            List<capitol> capitole = new List<capitol>();
            List<lectie> lectii = new List<lectie>();
            List<note> notele = new List<note>();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["userConnectionString"].ConnectionString);

            string command = "Select id,nume from [capitol] ";
            SqlCommand cap = new SqlCommand(command, conn);

            conn.Open();

            SqlDataReader reader2 = cap.ExecuteReader();

            while (reader2.Read())
            {
                int id_cap = int.Parse(reader2[0].ToString());
                string nume = reader2[1].ToString();

                capitol c = new capitol(id_cap, nume);

                capitole.Add(c);

            }

            conn.Close();
            conn.Open();
            string com = "Select id,nume,id_capitol from [lectie] ";
            SqlCommand lect = new SqlCommand(com, conn);
            SqlDataReader reader = lect.ExecuteReader();

            while (reader.Read())
            {
                int id = int.Parse(reader[0].ToString());
                int id_cap = int.Parse(reader[2].ToString());
                string nume = reader[1].ToString();

                lectie lec  = new lectie(id,nume,id_cap);

                lectii.Add(lec);

            }


            conn.Close();
            conn.Open();

            string comm = "Select nota,tip,id_continut from [test] where id_user=" + id_user + "";
            SqlCommand not = new SqlCommand(comm, conn);
            SqlDataReader reader1 = not.ExecuteReader();

            while (reader1.Read())
            {
                int nota = int.Parse(reader1[0].ToString());
                int id = int.Parse(reader1[2].ToString());
                string tip = reader1[1].ToString();
                string numel="",numec="";
                if (tip.StartsWith("lectie"))
                {
                    
                    for (int i = 0; i < lectii.Count; i++)
                    {
                        if (lectii[i].get_id() == id)
                        {
                            numel = lectii[i].get_nume();
                            numec = "";
                        }

                    }
                
                }
                else
                {
                    //Response.Write(tip);
                    for (int i = 0; i < capitole.Count; i++)
                    {
                        if (capitole[i].get_id() == id)
                        {
                            numec = capitole[i].get_nume();
                            numel = "";
                        }

                    }

                }


                note n = new note(id,nota,tip,numec,numel);

                notele.Add(n);

            }

            recomandari.Controls.Add(new LiteralControl("<hr>"));
            recomandari.Controls.Add(new LiteralControl("<br>"));
            recomandari.Controls.Add(new LiteralControl("<h1>NOTE SI INFORMATII</h1>"));
            recomandari.Controls.Add(new LiteralControl("<br>"));
            conn.Close();
            string afisare = "";
            for (int i = 0; i < notele.Count; i++)
            {
                if (notele[i].get_tip().StartsWith("lectie"))
                {
                        afisare = "La lectia <b>" + notele[i].get_numel() + "</b> ai nota " + notele[i].get_nota() + ".";
          
                }
                else
                {
                        afisare = "La capitolul <b>" + notele[i].get_numec() + "</b> nota pe testul final este " + notele[i].get_nota() + ".";
             
                }

                recomandari.Controls.Add(new LiteralControl("<p>"));
                recomandari.Controls.Add(new LiteralControl(afisare));
                recomandari.Controls.Add(new LiteralControl("</p>"));
            }

            recomandari.Controls.Add(new LiteralControl("<br/>"));

        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["utilizator"] == null)
                Response.Redirect("Default.aspx");

            lista_nivele = new List<nivel_pe_cap>();
            get_nivel();
            recomandare_azi();
            arata_nivel();
            afiseaza_note();
        }
    }
}