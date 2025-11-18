using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmLicenseHistory : Form
    {
        public frmLicenseHistory(int personID)
        {
            InitializeComponent();
            PersonInfoCard.Person = Person.Find(personID);
            DriverLicenses.LoadLicenses(personID);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
