using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;


namespace AHMFARPA018
{
    public enum enumType
    {
        HELP,
        FORM_LOAD,
        UPDATE_DB
    }

    static class Program
    {
        
        public static string vJob_Type = "CUSTOM";
        static string vAssembly_Name = "AHMFARPA018"; //assembly to load form
        public static string vApp_Path = q.sApp_Get_Current_Path();
        

        [STAThread]

        static void Main(string[] args)
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            sForm_Loading();


        }

        static void sForm_Loading()
        {

            sSetting_Application();

            

            sLoad_Form(5);
            

        }

        public static void sLoad_Form(Form oCurr, object sender)
        {
            int vID = q.sString_2_Integer((sender as Button).Text.Replace("&", "").Substring(0, 1));
            Console.WriteLine("vID=" + vID);
           
            sLoad_Form(vID, false);
            oCurr.Hide();
        }
        public static void sLoad_Form(int vID, Boolean isFirst_Load = true)
        {
           
            string vParent_Full_Name = q.sClass_Name_Get_Parent(typeof(Form5));

            string vFull_Name = vParent_Full_Name + "." + "Form" + vID;


            Form oForm = q.sClass_Create_NewInstance_fromString<Form>(vFull_Name, vAssembly_Name);
            int vWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            if (vWidth > 2000) oForm.WindowState = FormWindowState.Normal;

            Console.WriteLine("Calling  Method Run to instantiate Form5 by reading connection from Database");
            if (isFirst_Load) Application.Run(oForm);

        

            //Code below is intended for handling the case for second load
            oForm.Activate();

            try
            {
                
                oForm.Show();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Failed with error message: " + e.Message);
            }
        }

        static void sSetting_Application()
        {
            sdGlobal.sGlobal_Set();
        }
    }
}
