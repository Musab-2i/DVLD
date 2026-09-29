using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.GlobalClass;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.People
{
    public partial class frmAddUpdatePerson : frmBase
    {

        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);
        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNewPerson = 1, UpdatePerson = 2 }
        public enum enGender { Male = 0, Female = 1 }
        private enMode Mode;
        int _PersonID;
        clsPerson _Person;
        public frmAddUpdatePerson(int PeronsID)
        {
            InitializeComponent();
            _PersonID = PeronsID;
            Mode = _PersonID == -1 ? enMode.AddNewPerson : enMode.UpdatePerson;
        }

        private void _FillCountriesInCompoBox()
        {
            DataTable Countries = clsCountries.GetCountriesList();
            foreach (DataRow Country in Countries.Rows)
            {
                cbCountry.Items.Add(Country["CountryName"]);
            }
            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");
        }
        private void _MaleFemaleRadioImage()
        {
            if (_Person.Gender == 0)
            {
                rdMale.Checked = true;
                if(string.IsNullOrEmpty(_Person.ImagePath) && !File.Exists(_Person.ImagePath))
                {
                    pbPersonImage.Image = Resources.Male_512;
                }
                else
                {
                    pbPersonImage.ImageLocation = _Person.ImagePath;
                }
            }
            else
            {
                rdFemale.Checked = true;
                if (string.IsNullOrEmpty(_Person.ImagePath) && !File.Exists(_Person.ImagePath))
                {
                    pbPersonImage.Image = Resources.Female_512;
                }
                else
                {
                    pbPersonImage.ImageLocation = _Person.ImagePath;
                }
            }
        }
        private void _FillPersonInfoInControls()
        {
            //--1
            lblPersonID.Text =_Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            //--2
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            //--3
            _MaleFemaleRadioImage();
            txtPhoneNo.Text = _Person.PhoneNo;
            //--4
            txtEmail.Text = _Person.Email;
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountries.Find(_Person.NationalCountryID).CountryName);
            //--5
            txtAddress.Text = _Person.Address;

        }
        private void _SetDateOfBirthSettings()
        {
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
            dtpDateOfBirth.CustomFormat = "dd/MM/yyyy";
        }

        private void _ResetDefualtValue()
        {
            _FillCountriesInCompoBox();
            _SetDateOfBirthSettings();

            if (Mode == enMode.AddNewPerson)
            {
                this.Text = "Add New Person";
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
                return;
            }

        }
        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if(_Person == null)
            {
                MessageBox.Show($"This Form Will be closed because No Person With ID: {_PersonID}");
                this.Close();
                return;
            }
            this.Text = "Edit Person";
            lblMode.Text = $"Edit Person With ID: {_PersonID}";
            _FillPersonInfoInControls();

            // if Image Is Default Image Hide #RemoveLabel
            lblRemoveImage.Visible = !string.IsNullOrEmpty(_Person.ImagePath);

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValue();

            if (Mode == enMode.UpdatePerson)
            {
                _LoadData();
            }
        }

        private void _FillPersonInfoInPersonRecord()
        {
            int CountryID = clsCountries.Find(cbCountry.Text).CountryID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();

            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            // The ternary operator for gender assignment based on buttons
            _Person.Gender = rdMale.Checked ? (byte)enGender.Male : (byte)enGender.Female;

            _Person.PhoneNo = txtPhoneNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.NationalCountryID = CountryID;
            _Person.Address = txtAddress.Text.Trim();

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";
        }

        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                // If there is an old image stored for the object, it should be deleted from the system to free up space.
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("The file could not be deleted: " + ex.Message);
                    }
                }

                // If the user selects a new image (the path is not null)
                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();
                    if (clsUtil.CopyImageToProjectImageFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                // إظهار رسالة التنبيه التي طلبتها في حال وجود خطأ
                MessageBox.Show("Some fields are invalid! Please fix the errors before saving.",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                return; // إيقاف عملية الحفظ تماماً ومنع الوصول لقاعدة البيانات
            }

            if (!_HandlePersonImage())
                return;

            _FillPersonInfoInPersonRecord();

            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Text = "Edit Person";
                Mode = enMode.UpdatePerson;
                lblMode.Text = $"Edit Person With ID: {_Person.PersonID}";
                lblPersonID.Text = _Person.PersonID.ToString();

                // It returns the Person ID to any screen that calls it.
                DataBack?.Invoke(this,_Person.PersonID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Title = "Choose Contact Image";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbPersonImage.ImageLocation = openFileDialog1.FileName;
                    lblRemoveImage.Visible = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while processing the image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pbPersonImage.ImageLocation))
            {
                if (rdMale.Checked)
                {
                    pbPersonImage.Image = Resources.Male_512;
                }
                else
                {
                    pbPersonImage.Image = Resources.Female_512;
                }
            }
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(txt.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "This field is required!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, null);
            }
        }

        private void lblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rdMale.Checked)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }
            lblRemoveImage.Visible = false;
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
            // Protection against modification: We check the database "only" if the user has changed their national ID number
            // Or if it's a new person (_Person.NationalNo will be empty)
            if (txtNationalNo.Text.Trim() != _Person.NationalNo)
            {
                if (clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtNationalNo, "National Number is already used for another person!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            // Allow NULL In DataBase
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                return;

            if (!clsValidations.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAddress, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtAddress, null);
            }
        }
    }
}