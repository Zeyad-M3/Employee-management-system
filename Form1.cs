using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Employee_management_system
{
    public partial class Form1 : Form
    {
        private object notifyIcon1;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelEmployeeID_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmployeeID.Text) || string.IsNullOrEmpty(textBoxFullName.Text) || string.IsNullOrEmpty(textBoxJob.Text) || string.IsNullOrEmpty(textBoxSalary.Text) || string.IsNullOrEmpty(textBoxDepartment.Text) || string.IsNullOrEmpty(TextBoxHiredate.Text) || string.IsNullOrEmpty(TextBoxNumber.Text) || string.IsNullOrEmpty(textBoxContry.Text))
            {
                return;
            }

            ListViewItem item = new ListViewItem(textBoxEmployeeID.Text.Trim());
            item.SubItems.Add(textBoxFullName.Text.Trim());
            item.SubItems.Add(textBoxDepartment.Text.Trim());
            item.SubItems.Add(textBoxJob.Text.Trim());
            item.SubItems.Add(textBoxSalary.Text.Trim());
            item.SubItems.Add(TextBoxHiredate.Text.Trim());
            item.SubItems.Add(TextBoxNumber.Text.Trim());
            item.SubItems.Add(textBoxContry.Text.Trim());
            item.SubItems.Add(textBoxEmployeeID.Text.Trim());

            if (rbman.Checked)
                item.ImageIndex = 0;
            else
                item.ImageIndex = 1;



            listView1.Items.Add(item);

            string iconPath = @"C:\Users\A.S\Downloads\social-media.ico";
            if (System.IO.File.Exists(iconPath))
            {
                notifyIcon2.Icon = new Icon(iconPath);
            }
            else
            {
                notifyIcon2.Icon = SystemIcons.Application; // أيقونة افتراضية إذا لم يوجد الملف
            }
            notifyIcon2.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon2.BalloonTipTitle = "Client Added";
            notifyIcon2.BalloonTipText = "Client has been added to your app";
            notifyIcon2.ShowBalloonTip(1000);

            textBoxEmployeeID.Clear();
            textBoxFullName.Clear();
            textBoxDepartment.Clear();
            textBoxJob.Clear();
            textBoxSalary.Clear();
            TextBoxHiredate.Clear();
            TextBoxNumber.Clear();
            textBoxContry.Clear();

        }

        private void textBoxEmployeeID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEmployeeID.Text))
            {
                e.Cancel = true;
                textBoxEmployeeID.Focus();
                errorProvider1.SetError(textBoxEmployeeID, "FirstName should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBoxEmployeeID, "");
            }
        }

        private void textBoxEmployeeID_TextChanged(object sender, CancelEventArgs e)
        {

        }

        private void textBoxEmployeeID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFullName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFullName.Text))
            {
                e.Cancel = true;
                textBoxFullName.Focus();
                errorProvider1.SetError(textBoxFullName, "Full Name should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBoxFullName, "");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDepartment_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDepartment.Text))
            {
                e.Cancel = true;
                textBoxDepartment.Focus();
                errorProvider1.SetError(textBoxDepartment, "Department should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBoxDepartment, "");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxJob_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxJob.Text))
            {
                e.Cancel = true;
                textBoxJob.Focus();
                errorProvider1.SetError(textBoxJob, "Job should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBoxJob, "");
            }
        }

        private void textBoxSalary_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSalary.Text) || !decimal.TryParse(textBoxSalary.Text, out _))
            {
                e.Cancel = true;
                textBoxSalary.Focus();
                errorProvider1.SetError(textBoxSalary, "Salary should have a valid value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBoxSalary, "");
            }
        }

        private void TextBoxHiredate_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxHiredate.Text) || !DateTime.TryParse(TextBoxHiredate.Text, out _))
            {
                e.Cancel = true;
                TextBoxHiredate.Focus();
                errorProvider1.SetError(TextBoxHiredate, "Hire Date should have a valid value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(TextBoxHiredate, "");
            }
        }

        private void TextBoxNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxNumber.Text) || !long.TryParse(TextBoxNumber.Text, out _) || TextBoxNumber.Text.Length != 11)
            {
                e.Cancel = true;
                TextBoxNumber.Focus();
                errorProvider1.SetError(TextBoxNumber, "Number should have a valid value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(TextBoxNumber, "");
            }
        }

        private void textBoxContry_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxContry.Text))
            {
                e.Cancel = true;
                textBoxContry.Focus();
                errorProvider1.SetError(textBoxContry, "Country should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBoxContry, "");
            }
        }

        private void TextBoxNumber_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxEmployeeID.Clear();
            textBoxFullName.Clear();
            textBoxDepartment.Clear();
            textBoxJob.Clear();
            textBoxSalary.Clear();
            TextBoxHiredate.Clear();
            TextBoxNumber.Clear();
            textBoxContry.Clear();
            errorProvider1.Clear();

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(listView1.SelectedItems[0].Text);
        }

        private void rbSmallIcon_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;

        }

        private void rbDetailsView_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void rbLargeIcon_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;

        }

        private void rbList_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.List;

        }

        private void rbTile_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.Tile;

        }
        private void UpdatePicture()
        {
            if (rbman.Checked)
                pictureBox2.Image = imageList1.Images[0];
            else
                pictureBox2.Image = imageList1.Images[1];
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            UpdatePicture();


        }

        private void pictureBox2_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            UpdatePicture();
        }

        private void pictureBox2_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdatePicture();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdatePicture();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {


        }

        void UpdateLabelText(ListViewItem item)
        {
            if (item != null)
            {
                labelFullName.Text = item.SubItems[1].Text.Trim(); labelFullName.Visible = true; // العمود الثاني (Full Name)
                label16.Text = item.SubItems[2].Text.Trim(); label16.Visible = true;// العمود الثالث (Department)
                label17.Text = item.SubItems[3].Text.Trim(); label17.Visible = true; // العمود الرابع (Job)
                label18.Text = item.SubItems[4].Text.Trim(); label18.Visible = true; // العمود الخامس (Salary)
                label19.Text = item.SubItems[5].Text.Trim(); label19.Visible = true; // العمود السادس (Hire Date)
                label20.Text = item.SubItems[6].Text.Trim(); label20.Visible = true; // العمود السابع (Phone Number)
                label21.Text = item.SubItems[7].Text.Trim(); label21.Visible = true; // العمود الثامن (Country)
            }
            else
            {
                labelFullName.Text = "Not Found"; labelFullName.Visible = true;
                label16.Text = "Not Found";      ; label16.Visible = true;// العمو
                label17.Text = "Not Found";      ; label17.Visible = true; // العمود
                label18.Text = "Not Found";      ; label18.Visible = true; // العمود
                label19.Text = "Not Found";      ; label19.Visible = true; // العمود
                                                 ; label20.Visible = true; // العمود
                                                 ; label21.Visible = true; // العمود
                label20.Text = "Not Found";
                label21.Text = "Not Found";
            }

        }
       
                
                
        private void button4_Click(object sender, EventArgs e)
        {
            bool found = false;
            ListViewItem foundItem = null;

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Text == textBoxSerchId.Text.Trim())
                {
                    found = true;
                    foundItem = item;
                    break;
                }
            }

            if (found)
            {
                MessageBox.Show("Employee Found");
                UpdateLabelText(foundItem); // تمرير العنصر الموجود إلى الطريقة
            }
            else
            {

                MessageBox.Show("Employee Not Found");
                UpdateLabelText(null); // أو يمكنك مسح النصوص إذا لم يُعثر على العنصر
            }
        }

        private void button5_Click(object sender, EventArgs e)
        { 
          

            if (string.IsNullOrWhiteSpace(textBoxSerchId.Text))
            {
                MessageBox.Show("Please enter an ID to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool found = false;
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Text == textBoxSerchId.Text.Trim())
                {
                    if (MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return; // User chose not to delete
                    }

                    listView1.Items.Remove(item);
                    found = true;
                    MessageBox.Show("Employee deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Employee ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxEmployeeID.Clear();
            textBoxFullName.Clear();
            textBoxDepartment.Clear();
            textBoxJob.Clear();
            textBoxSalary.Clear();
            TextBoxHiredate.Clear();
            TextBoxNumber.Clear();
            textBoxContry.Clear();
            errorProvider1.Clear();
        }

        private void tabaddEmployee_Click(object sender, EventArgs e)
        {

        }

        private void textBoxFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void butRemoveAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem existingItem in listView1.Items)
            {
                // Remove the existing item
                listView1.Items.Remove(existingItem);
                
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            UpdatePicture();
        }

        private void label16_Click(object sender, EventArgs e)
        {
          



        }

        private void buttonAdd10_Click(object sender, EventArgs e)
        {

            string iconPath = @"C:\Users\A.S\Downloads\social-media.ico";
            if (System.IO.File.Exists(iconPath))
            {
                notifyIcon2.Icon = new Icon(iconPath);
            }
            else
            {
                notifyIcon2.Icon = SystemIcons.Application; // أيقونة افتراضية إذا لم يوجد الملف
            }
            notifyIcon2.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon2.BalloonTipTitle = "Client Added";
            notifyIcon2.BalloonTipText = "Client has been added to your app";
            notifyIcon2.ShowBalloonTip(1000);

            for (int i = 1; i <= 10; i++)
            {
                ListViewItem item = new ListViewItem(i.ToString());

                if (i % 2 == 0)
                    item.ImageIndex = 1;
                else
                    item.ImageIndex = 0;


                item.SubItems.Add("Person" + i);
                item.SubItems.Add("Department" + i);
                item.SubItems.Add("Job" + i);
                item.SubItems.Add((i * 1000).ToString("C")); // Example salary
                item.SubItems.Add(DateTime.Now.AddDays(-i).ToShortDateString()); // Example hire date
                item.SubItems.Add("0123456789" + i); // Example phone number
                item.SubItems.Add("Country" + i);

                listView1.Items.Add(item);
            }
        }
    }
}

