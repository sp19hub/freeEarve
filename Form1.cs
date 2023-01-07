using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ExcelDataReader;
using freeArve.service;
using freeArve.util;
using Microsoft.IdentityModel.Tokens;

namespace freeArve
{
    public partial class Form1 : Form
    {
        public const string APP_NAME = "freeEArve";
        public const string CONFIG_FILES_EXTENSION = ".earve.ini";
        private string paymentsFilePath;
        private string regNrMappingFilePath;
        private string OUT_FILENAME;
        private ConfigFile configFile;
        private List<string> validationErrorMessages;
        public static RichTextBox rtRef;

        Dictionary<string, string> regNr2accountMap = new Dictionary<string, string>();
        Dictionary<string, string> regNr2referenceNumberMap = new Dictionary<string, string>();

        public static void addMsg(string msg, Boolean displayRightAway = false)
        {
            StringBuilder sb = new StringBuilder(rtRef.Text);
            rtRef.Text = sb.AppendLine(msg).ToString();
            sb.Clear();
            if (displayRightAway) {
                rtRef.Show();
            }
        }

        public Form1()
        {
            InitializeComponent();
            try
            {
                resultTextBox.Hide();
                rtRef = resultTextBox;
                fillConfigFileCombo();
                loadAndFixConfiguration();

                OUT_FILENAME = Conf.saveToFolderPath + "\\" + APP_NAME + "_Swedbank_" + Conf.sellerRegNumber + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xml";

                paymentsFilePath = Conf.paymentsFilePath;
                regNrMappingFilePath = Conf.regNrMappingFilePath;

                labelFilePath2.Text = regNrMappingFilePath;
                selectedSourceFileLabel.Text = paymentsFilePath;
            } catch (Exception e) {
                Util.reportException(e, $"Please check *{CONFIG_FILES_EXTENSION} configuration file");
            }
        }

        private void fillConfigFileCombo() {
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            string dirPath = new FileInfo(EXE).Directory.FullName;
            FileInfo[] filePaths = new DirectoryInfo(dirPath).GetFiles("*" + CONFIG_FILES_EXTENSION);

            if (filePaths.Length == 0) { // read one folder up
                filePaths = new DirectoryInfo(dirPath).Parent.GetFiles("*" + CONFIG_FILES_EXTENSION);
            }
            if (filePaths.Length == 0) {
                throw new ArgumentException($"Configuration file{Environment.NewLine} {EXE}\\*{CONFIG_FILES_EXTENSION} {Environment.NewLine}is missing.");
            }


            foreach (FileInfo fileInfo in filePaths) {
                ComboboxItem item = new ComboboxItem()
                {
                    Text = fileInfo.Name,
                    Value = fileInfo.FullName
                };

                comboConfigFiles.Items.Add(item);
            }
            comboConfigFiles.SelectedIndex = 0;

            if (comboConfigFiles.Items.Count == 1) {
                comboConfigFiles.Enabled = false;
                comboConfigFiles.Hide();
                configFileLink.Left = comboConfigFiles.Left;
                configFileLink.Width = this.Width - 50 - comboConfigFiles.Left;
            }
        }

