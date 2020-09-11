using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tools;
using Tools.Utilities;
using AHMFARPA018.Properties;
using System.Configuration;
using System.Net;




namespace AHMFARPA018
{
    public partial class Form5 : Form
    {
        
        #region init var2
        static string[] arrItem_List = {
        };
        
        #endregion

        public Form5()
        {
            
            InitializeComponent();
            
        }

        private void Form5_Load22(object sender, EventArgs e)
        {
         
            if (Settings.Default.Location != null) this.Location = Settings.Default.Location;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //In the beginning of loading the form, code below will read the both xls SAP and user 
            //which specified in app.config then update the sql database
            //the connection string is established in the library

            string vPath_SAP = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='WinForm_XLSX_SAP'");

            

            System.Diagnostics.Debug.WriteLine("Starting Form load");

            Logic.cBackend.sUpdate_DB_From_Excel(vPath_SAP);
           




            //variable below just used to setup the screen position
            int vScreen_Height = Screen.PrimaryScreen.WorkingArea.Height;
            int vForm_Height = this.Height;
            if (vForm_Height > vScreen_Height) this.Height = vScreen_Height;
            if (Settings.Default.Location != null) this.Location = Settings.Default.Location;
         
           string vPeriod = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='PERIOD'");

            if (vPeriod.Length > 6) { vPeriod = ""; }


            txtPeriod.Text = vPeriod; 
           
            

            btnRefresh.Select();


          if (vPeriod != "") //if the period is not blank then refresh
            {
                btnSearch_Click(null, null);  //refresh
            }

      

        }

       

        private void button99_Click(object sender, EventArgs e)
        {
            Settings.Default.Location = this.Location;
            Settings.Default.Save();
            q.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            q.Exit();
        }

