using RFID;
using System;

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
            cboBoxUSB.Items.Clear();
            iHidNumber = CFHidApi.CFHid_GetUsbCount();
            for (iIndex = 0; iIndex < iHidNumber; iIndex++)
            {
                CFHidApi.CFHid_GetUsbInfo(iIndex, arrBuffer);
                strSN = System.Text.Encoding.Default.GetString(arrBuffer);

                System.Diagnostics.Debug.WriteLine(strSN);

                cboBoxUSB.Items.Add(strSN);
            }
            if (iHidNumber > 0)
                cboBoxUSB.SelectedIndex = 0;
        }

        private void btnScanUSB_Click(object sender, EventArgs e)
        {
            string strSN = "";
            byte[] arrBuffer = new byte[256];
            int iHidNumber = 0;
            UInt16 iIndex = 0;
            cboBoxUSB.Items.Clear();
            iHidNumber = CFHidApi.CFHid_GetUsbCount();
            for (iIndex = 0; iIndex < iHidNumber; iIndex++)
            {
                CFHidApi.CFHid_GetUsbInfo(iIndex, arrBuffer);
                strSN = System.Text.Encoding.Default.GetString(arrBuffer);
                cboBoxUSB.Items.Add(strSN);
            }
            if (iHidNumber > 0)
                cboBoxUSB.SelectedIndex = 0;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string btnConnectText = btnConnect.Text;

            if (btnConnect.Text.Equals("Conectar"))
            {
                byte[] arrBuffer = new byte[64];

                if (RFID.CFHidApi.CFHid_OpenDevice((UInt16)cboBoxUSB.SelectedIndex))
                {
                    if (RFID.CFHidApi.CFHid_GetDeviceSystemInfo(0xFF, arrBuffer) == false) //获取系统信息失败
                    {


                        SetText("No se pudo obtener la informacion del dispositivo\r\n");
                        CFHidApi.CFHid_CloseDevice();
                        return;
                    }
                       var resp = CFHidApi.CFHid_GetDeviceSystemInfo(0xFF, arrBuffer);
                    System.Diagnostics.Debug.WriteLine("arrBuffer: ", arrBuffer.ToString());
                    //    System.Diagnostics.Debug.WriteLine("resp deviceSystemInfo", resp.ToString());
                    SetText("Conección Exitosa\r\n");
                    btnConnect.Text = "Desconectar";
                }
                else
                {
                    this.SetText("Coneccion Fallida\r\n");
                    return;
                }
                //System.Diagnostics.Debug.WriteLine("Entra al IF");

                string str = "", str1 = "";
                str = String.Format("SoftVer:{0:D}.{0:D}\r\n", arrBuffer[0] >> 4, arrBuffer[0] & 0x0F);
                this.SetText(str);
                str = String.Format("HardVer:{0:D}.{0:D}\r\n", arrBuffer[1] >> 4, arrBuffer[1] & 0x0F);
                this.SetText(str);
                str = "SN:";
                for (int i = 0; i < 7; i++)
                {
                    str1 = String.Format("{0:X2}", arrBuffer[2 + i]);
                    str = str + str1;
                }
                str = str + "\r\n";
                this.SetText(str);
                //button1.Enabled = false;
                //button2.Enabled = true;

            }
            else if (btnConnect.Text.Equals("Desconectar"))
            {
                //timer1.Enabled = false;
                CFHidApi.CFHid_CloseDevice();
                //button1.Enabled = true;
                //button2.Enabled = false;
                //button6.Enabled = true;
                //button11.Enabled = false;
                this.SetText("Conección finalizada\r\n");
                btnConnect.Text = "Conectar";
            }



        }

        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (this.textBox1.TextLength > 1000) this.textBox1.Text = "";
                textBox1.Text = this.textBox1.Text + text;
                textBox1.SelectionStart = this.textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }

        private void btnStartRead_Click(object sender, EventArgs e)
        {
            SetText("Leyendo\r\n");
            CFHidApi.CFHid_ClearTagBuf();

            System.Diagnostics.Debug.WriteLine(timer1.Enabled);
            bool readInfo;

            readInfo = CFHidApi.CFHid_StartRead(0xFF);

            System.Diagnostics.Debug.WriteLine(readInfo);
            timer1.Interval = 100;
            timer1.Enabled = true;
            //button6.Enabled = false;
            //button11.Enabled = true;
        }


        private void btnStopRead_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            CFHidApi.CFHid_StopRead(0xFF);

            byte[] arrBuffer = new byte[64000];
            int iNum = 0;
            int iTotalLen = 0;
            byte bRet = 0;

            bRet = CFHidApi.CFHid_GetTagBuf(arrBuffer, out iTotalLen, out iNum);

            System.Diagnostics.Debug.WriteLine("bRet: ", bRet);
        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("entro al timer");
            byte[] arrBuffer = new byte[64000];
            int iNum = 0;
            int iTotalLen = 0;
            byte bRet = 0;

            bRet = CFHidApi.CFHid_GetTagBuf(arrBuffer, out iTotalLen, out iNum);

            System.Diagnostics.Debug.WriteLine("bRet: ", bRet);

            System.Diagnostics.Debug.WriteLine("bRet data: ", arrBuffer, iTotalLen, iNum);

            //if (bRet == 1)
            //{
            //    this.SetText("DevOut");
            //    return; //DevOut
            //}
            //else if (bRet == 0) {
            //    System.Diagnostics.Debug.WriteLine("bRet: 0?");

            //    return;
            //}; //No Connect
            int iTagLength = 0;
            int iTagNumber = 0;
            iTagLength = iTotalLen;
            iTagNumber = iNum;
            if (iTagNumber == 0) return;
            int iIndex = 0;
            int iLength = 0;
            byte bPackLength = 0;
            int i = 0;
            int iIDLen = 0;
            for (iIndex = 0; iIndex < iTagNumber; iIndex++)
            {
                bPackLength = arrBuffer[iLength];
                string str2 = "";
                string str1 = "";
                str1 = arrBuffer[1 + iLength + 0].ToString("X2");
                str2 = str2 + "Type:" + str1 + " ";  //Tag Type
                if ((arrBuffer[1 + iLength + 0] & 0x80) == 0x80)
                {   // with TimeStamp , last 6 bytes is time
                    iIDLen = bPackLength - 7;
                }
                else iIDLen = bPackLength - 1;

                str1 = arrBuffer[1 + iLength + 1].ToString("X2");
                str2 = str2 + "Ant:" + str1 + " Tag:";  //Ant

                string str3 = "";
                for (i = 2; i < iIDLen; i++)
                {
                    str1 = arrBuffer[1 + iLength + i].ToString("X2");
                    str3 = str3 + str1 + " ";
                }
                str2 = str2 + str3;
                str1 = arrBuffer[1 + iLength + i].ToString("X2");
                str2 = str2 + "RSSI:" + str1 + "\r\n";  //RSSI
                iLength = iLength + bPackLength + 1;
                this.SetText(str2);
            }
        }

    }
}