        private void loadAndFixConfiguration()
        {
            configFile = new ConfigFile((comboConfigFiles.SelectedItem as ComboboxItem).Value.ToString());
            configFileLink.Text = new FileInfo(configFile.path).Name;
            ConfLoader.loadConf(configFile);
            labelCompanyName.Text = Conf.sellerName;
            labelAccountNumber.Text = Conf.sellerAccount;

            if (string.IsNullOrEmpty(Conf.logFileFolderPath) || !Directory.Exists(Conf.logFileFolderPath))
            {
                string? binFolderPath = Path.GetDirectoryName(Application.ExecutablePath);
                if (!string.IsNullOrEmpty(binFolderPath)) {
                    Conf.logFileFolderPath = binFolderPath;
                } else {
                    Conf.logFileFolderPath = Util.getDesktopFolderPath();
                }
                configFile.Write("logFileFolderPath", Conf.logFileFolderPath, ConfLoader.SECTION_FILES);
            }

            if (Conf.deleteLogFileOnStartUp) {
                Log.deleteLogFile();
            }

            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Log.info("\r\n***********************************************");
            Log.info($"Started at {dateStr} with conf: {configFile.path}");

            if (string.IsNullOrEmpty(Conf.saveToFolderPath) || !Directory.Exists(Conf.saveToFolderPath))
            {
                Conf.saveToFolderPath = Util.getDesktopFolderPath();
                configFile.Write("saveToFolderPath", Conf.saveToFolderPath, ConfLoader.SECTION_FILES);
                Log.info("saveToFolderPath saved with: " + Conf.saveToFolderPath);
            }

        }

        private void btnConvertAndSave_Click(object sender, EventArgs eArgs)
        {
            try
            {
                rtRef.Text = "";
                validationErrorMessages = new List<string>();
                string sellerContractId = Conf.sellerContractId;

                try
                {
                    List<Invoice> invoices = collectInvoices(regNrMappingFilePath, paymentsFilePath, sellerContractId);
                    btnConvertAndSave.Hide();

                    if (invoices.Count() == 0)
                    {
                        MessageBox.Show("Didn't convert any invoices. Check the logs.");
                        Log.info("Didn't convert any invoices. Check the logs.");
                    } else {
                        saveResultToFile(invoices);
                        string msg = "Converted: " + invoices.Count + " invoices." + Environment.NewLine;

                        decimal totalSum = 0;
                        invoices.ForEach(inv => totalSum += inv.PaymentInfo.PaymentTotalSum);
                        msg += "TotalSum:  " + totalSum + Environment.NewLine + Environment.NewLine + "Saved to:" + Environment.NewLine + OUT_FILENAME;
                        Log.info(msg);
                        MessageBox.Show(msg);
                    }

                    resultTextBox.SelectionStart = resultTextBox.Text.Length;
                    // scroll it automatically
                    resultTextBox.ScrollToCaret();
                    resultTextBox.Show();

                    linkLabelLogFile.Visible = true;
                    linkLabelOutputFile.Visible = true;
                }
                catch (ValidationException handled)
                {
                    Util.reportException(handled, handled.Message);
                }
            } catch (Exception e) {
                Util.reportException(e, "Convertion failed");
            }
        }