        void sSearch(Boolean isReload)
        {
            
           
           
            string vFlag = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='EXECUTE_UIPATH'");

            cWinForm_Tools.sCursor_Change();
            string[] arrStep_ID_Selected = { };
            if (Program.vJob_Type == "MONTHLY") arrStep_ID_Selected = q.sSplit("1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28", ",");
            else if(Program.vJob_Type == "DAILY") arrStep_ID_Selected = q.sSplit("3", ",");

            #region update screen
            //sdConnection.sSave("UPDATE TPARAM SET PARAM_VALUE=0 WHERE PARAM_NAME='EXECUTE_UIPATH'");
            if (isReload == true || treeView1.Nodes.Count == 0)
            {
                treeView1.Nodes.Clear();
                isReload = true;
            }
            else isReload = false;
            treeView1.CheckBoxes = true;
            treeView1.ShowNodeToolTips = true;
            string vPeriod = txtPeriod.Text;

            var oList_Step = sdConnection.sRead_2_List<DataModel.tSTEP>("SELECT ISTEPID,VSTEPNAME,VSTEPPATH,ISTEPSEQ,ISTEPTYPE FROM AHMFARPA_MSTSTEPS ORDER BY ISTEPSEQ");




            int PeriodIsNumber = 1;
            int CountPeriod = 0;
            int Last2DigitPeriod = 0;
            //row below is trying to parse the vPeriod into the number. If the parsing is failed, then the PeriodISnumber will become 0, otherwise the PeriodIsNumber=vPeriod 
            int.TryParse(vPeriod, out PeriodIsNumber);
            CountPeriod = vPeriod.Length;
            Last2DigitPeriod = q.sString_2_Integer((vPeriod.Substring(CountPeriod - 2)));
            if (PeriodIsNumber == 0 || CountPeriod != 6 || Last2DigitPeriod > 12) //If Period Number is 0, it means that vPeriod is not Integer
            { MessageBox.Show("Nilai Text Box di Period Harus Angka dengan Format YYYYMM dan Panjang 6 Digit"); }

            else
            {
                var oList_JobHistory = sdConnection.sRead_2_List<DataModel.tJOB_HISTORY>("SELECT IPERIOD,ISTEPID,VPLANTID,VSTATUSID,VSTATUSDESC,IUSERFLAG,DLASTUPD,DTIMESTART,DTIMEEND FROM AHMFARPA_HISJOBS WHERE IPERIOD='" + vPeriod + "'");


                int iNode = 0;
                foreach (var oStep in oList_Step)
                {

                    int vStep_ID = oStep.ISTEPID;
                    Boolean vChecked = false;
                    if (q.sArray_Find(arrStep_ID_Selected, vStep_ID.ToString(), true).Length > 0)
                    {
                        vChecked = true;
                    }
                    string vList_Plant_Checked = "";
                    #region get list plant checked
                    if (vPeriod != "")
                    {
                        var lstJobChecked = (from a in oList_JobHistory
                                             where a.IPERIOD == q.sInt_Try_Parse(vPeriod)
                  && a.ISTEPID == vStep_ID && a.IUSERFLAG == 1
                                             select a);
                        if (lstJobChecked.Count() > 0) vChecked = true;
                        foreach (var oJobChecked in lstJobChecked)
                        {
                            vList_Plant_Checked += ";" + oJobChecked.VPLANTID + ";";
                        }
                    }
                    #endregion

                    TreeNode oNode;
                    if (isReload == false)
                    {
                        oNode = treeView1.Nodes[iNode];
                    }
                    else
                    {
                        oNode = treeView1.Nodes.Add(oStep.ISTEPSEQ + "_" + oStep.VSTEPNAME);
                    }

                    oNode.Tag = oStep.ISTEPID;
                    oNode.Checked = vChecked;

                    List<DataModel.tPLANT> oList_Plant;
                    if (oStep.ISTEPTYPE == 1) oList_Plant = new List<DataModel.tPLANT> { new DataModel.tPLANT() { VPLANTID = "ALL" } };
                    else oList_Plant = sdConnection.sRead_2_List<DataModel.tPLANT>("SELECT VPLANTID,VPLANTDESC,VPLANTSTEP FROM AHMFARPA_MSTPLANTS");

                    int iNode_Child = 0;

                    foreach (var oPlant in oList_Plant)
                    {


                        Boolean isPlant_Show = q.sString_Find("," + oPlant.VPLANTSTEP + ",", "," + oStep.ISTEPID.ToString() + ",");
                        if (isPlant_Show || oPlant.VPLANTID == "ALL")
                        {

                            string vPlant_ID = oPlant.VPLANTID;
                            Boolean vChecked_Plant = false;
                            if (q.sString_Find(vList_Plant_Checked, ";" + vPlant_ID + ";")) vChecked_Plant = true;
                            var lstJobPlant = (from a in oList_JobHistory
                                               where a.IPERIOD == q.sInt_Try_Parse(vPeriod)
                                                   && a.ISTEPID == vStep_ID && a.VPLANTID == vPlant_ID
                                               select a).FirstOrDefault();


                            string vStatus = ""; string vToolTip_Text = ""; string vDuration = "", vStatus_Description = ""; string vPercent = "";


                            q.sTry_Catch_Wrap(() =>
                            {
                                vStatus_Description = lstJobPlant.VSTATUSDESC;
                                if (lstJobPlant.VSTATUSID == "P") vStatus_Description = "Start at " + lstJobPlant.DTIMESTART.ToString();

                                vStatus = " : " + lstJobPlant.VSTATUSID + " : " + vStatus_Description + " : " + vPercent;
                                if (lstJobPlant.DTIMESTART != null && lstJobPlant.DTIMEEND != null)
                                {
                                    string vTemp_Duration = q.sDate_Difference_2_String_Readable((DateTime)lstJobPlant.DTIMEEND, (DateTime)lstJobPlant.DTIMESTART);
                                    vDuration = ", Duration = " + vTemp_Duration;
                                    vToolTip_Text = "Last Updated = " + lstJobPlant.DLASTUPD.ToString() + vDuration;
                                    if (lstJobPlant.VSTATUSID != "S") vToolTip_Text = vToolTip_Text + q.vNewLine + lstJobPlant.VSTATUSDESC;
                                    int vLoc = lstJobPlant.DLASTUPD.ToString().IndexOf(" ");
                                    string vTemp_Time_Update = lstJobPlant.DLASTUPD.ToString().Substring(vLoc);
                                    //vStatus = vStatus + ", Duration = " + vTemp_Duration + " Sec, Time = " + vTemp_Time_Update;
                                }
                            });

                            TreeNode oNode_Child;
                            if (isReload == false)
                            {
                                oNode_Child = oNode.Nodes[iNode_Child];
                                oNode_Child.Text = vPlant_ID + vStatus;
                            }
                            else
                            {
                                oNode_Child = oNode.Nodes.Add(vPlant_ID + vStatus);
                            }
                            //123
                            oNode_Child.ToolTipText = vToolTip_Text;
                            oNode_Child.Tag = oPlant.VPLANTID;
                            if (q.sArray_Find(arrStep_ID_Selected, vStep_ID.ToString(), true).Length > 0)
                            {
                                vChecked_Plant = true;
                            }
                            if (q.sString_Find(vStatus, ": S :")) vChecked_Plant = false;
                            oNode_Child.Checked = vChecked_Plant;
                            iNode_Child++;
                        }
                    }
                    iNode++;
                }
                #endregion

                sStatus_UIPath_Update(vFlag);
                cWinForm_Tools.sCursor_Change(true);

            }//closing tag for else after checking perio
        }

