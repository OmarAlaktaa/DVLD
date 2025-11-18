using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static DVLD_BusinessLayer.Validations;

namespace DVLD
{
    public partial class ctrlPersonCard : UserControl
    {
        int PersonID = -1;
        public Person person = null;
        PersonValidations PersonValid = new PersonValidations();
        private bool ImageChoosed = false;

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public ctrlPersonCard(Person person)
        {
            InitializeComponent();
            this.person = person;
        }

        public event Action<int> OnNewPersonSaved;

        protected virtual void PersonSaved(int ID)
        {
            Action<int> handler = OnNewPersonSaved;
            if (handler != null)
            {
                handler(ID); // Raise event with the parameter
            }
        }

        public event Action OnCloseButtonClick;
        protected virtual void btnClose_Click()
        {
            Action handler = OnCloseButtonClick;
            handler?.Invoke();
        }

        private void PersonCard_Load(object sender, EventArgs e)
        {
            LoadCountriesNamesToComboBox();
            PresentPersonInfo();
            Validation();
        }

        private void LoadCountriesNamesToComboBox()
        {
            List<string> listCountriesNames = Countries.GetAllCountries();
            cbCountries.DataSource = listCountriesNames;
        }

        private void CheckGendorBoxes()
        {
            switch (person.Gendor)
            {
                case "Male":
                {
                    rbMale.Checked = true;
                    break;
                }
                case "Female":
                {
                    rbFemale.Checked = true;
                    break;
                }
            }
        }

        private void ChooseCountry()
        {
            string CountryName = Countries.GetCountryName(person.NationalityCountryID);
            if (CountryName == null)
            {
                MessageBox.Show("Error!, \n(try to contact Omar.Ak)");
            }
            else
            {
                cbCountries.SelectedItem = CountryName;
            }
        }

        private void PresentPersonInfo()
        {
            if (person == null)
                return;
            else
            {
                tbNationalNo.Text = person.NationalNumber;
                tbFirstName.Text = person.FirstName;
                tbSecondName.Text = person.SecondName;
                tbThirdName.Text = person.ThirdName;
                tbLastName.Text = person.LastName;
                dtpDateOfBirth.Value = person.DateOfBirth;
                CheckGendorBoxes();
                tbPhone.Text = person.Phone;
                tbEmail.Text = person.Email;
                ChooseCountry();
                tbAddress.Text = person.Address;
                pbPersonImage.ImageLocation = person.ImagePath;
            }

        }

        private void Validation()
        {
            dtpDateOfBirth.MaxDate = DateTime.Now - new TimeSpan(18 * 365, 0, 0, 0, 0);
            ofdPersonImage.Filter = "Image Files (*.jpg;*.jpeg;*.png;)|*.jpg;*.jpeg;*.png;";
        }

        private void ValidateUniqueNationalNumber()
        {
            if (People.IsNationalNumberExist(tbNationalNo.Text))
            {
                // Selected NationalNo Already Exist.
                PersonValid.ValidNationalNumber = false;
                epNationalNo.SetError(tbNationalNo, "National Number is used for another person!");
            }
            else
            {
                PersonValid.ValidNationalNumber = true;
                epNationalNo.Dispose();
            }
        }

        private void tbNationalNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateNationalNumber();
        }

