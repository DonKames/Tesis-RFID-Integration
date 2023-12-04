using RFID;
using System;
using System.Reflection;
using System.Text;
using Tesis_RFID_Integration.api;
using Tesis_RFID_Integration.models;

namespace Tesis_RFID_Integration
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Reading> readingsInfo = new();
        private WarehouseAPI warehouseAPI;
        private BranchAPI branchAPI;
        private ProductAPI productAPI;

        List<Branch> branchesNames;
        List<Warehouse> warehousesNames;

        Warehouse selectedWarehouse;

        public Form1()
        {
            InitializeComponent();
            //button2.Enabled = false;
            //button11.Enabled = false;

            //Console.WriteLine("inicio");
            System.Diagnostics.Debug.WriteLine("inicio");

            // Inicializacion de APIs
            warehouseAPI = new WarehouseAPI();
            branchAPI = new BranchAPI();
            productAPI = new ProductAPI();

            InitializeCboBoxes();

            // Desactivacion Botones
            btnReadOnce.Enabled = false;
            btnStartRead.Enabled = false;
            btnStopRead.Enabled = false;


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

        private void InitializeCboBoxes()
        {
            InitializeCboBoxBranches();
            InitializeCboBoxWarehouses();
        }

        private async void InitializeCboBoxBranches()
        {
            try
            {

                //var branches = await branchAPI.GetBranchNamesAsync();
                branchesNames = await branchAPI.GetBranchNamesAsync();


                if (branchesNames != null)
                {
                    foreach (var branch in branchesNames)
                    {
                        cboBoxBranches.Items.Add(branch);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                MessageBox.Show("Error al obtener nombres de almacenes: " + ex.Message);
            }
        }
        private async void InitializeCboBoxWarehouses()
        {
            try
            {

                warehousesNames = await warehouseAPI.GetWarehousesNamesAsync();

            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                MessageBox.Show("Error al obtener nombres de almacenes: " + ex.Message);
            }
        }

        private async Task<Product> GetProductByEPC(string EPC)
        {
            try
            {

                Product productByEPC = await productAPI.GetProductByEPCAsync(EPC);

                return productByEPC;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el EPC: " + ex.Message);
                return null;
            }
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
                    btnReadOnce.Enabled = true;
                    btnStartRead.Enabled = true;
                    btnStopRead.Enabled = true;
                    lblConnectionStatus.Text = "Conectado";
                    lblConnectionStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    SetText("Conexión Fallida\r\n");
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
                btnReadOnce.Enabled = false;
                btnStartRead.Enabled = false;
                btnStopRead.Enabled = false;
                lblConnectionStatus.Text = "Desconectado";
                lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
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
            // Verificacion del modo del lector.
            byte bParamAddr = 0;
            byte[] bValue = new byte[2];

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x02;

            if (CFHidApi.CFHid_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false)
            {
                SetText("No se pudo recuperar el modo del lector.");
                return;
            }

            // Si el lector no esta en modo activa, se setea el estado mencionado
            if (bValue[0] != 01)
            {
                System.Diagnostics.Debug.WriteLine("Cambiando a Modo Activo");

                byte paramToSet = 0;
                byte valueToSet = 0;

                /*  01: Transport
                    02: WorkMode
                    03: DeviceAddr
                    04: FilterTime
                    05: RFPower
                    06: BeepEnable
                    07: UartBaudRate*/
                paramToSet = 0x02;

                // ActiveMode = 01
                valueToSet = 01;

                if (CFHidApi.CFHid_SetDeviceOneParam(0xFF, paramToSet, valueToSet) == false)
                {
                    SetText("Fallo el Seteo al Modo Activo");
                    return;
                }
                SetText("Modo Activo Seteado");

                //CFHidApi.CFHid_StopRead(0xFF);
            }
            else
            {
                SetText("Modo Activo Listo\r\n");
            }
            string str = BitConverter.ToString(bValue);
            System.Diagnostics.Debug.WriteLine("get: ", str);


            //CFHidApi.CFHid_ClearTagBuf();
            bool readInfo = CFHidApi.CFHid_StartRead(0xFF);

            SetText("Iniciando lectura...\r\n");
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }


        private void btnStopRead_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            CFHidApi.CFHid_StopRead(0xFF);
        }


        private async void timer1_Tick(object sender, EventArgs e)
        {

            byte[] arrBuffer = new byte[64000];
            byte bRet;


            bRet = CFHidApi.CFHid_GetTagBuf(arrBuffer, out int iTotalLen, out int iNum);

            if (bRet == 1)
            {
                SetText("DevOut");
                return; //DevOut
            }
            else if (bRet == 0) return; //No Connect

            int iTagLength = 0;
            int iTagNumber = 0;
            iTagLength = iTotalLen;
            iTagNumber = iNum;

            if (iTagNumber == 0) return;

            //int iIndex = 0;
            int iLength = 0;
            byte bPackLength;
            int i;
            int iIDLen;

            for (int iIndex = 0; iIndex < iTagNumber; iIndex++)
            {

                bPackLength = arrBuffer[iLength];
                string str2 = "";
                string str1 = "";
                str1 = arrBuffer[1 + iLength + 0].ToString("X2");

                // Type 
                string type = str1;

                str2 = str2 + "Type:" + str1 + " ";  //Tag Type
                if ((arrBuffer[1 + iLength + 0] & 0x80) == 0x80)
                {   // with TimeStamp , last 6 bytes is time
                    iIDLen = bPackLength - 7;
                }
                else iIDLen = bPackLength - 1;

                str1 = arrBuffer[1 + iLength + 1].ToString("X2");

                // Antenna
                string antenna = str1;

                str2 = str2 + "Ant:" + str1 + " Tag:";  //Ant

                string str3 = "";
                for (i = 2; i < iIDLen; i++)
                {
                    str1 = arrBuffer[1 + iLength + i].ToString("X2");
                    str3 = str3 + str1;
                    //str3 = str3 + str1 + " ";

                }

                // EPC
                string epc = str3;

                str2 = str2 + str3;
                str1 = arrBuffer[1 + iLength + i].ToString("X2");

                // RSSI
                string rssi = str1;

                str2 = str2 + "RSSI:" + str1 + "\r\n";  //RSSI
                iLength = iLength + bPackLength + 1;




                // Logica de actualizacion
                // Si la lectura NO esta en el Dictionary
                if (!readingsInfo.TryGetValue(epc, out Reading lastReadingByEPC))
                {
                    Reading reading = new Reading
                    {
                        //Type = arrBuffer[1 + iLength + 0].ToString("X2"),
                        Type = type,
                        //Antenna = arrBuffer[1 + iLength + 1].ToString("X2"),
                        Antenna = antenna,
                        //Tag = "", // Aquí debes agregar la representación adecuada del Tag
                        EPC = epc,
                        //RSSI = arrBuffer[1 + iLength + i].ToString("X2")
                        RSSI = rssi,
                    };

                    System.Diagnostics.Debug.WriteLine("Arriba de updateEPCLocation");

                    string EPCWithoutSpace = epc.Replace(" ", "");

                    Product product = await GetProductByEPC(EPCWithoutSpace);

                    // Verificar si existe el epc en la bodega
                    if (product != null)
                    {
                        // Verificar si la bodega del producto es la misma de la antena
                        if (product.WarehouseId != selectedWarehouse.Id)
                        {
                            System.Diagnostics.Debug.WriteLine("Bodegas de producto y antena, distintas.");
                        
                            System.Diagnostics.Debug.WriteLine($"reading selectedBodega: {selectedWarehouse.Id}");
                            
                            UpdateEPCLocation(epc, selectedWarehouse.Id);
                        }

                        reading.Warehouse = selectedWarehouse.Id;

                        System.Diagnostics.Debug.WriteLine($"reading Bodega: {reading.Warehouse}");
                    }

                    reading.LastAPICalled = DateTime.Now;

                    readingsInfo.Add(epc, reading);

                    System.Diagnostics.Debug.WriteLine("Se agrega al Dictionary");

                    System.Diagnostics.Debug.WriteLine($"obteniendo el epc recien agregado: {readingsInfo.TryGetValue(epc, out Reading testing)}");
                        
                    System.Diagnostics.Debug.WriteLine($"bodega recien agregada?: {testing?.Warehouse}");
                }

                // Si la lectura esta en el dictionary
                else
                {
                    System.Diagnostics.Debug.WriteLine($"El dictionary tiene el EPC: {lastReadingByEPC.EPC}");

                    // Si el epc tiene Bodega en el dictionary
                    if (lastReadingByEPC.Warehouse != 0)
                    {
                        if (lastReadingByEPC.Warehouse != selectedWarehouse.Id)
                        {
                            System.Diagnostics.Debug.WriteLine($"EPC: {lastReadingByEPC.EPC} con bodega Distinta: {lastReadingByEPC.Warehouse} ");

                            UpdateEPCLocation(epc, selectedWarehouse.Id);

                            lastReadingByEPC.Warehouse = selectedWarehouse.Id;

                            lastReadingByEPC.LastAPICalled = DateTime.Now;

                            System.Diagnostics.Debug.WriteLine($"EPC: {lastReadingByEPC.EPC} con bodega Distinta: {lastReadingByEPC.Warehouse} ");

                        }

                    }
                    else
                    {
                        // Si no tiene Bodega y ademas pasaron mas de 30 segundos desde la ultima verificacion
                        if ((DateTime.Now - lastReadingByEPC.LastAPICalled).TotalSeconds > 10)
                        {
                            lastReadingByEPC.LastAPICalled = DateTime.Now;
                            System.Diagnostics.Debug.WriteLine($"Sin bodega y paso el tiempo.");

                            Product prod = await GetProductByEPC(epc);

                            if (prod != null)
                            {
                                if (prod.WarehouseId != selectedWarehouse.Id)
                                {
                                    try
                                    {
                                        UpdateEPCLocation(epc, selectedWarehouse.Id);
                                        lastReadingByEPC.Warehouse = selectedWarehouse.Id;
                                    }
                                    catch (Exception ex)
                                    {
                                        System.Diagnostics.Debug.WriteLine (ex.ToString());
                                    }
                                }
                            }
                        }
                    }
                }


                // 
                // Comentario EXTRA (usar columna isOut? para verificar la salida en casos de solo 1 antena)
                //



                SetText(str2);
            }

            //foreach (var lectura in lecturas)
            //{
            //    string output = $"Type: {lectura.Type}, Antenna: {lectura.Antenna}, Tag: {lectura.EPC}, RSSI: {lectura.RSSI}";
            //    System.Diagnostics.Debug.WriteLine(output);
            //}
        }


        private async void UpdateEPCLocation(string EPC, int warehouseId)
        {

            string EPCWithoutSpace = EPC.Replace(" ", "");

            System.Diagnostics.Debug.WriteLine($"UpdateEPCLocation: {EPCWithoutSpace}");

            Product respProduct = await productAPI.UpdateProductWarehouse(EPCWithoutSpace, warehouseId);

            System.Diagnostics.Debug.WriteLine($"respProduct: {respProduct.EPC}, bodega: {respProduct.WarehouseId}");

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

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] arrBuffer = new byte[64000];  // Tamaño del buffer según necesidad
            int totalLength = 0;
            int tagNumber = 0;

            byte result = CFHidApi.CFHid_GetTagBuf(arrBuffer, out totalLength, out tagNumber);

            System.Diagnostics.Debug.WriteLine(result);

            System.Diagnostics.Debug.WriteLine(result.ToString("X2"));

            if (result == 2)
            {
                System.Diagnostics.Debug.WriteLine("Positivo: {0}, {1}", totalLength, tagNumber);

            }
            else
            {
                SetText("Fallo al recuperar buffer de tags.");
                System.Diagnostics.Debug.WriteLine("Negativo: {0}, {1}", totalLength, tagNumber);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte bParamAddr = 0;
            byte[] bValue = new byte[2];

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x02;

            if (CFHidApi.CFHid_ReadDeviceOneParam(0xFF, bParamAddr, bValue) == false)
            {
                this.SetText("Faild");
                return;
            }
            else
            {
                string str = BitConverter.ToString(bValue);
                System.Diagnostics.Debug.WriteLine("get: {0} ", str);
                System.Diagnostics.Debug.WriteLine("get: {0}", BitConverter.ToString(bValue));

                //this.SetText();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte bParamAddr = 0;
            byte bValue = 0;

            /*  01: Transport
                02: WorkMode
                03: DeviceAddr
                04: FilterTime
                05: RFPower
                06: BeepEnable
                07: UartBaudRate*/
            bParamAddr = 0x02;
            //bValue = 26;   //RF = 26

            bValue = (byte)01;

            if (CFHidApi.CFHid_SetDeviceOneParam(0xFF, bParamAddr, bValue) == false)
            {
                this.SetText("Faild");
                return;
            }
            this.SetText("Success");
        }

        private void cboBoxBranches_SelectedValueChanged(object sender, EventArgs e)
        {
            cboBoxWarehouses.Items.Clear();

            Branch selectedBranch = (Branch)cboBoxBranches.SelectedItem;
            //System.Diagnostics.Debug.WriteLine("Cambio la sucursal - Nombre:{0} - ID:{1}", selectedBranch.Name, selectedBranch.Id);

            //System.Diagnostics.Debug.WriteLine("modelo bodega - nombre: {0}, branchId: {1}, id: {2}", warehousesNames[0].Name, warehousesNames[0].BranchId, warehousesNames[0].Id);

            List<Warehouse> cboBoxOptionsWarehouses = warehousesNames.FindAll(w => w.BranchId == selectedBranch.Id);

            foreach (Warehouse w in cboBoxOptionsWarehouses)
            {
                cboBoxWarehouses.Items.Add(w);
            }
        }

        private void btnUpdateSettings_Click(object sender, EventArgs e)
        {
            Warehouse warehouse = (Warehouse)cboBoxWarehouses.SelectedItem;
            lblWarehouseSetted.Text = "ID: " + warehouse.Id + " - " + warehouse.Name;
            selectedWarehouse = warehouse;
        }
    }
}