        private void addValidationError(string msg)
        {
            validationErrorMessages.Add(msg);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private List<Invoice> collectInvoices(string regNr2accountMappingFileName, string paymentFileName, string sellerContractId)
        {
            // Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            loadMappingsFromExcel(regNr2accountMappingFileName);
            InvoicesExcelReader invoicesExcelReader = new InvoicesExcelReader(paymentFileName);
            List<InputRow> paymentRows = invoicesExcelReader.readInPaymentsFile();
            if (paymentRows == null || paymentRows.Count == 0)
            {
                addValidationError("Empty or invalid file provided: " + paymentFileName);
                throw new ValidationException();
            }

            List<Invoice> invoices = new List<Invoice>();

            int rowIdx = 0;
            foreach (InputRow row in paymentRows)
            {
                rowIdx++;
                if (isValidationOK(row))
                {
                    try
                    {
                        Invoice invoice = composeSingleInvoice(rowIdx, row);
                        invoices.Add(invoice);
                    } catch (InvalidBankAccountException bankEx) {
                        Log.logException(bankEx, $"Row {rowIdx}");
                        if (Conf.failOnInvalidBankAccount) {
                            throw bankEx;
                        }
                    } catch (Exception ex)
                    {
                        Log.error(ex.ToString());
                        if (ex is InvalidBankAccountException)
                        {
                            throw ex; // fail hard
                        }
                    }
                }
            }

            if (!validationErrorMessages.IsNullOrEmpty()) {
                Log.error(string.Join("\n", validationErrorMessages.ToArray()));
                if (Conf.failOnAnyError == 1)
                {
                    throw new ValidationException("Invalid rows exist. Use [failOnAnyError=0] to ignore all errors.");
                }
            }

            return invoices;
        }

        private bool isValidationOK(InputRow row)
        {
            if (row.payerRegNumber.Trim().Length != 11)
            {
                addValidationError($"Incorrect reg number '{row.payerRegNumber}' on line {row.rowNumberInSourceFile}");
                return false;
            }
            if (!regNr2accountMap.ContainsKey(row.payerRegNumber))
            {
                addValidationError($"Reg number '{row.payerRegNumber}' (on line {row.rowNumberInSourceFile}) is not found in the regnumbers' file.");
                return false;
            }
            if (Conf.mappingFileColumnRefNr >= 0 && !regNr2referenceNumberMap.ContainsKey(row.payerRegNumber)) {
                addValidationError($"Reference number for regNr '{row.payerRegNumber}' (on line {row.rowNumberInSourceFile}) is not found in the regnumbers' file.");
                return false;
            }

            if (row.amount <= 0)
            {
                addValidationError($"Line {row.rowNumberInSourceFile} has amount '{row.amount}'");
                return false;
            }
            return true;
        }

        private Invoice composeSingleInvoice(int rowIdx, InputRow row)
        {
                string payerRegNumber = row.payerRegNumber;
                string accountNumber = regNr2accountMap[payerRegNumber];

                BillPartyRecord sellerParty = new BillPartyRecord()
                {
                    Name = Conf.sellerName,
                    RegNumber = Conf.sellerRegNumber
                };
                string invoiceUUID = "fea_" + Conf.sellerRegNumber + "_" + DateTime.Now.ToString("yyMMddHHmm") + "_" + rowIdx;

                DateTime dueDate = row.dueDate;
                converter.Bank bank = BankUtil.determineBankByAccount(accountNumber);
                decimal amount = row.amount + 0.00M;

                string refNumber;
                if (Conf.mappingFileColumnRefNr >= 0) {
                    refNumber = regNr2referenceNumberMap[payerRegNumber];
                } else {
                    refNumber = row.refNumber;
                }

                Invoice invoice = new Invoice
                {
                    invoiceId = row.invoiceNumber,
                    channelId = bank.SWIFT,
                    regNumber = row.payerRegNumber,
                    channelAddress = accountNumber,
                    invoiceGlobUniqId = invoiceUUID,
                    sellerContractId = Conf.sellerContractId,
                    sellerRegnumber = Conf.sellerRegNumber,
                    PaymentInfo = generatePaymentInfo(row, amount, dueDate, invoiceUUID, refNumber), 
                };
                BillPartyRecord buyerParty = new BillPartyRecord()
                {
                    Name = stripBuggySuffixInPayerName(row.payerName),
                    RegNumber = row.payerRegNumber
                };

                invoice.InvoiceParties = new InvoiceParties()
                {
                    BuyerParty = buyerParty,
                    SellerParty = sellerParty
                };
                InvoiceInformationType invoiceInformationType = new InvoiceInformationType
                {
                    type = (row.amount > 0 ? "DEB" : "CRE")
                };
                invoice.InvoiceInformation = new InvoiceInformation()
                {
                    Type = invoiceInformationType,
                    DocumentName = string.IsNullOrEmpty(row.documentName) ? "EARVE" : row.documentName,
                    InvoiceNumber = row.invoiceNumber,
                    PaymentReferenceNumber = refNumber,
                    InvoiceDate = dueDate
                };
                invoice.InvoiceSumGroup = generateInvoiceSumGroup(row);
                invoice.InvoiceItem = generateInvoiceItem(row);
            return invoice;
        }

        private static string stripBuggySuffixInPayerName(string payerName)
        {
            if (!string.IsNullOrEmpty(payerName) && payerName.IndexOf("/") > 0)
            {
                payerName = payerName.Substring(0, payerName.IndexOf("/")).Trim();
            }
            return payerName;
        }

        private static InvoiceItem generateInvoiceItem(InputRow row)
        {
            InvoiceItem invoiceItem = new InvoiceItem();
            invoiceItem.InvoiceItemGroup = new InvoiceItemGroup[1];
            InvoiceItemGroup iig = new InvoiceItemGroup();

            List<ItemEntry> items = new List<ItemEntry>();
            try
            {
                ItemEntry item = new ItemEntry();
                item.Description = Util.normalizeStr(row.description);
                items.Add(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            iig.ItemEntry = items.ToArray();
            invoiceItem.InvoiceItemGroup[0] = iig;
            return invoiceItem;
        }

        
        private static PaymentInfo generatePaymentInfo(InputRow row, decimal amount, DateTime dueDate, string invoiceUuid, string referenceNumber)
        {
            Log.debug("generatePaymentInfo: amount=" + amount + "; dueDate=" + dueDate + "; invoiceUuid=" + invoiceUuid + "; payerName=" + row.payerName);
            PaymentInfo paymentInfo = new PaymentInfo()
            {
                Currency = "EUR",
                PaymentId = invoiceUuid,
                PaymentTotalSum = amount,
                PayerName = row.payerName,
                Payable = "YES",
                PayDueDate = dueDate,
                PayDueDateSpecified = true,

                PayToName = Conf.sellerName,
                PayToAccount = Conf.sellerAccount,
            };


            paymentInfo.ItemsElementName = new ItemsChoiceType[2];
            paymentInfo.ItemsElementName[0] = ItemsChoiceType.PaymentRefId;
            paymentInfo.ItemsElementName[1] = ItemsChoiceType.PaymentDescription;
            string[] items = new string[2];
            string descriptionDetail = string.IsNullOrEmpty(row.descriptionDetail) ? null : (Conf.descriptionDetailPrefix + " " + row.descriptionDetail);
            Log.debug($"descriptionDetail(row{row.rowNumberInSourceFile}) = {descriptionDetail}");
            items[0] = referenceNumber;
            items[1] = Util.normalizeStr(descriptionDetail);            
            paymentInfo.Items = items;

            return paymentInfo;
        }

        private static InvoiceSumGroup[] generateInvoiceSumGroup(InputRow row)
        {
            InvoiceSumGroup invoiceSumGroup = new InvoiceSumGroup();
            // AccountingRecord accountingRecord = new AccountingRecord();
            // AccountingRecordJournalEntry[] entries = new AccountingRecordJournalEntry[1];
            // AccountingRecordJournalEntry entryPayer = new AccountingRecordJournalEntry();
            // entryPayer.CostObjective = row.payerRegNumber;
            // entries[0] = entryPayer;
            // accountingRecord.JournalEntry = entries;
            // accountingRecord.Description = row.description;
            // invoiceSumGroup.Accounting = accountingRecord;
           
            // invoiceSumGroup.InvoiceSum = row.amount + 0.00M;
            // invoiceSumGroup.InvoiceSumSpecified = true;
            // invoiceSumGroup.TotalVATSum = 0.00M;
            // invoiceSumGroup.TotalVATSumSpecified = false;
            invoiceSumGroup.TotalSum = row.amount + 0.00M;// + invoiceSumGroup.TotalVATSum;
            InvoiceSumGroup[] group = new InvoiceSumGroup[1];
            group[0] = invoiceSumGroup;
            return group;
        }


        private void loadMappingsFromExcel(string mappingFileName)
        {
            using (var stream = File.Open(mappingFileName, FileMode.Open, FileAccess.Read))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                ExcelReaderConfiguration excelReaderConfiguration = new ExcelReaderConfiguration
                {
                    FallbackEncoding = Encoding.Latin1
                };

                using (var reader = ExcelReaderFactory.CreateReader(stream, excelReaderConfiguration))
                {
                    while (reader.Read())
                    {
                        if (reader.FieldCount >= 2 && reader.GetValue(Conf.mappingFileColumnRegNr) != null && reader.GetValue(Conf.mappingFileColumnAccountNr) != null)
                        {
                            string regNr = reader.GetValue(Conf.mappingFileColumnRegNr).ToString().Trim();
                            string accountNr = reader.GetValue(Conf.mappingFileColumnAccountNr).ToString().Trim();
                            regNr2accountMap.TryAdd(regNr, accountNr);

                            if (Conf.mappingFileColumnRefNr != -1 && reader.GetValue(Conf.mappingFileColumnRefNr) != null)
                            {
                                string referenceNumber = reader.GetValue(Conf.mappingFileColumnRefNr).ToString().Trim();
                                regNr2referenceNumberMap.TryAdd(regNr, referenceNumber);
                            }
                        }
                    }
                }
            }
        }

        private void btnOpenFileDialogue1_Click(object sender, EventArgs e)
        {
            resultTextBox.Hide();
            fdRegNrMapping = new OpenFileDialog();
            fdRegNrMapping.Title = "Vali arvete fail";
            fdRegNrMapping.Filter = "Excel Files|*.xls;*.xlsx;*.csv";
            DialogResult dialogResult = fdRegNrMapping.ShowDialog();

            if (dialogResult == DialogResult.OK && !string.IsNullOrEmpty(fdRegNrMapping.FileName))
            {
                paymentsFilePath = fdRegNrMapping.FileName;
                selectedSourceFileLabel.Text = fdRegNrMapping.FileName;
                configFile.Write("PaymentsFilePath", paymentsFilePath, ConfLoader.SECTION_FILES);
            }
        }



        private void buttonMappingFile_Click(object sender, EventArgs e)
        {
            resultTextBox.Hide();
            fdRegNrMapping = new OpenFileDialog();
            fdRegNrMapping.Title = "Vali isikukoodide ja konto numbrite vastavuse fail";
            fdRegNrMapping.Filter = "Excel Files|*.xls;*.xlsx;*.csv";
            DialogResult dialogResult = fdRegNrMapping.ShowDialog();

            if (dialogResult == DialogResult.OK && !string.IsNullOrEmpty(fdRegNrMapping.FileName))
            {
                labelFilePath2.Text = fdRegNrMapping.FileName;
                regNrMappingFilePath = fdRegNrMapping.FileName;
                configFile.Write("RegNrMappingFilePath", regNrMappingFilePath, ConfLoader.SECTION_FILES);
            }
        }


        private void saveResultToFile(List<Invoice> invoices)
        {
            E_Invoice e_Invoice = new E_Invoice();
            Header header = new Header();
            header.Date = DateTime.Today;
            header.FileId = "fea_" + DateTime.Now.ToString("yyyyMMddHHmm");
            header.AppId = "EARVE";
            header.Version = "1.11";
            e_Invoice.Header = header;

            // footer
            Footer footer = new Footer();
            string invoicesCount = invoices.Count().ToString();
            footer.TotalNumberInvoices = invoicesCount;
            decimal totalAmount = 0;
            invoices.ForEach(invoice => totalAmount += invoice.PaymentInfo.PaymentTotalSum);
            footer.TotalAmount = totalAmount + 0.00M;
            e_Invoice.Invoice = invoices.ToArray();
            e_Invoice.Footer = footer;


            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            using (var writer = XmlWriter.Create(OUT_FILENAME, xmlWriterSettings))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                var serializer2 = new XmlSerializer(e_Invoice.GetType());
                serializer2.Serialize(writer, e_Invoice, ns);
            }
        }

        private void configFileLink_Clicked(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", configFile.path); 
        }

        private void linkLabelOutputFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", OUT_FILENAME);
        }

        private void linkLabelLogFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", Log.getLogFilePath());
        }

        private void comboConfigFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAndFixConfiguration();
        }
    }
}
