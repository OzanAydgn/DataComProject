using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DisturberServer
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }


        TcpListener listener;
        bool isRunning = false;

        private void StartListening()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 5000);
                listener.Start();
                Log("Server 5000 portunda başlatıldı. Sender bekleniyor...");

                while (isRunning)
                {
                    TcpClient senderClient = listener.AcceptTcpClient();
                    Log("-> Sender bağlandı!");

                    NetworkStream stream = senderClient.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    string gelenPaket = reader.ReadLine();

                    if (string.IsNullOrEmpty(gelenPaket)) { senderClient.Close(); continue; }

                    Log($"Gelen Paket: {gelenPaket}");

                    string[] parcalar = gelenPaket.Split('|');
                    if (parcalar.Length == 3)
                    {
                        string hamVeri = parcalar[0];    
                        string yontem = parcalar[1];     
                        string kontrol = parcalar[2];

                        string bozukVeri = ApplyCorruption(hamVeri);

                        if (hamVeri != bozukVeri)
                            Log($"Bozma Uygulandı: {hamVeri} -> {bozukVeri}");
                        else
                            Log("Veri bozulmadan iletiliyor.");

                        string gidenPaket = $"{bozukVeri}|{yontem}|{kontrol}";

                        SendToReceiver(gidenPaket);
                    }

                    senderClient.Close();
                }
            }
            catch (Exception ex)
            {
                Log("Server Hatası: " + ex.Message);
            }
        }

        private void SendToReceiver(string paket)
        {
            try
            {
                TcpClient receiverClient = new TcpClient("127.0.0.1", 6000);
                NetworkStream ns = receiverClient.GetStream();
                StreamWriter writer = new StreamWriter(ns);
                writer.WriteLine(paket);
                writer.Flush();
                receiverClient.Close();
                Log("-> Paket Receiver'a iletildi.");
            }
            catch
            {
                Log("UYARI: Receiver (Port 6000) bulunamadı! Paket havada kaldı.");
            }
        }

        private string ApplyCorruption(string data)
        {
            if (rbNone != null && rbNone.Checked) return data;

            char[] chars = data.ToCharArray();
            Random rnd = new Random();

            if (rbBitFlip != null && rbBitFlip.Checked && chars.Length > 0)
            {
                int index = rnd.Next(chars.Length);
                chars[index] = (char)(chars[index] ^ 1);
            }

            else if (rbCharReplace != null && rbCharReplace.Checked && chars.Length > 0)
            {
                int index = rnd.Next(chars.Length);
                char originalChar = chars[index];
                char newChar;

                do
                {
                    newChar = (char)rnd.Next(65, 91);
                }
                while (newChar == originalChar);

                chars[index] = newChar;
            }

            return new string(chars);
        }

        private void Log(string message)
        {
            if (lstServerLog.InvokeRequired)
            {
                lstServerLog.Invoke(new Action<string>(Log), message);
            }
            else
            {
                lstServerLog.Items.Add(message);
                lstServerLog.TopIndex = lstServerLog.Items.Count - 1;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            isRunning = true;
            btnStart.Enabled = false; 
            btnStart.Text = "Server Dinliyor...";

            Task.Run(() => StartListening());
        }
    }

}
