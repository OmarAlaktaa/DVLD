using DVLD_BusinessLayer;
using System.Windows.Forms;
using System;

namespace DVLD
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private LDLApplication _ldlapplication;

        public LDLApplication LDLApplication
        {
            get
            {
                return _ldlapplication;
            }
            set
            {
                _ldlapplication = value;
                if (value!= null)
                   FillLDLApplicationInfo(value);
            }
        }

        private void FillLDLApplicationInfo(LDLApplication LDLApplication)
        {
            lblDLAppID.Text = LDLApplication.ID.ToString();
            lblAppliedForLicense.Text = LDLApplication.DrivingClass.ClassName;
            lblPassedTests.Text = LDLApplication.PassedTests.ToString() + "/3";
            ApplicationInfo.application = LDLApplication.Application;// (set) property automaticlly display ApplicationInfo
        }

        public event Action<int> OnPersonEdited;

        protected virtual void ApplicationInfo_OnPersonSaved(int obj)
        {
            Action<int> PersonEditedHandler = OnPersonEdited;
            if (PersonEditedHandler != null) 
            {
                PersonEditedHandler(obj);
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
