using System;
using System.Windows.Forms;

namespace WgConfigHelper
{
    // Простая форма для ввода SSH параметров: Host, User, Password, RemotePath
    public class UploadForm : Form
    {
        private TextBox txtHost;
        private TextBox txtUser;
        private TextBox txtPassword;
        private TextBox txtRemotePath;
        private Button btnOk;
        private Button btnCancel;

        public string Host => txtHost.Text.Trim();
        public string User => txtUser.Text.Trim();
        public string Password => txtPassword.Text;
        public string RemotePath => txtRemotePath.Text.Trim();

        public UploadForm(string defaultHost = "", string defaultUser = "")
        {
            Text = "Upload via SSH";
            Width = 420;
            Height = 220;
            StartPosition = FormStartPosition.CenterParent;

            var lblHost = new Label() { Left = 10, Top = 10, Text = "Host", AutoSize = true };
            txtHost = new TextBox() { Left = 100, Top = 6, Width = 290, Text = defaultHost };

            var lblUser = new Label() { Left = 10, Top = 40, Text = "User", AutoSize = true };
            txtUser = new TextBox() { Left = 100, Top = 36, Width = 290, Text = defaultUser };

            var lblPassword = new Label() { Left = 10, Top = 70, Text = "Password", AutoSize = true };
            txtPassword = new TextBox() { Left = 100, Top = 66, Width = 290, UseSystemPasswordChar = true };

            var lblRemote = new Label() { Left = 10, Top = 100, Text = "Remote Path", AutoSize = true };
            txtRemotePath = new TextBox() { Left = 100, Top = 96, Width = 290, Text = "/root/wg0-client.conf" };

            btnOk = new Button() { Text = "OK", Left = 220, Width = 80, Top = 130, DialogResult = DialogResult.OK };
            btnCancel = new Button() { Text = "Cancel", Left = 310, Width = 80, Top = 130, DialogResult = DialogResult.Cancel };

            btnOk.Click += (s, e) =>
            {
                // простая валидация
                if (string.IsNullOrWhiteSpace(Host) || string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(RemotePath))
                {
                    MessageBox.Show("Введите Host, User и Remote Path.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                    return;
                }
                this.DialogResult = DialogResult.OK;
                Close();
            };

            Controls.AddRange(new Control[] { lblHost, txtHost, lblUser, txtUser, lblPassword, txtPassword, lblRemote, txtRemotePath, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}