        private void tbEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateEmailFormat();
        }

        private bool EmailValidFormat(string E)
        {
            return Email.isValidFormat(E);
        }

        private void ValidateEmailFormat()
        {
            if (!EmailValidFormat(tbEmail.Text))
            {
                PersonValid.ValidEmail = false;
                epEmail.SetError(tbEmail, "Invalid Email Address Format!");
            }
            else
            {
                PersonValid.ValidEmail = true;
                epEmail.Dispose();
            }
        }

        private void tbAddress_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateAddress();
        }

        private void ValidateAddress()
        {
            if (!ValidateString.isValide(tbAddress.Text))
            {
                PersonValid.ValidAddress = false;
                epAddress.SetError(tbAddress, "Address Cannot be Empty!");
            }
            else
            {
                PersonValid.ValidAddress = true;
                epAddress.Dispose();
            }
        }

        public void ValidatePhone()
        {
            if (!ValidateString.isValide(tbPhone.Text)) // Phone field is not valid
            {
                PersonValid.ValidPhone = false;
                epPhone.SetError(tbPhone, "Phone field cannot be empty!");
            }
            else
            {
                PersonValid.ValidPhone = true;
                epPhone.Dispose();
            }
        }

        private void tbPhone_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidatePhone();
        }

        public bool ValidateNameFields()
        {
            return 
                PersonValid.ValidName =
                ValidateString.isValide(tbFirstName.Text)
                && ValidateString.isValide(tbSecondName.Text)
                && ValidateString.isValide(tbThirdName.Text)
                && ValidateString.isValide(tbLastName.Text); ;
        }

        private void ValidateUpdateNationalNumber()
        {
            if (tbNationalNo.Text == this.person.NationalNumber)
            {
                PersonValid.ValidNationalNumber = true;
                return;
            }
            else
            {
                ValidateUniqueNationalNumber();
            }
        }

        private void ValidateNationalNumber()
        {
            if (person == null || this.person.Mode != Person.Modes.eUpdate)
            {
                ValidateUniqueNationalNumber();
            }
            else
            {
                ValidateUpdateNationalNumber();
            }
        }

        public void ValidateFields()
        {
            ValidateNationalNumber();
            ValidateNameFields();
            ValidatePhone();
            ValidateEmailFormat();
            ValidateAddress();
        }

        private bool AllFieldsValid()
        {
            ValidateFields();
            return PersonValid.AllValid;
    
        }

        public string getCheckedGendor()
        {
            if (rbMale.Checked)
            {
                return "Male";
            }
            else if (rbFemale.Checked)
            {
                return "Female";
            }

            return null;
        }

        private string ChoosedImageLocation()
        {
            string ImageLocation = string.Empty;
            if (ImageChoosed)
            {
                ImageLocation = Image.StoreImage(pbPersonImage.ImageLocation);
            }
            return ImageLocation;
        }

        public Person ReadNewPersonInfoFromCard()
        {
            string ImageLocation = ChoosedImageLocation();
            Person P = new Person(tbNationalNo.Text, tbFirstName.Text,
                tbSecondName.Text, tbThirdName.Text, tbLastName.Text,
                dtpDateOfBirth.Value, getCheckedGendor(), tbAddress.Text,
                tbPhone.Text, tbEmail.Text, Countries.GetCountryID(cbCountries.SelectedItem.ToString()),
                ImageLocation);

            return P;
        }
       
        public void ReadUpdatedPersonInfo()
        {
            if (person == null)
                return;

            person.NationalNumber = tbNationalNo.Text;
            person.FirstName = tbFirstName.Text;
            person.SecondName = tbSecondName.Text;
            person.ThirdName = tbThirdName.Text;
            person.LastName = tbLastName.Text;
            person.DateOfBirth = dtpDateOfBirth.Value;
            person.Address = tbAddress.Text;
            person.Phone = tbPhone.Text;
            person.Email = tbEmail.Text;
            person.NationalityCountryID = Countries.GetCountryID(cbCountries.SelectedItem.ToString());
            person.ImagePath = ChoosedImageLocation();
            person.Mode = Person.Modes.eUpdate;
        }

        public void SavePersonInfo()
        {
            if (person == null)
            {
                person = ReadNewPersonInfoFromCard();
                if (person == null)
                {
                    MessageBox.Show("Error!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ReadUpdatedPersonInfo();
            }

            if (person.Save(ref PersonID))
            {
                MessageBox.Show($"Person Information Saved Successfully!\nPerson ID : {PersonID}",
                    "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PersonSaved(PersonID); // Fire Action (Person Saved)
            }
            else
            {
                MessageBox.Show("Error while saving new data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveInfo()
        {
            if (AllFieldsValid())
            {
                SavePersonInfo();
            }
            else
            {
                MessageBox.Show("Make sure you fill all fields!",
                   "Validating Error!", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        private void llImagePath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ofdPersonImage.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.ImageLocation = ofdPersonImage.FileName;
                ImageChoosed = true;
            }
            else
            {
                ImageChoosed = false;
            }
        }

        private void ChangeDefaultImage()
        {
            if (ImageChoosed)
            {
                return;
            }
            if (rbMale.Checked)
            {
                pbPersonImage.Image = DVLD.Properties.Resources.Male_512;
            }
            else if (rbFemale.Checked)
            {
                pbPersonImage.Image = DVLD.Properties.Resources.Female_512;
            }
            else
            {
                MessageBox.Show("Error while loading Image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDefaultImage();
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDefaultImage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnClose_Click();
        }
    }
}
