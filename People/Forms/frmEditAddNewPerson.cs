using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmEditAddNewPerson : Form
    {
        public frmEditAddNewPerson()
        {
            InitializeComponent();
        }

        public frmEditAddNewPerson(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
            ctrlPersonCard1.person = Person.Find(PersonID);
        }

        public delegate void PersonSavedEventHandler(int SavedPersonID);

        public event PersonSavedEventHandler PersonSaved;

        public int PersonID;

        private void frmEditAddNewPerson_Load(object sender, System.EventArgs e)
        {
            IntializePerson();
        }

        public void IntializePerson()
        {
            if (ctrlPersonCard1.person != null)
            {
                lblID.Text = ctrlPersonCard1.person.PersonID.ToString();
                lblEditAddNewPersonHeader.Text = "Update Person";
            }
            else
            {
                lblID.Text = "N / A";
                lblEditAddNewPersonHeader.Text = "Add New Person";
            }
        }
         
        private void ctrlPersonCard1_OnPersonSaved(int obj)
        {
            lblID.Text = obj.ToString();
            PersonSaved?.Invoke(obj);
        }

        private void ctrlPersonCard1_OnCloseButtonClick()
        {
            this.Close();
        }
    }
}
