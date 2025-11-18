using DVLD_BusinessLayer;
using System.Windows.Forms;
using System;

namespace DVLD
{
    public partial class ctrlPersonInfoCard : UserControl
    {
        public ctrlPersonInfoCard()
        {
            InitializeComponent();
        }

        public ctrlPersonInfoCard(int PersonID)
        {
            InitializeComponent();
            LoadPersonInfo(PersonID);
        }

        private Person _person;

        public Person Person
        {
            get { return _person; }
            set
            {
                if (value == null) return;
                this._person = value;
                LoadPersonInfo(value);
            }
        }

        public event Action<int> OnPersonSaved;

        protected virtual void PersonSaved(int SavedPersonID)
        {
            Action<int> handler = OnPersonSaved;
            if (handler != null)
            {
                handler(SavedPersonID);
            }
        }

        public void LoadPersonInfo(int PersonID)
        {
            Person P = Person.Find(PersonID);
            LoadPersonInfo(P);
        }

        public void LoadPersonInfo(Person P)
        {
            if (P == null)
            {
                //MessageBox.Show("Error While Loading Person!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                lblID.Text = "[???]";
                lblName.Text = "???";
                lblNationalNumber.Text = "???";
                lblGender.Text = "???";
                lblEmail.Text = "???";
                lblAddress.Text = "???";
                lblDateOfBirth.Text = "???";
                lblPhoneNumber.Text = "???";
                lblCountry.Text = "???";
                pbPersonImage.Image = null;
            }
            else
            {
                _person = P;
                lblID.Text = P.PersonID.ToString();
                lblName.Text = GetFullName(ref P);
                lblNationalNumber.Text = P.NationalNumber;
                lblGender.Text = P.Gendor;
                lblEmail.Text = P.Email;
                lblAddress.Text = P.Address;
                lblDateOfBirth.Text = P.DateOfBirth.ToShortDateString();
                lblPhoneNumber.Text = P.Phone;
                lblCountry.Text = Countries.GetCountryName(P.NationalityCountryID);
                pbPersonImage.ImageLocation = P.ImagePath;
            }
        }

        public string GetFullName(ref Person person)
        {
            return person.FirstName + " " + person.SecondName + " " + person.ThirdName + " " + person.LastName;
        }

        private void llEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Person == null)
            {
                MessageBox.Show("Sorry, No Person Loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                frmEditAddNewPerson EditForm = new frmEditAddNewPerson(Person.PersonID);
                EditForm.PersonSaved += PersonEdited;
                EditForm.PersonSaved += PersonSaved;
                EditForm.Show();
            }
        }

        public void ClearCard()
        {
            Person = null;
            lblID.Text = "???";
            lblName.Text = "???"; 
            lblNationalNumber.Text = "???";
            lblGender.Text = "???";
            lblEmail.Text = "???";
            lblAddress.Text = "???";
            lblDateOfBirth.Text = "???";
            lblPhoneNumber.Text = "???";
            lblCountry.Text = "???";
            pbPersonImage.Image = null;
        }

        private void PersonEdited(int PersonID)
        {
            _person = Person.Find(PersonID);
            LoadPersonInfo(_person);
        }
    }
}
