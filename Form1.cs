using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace felhasznalok_Komaromi_Laura_2022_04_06
{
    public partial class Form1 : Form
    {
        class User
        {
            private string nev;
            private string user;
            private string jogosultsag;
            private string jelszo;
            private int telszam;

            public User(string nev, string user, string jogosultsag, string jelszo, int telszam)
            {
                setNev(nev);
                setUser(user);
                setJogosultsag(jogosultsag);
                setJelszo(jelszo);
                setTelszam(telszam);
            }

            public User(string sor)
            {
                string[] tomb = sor.Split(';');
                setNev(tomb[0]);
                setUser(tomb[1]);
                setJogosultsag(tomb[2]);
                setJelszo(tomb[3]);
                setTelszam(Convert.ToInt32(tomb[4]));
            }

            public void setNev(string nev)
            {
                if (nev != "")
                {
                    this.nev = nev;
                }
            }

            public void setUser(string user)
            {
                if (user != "")
                {
                    this.user = user;
                }
            }

            public void setJogosultsag(string jogosultsag)
            {
                if (jogosultsag != "")
                {
                    this.jogosultsag = jogosultsag;
                }
            }

            public void setJelszo(string jelszo)
            {
                if (jogosultsag != "")
                {
                    this.jelszo = jelszo;
                }
            }

            public void setTelszam(int telefonszam)
            {
                if (telefonszam > 0)
                {
                    this.telszam = telefonszam;
                }
            }

            public string getNev()
            {
                return this.nev;
            }

            public string getUser()
            {
                return this.user;
            }

            public string getJogosultsag()
            {
                return this.jogosultsag;
            }

            public string getJelszo()
            {
                return this.jelszo;
            }

            public int getTelszam()
            {
                return this.telszam;
            }

            public string getSor()
            {
                string sor = getNev() + ";" + getUser() + ";" + getJogosultsag() + ";" + getJelszo() + ";" + getTelszam();
                return sor;
            }
        }

        List<User> felhasználóLista = new List<User>();
        public Form1()
        {
            InitializeComponent();
        }

        private void belepB_Click_1(object sender, EventArgs e)
        {
            try
            {
                string nev = nevTB.Text;
                string user = userTB.Text;
                string jogosultsag = jogosultsagCB.Text;
                string jelszo = jelszoTB.Text;
                string telefonszam = telszamTB.Text;
                if (nev != "" && user != "" && jogosultsag != "" && jelszo != "" && telefonszam != "")
                {
                    string sor = nev + ";" + user + ";" + jogosultsag + ";" + jelszo + ";" + telefonszam;
                    User fh = new User(sor);
                    felhasználóLista.Add(fh);
                    listBox1.Items.Add(fh.getSor());
                    if (fh.getJogosultsag() == "admin")
                    {
                        adminLB.Items.Add(fh.getSor());
                    }
                    else if (fh.getJogosultsag() == "user")
                    {
                        userLB.Items.Add(fh.getSor());
                    }
                    torolTB();
                }
                else
                {
                    MessageBox.Show("Hibás adatfelvitel!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void jogosultsagCB_Leave(object sender, EventArgs e)
        {
            jelszoTB.Text = general();
        }

        //10 karakterből álló jelszó, 2 egymás utáni karakter nem lehet azonos
        public string general()
        {
            Random vel = new Random();
            string jelszo = "";
            char[] tomb = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int i = 0;
            while (i < 10)
            {
                int szam = vel.Next(0, tomb.Length);
                if (i == 0)
                {
                    jelszo += tomb[szam];
                    i++;
                }
                else if (i > 0 && tomb[szam] != jelszo[i - 1])
                {
                    jelszo += tomb[szam];
                    i++;
                }
            }
            return jelszo;
        }

        public void torolTB()
        {
            nevTB.Text = "";
            userTB.Text = "";
            jogosultsagCB.Text = "";
            jelszoTB.Text = "";
            telszamTB.Text = "";
        }

        private void egyediB_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            HashSet<String> nevek = new HashSet<string>();
            for (int i = 0; i < felhasználóLista.Count; i++)
            {
                nevek.Add(felhasználóLista[i].getNev());
            }
            foreach (var nev in nevek)
            {
                listBox2.Items.Add(nev);
            }
        }

        private void egyedi2B_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            List<User> egyediLista = new List<User>();
            bool szerepel = false;
            for (int i = 0; i < felhasználóLista.Count; i++)
            {
                szerepel = false;
                for (int j = 0; j < egyediLista.Count; j++)
                {
                    if (felhasználóLista[i].getNev() == egyediLista[j].getNev())
                    {
                        szerepel = true;
                    }
                }
                if (!szerepel)
                {
                    egyediLista.Add(felhasználóLista[i]);
                    listBox2.Items.Add(felhasználóLista[i].getSor());
                }
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string sor = listBox1.SelectedItem.ToString();
                szetszed(sor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void szetszed(string sor)
        {
            string[] tomb = sor.Split(';');
            nevTB.Text = tomb[0];
            userTB.Text = tomb[1];
            jogosultsagCB.Text = tomb[2];
            jelszoTB.Text = tomb[3];
            telszamTB.Text = tomb[4];
        }

        private void jogosCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            kivalasztLB.Items.Clear();
            osszL.Visible = true;
            OsszesenL.Visible = true;
            int db = 0;
            string keres = jogosCB.Text;
            for (int i = 0; i < felhasználóLista.Count; i++)
            {
                if (felhasználóLista[i].getJogosultsag() == keres)
                {
                    kivalasztLB.Items.Add(felhasználóLista[i].getSor());
                    db++;
                }
            }
            osszL.Text = db.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string fajl = "adatok.txt";                
                StreamWriter sw = new StreamWriter(fajl);
                foreach (string sor in listBox1.Items)
                {
                    sw.WriteLine(sor);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fOlvB_Click(object sender, EventArgs e)
        {
            try
            {
                string fajl = "adatok.txt";
                string sor = "";
                Encoding utf8 = Encoding.UTF8;
                StreamReader sr = new StreamReader(fajl, utf8);
                while (!sr.EndOfStream)
                {
                    sor = sr.ReadLine();                    
                    User u = new User(sor);
                    felhasználóLista.Add(u);
                    listBox1.Items.Add(u.getSor());
                    if (u.getJogosultsag() == "admin")
                    {
                        adminLB.Items.Add(u.getSor());
                    }
                    else if (u.getJogosultsag() == "user")
                    {
                        userLB.Items.Add(u.getSor());
                    }
                    torolTB();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void torolB_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBox1.SelectedIndex;
                string sor  = listBox1.SelectedItem.ToString();
                felhasználóLista.RemoveAt(index);
                listBox1.Items.Remove(sor);                
                adminLB.Items.Remove(sor);
                userLB.Items.Remove(sor);
                kivalasztLB.Items.Remove(sor);
                listBox2.Items.Remove(sor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