        private static string vExecute_UIPath = "0";
        private void sStatus_UIPath_Update(string vFlag)
        {
            #region change flag EXECUTE_UIPATH
            if(vFlag == "") vFlag = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='EXECUTE_UIPATH'");

            if (vFlag.Equals("0")) lblStatus.Text = "Stopped";
            if (vFlag.Equals("1")) lblStatus.Text = "Waiting to Run";
            if (vFlag.Equals("2")) lblStatus.Text = "Running";
            vExecute_UIPath = vFlag;

            if (vFlag.Equals("0")) sTimer_Set(false);
            #endregion
        }

        private void sTimer_Set(Boolean isEnabled)
        {
            if (isEnabled)
            {
                timer1.Enabled = true;               
                chkTimer.Checked = true;
              
            } else
            {
                timer1.Enabled = false;
                chkTimer.Checked = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
                //Logic explanation, if  EXECUTE_UIPATH = 1, then get search period value from database
                //else get the period value from textbox (because when the job is running, it is not possible to get other period
                string vFlag = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='EXECUTE_UIPATH'");
                if(vFlag == "1")
                {
                    string vPeriod = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='PERIOD'");
                    txtPeriod.Text = vPeriod;
                }
               sSearch(false);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var isChecked = checkBox1.Checked;
            foreach(TreeNode oNode in treeView1.Nodes)
            {
                oNode.Checked = isChecked;
                sCheckTreeView(oNode, isChecked);
            }
        }

        private Boolean isTImer_Enabled = false;
        private void sTimer1_Pending(Boolean isPending)
        {
            if (isPending)
            {
                if (chkTimer.Checked == true)
                {
                    sTimer_Set(false);
                    isTImer_Enabled = true;
                }
            } else
            {
                if (isTImer_Enabled)
                {
                    sTimer_Set(true);
                    isTImer_Enabled = false;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Check whether the period is number or not
               string vPeriod = txtPeriod.Text;



                int PeriodIsNumber = 1;
                int CountPeriod = 0;
                int Last2DigitPeriod = 0;
                //row below is trying to parse the vPeriod into the number. If the parsing is failed, then the PeriodISnumber will become 0, otherwise the PeriodIsNumber=vPeriod 
                int.TryParse(vPeriod, out PeriodIsNumber);
                CountPeriod = vPeriod.Length;


            Last2DigitPeriod = q.sString_2_Integer((vPeriod.Substring(CountPeriod - 2)));
            String CountFlag1Job = sdConnection.sRead_2_Text("select count(1) from [AHMFARPA_HISJOBS] where IUSERFLAG=1 and IPERIOD="+vPeriod);

            if (PeriodIsNumber == 0 || CountPeriod != 6) //If Period Number is 0, it means that vPeriod is not Integer
            { MessageBox.Show("Nilai Text Box di Period Harus Angka dengan Format YYYYMM dan Panjang 6 Digit");}

           

            else if (Last2DigitPeriod > 12 || Last2DigitPeriod < 1)
            {
                MessageBox.Show("Nilai Text Box di Period Harus Angka dengan Format YYYYMM dan Panjang 6 Digit. Nilai MM tidak boleh lebih besar dari 12");
            }
        
            else
            {
                //in Beginning, set all UserFlag to 0
                sdConnection.sSave("update AHMFARPA_HISJOBS set IUSERFLAG=0");

                //====================================================================================================
                //step below will set the execute_uipath parameter in database to 1 

                sTimer1_Pending(false);

                cWinForm_Tools.sCursor_Change();
                #region change status
                var oParam1 = new DataModel.tPARAM();
                oParam1.VPARAMNAME = "EXECUTE_UIPATH";
                oParam1.VPARAMVAL = "1";
                oParam1.oBasic_Operation.Update();
                sStatus_UIPath_Update("");

                #endregion






                int i = 1;

                foreach (TreeNode oNode in treeView1.Nodes)
                {
                    foreach (TreeNode oPlants in oNode.Nodes)
                    {

                        if (oPlants.Checked)
                        {

                            string vStatus = sdConnection.sRead_2_Text("SELECT TOP 1 VSTATUSID FROM AHMFARPA_HISJOBS WHERE IPERIOD=" + vPeriod + " and ISTEPID=" + oNode.Tag + " and VPLANTID='" + oPlants.Tag.ToString() + "'");


                            if (vStatus == "S")
                            {
                                MessageBox.Show("Step " + oNode.Tag + " Pada Plant: " + oPlants.Tag.ToString() + " tidak boleh diexecute karena Status=S. Mohon update status terlebih dahulu");


                                oParam1.VPARAMVAL = "0";
                                oParam1.oBasic_Operation.Update();
                                sStatus_UIPath_Update("");

                                goto End;

                            }

                        }



                        if (oPlants.Checked)
                        {

                            var oJobHistory = new DataModel.tJOB_HISTORY();
                            oJobHistory.IPERIOD = q.sInt_Try_Parse(vPeriod);
                            oJobHistory.ISTEPID = q.sInt_Try_Parse(oNode.Tag);
                            oJobHistory.VPLANTID = oPlants.Tag.ToString();
                            oJobHistory.IUSERFLAG = 1;
                            oJobHistory.DLASTUPD = q.sDateTime_Now();
                            oJobHistory.oBasic_Operation.Upsert();
                        }


                    } // end of inner foreach (plant)

                    i++;
                } //end of outer foreach (step)

                var oParam = new DataModel.tPARAM();
                oParam.VPARAMNAME = "PERIOD";
                oParam.VPARAMVAL = vPeriod;
                oParam.oBasic_Operation.Upsert();


                cWinForm_Tools.sCursor_Change(true);
                sTimer_Set(true);

                //this part is disabled to make sure that the job in orchestrator is not run automatically.
                //=================================================================================================================================
                // the step below will get token and szend request to orchestrator to start the job
                 
                try
                {

                    string vUsername = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='WinForm_Username'");
                    
                    string vPassword = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='WinForm_Password'");
                    
                    string vTenant = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='WinForm_Tenant'");
                    

                    string token = Logic.cBackend.GetToken(vUsername, vPassword, vTenant);

                    Console.WriteLine("TOKEN="+token);
                    
                 Logic.cBackend.SendRequest(token);


                }

                catch (Exception we)
                {

                    if (we.Message.Contains("400"))
                    {
                        MessageBox.Show("Error code 400. Detail Error: " + we.Message);
                    }

                    else
                    {
                        MessageBox.Show(we.Message + ". Please check the following items to troubleshoot:" + Environment.NewLine + "1. Connection to Orchestrator" + Environment.NewLine + "2. Username, Password, and Tenant Name that is stored in Database for login to Orchestrator");

                    }
                }//end of try catch

                
    
                
                   

                chkTimer.Checked = false;

                End:;
                sSearch(false); 

            } //end of else

        } //end of function button Ok (Execute) Click



        

        private void timer1_Tick(object sender, EventArgs e)
        {
            string vFlag = vExecute_UIPath;
            sSearch(false);
            timer1.Interval = 6000;

        }

        private void chkTimer_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTimer.Checked == false)
            {
                sTimer_Set(false);
            } else
            {
                sTimer_Set(true);
            }
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (q.sString_Find(e.Node.Text, ": S :"))
            {
                if (! e.Node.Checked)
                {
                    e.Cancel = false;
                }
            }
        }


        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            sCheckTreeView(e.Node, e.Node.Checked);
        }

