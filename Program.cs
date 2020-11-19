using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace An_Improved_N_DIST
{
    static class Program
    {



        public static OleDbConnection con = new OleDbConnection();
        public static OleDbCommand cmd1 = new OleDbCommand();
        public static OleDbDataAdapter ad1 = new OleDbDataAdapter();
        public static DataSet ds = new DataSet();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]






        static void Main()
        {

            con.ConnectionString = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + (Application.StartupPath + "\\DB Soft Bigram Distance.MDB;Persist Security Info=False"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
