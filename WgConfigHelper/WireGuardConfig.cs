using System.Text;

namespace WgConfigHelper
{
    public class WireGuardConfig
    {
        public string PrivateKey { get; set; }
        public string Address { get; set; } = "10.10.10.2/24";
        public string DNS { get; set; } = "1.1.1.1";
        public string ServerPublicKey { get; set; }
        public string Endpoint { get; set; }
        public string AllowedIPs { get; set; } = "0.0.0.0/0, ::/0";
        public string PersistentKeepalive { get; set; } = "25";

        public string BuildClientConfig()
        {
            var sb = new StringBuilder();
            sb.AppendLine("[Interface]");
            sb.AppendLine($"PrivateKey = {PrivateKey}");
            if (!string.IsNullOrWhiteSpace(Address)) sb.AppendLine($"Address = {Address}");
            if (!string.IsNullOrWhiteSpace(DNS)) sb.AppendLine($"DNS = {DNS}");
            sb.AppendLine();

            sb.AppendLine("[Peer]");
            sb.AppendLine($"PublicKey = {ServerPublicKey}");
            if (!string.IsNullOrWhiteSpace(Endpoint)) sb.AppendLine($"Endpoint = {Endpoint}");
            if (!string.IsNullOrWhiteSpace(AllowedIPs)) sb.AppendLine($"AllowedIPs = {AllowedIPs}");
            if (!string.IsNullOrWhiteSpace(PersistentKeepalive)) sb.AppendLine($"PersistentKeepalive = {PersistentKeepalive}");

            return sb.ToString();
        }
    }
}