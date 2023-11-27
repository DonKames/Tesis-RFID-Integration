using RFID;

namespace Tesis_RFID_Integration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //button2.Enabled = false;
            //button11.Enabled = false;

            //Console.WriteLine("inicio");
            System.Diagnostics.Debug.WriteLine("inicio");

            string strSN = "";
            byte[] arrBuffer = new byte[512];
            int iHidNumber = 0;
            UInt16 iIndex = 0;
            //comboBox1.Items.Clear();
            iHidNumber = CFHidApi.CFHid_GetUsbCount();
            for (iIndex = 0; iIndex < iHidNumber; iIndex++)
            {
                RFID.CFHidApi.CFHid_GetUsbInfo(iIndex, arrBuffer);
                strSN = System.Text.Encoding.Default.GetString(arrBuffer);

                System.Diagnostics.Debug.WriteLine(strSN);

                //comboBox1.Items.Add(strSN);
            }
            //if (iHidNumber > 0)
            //    comboBox1.SelectedIndex = 0;
        }
    }
}