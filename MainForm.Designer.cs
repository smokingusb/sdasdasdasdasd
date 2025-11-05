using System.Windows.Forms;

namespace WgConfigHelper
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtServerPublicKey;
        private TextBox txtClientPrivateKey;
        private TextBox txtEndpoint;
        private TextBox txtAllowedIPs;
        private TextBox txtAddress;
        private TextBox txtDNS;
        private TextBox txtPersistentKeepalive;
        private Button btnCreate;
        private Button btnSave;
        private Button btnCopy;
        private TextBox txtPreview;
        private Button btnUpload;
        private TextBox txtServerHost;
        private TextBox txtSshUser;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtServerPublicKey = new TextBox() { Left = 12, Top = 12, Width = 560, PlaceholderText = "Server PublicKey" };
            this.txtClientPrivateKey = new TextBox() { Left = 12, Top = 40, Width = 560, PlaceholderText = "Client PrivateKey" };
            this.txtEndpoint = new TextBox() { Left = 12, Top = 68, Width = 280, PlaceholderText = "Endpoint (host:port)" };
            this.txtAddress = new TextBox() { Left = 300, Top = 68, Width = 140, PlaceholderText = "Address (10.x.x.x/24)" };
            this.txtDNS = new TextBox() { Left = 450, Top = 68, Width = 120, PlaceholderText = "DNS" };
            this.txtAllowedIPs = new TextBox() { Left = 12, Top = 96, Width = 560, PlaceholderText = "AllowedIPs (0.0.0.0/0, ::/0)" };
            this.txtPersistentKeepalive = new TextBox() { Left = 12, Top = 124, Width = 120, PlaceholderText = "PersistentKeepalive" };
            this.btnCreate = new Button() { Left = 140, Top = 120, Width = 80, Text = "Create" };
            this.btnSave = new Button() { Left = 230, Top = 120, Width = 80, Text = "Save" };
            this.btnCopy = new Button() { Left = 320, Top = 120, Width = 80, Text = "Copy" };
            this.btnUpload = new Button() { Left = 410, Top = 120, Width = 162, Text = "Upload via SSH (optional)" };

            this.txtPreview = new TextBox() { Left = 12, Top = 156, Width = 560, Height = 300, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = false };

            this.txtServerHost = new TextBox() { Left = 12, Top = 468, Width = 280, PlaceholderText = "SSH Host (e.g. 185.239.48.235)" };
            this.txtSshUser = new TextBox() { Left = 300, Top = 468, Width = 120, PlaceholderText = "SSH User (root)" };

            this.ClientSize = new System.Drawing.Size(584, 520);
            this.Controls.AddRange(new Control[] {
                txtServerPublicKey, txtClientPrivateKey, txtEndpoint, txtAddress, txtDNS, txtAllowedIPs,
                txtPersistentKeepalive, btnCreate, btnSave, btnCopy, txtPreview, btnUpload, txtServerHost, txtSshUser
            });
            this.Text = "WireGuard Config Helper";

            btnCreate.Click += btnCreate_Click;
            btnSave.Click += btnSave_Click;
            btnCopy.Click += btnCopy_Click;
            btnUpload.Click += btnUpload_Click;
        }
    }
}