        void sCheckTreeView(TreeNode oNode, Boolean isChecked)
        {
            foreach (TreeNode item in oNode.Nodes)
            {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0)
                {
                    sCheckTreeView(item, isChecked);
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string vCommand = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='0_Excel_Log_File_Path'");
            Boolean isExist = q.sFile_IsExist(vCommand);
            if(isExist) System.Diagnostics.Process.Start(vCommand);
        }

        private void buttonReset_Click_1(object sender, EventArgs e)
        {
            string vPeriod = txtPeriod.Text;
            if (vPeriod != "")
            {
                string vSQL = "DELETE FROM AHMFARPA_HISJOBS WHERE IPERIOD=" + vPeriod;
                sdConnection.sSave(vSQL);
                vSQL = "UPDATE AHMFARPA_MSTPARAMS SET VPARAMVAL=0 WHERE VPARAMNAME='EXECUTE_UIPATH'";
                sdConnection.sSave(vSQL);
                sTimer_Set(false);
                sSearch(true);
            }
        }

        

        private void BtnReset_Click(object sender, EventArgs e)
        {
            sTimer1_Pending(false);

            cWinForm_Tools.sCursor_Change();
            #region change status
            var oParam1 = new DataModel.tPARAM();
            oParam1.VPARAMNAME = "EXECUTE_UIPATH";
            oParam1.VPARAMVAL = "0";
            oParam1.oBasic_Operation.Update();
            sStatus_UIPath_Update("");

            #endregion

            string vPeriod = txtPeriod.Text;
            int i = 1;
            foreach (TreeNode oNode in treeView1.Nodes)
            {
                foreach (TreeNode oPlants in oNode.Nodes)
                {
                    if (oPlants.Checked)
                    {
                        var oJobHistory = new DataModel.tJOB_HISTORY();
                        oJobHistory.IPERIOD = q.sInt_Try_Parse(vPeriod);
                        oJobHistory.ISTEPID = q.sInt_Try_Parse(oNode.Tag);
                        oJobHistory.VPLANTID = oPlants.Tag.ToString();
                        oJobHistory.VSTATUSID = "";
                        oJobHistory.VSTATUSDESC = "";
                        oJobHistory.IUSERFLAG = 0;
                        oJobHistory.DLASTUPD = q.sDateTime_Now();
                        oJobHistory.oBasic_Operation.Upsert();
                        
                    }

                }

                i++;
            }

            

            int PeriodIsNumber = 1;
            int CountPeriod = 0;
            int Last2DigitPeriod = 0;
            CountPeriod = vPeriod.Length;
            Last2DigitPeriod = q.sString_2_Integer((vPeriod.Substring(CountPeriod - 2)));
            //row below is trying to parse the vPeriod into the number. If the parsing is failed, then the PeriodISnumber will become 0, otherwise the PeriodIsNumber=vPeriod 
            int.TryParse(vPeriod, out PeriodIsNumber);


            if (PeriodIsNumber == 0 || CountPeriod != 6 || Last2DigitPeriod > 12) //If Period Number is 0, it means that vPeriod is not Integer
            { MessageBox.Show("Nilai Text Box di Period Harus Angka dengan Format YYYYMM dan Panjang 6 Digit dengan Nila MM tidak boleh lebih besar dari 12 "); }
            else
            {
                var oParam = new DataModel.tPARAM();
                oParam.VPARAMNAME = "PERIOD";
                oParam.VPARAMVAL = vPeriod;
                oParam.oBasic_Operation.Upsert();


                cWinForm_Tools.sCursor_Change(true);
                sTimer_Set(true);
                chkTimer.Checked = false;



                //Do Refresh Screen after reset db job
                //Logic explanation, if  EXECUTE_UIPATH = 1, then get search period value from database
                //else get the period value from textbox (because when the job is running, it is not possible to get other period
                string vFlag = sdConnection.sRead_2_Text("SELECT VPARAMVAL FROM AHMFARPA_MSTPARAMS WHERE VPARAMNAME='EXECUTE_UIPATH'");
                if (vFlag == "1")
                {

                    txtPeriod.Text = vPeriod;
                }
                sSearch(false);
            }

        }

        private void lblStatus_Description_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
 