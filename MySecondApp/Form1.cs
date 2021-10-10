using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace MySecondApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = DateTime.Now.ToString();
        }

        // disable close button
        private bool isEnableCloseButton = false;
        protected override CreateParams CreateParams
        {
            get
            {
                if (isEnableCloseButton)
                {
                    CreateParams parameters = base.CreateParams;
                    return parameters;
                }
                else
                {
                    int CS_NOCLOSE = 0x200;
                    CreateParams parameters = base.CreateParams;
                    parameters.ClassStyle |= CS_NOCLOSE;
                    return parameters;
                }
            }
        }

        private void ShowProps()
        {
            decimal pi = Properties.Settings.Default.PI;
            MessageBox.Show("button click event, PI=" + pi.ToString());

            Properties.Settings.Default.Reset();
            // Properties.Settings.Default.Reload();
            string tip0 = string.Format("x={0}, y={1}", Properties.Settings.Default.WindowsLocation.X.ToString(),
                Properties.Settings.Default.WindowsLocation.X.ToString());
            MessageBox.Show(tip0);

            Properties.Settings.Default.WindowsLocation = this.Location;
            string tip1 = string.Format("x={0}, y={1}", this.Location.X.ToString(), this.Location.X.ToString());
            MessageBox.Show(tip1);

            // Properties.Settings.Default.Save();
        }

        private bool ValidateUserInfo()
        {
            string username = this.username.Text;
            string password = this.password.Text;
            string userinfo = string.Format("username={0}, password={1}", username, password);
            MessageBox.Show(userinfo, "Show User Info", MessageBoxButtons.OK);
            if(password != "123")
            {
                return false;
            }
            return true;
        }

        private void ShowForm2()
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            // form2.Visible = true;
        }

        private void ShowForm2withThread()
        {
            this.Close();
            Thread thread = new Thread(ShowForm2);
            thread.IsBackground = false;
            thread.Start();
        }

        private void button_Click(object sender, EventArgs e)
        {
            // this.ShowProps();
            //if(this.ValidateUserInfo() == false)
            //{
            //    MessageBox.Show("user name or password is not correct!", "Error Info", MessageBoxButtons.OK);
            //    return;
            //}
            // this.ShowForm2withThread();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Focus();
                if (!Validate())
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }
            this.ShowForm2();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.infoProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            foreach (Control control in this.Controls)
            {
                string toolTip = this.toolTip.GetToolTip(control);
                if (toolTip.Length == 0) continue;
                this.infoProvider.SetError(control, toolTip);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // this.Close();
            Application.Exit();
        }

        private void username_Validating(object sender, CancelEventArgs e)
        {
            //string error = "";
            //if(((Control)sender).Text.Trim().Length == 0)
            //{
            //    error = "Please enter a name";
            //    e.Cancel = true;
            //}
            //this.errorProvider.SetError((Control)sender, error);

            UpdateErrorStatus(text_IsNotEmpty(((Control)sender).Text), (Control)sender, e);
        }

        private void password_Validating(object sender, CancelEventArgs e)
        {
            //string error = "";
            //if (((Control)sender).Text.Trim().Length == 0)
            //{
            //    error = "Please enter a password";
            //    e.Cancel = true;
            //}
            //this.errorProvider.SetError((Control)sender, error);

            UpdateErrorStatus(text_IsNotEmpty(((Control)sender).Text), (Control)sender, e);
        }

        private bool text_IsNotEmpty(string text)
        {
            if (text.Trim().Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UpdateErrorStatus(bool isValid, Control control, CancelEventArgs e)
        {
            string toolTip = this.toolTip.GetToolTip(control);
            if (isValid)
            {
                // Show the info when there is text in the text box
                this.errorProvider.SetError(control, null);
                this.infoProvider.SetError(control, null);
            }
            else
            {
                // Show the error when there is no text in the text box
                this.errorProvider.SetError(control, toolTip);
                this.infoProvider.SetError(control, null);
                e.Cancel = true;
            }
        }
    }
}
