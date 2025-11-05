using System;
using System.IO;
using System.Windows.Forms;

namespace WgConfigHelper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Автоматические значения по умолчанию
            txtServerPublicKey.Text = "Fgwqj508NI2KWcak7NwMB5tHROaYclp9HuxOHqbVSzw=";
            txtEndpoint.Text = "s1410234.smartape-vps.com:51820";
            txtAllowedIPs.Text = "0.0.0.0/0, ::/0";
            txtAddress.Text = "10.10.10.2/24";
            txtDNS.Text = "1.1.1.1";
            txtPersistentKeepalive.Text = "25";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var cfg = new WireGuardConfig
            {
                PrivateKey = txtClientPrivateKey.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                DNS = txtDNS.Text.Trim(),
                ServerPublicKey = txtServerPublicKey.Text.Trim(),
                Endpoint = txtEndpoint.Text.Trim(),
                AllowedIPs = txtAllowedIPs.Text.Trim(),
                PersistentKeepalive = txtPersistentKeepalive.Text.Trim()
            };

            string content = cfg.BuildClientConfig();
            txtPreview.Text = content;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPreview.Text))
            {
                MessageBox.Show("Сначала сгенерируйте конфигурацию (Create).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog();
            sfd.Filter = "WireGuard config (*.conf)|*.conf|All files (*.*)|*.*";
            sfd.FileName = "wg0-client.conf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, txtPreview.Text);
                MessageBox.Show("Saved: " + sfd.FileName, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPreview.Text))
            {
                Clipboard.SetText(txtPreview.Text);
                MessageBox.Show("Скопировано в буфер обмена", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPreview.Text))
            {
                MessageBox.Show("Сначала сгенерируйте конфигурацию (Create).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new UploadForm(txtServerHost.Text, txtSshUser.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string remotePath = dlg.RemotePath;
                    string host = dlg.Host;
                    string user = dlg.User;
                    string password = dlg.Password;

                    // Создать временный файл
                    string tmp = Path.GetTempFileName();
                    File.WriteAllText(tmp, txtPreview.Text);

                    var uploader = new SshUploader(host, user, password);
                    await uploader.UploadFileAsync(tmp, remotePath);

                    MessageBox.Show("Uploaded to " + host + ":" + remotePath, "Uploaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}