using BusinessLayer;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }
        private bool _FilterEnable = true;
        public bool FilterEnable
        {
            get { return _FilterEnable; }
            set
            {
                _FilterEnable = value;
                gbFilter.Enabled = _FilterEnable;
            }
        }
        
        public int PersonID { get; private set; } = -1;
        //public clsPerson SelectedPersonInfo { get { return ctrlPersonCard1.SelectedPersonInfo; } }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void _InvokeDelegate()
        {
            if (FilterEnable && this.PersonID != -1)
            {
                OnPersonSelected?.Invoke(this.PersonID);
            }
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbSearchPerson.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
            this.PersonID = ctrlPersonCard1.PersonID;

            _InvokeDelegate();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            cbSearchPerson.SelectedIndex = 0;
            txtFilterValue.Text = NationalNo;
            ctrlPersonCard1.LoadPersonInfo(NationalNo);
            this.PersonID = ctrlPersonCard1.PersonID;

            _InvokeDelegate();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
            }
            if (cbSearchPerson.Text == "Person ID")
            {
                // السماح فقط بالأرقام (IsDigit) وزر المسح Backspace (IsControl)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // إلغاء الضغطة تماماً وكأنها لم تحدث!
                }
            }
        }

        private bool CheckIfTextBoxValueIsValidToSearch()
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text))
                return false;

            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            switch (cbSearchPerson.Text)
            {
                case "National No":
                    if (CheckIfTextBoxValueIsValidToSearch())
                    {
                        LoadPersonInfo(txtFilterValue.Text.Trim());
                    }
                    break;
                case "Person ID":
                    if (CheckIfTextBoxValueIsValidToSearch())
                    {
                        //LoadPersonInfo(int.Parse(txtFilterValue.Text));
                        LoadPersonInfo(Convert.ToInt32(txtFilterValue.Text.Trim()));
                    }
                    break;
            }
        }

        private void Frm_DataBack(object sender, int PersonID)
        {
            // Update the filter to display the new ID
            cbSearchPerson.SelectedIndex = 1; 
            txtFilterValue.Text = PersonID.ToString();

            // Call the search function to display the person's card 
            LoadPersonInfo(PersonID);

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(-1);
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbSearchPerson.SelectedIndex = 1;
            txtFilterValue.Focus();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
    }
}