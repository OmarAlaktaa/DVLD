using DVLD_BusinessLayer;
using System.Windows.Forms;
using System;

namespace DVLD
{
    public partial class ctrlApplicationInfo : UserControl
    {
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private DVLD_BusinessLayer.Application _application;

        public DVLD_BusinessLayer.Application application
        {
            get
            {
                return _application;
            }
            set
            {
                if (_application != value)
                {
                    _application = value;
                    FillApplicationInfo(value);
                }
            }
        }

        public void LoadApplication(int ApplicationID)
        {
            application = new DVLD_BusinessLayer.Application(ApplicationID);
        }

        public void FillApplicationInfo(DVLD_BusinessLayer.Application Application)
        {
            if (Application == null) return;
            lblApplicationID.Text = Application.ID.ToString();
            lblStatus.Text = Application.ApplicationStatus.ToString();
            lblFees.Text = Application.PaidFees.ToString();
            lblType.Text = Application.ApplicationType.ApplicationTypeTitle;
            lblApplicant.Text = Person.Find(Application.ApplicantPersonID).FullName;
            lblDate.Text = Application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = Application.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = User.Find(Application.CreatedByUserID).Username;
        }

        private void llPersonDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails Form = new frmPersonDetails(application.ApplicantPersonID);
            Form.PersonSaved += ReloadPersonInfo;
            Form.PersonSaved += PersonSaved;
            Form.ShowDialog(this);
        }

        private void ReloadPersonInfo(int PersonID)
        {
            this.application.ApplicantPersonID = PersonID;
            lblApplicant.Text = Person.Find(PersonID).FullName;
        }

        public event Action<int> OnPersonSaved;
        
        protected virtual void PersonSaved(int PersonID)
        {
            Action<int> handler = OnPersonSaved;
            if (handler != null)
            {
                handler(PersonID);
            }
        }
    
    }
}
