using System.Net;
using System.Net.Sockets;
namespace DotSyncServer.Services;

public class IPService
{
    private readonly string _httpPort = "5247";
    private readonly string _httpsPort = "7244";
    private readonly string _localhost = "127.0.0.1";

    public List<string> GetInternalIPs()
    {
        var ips = new List<string>();
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString() != _localhost)
            {
                ips.Add($"http://{ip.ToString()}:{_httpPort}");
                ips.Add($"https://{ip.ToString()}:{_httpsPort}");
            }
        }
        return ips;
    }
}
