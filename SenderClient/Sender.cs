using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace SenderClient
{
    public partial class Sender : Form
    {
        public Sender()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string veri = txtMessage.Text;

            string yontem = cmbMethod.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(veri) || string.IsNullOrEmpty(yontem))
            {
                MessageBox.Show("Lütfen mesaj ve yöntem seçin!");
                return;
            }

            string kontrolBilgisi = "";
            switch (yontem)
            {
                case "Parity":
                    kontrolBilgisi = CalculateParity(veri);
                    break;
                case "CRC":
                    kontrolBilgisi = ComputeCRC16(veri); break;
                default:
                    kontrolBilgisi = "0000";
                    break;
            }

            string paket = $"{veri}|{yontem}|{kontrolBilgisi}";

            lstSenderLog.Items.Add($"Paket: {paket}");

            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 5000);
                NetworkStream stream = client.GetStream();
                byte[] dataToSend = System.Text.Encoding.ASCII.GetBytes(paket);
                stream.Write(dataToSend, 0, dataToSend.Length);

                lstSenderLog.Items.Add("-> Server'a gönderildi.");
                client.Close();
            }
            catch
            {
                lstSenderLog.Items.Add("HATA: Server kapalı!");
                MessageBox.Show("Server'a bağlanılamadı. Server projesini başlattın mı?");
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