using RFID;
using System;
using System.Text;

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

                //System.Diagnostics.Debug.WriteLine(strSN);

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

                if (CFHidApi.CFHid_OpenDevice((UInt16)cboBoxUSB.SelectedIndex))
                {
                    if (CFHidApi.CFHid_GetDeviceSystemInfo(0xFF, arrBuffer) == false) //获取系统信息失败
                    {


                        SetText("No se pudo obtener la informacion del dispositivo\r\n");
                        CFHidApi.CFHid_CloseDevice();
                        return;
                    }

                    //for (int i = 0; i < arrBuffer.Length; i++)
                    //{
                    //    //System.Diagnostics.Debug.WriteLine($"arrBuffer[{i}]: {arrBuffer[i]:X2}");
                    //    System.Diagnostics.Debug.WriteLine($"arrBuffer[{i}]: {arrBuffer[i]:X2} (Hex) - {arrBuffer[i]} (Dec)");
                    //}

                    var resp = CFHidApi.CFHid_GetDeviceSystemInfo(0xFF, arrBuffer);
                    System.Diagnostics.Debug.WriteLine("arrBuffer: ", System.Text.Encoding.Default.GetString(arrBuffer));
                    //    System.Diagnostics.Debug.WriteLine("resp deviceSystemInfo", resp.ToString());
                    SetText("Conección Exitosa\r\n");
                    btnConnect.Text = "Desconectar";
                }
                else
                {
                    this.SetText("Conexión Fallida\r\n");
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
            CFHidApi.CFHid_ClearTagBuf();
            //bool readInfo = CFHidApi.CFHid_StartRead(0xFF);
            //if (readInfo)
            //{
            SetText("Iniciando lectura...\r\n");
            timer1.Interval = 100;
            timer1.Enabled = true;
            //}
            //else
            //{
            //    SetText("Error al iniciar la lectura.\r\n");
            //}
            //SetText("Leyendo\r\n");
            //bool readInfo;

            //readInfo = CFHidApi.CFHid_StartRead(0xFF);

            //CFHidApi.CFHid_ClearTagBuf();

            //System.Diagnostics.Debug.WriteLine(timer1.Enabled);


            //System.Diagnostics.Debug.WriteLine(readInfo);
            //timer1.Interval = 100;
            //timer1.Enabled = true;
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
            byte[] buffer = new byte[1024];
            ushort totalLength = 0;
            ushort cardNum = 0;
            if (CFHidApi.CFHid_InventoryG2(0xFF, buffer, out totalLength, out cardNum))
            {
                if (cardNum > 0)
                {
                    int index = 0;
                    for (int i = 0; i < cardNum; i++)
                    {
                        StringBuilder epc = new StringBuilder();
                        for (int j = 0; j < 12; j++) // Asumiendo que el EPC tiene 12 bytes
                        {
                            epc.AppendFormat("{0:X2} ", buffer[index + j]);
                        }
                        index += 12;
                        SetText("EPC: " + epc.ToString() + "\r\n");
                    }
                }
            }

            // VENIA
            //System.Diagnostics.Debug.WriteLine("entro al timer");
            //byte[] arrBuffer = new byte[64000];
            //int iNum = 0;
            //int iTotalLen = 0;
            //byte bRet = 0;

            //bRet = CFHidApi.CFHid_GetTagBuf(arrBuffer, out iTotalLen, out iNum);

            //for (int j = 0; j < arrBuffer.Length; j++)
            //{
            //    System.Diagnostics.Debug.WriteLine($"arrBuffer[{j}]: {arrBuffer[j]:X2} (Hex) - {arrBuffer[j]} (Dec)");

            //}

            //System.Diagnostics.Debug.WriteLine("arrBuffer.Length: ", arrBuffer.Length.ToString());

            //System.Diagnostics.Debug.WriteLine("bRet: ", bRet);

            //System.Diagnostics.Debug.WriteLine("bRet data: ", arrBuffer, iTotalLen, iNum);

            ////if (bRet == 1)
            ////{
            ////    this.SetText("DevOut");
            ////    return; //DevOut
            ////}
            ////else if (bRet == 0) {
            ////    System.Diagnostics.Debug.WriteLine("bRet: 0?");

            ////    return;
            ////}; //No Connect
            //int iTagLength = 0;
            //int iTagNumber = 0;
            //iTagLength = iTotalLen;
            //iTagNumber = iNum;
            //if (iTagNumber == 0) return;
            //int iIndex = 0;
            //int iLength = 0;
            //byte bPackLength = 0;
            //int i = 0;
            //int iIDLen = 0;
            //for (iIndex = 0; iIndex < iTagNumber; iIndex++)
            //{
            //    bPackLength = arrBuffer[iLength];
            //    string str2 = "";
            //    string str1 = "";
            //    str1 = arrBuffer[1 + iLength + 0].ToString("X2");
            //    str2 = str2 + "Type:" + str1 + " ";  //Tag Type
            //    if ((arrBuffer[1 + iLength + 0] & 0x80) == 0x80)
            //    {   // with TimeStamp , last 6 bytes is time
            //        iIDLen = bPackLength - 7;
            //    }
            //    else iIDLen = bPackLength - 1;

            //    str1 = arrBuffer[1 + iLength + 1].ToString("X2");
            //    str2 = str2 + "Ant:" + str1 + " Tag:";  //Ant

            //    string str3 = "";
            //    for (i = 2; i < iIDLen; i++)
            //    {
            //        str1 = arrBuffer[1 + iLength + i].ToString("X2");
            //        str3 = str3 + str1 + " ";
            //    }
            //    str2 = str2 + str3;
            //    str1 = arrBuffer[1 + iLength + i].ToString("X2");
            //    str2 = str2 + "RSSI:" + str1 + "\r\n";  //RSSI
            //    iLength = iLength + bPackLength + 1;
            //    this.SetText(str2);
            //}
        }

        private void btnReadOnce_Click(object sender, EventArgs e)
        {
            byte[] arrBuffer = new byte[64000];
            ushort totalLength = 0;
            ushort tagNum = 0;
            SetText("***Modo Respuesta***\r\n");
            if (CFHidApi.CFHid_InventoryG2(0xFF, arrBuffer, out totalLength, out tagNum))
            {
                //System.Diagnostics.Debug.WriteLine("InventoryG2: buffer={0}, totalLength={1}, tagNum={2}", BitConverter.ToString(arrBuffer), totalLength, tagNum);
                System.Diagnostics.Debug.WriteLine("InventoryG2: totalLength={0}, tagNum={1}", totalLength, tagNum);


                if (tagNum > 0)
                {
                    if (tagNum == 1)
                    {
                        {
                            StringBuilder epc = new StringBuilder();
                            for (int j = 0; j < 12; j++)
                            {
                                epc.AppendFormat("{0:X2} ", arrBuffer[j + 3]);
                            }
                            lblEPC.Text = epc.ToString();
                            lblEPC.ForeColor = Color.Green;
                            SetText("EPC: " + epc.ToString() + "\r\n");
                        }
                    }
                    else
                    {
                        lblEPC.Text = "Mas de 1 tag en lectura.";
                        lblEPC.ForeColor = Color.Red;
                        SetText("Mas de 1 tag en lectura.\r\n");
                    }
                }
                else
                {
                    lblEPC.Text = "Sin Lecturas.";
                    lblEPC.ForeColor = Color.Red;
                    SetText("Sin Lecturas.\r\n");
                }
            }
            else
            {
                SetText("Sin lectura de Tags.\r\n");
                return;
            }
        }
    }
}