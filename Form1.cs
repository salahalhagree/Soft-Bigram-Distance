using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace An_Improved_N_DIST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //-------------------------
        public static int sm = 0;
        public static int sm1 = 0;
        private int n = 2;

        public static string[] x = new string[100];
        public static string[] y = new string[100];
        public static double[] xx = new double[100];
        public static double[] yy = new double[100];

        public static double[] x2014 = new double[100];
        public static double[] y2014 = new double[100];
        public static double[] xy2014 = new double[100];
        //-------------------------------


        private void button1_Click(object sender, EventArgs e)
        {
            //----------------Soft Bigram Distance --------------------------------------------------
            DateTime dt1 = new DateTime();
            dt1 = DateTime.Now;
            progressBar1.Value = 0;
            double Cwq;
            progressBar1.Maximum = dataGridView4.Rows.Count - 1;
            //-------
            for (int a = 0; a < dataGridView4.RowCount - 1; a++)
            {
                Cwq = 0;
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                string s = "-" + Convert.ToString(dataGridView4.Rows[a].Cells[1].Value.ToString());
                string t = "-" + Convert.ToString(dataGridView4.Rows[a].Cells[2].Value.ToString());
                int n = s.Length;
                int m = t.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    listBox1.Items.Add(s.Substring(i, 2));
               }
                for (int i = 0; i < n - 1; i++)
                {
                    x[i] = Convert.ToString(listBox1.Items[i]);
                }
                for (int i = 0; i < m - 1; i++)
                {
                    listBox2.Items.Add(t.Substring(i, 2));
                }
                for (int i = 0; i < m - 1; i++)
                {
                    y[i] = Convert.ToString(listBox2.Items[i]);
                }
                //----------------------------------------
                   double[,] LevenshteinArabic = new double[n + 1, m + 1];   ////Soft Bigram Distance
                // Step 1
                if (n == 0)
                {
                    Console.WriteLine(m + "--------N----");
                }
                if (m == 0)
                {
                    Console.WriteLine(n + "------M------");
                }
                // Step 2
                for (int i = 0; i <= n; i++)
                {
                    LevenshteinArabic[i, 0] = i;  ////Soft Bigram Distance
                }
                for (int j = 0; j <= m; j++)
                {
                     LevenshteinArabic[0, j] = j;  ////Soft Bigram Distance
                }
                double cost1 = 1;
                double cost2 = 1;
                int n1 = listBox1.Items.Count;
                int m1 = listBox2.Items.Count;
                for (int i = 1; i <= n1; i++)
                {
                    dataG.Rows.Add();
                    dataG.Rows[i - 1].Cells[0].Value = x[i - 1];
                    for (int j = 1; j <= m1; j++)
                    {
                        string s1 = x[i - 1];
                        string s2 = y[j - 1];
                        string s11 = "";
                        string s12 = "";
                        string s21 = "";
                        string s22 = "";
                        if (s1 != null)
                        {
                            s12 = s1.Substring(1, 1);
                            s11 = s1.Substring(0, 1);
                        }
                        if (s2 != null)
                        {
                            s21 = s2.Substring(0, 1);
                            s22 = s2.Substring(1, 1);
                        }
                        //-----------------------------------
                        dataG.Columns[j].HeaderText = y[j - 1];
                        if (x[i - 1] == y[j - 1]) Cwq = Cwq + 1;
                        //-------------------  transposition and  substitution operations----------------
                        if ((s11 == s21) && (s12 == s22))
                        {
                            //-------------W1
                            cost1 = Convert.ToDouble(textcost1.Text);
                        }
                        else if ((s11 != s21) && (s12 != s22) && (s11 != s22) && (s12 != s21))
                        {
                            //-------------W2
                             cost1 = Convert.ToDouble(textcost2.Text);//1;
                            //-----------------------------------------------
                        }
                        else if ((s11 == s22) && (s12 == s21))
                        {
                            //-------------W3
                            cost1 = Convert.ToDouble(textcost3.Text);//0.10;
                        }
                        else if ((s11 != s21) && (s12 == s22))
                        {
                            //-------------W4
                            cost1 = Convert.ToDouble(textcost4.Text);
                        }
                        else if ((s11 == s21) && (s12 != s22))
                        {
                            //-------------W5
                            cost1 = Convert.ToDouble(textcost5.Text);//0.10;
                            if (((i == 1) && (j == 1)))     //حال هاصة من اجل جعلها صفر
                            {
                                cost1 = 1;
                            }
                        }
                        //------------- 
                        else if ((s11 != s22) && (s12 == s21))  
                        {
                            //-------------W6
                            cost1 = Convert.ToDouble(textcost6.Text);//W6    0.12;
                            //-------------W8------------- deletion and insertion operations
                            cost2 = Convert.ToDouble(textcost8.Text);//W8   0.2;
                        }
                        else if ((s11 == s22) && (s12 != s21))
                        {
                            //-------------W7
                            cost1 = Convert.ToDouble(textcost7.Text); //W7   0.12;
                            //-------------W9------------- deletion and insertion operations
                            cost2 = Convert.ToDouble(textcost9.Text);// W9    0.2;
                        }
                        else
                        {
                            cost1 = 1;
                            cost2 = 1;        
                        }
                        //----------------------------------------------------
                        LevenshteinArabic[i, j] = Math.Min(Math.Min(LevenshteinArabic[i - 1, j] + cost2, LevenshteinArabic[i, j - 1] + cost2), LevenshteinArabic[i - 1, j - 1] + (cost1));
                         //-----------------------------------
                        dataG.Rows[i - 1].Cells[j].Value = LevenshteinArabic[i, j];// Math.Min(Math.Min(Levenshtein1[i - 1, j] + cost1, Levenshtein1[i, j - 1] + cost1), Levenshtein1[i - 1, j - 1] + (cost1));
                    }
                }
                dataG.Rows.Clear();
                progressBar1.Value += 1;
                if (progressBar1.Value >= progressBar1.Maximum)
                {
                  progressBar1.Value = 0;
                }
                //-----------------
                dataGridView4.Columns[10].HeaderText = "Distance Soft Bigram Distance";
                dataGridView4.Columns[11].HeaderText = " Similarity Soft Bigram Distance";
                dataGridView4.Rows[a].Cells[10].Value = LevenshteinArabic[n1, m1];
                dataGridView4.Rows[a].Cells[11].Value = 1 - (LevenshteinArabic[n1, m1] / Math.Max(n1, m1));
             }
            //نهاية الاجرااء
            DateTime dt2 = new DateTime();
            dt2 = DateTime.Now;
            TimeSpan tss = dt2 - dt1;
            textBox8.Text = Convert.ToString(tss);
            //------------------END -------Proposed  Soft Bigram Distance (Soft-Bidist)---------------
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  N-DIST 2005

            int n = int.Parse(textngram.Text);           
            for (int a = 0; a < dataGridView4.RowCount - 1; a++)
            {              
                string source = Convert.ToString(dataGridView4.Rows[a].Cells[1].Value.ToString());
                string target = Convert.ToString(dataGridView4.Rows[a].Cells[2].Value.ToString());
                int sl = source.Length;
                int tl = target.Length;

                if (sl == 0 || tl == 0)
                {
                    if (sl == tl)
                    {
                        textBox1.Text = "1";
                    }
                    else
                    {
                        textBox1.Text = "1";
                    }
                }

                int cost = 0;
                if (sl < n || tl < n)
                {
                    for (int ii = 0, ni = Math.Min(sl, tl); ii < ni; ii++)
                    {
                        if (source[ii] == target[ii])
                        {
                            cost++;
                        }
                    }
                    textBox1.Text = Convert.ToString((float)cost / Math.Max(sl, tl));
                }

                char[] sa = new char[sl + n - 1];
                float[] p; //'previous' cost array, horizontally
                float[] d; // cost array, horizontally
                float[] _d; //placeholder to assist in swapping p and d

                //construct sa with prefix
                for (int ii = 0; ii < sa.Length; ii++)
                {
                    if (ii < n - 1)
                    {
                        sa[ii] = (char)0; //add prefix
                    }
                    else
                    {
                        sa[ii] = source[ii - n + 1];
                    }
                }
                p = new float[sl + 1];
                d = new float[sl + 1];

                // indexes into strings s and t
                int i; // iterates through source
                int j; // iterates through target

                char[] t_j = new char[n]; // jth n-gram of t

                for (i = 0; i <= sl; i++)
                {
                    p[i] = i;
                }
                //for loop  1
                for (j = 1; j <= tl; j++)
                {
                    //construct t_j n-gram 
                    if (j < n)
                    {
                        for (int ti = 0; ti < n - j; ti++)
                        {
                            t_j[ti] = (char)0; //add prefix
                            
                        }
                        for (int ti = n - j; ti < n; ti++)
                        {
                            t_j[ti] = target[ti - (n - j)];
                           
                        }
                    }
                    else
                    {
                        t_j = target.Substring(j - n, n).ToCharArray();
                       

                    }
                    d[0] = j;
                    //for loop  2
                    for (i = 1; i <= sl; i++)
                    {
                        cost = 0;
                        int tn = n;

                        dataG.Rows.Add();
                        dataG.Rows[i - 1].Cells[0].Value = sa[i];

                        //compare sa to t_j
                        //for loop  3
                        for (int ni = 0; ni < n; ni++)
                        {
                            if (
                                sa[i - 1 + ni] != t_j[ni])
                            {
                                cost++;
                            }
                            else if (sa[i - 1 + ni] == 0)
                            { //discount matches on prefix
                                tn--;
                            }
                            dataG.Columns[j].HeaderText = Convert.ToString(t_j[ni]);
                        }
                        float ec = (float)cost / tn;
                        d[i] = Math.Min(Math.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + ec);
                        dataG.Rows[i - 1].Cells[j - 1].Value = d[i];//
                        
                    }
                    // copy current distance counts to 'previous row' distance counts
                    _d = p;
                    p = d;
                    d = _d;
                    dataG.Rows.Clear();

                }

                // our last action in the above loop was to switch d and p, so p now
                // actually has the most recent cost counts
                dataGridView4.Columns[8].HeaderText = "  Distance N-DIST (2005) ";
                dataGridView4.Columns[9].HeaderText = "Similarity N-DIST (2005)  ";

                dataGridView4.Rows[a].Cells[8].Value = (float)p[sl];
                dataGridView4.Rows[a].Cells[9].Value = 1.0f - ((float)p[sl] / Math.Max(tl, sl));
               
            }
          
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Dataset2
            //Dataset3
            //Dataset4
            //Dataset5
            //Dataset6
            //Dataset7
            //Dataset8
            //Dataset9
            //Dataset10
            //Dataset11
            //Dataset12

            String sq = "SELECT * FROM Dataset1  ORDER BY no2019  ";
            Program.cmd1.CommandText = sq;
            Program.cmd1.Connection = Program.con;
            Program.ad1.SelectCommand = Program.cmd1;
            Program.ad1.Fill(Program.ds, "Dataset1");
            dataGridView4.DataSource = Program.ds.Tables["Dataset1"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //===========================================
            DateTime dt1 = new DateTime();
            dt1 = DateTime.Now;
            sm1 = Convert.ToInt32(DateTime.Now.Second.ToString());
            //sm = 0;
            timer1.Start();
            timer1.Enabled = true;
           progressBar1.Value = 0;
            double v1;
            double v2;
            double v3;
            double d1;
            double d2;
            double d3;
            double cost;
            progressBar1.Maximum = dataGridView4.Rows.Count - 1;
          double[,] Levenshtein = new double[100 + 1, 100 + 1];   ////Levenshtein
            double cost1;
            string s;
            string t;
            int n;
            int m;
            int i;
            int j;
            int a;
            for (a = 0; a < dataGridView4.RowCount - 1; a++)
            {
                 listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox1.Dispose();
                listBox2.Dispose();
                s = Convert.ToString(dataGridView4.Rows[a].Cells[1].Value.ToString());
                t = Convert.ToString(dataGridView4.Rows[a].Cells[2].Value.ToString());
                n = s.Length;
                m = t.Length;
                for (i = 0; i < n; i++)
                {
                    listBox1.Items.Add(s.Substring(i, 1));
  
                }
                for (i = 0; i < n; i++)
                {
                    x[i] = Convert.ToString(listBox1.Items[i]);
                }
                for (i = 0; i < m; i++)
                {
                    listBox2.Items.Add(t.Substring(i, 1));                 
                }
                for (i = 0; i < m; i++)
                {

                    y[i] = Convert.ToString(listBox2.Items[i]);

                }
                 // Step 1
                if (n == 0)
                {
                    Console.WriteLine(m + "--------mm----");
                }
                if (m == 0)
                {
                    Console.WriteLine(n + "------mm------");
                }
                // Step 2
                for (i = 0; i <= n; i++)
                {
                    Levenshtein[i, 0] = i;
                 }

                for (j = 0; j <= m; j++)
                {
                    Levenshtein[0, j] = j;  ////Levenshtein
                }

                for (i = 1; i <= n; i++)
                {
                    dataG.Rows.Add();
                     for (j = 1; j <= m; j++)
                    {
                            cost1 = (x[i - 1] == y[j - 1]) ? 0 : 1;//Levenshtein

                        Levenshtein[i, j] = Math.Min(Math.Min(Levenshtein[i - 1, j] + 1, Levenshtein[i, j - 1] + 1), Levenshtein[i - 1, j - 1] + Convert.ToDouble(cost1));
                        dataG.Rows[i - 1].Cells[j - 1].Value = Levenshtein[i, j];// Math.Min(Math.Min(Levenshtein[i - 1, j] + 1, Levenshtein[i, j - 1] + 1), Levenshtein[i - 1, j - 1] + Convert.ToDouble(cost1)); ;
                        sm++;
                    }
                }
                dataG.Rows.Clear();
                progressBar1.Value += 1;
                if (progressBar1.Value >= progressBar1.Maximum)
                {
               progressBar1.Value = 0;
                }
                dataGridView4.Columns[4].HeaderText = "Distance Levenshtein";
                dataGridView4.Columns[5].HeaderText = "Similarity Levenshtein";
                dataGridView4.Rows[a].Cells[4].Value = Levenshtein[n, m];
                dataGridView4.Rows[a].Cells[5].Value = 1 - (Levenshtein[n, m] / Math.Max(n, m));
                
            }
            DateTime dt2 = new DateTime();
            dt2 = DateTime.Now;
            TimeSpan tss = dt2 - dt1;
           textBox8.Text = Convert.ToString(tss);
            //-------------END-----Levenshtein

            //================================================
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //000000000000000000000000000000000000
            DateTime dt1 = new DateTime();
            dt1 = DateTime.Now;
            progressBar1.Value = 0;
            double v1;
            double v2;
            double v3;
            double d1;
            double d2;
            double d3;
            double cost;
            progressBar1.Maximum = dataGridView4.Rows.Count - 1;
            for (int a = 0; a < dataGridView4.RowCount - 1; a++)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();

                string s = Convert.ToString(dataGridView4.Rows[a].Cells[1].Value.ToString());
                string t = Convert.ToString(dataGridView4.Rows[a].Cells[2].Value.ToString());
                int n = s.Length;
                int m = t.Length;
                for (int i = 0; i < n; i++)
                {
                    listBox1.Items.Add(s.Substring(i, 1));
                 }
                for (int i = 0; i < n; i++)
                {
                    x[i] = Convert.ToString(listBox1.Items[i]);
                }

                for (int i = 0; i < m; i++)
                {
                    listBox2.Items.Add(t.Substring(i, 1));
                   
                }
                for (int i = 0; i < m; i++)
                {
                    y[i] = Convert.ToString(listBox2.Items[i]);
                }
               
                //--------------------------------------------------------------

                double[,] Levenshtein = new double[n + 1, m + 1];   //Damerau-Levenshtein distance
                // Step 1
                if (n == 0)
                {
                    Console.WriteLine(m + "--------mm----");
                }

                if (m == 0)
                {
                    Console.WriteLine(n + "------mm------");
                }

                // Step 2
                for (int i = 0; i <= n; i++)
                {
                    Levenshtein[i, 0] = i;  //Damerau-Levenshtein distance
                }

                for (int j = 0; j <= m; j++)
                {
                    Levenshtein[0, j] = j;  //Damerau-Levenshtein distance
                }

                for (int i = 1; i <= n; i++)
                {
                    dataG.Rows.Add();

                    for (int j = 1; j <= m; j++)
                    {

                        double cost1 = (x[i - 1] == y[j - 1]) ? 0 : 1;// Damerau Levenshtein

                        Levenshtein[i, j] = Math.Min(Math.Min(Levenshtein[i - 1, j] + 1, Levenshtein[i, j - 1] + 1), Levenshtein[i - 1, j - 1] + Convert.ToDouble(cost1));
                        //-----------Damerau-Levenshtein distance---------------
                        if ((i > 1 && j > 1) && (x[i - 1] == y[j - 2]) && (x[i - 2] == y[j - 1]))
                            Levenshtein[i, j] = Math.Min(Levenshtein[i, j], Levenshtein[i - 2, j - 2] + cost1);
                        dataG.Rows[i - 1].Cells[j - 1].Value = Levenshtein[i, j];
                        //-----------------------
                    }
                }

                progressBar1.Value += 1;
                if (progressBar1.Value >= progressBar1.Maximum)
                {
                    progressBar1.Value = 0;
                }

                dataGridView4.Columns[6].HeaderText = "  Distance Damerau Levenshtein";
                dataGridView4.Columns[7].HeaderText = "Similarity  Damerau Levenshtein";
                //-----------------
                dataGridView4.Rows[a].Cells[6].Value = Levenshtein[n, m];//Damerau-Levenshtein distance
                dataGridView4.Rows[a].Cells[7].Value = 1 - (Levenshtein[n, m] / Math.Max(n, m));//Damerau-Levenshtein distance
                //------------------Damerau-Levenshtein distance
            }
            //-------------------------------------------------------------------
            DateTime dt2 = new DateTime();
            dt2 = DateTime.Now;
            TimeSpan tss = dt2 - dt1;
            //sm = Convert.ToInt32(DateTime.Now.Second.ToString());
            textBox8.Text = Convert.ToString(tss);

            //00000000000000000 END Damerau-Levenshtein distance00000000000000000000
        }
    }
}
