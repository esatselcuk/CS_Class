using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GH_CS_Class
{
    class clsDonanim
    {
        public string mac_adresi_getir()
        {
            //https://mustafabukulmez.com/2018/04/26/c-sharp-bilgisayar-bilgileri-almak/ adresinden faydalanılmıştır
            try
            {
                String macadress = string.Empty;
                string mac = null;
                //Birden fazla ethernet olması durumunda bu metod ilk mac adresini aldığında işlemi bitirecektir.
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    OperationalStatus ot = nic.OperationalStatus;
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macadress = nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
                for (int i = 0; i <= macadress.Length - 1; i++)
                {
                    // 005056C00001 şeklinde mac adresini alırken parça parça aldığından 
                    //aralarına : işaretini biz atıyoruz. 00:50:56:C0:00:01 şeklinde dönüşüyor
                    mac = mac + ":" + macadress.Substring(i, 2);
                    i++;
                }
                mac = mac.Remove(0, 1);             // en sonda kalan fazladan : işaretini siliyoruz.
                //Alınan mac adresi bilgileri return ile geri döndürülüyor.
                return mac;
            }
            catch (Exception ex)
            {
                //Herhangi Bir hata oluşması durumunda hata mesajı ile bilirte açıklama dönülecektir.
                return "Mac Bilgileri Alınamadı : Hata Detayı (" + ex.Message + ")";
            }
        }

        List<string> mac_adresleri()
        {
            List<string> liste = new List<string>();
            try
            {
                String macadress = string.Empty;

                //Birden fazla ethernet olması durumunda bu metod ilk mac adresini aldığında işlemi bitirecektir.
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    OperationalStatus ot = nic.OperationalStatus;
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macadress = nic.GetPhysicalAddress().ToString();
                        string mac = null;
                        if (string.IsNullOrEmpty(macadress) == false)
                        {
                            for (int i = 0; i <= macadress.Length - 1; i++)
                            {
                                // 005056C00001 şeklinde mac adresini alırken parça parça aldığından 
                                //aralarına : işaretini biz atıyoruz. 00:50:56:C0:00:01 şeklinde dönüşüyor
                                mac = mac + ":" + macadress.Substring(i, 2);
                                i++;
                            }
                            mac = mac.Remove(0, 1);             // en sonda kalan fazladan : işaretini siliyoruz.

                            liste.Add(mac);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //Herhangi Bir hata oluşması durumunda hata mesajı verilecektir.
                MessageBox.Show("Mac Bilgileri Alınamadı : Hata Detayı (" + ex.Message + ")");
            }

            //Alınan mac adresi bilgileri return ile geri döndürülüyor.
            return liste;
        }

        //Birden fazla ethernet kartı - nic olması durumunda internet bağlantısı olan - aktif cihazın mac adresini verir
        //Örnek Kullanım PhysicalAddress mac = GetCurrentMAC("www.google.com");
        private PhysicalAddress GetCurrentMAC(string allowedURL = "www.google.com.tr")
        {
            TcpClient client = new TcpClient();
            client.Client.Connect(new IPEndPoint(Dns.GetHostAddresses(allowedURL)[0], 80));
            while (!client.Connected)
            {
                Thread.Sleep(500);
            }
            IPAddress address2 = ((IPEndPoint)client.Client.LocalEndPoint).Address;
            client.Client.Disconnect(false);
            NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            client.Close();
            if (allNetworkInterfaces.Length > 0)
            {
                foreach (NetworkInterface interface2 in allNetworkInterfaces)
                {
                    UnicastIPAddressInformationCollection unicastAddresses = interface2.GetIPProperties().UnicastAddresses;
                    if ((unicastAddresses != null) && (unicastAddresses.Count > 0))
                    {
                        for (int i = 0; i < unicastAddresses.Count; i++)
                            if (unicastAddresses[i].Address.Equals(address2))
                                return interface2.GetPhysicalAddress();
                    }
                }
            }
            return null;
        }

        //İşlemci ID Bilgisini Almak İçin Hazırlanmıştır.
        public static String GetProcessorId()
        {

            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            String Id = String.Empty;
            foreach (ManagementObject mo in moc)
            {

                Id = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return Id;

        }
        //İşlemci Üreticisi Alınıyor
        public string GetCPUManufacturer()
        {
            string cpuMan = String.Empty;
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            ManagementObjectCollection objCol = mgmt.GetInstances();
            foreach (ManagementObject obj in objCol)
            {
                if (cpuMan == String.Empty)
                {
                    cpuMan = obj.Properties["Manufacturer"].Value.ToString();
                }
            }
            return cpuMan;
        }
        //İşlemci GHZ Alınmaya çalışılıyor
        public double? GetCpuSpeedInGHz()
        {
            double? GHz = null;
            using (ManagementClass mc = new ManagementClass("Win32_Processor"))
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    GHz = 0.001 * (UInt32)mo.Properties["CurrentClockSpeed"].Value;
                    break;
                }
            }
            return GHz;
        }
        //HDD Seri No Bilgisi Alınıyor
        public string GetHDDSerialNo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            string serial_number = "";
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                serial_number = wmi_HD["SerialNumber"].ToString();
            }
            return serial_number;
        }

        //Anakart Üretici Bilgisi Alınıyor
        public string GetMainBoardMaker()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }
                catch { }
            }
            return "Bilinmiyor";
        }

        //Anakart BIOS Üretici Bilgileri Alınıyor
        public string GetBIOSMaker()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }
                catch { }
            }
            return "Bilinmiyor";
        }
        //BIOS Seri Numarası Alınıyor
        public string GetBIOSSerialNumber()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("SerialNumber").ToString();
                }
                catch { }
            }
            return "Bilinmiyor";

        }
        //Fiziksel Ram Miktarı Alınıyor
        public string GetPhysicalMemory()
        {
            ManagementScope oMs = new ManagementScope();
            ObjectQuery oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oCollection = oSearcher.Get();

            long MemSize = 0;
            long mCap = 0;

            foreach (ManagementObject obj in oCollection)
            {
                mCap = Convert.ToInt64(obj["Capacity"]);
                MemSize += mCap;
            }
            MemSize = (MemSize / 1024) / 1024 / 1024;
            return MemSize.ToString() + " GB";
        }
    }
}
