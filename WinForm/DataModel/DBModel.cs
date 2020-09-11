using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Database.DataModel;

namespace DataModel
{
	[cAttribute_Table(vConnection_Number=0, vTable_Name= "AHMFARPA_MSTPARAMS")]
	public class tPARAM : cTable_Operation_PK
	{
		[cAttribute_Table(isIdentity=true)]
		public String VPARAMNAME;
		public String VPARAMVAL;
		public String VPARAMDESC;

        public String VCREA;
        public DateTime? DCREA;
        public String VMODI;
        public DateTime? DMODI;
    }

	[cAttribute_Table(vConnection_Number=0, vTable_Name= "AHMFARPA_MSTSTEPS")]
	public class tSTEP : cTable_Operation_PK
	{
		[cAttribute_Table(isIdentity=true)]
		public Int32 ISTEPID;
		public String VSTEPNAME;
		public String VSTEPPATH;
		public Int32? ISTEPSEQ;
        public Int32? ISTEPTYPE;

        public String VCREA;
        public DateTime? DCREA;
        public String VMODI;
        public DateTime? DMODI;
    }

	[cAttribute_Table(vConnection_Number=0, vTable_Name= "AHMFARPA_MSTPLANTS")]
	public class tPLANT : cTable_Operation_PK
	{
		[cAttribute_Table(isIdentity=true)]
		public String VPLANTID;
        public String VPLANTDESC;
        public String VPLANTSTEP;

        public String VCREA;
        public DateTime? DCREA;
        public String VMODI;
        public DateTime? DMODI;
    }

	[cAttribute_Table(vConnection_Number=0, vTable_Name= "AHMFARPA_HISJOBS")]
	public class tJOB_HISTORY : cTable_Operation_PK
	{
		[cAttribute_Table(isIdentity=true)]
		public Int32 IPERIOD;
		[cAttribute_Table(isIdentity=true)]
		public Int32 ISTEPID;
		[cAttribute_Table(isIdentity=true)]
		public String VPLANTID;
		public String VSTATUSID;
		public String VSTATUSDESC;
		public Int32? IUSERFLAG;
		public DateTime? DLASTUPD;
        public DateTime? DTIMESTART;
        public DateTime? DTIMEEND;

        public String VCREA;
        public DateTime? DCREA;
        public String VMODI;
        public DateTime? DMODI;
    }



    [cAttribute_Table(vConnection_Number = 0, vTable_Name = "AHMFARPA_LOGDETAIL")]
    public class tLOG_DETIL : cTable_Operation_PK
    {
        [cAttribute_Table(isIdentity = true)]
        public Int32 ISTEPID;
        [cAttribute_Table(isIdentity = true)]
        public String VPLANTID;
        public DateTime? DLOGTIME;
        public String VLOGDESC;

        public String VCREA;
        public DateTime? DCREA;
        public String VMODI;
        public DateTime? DMODI;
    }
}