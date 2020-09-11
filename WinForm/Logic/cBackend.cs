using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using ExcelLib;
using System.Configuration;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace AHMFARPA018.Logic
{
    public class cBackend
    {
        
        //the method below is used to sendREquest to Orchestrator that will be used to get the token
        public static string GetToken(string username, string password, string tenancyName = null)
        {
            
            string vURL_TOKEN = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='WinForm_URL_TOKEN'");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(vURL_TOKEN);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var input = "{\"usernameOrEmailAddress\":\"" + username + "\"," +
                            "\"password\":\"" + password + "\"}";

                if (tenancyName != null)
                {
                    input = input.TrimEnd('}') + "," +
                            "\"tenancyName\":\"" + tenancyName + "\"}";
                }

                streamWriter.Write(input);
                streamWriter.Flush();
                streamWriter.Close();
            }




            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            httpResponse.Close();

            
            var entries = response.TrimStart('{').TrimEnd('}').Replace("\"", String.Empty).Split(',');

            foreach (var entry in entries)
            {
                if (entry.Split(':')[0] == "result")
                {
                    return entry.Split(':')[1];
                }
            }

            return null;
        }


        //method below will send the request to orchestrator throughout POST request, thus sending the job in the Pending set
        public static void SendRequest(string token)
        {   
            string vURL_REQUEST = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='WinForm_URL_Request'");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(vURL_REQUEST);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
               
               string json= sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='WinForm_JSON'");



                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();


           using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            

        }

        


        //Method below will read the excel parameter file and update the database accordingly. 
        //This method will be exectued everytime the program is running
        public static void sUpdate_DB_From_Excel(string vPath_RPA_SAP)
        {
            
            Boolean isExist1 = q.sFile_IsExist(vPath_RPA_SAP);
            
          
            if (isExist1 == false)
            {
                string vError = "RPA SAP XLSX is not exist = " + vPath_RPA_SAP;
                
                throw new Exception(vError);
            }
            
          
            sUpdate_tPlant(vPath_RPA_SAP);
           
            sUpdate_tParam(vPath_RPA_SAP, "Controlling Area", "ControllingArea");
            sUpdate_tParam(vPath_RPA_SAP, "Cycle", "Cycle");
            sUpdate_tParam(vPath_RPA_SAP, "Company Code", "CompanyCode");




            try { sdConnection.sConnection_Close(0); }
            catch (Exception e)
            {
                Console.WriteLine("Connection already closed " + e.Message);
            }
        }

        



        static void sUpdate_tParam2(string vPath_XLSX, string vSheet_Name, string vList_Column, string vCheckColEmpty, string vAccess_Col, string vExtension_Name)
        {
            string vError = "";
            Hashtable[] arrHash = stExcel.sExcel_Read_2_Hashtable_Until_Empty(vPath_XLSX, vSheet_Name, "A2", vCheckColEmpty, vList_Column, out vError);
          
            foreach (var hashMenu in arrHash)
            {
                string vCol_Name = q.sSplit(vAccess_Col, ",")[0];
                string vCol_Value = q.sSplit(vAccess_Col, ",")[1];

                string vName = vExtension_Name + hashMenu[vCol_Name].ToString();
                string vValue = hashMenu[vCol_Value].ToString();
                string vDescription = hashMenu["Rule"].ToString() + " = " + hashMenu["Sample"].ToString();

                DataModel.tPARAM oParam = new DataModel.tPARAM();
                oParam.VPARAMNAME = vName;
                oParam.VPARAMVAL = vValue;
                oParam.VPARAMDESC = vDescription;
                oParam.oBasic_Operation.Upsert();
                
            }
        }

        static void sUpdate_tParam(string vPath_XLSX, string vSheet_Name, string vParam_Name)
        {
            string vError = "";
            Hashtable[] arrHash = stExcel.sExcel_Read_2_Hashtable_Until_Empty(vPath_XLSX, vSheet_Name, "A2", "", "Name", out vError);
             
            
            string vResult = "";
            foreach (var hashMenu in arrHash)
            {
                string vValue = hashMenu["Name"].ToString();
                vResult = q.sVBCLRF_Max_Line(vResult, vValue, ",");
            }
            DataModel.tPARAM oParam = new DataModel.tPARAM();
            oParam.VPARAMNAME = vParam_Name;
            oParam.VPARAMVAL = vResult;
            oParam.oBasic_Operation.Upsert();
        }

        static void sUpdate_tPlant(string vPath_XLSX)
        {
            sdConnection.sSave("DELETE FROM AHMFARPA_MSTPLANTS");
            string vList_Col_Excel = "Plant,Description,LIST_STEP_NO"; string vError = "";
            System.Diagnostics.Debug.WriteLine("vPath_XLSX: " + vPath_XLSX);
            
           
                Hashtable[] arrHash = stExcel.sExcel_Read_2_Hashtable_Until_Empty(vPath_XLSX, "Plant", "A2", "Plant", vList_Col_Excel, out vError);
           

            foreach (var hashMenu in arrHash)
                {
                
                    string vPlant = hashMenu["Plant"].ToString();
                    string vDescription = hashMenu["Description"].ToString();
                    string vList_Step_No = hashMenu["LIST_STEP_NO"].ToString();
                    DataModel.tPLANT oPlant = new DataModel.tPLANT();
                
                
                oPlant.VPLANTID = vPlant;
                oPlant.VPLANTDESC = vDescription;
                oPlant.VPLANTSTEP = vList_Step_No;
               

                oPlant.oBasic_Operation.Upsert();
               
            }

            

           
        }















    }
}
