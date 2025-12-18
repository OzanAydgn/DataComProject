using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ReceiverClient
{
    public partial class Receiver : Form
    {
        TcpListener listener;
        bool isListening = false;

        public Receiver()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isListening) return;

            isListening = true;
            btnStart.Enabled = false;
            btnStart.Text = "Dinleniyor (Port 6000)...";

            Task.Run(() => StartListening());
        }

        private void StartListening()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 6000);
                listener.Start();
                Log("Sistem Hazır. Veri bekleniyor...", Color.Black);

                while (isListening)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream);

                    try
                    {
                        string paket = reader.ReadLine();
                        if (!string.IsNullOrEmpty(paket))
                        {
                            AnalyzePacket(paket);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log("Okuma Hatası: " + ex.Message, Color.Red);
                    }
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Log("Bağlantı Hatası: " + ex.Message, Color.Red);
            }
        }

        private void AnalyzePacket(string paket)
        {
            string[] parcalar = paket.Split('|');

            if (parcalar.Length != 3)
            {
                Log("Hatalı Paket Formatı!", Color.Orange);
                return;
            }

            string gelenVeri = parcalar[0];     
            string yontem = parcalar[1];         
            string gelenKod = parcalar[2];       

            string hesaplananKod = "";

            switch (yontem)
            {
                case "Parity":
                    hesaplananKod = CalculateParity(gelenVeri);
                    break;
                case "CRC":
                    hesaplananKod = ComputeCRC16(gelenVeri); break;
                default:
                    hesaplananKod = "0000";
                    break;
            }

            bool isCorrect = (gelenKod == hesaplananKod);

            Log("--------------------------------", Color.Black);
            Log($"Gelen Veri: {gelenVeri}", Color.Blue);
            Log($"Yöntem: {yontem}", Color.Black);
            Log($"Gelen Kod (Sender): {gelenKod}", Color.Black);
            Log($"Hesaplanan Kod (Receiver): {hesaplananKod}", Color.Black);

            if (isCorrect)
            {
                Log("DURUM: DATA CORRECT (Veri Sağlam)", Color.Green);
            }
            else
            {
                Log("DURUM: DATA CORRUPTED (Veri Bozulmuş!)", Color.Red);
            }
        }

        private string CalculateParity(string text)
        {
            int oneCount = 0;
            foreach (char c in text)
            {
                string binary = Convert.ToString(c, 2);
                foreach (char bit in binary) if (bit == '1') oneCount++;
            }
            return (oneCount % 2 == 0) ? "0" : "1";
        }

        private void Log(string message, Color color)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action<string, Color>(Log), message, color);
            }
            else
            {
                rtbLog.SelectionStart = rtbLog.TextLength;
                rtbLog.SelectionLength = 0;
                rtbLog.SelectionColor = color;
                rtbLog.AppendText(message + "\n");
                rtbLog.SelectionColor = rtbLog.ForeColor;
                rtbLog.ScrollToCaret();
            }
        }

        private string ComputeCRC16(string data)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data);
            ushort crc = 0xFFFF;

            for (int i = 0; i < bytes.Length; ++i)
            {
                crc ^= (ushort)(bytes[i]);
                for (int j = 0; j < 8; ++j)
                {
                    if ((crc & 1) != 0)
                        crc = (ushort)((crc >> 1) ^ 0xA001);
                    else
                        crc >>= 1;
                }
            }
            return crc.ToString("X4");
        }

    }
}