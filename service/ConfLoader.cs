using freeArve.util;

namespace freeArve.service;

public class ConfLoader
{
    public static string SECTION_GENERAL = "general";
    public static string SECTION_FILES = "files";
    public static string SECTION_COMPANY = "company";
    public static string SECTION_PAYMENT_FILE = "payments_file_mapping";
    public static string SECTION_REGNR_MAPPING_FILE = "regnumbers_file_mapping";

    public static void loadConf(ConfigFile configFile)
    {
        try
        {
            Conf.determineColumnIndiciesByTitle = Convert.ToInt32(configFile.Read("determineColumnIndiciesByTitle", SECTION_PAYMENT_FILE));
            Conf.descriptionDetailPrefix = configFile.Read("descriptionDetailPrefix", SECTION_PAYMENT_FILE);

            Conf.deleteLogFileOnStartUp = configFile.Read("deleteLogFileOnStartUp", SECTION_GENERAL) == "1";
            Conf.failOnInvalidBankAccount = configFile.Read("failOnInvalidBankAccount", SECTION_GENERAL) == "1";
            if (Conf.determineColumnIndiciesByTitle == 1)
            {
                Conf.titleAmount = configFile.Read("titleAmount", SECTION_PAYMENT_FILE);
                Conf.titlePayerName = configFile.Read("titlePayerName", SECTION_PAYMENT_FILE);
                Conf.titleDueDate = configFile.Read("titleDueDate", SECTION_PAYMENT_FILE);
                Conf.titleDescription = configFile.Read("titleDescription", SECTION_PAYMENT_FILE);
                Conf.titleDescriptionDetail = configFile.Read("titleDescriptionDetail", SECTION_PAYMENT_FILE);
                Conf.titleRegNumber = configFile.Read("titleRegNumber", SECTION_PAYMENT_FILE);
                Conf.titleInvoiceNumber = configFile.Read("titleInvoiceNumber", SECTION_PAYMENT_FILE);
                Conf.titleRefNr = configFile.Read("titleRefNr", SECTION_PAYMENT_FILE);
                Conf.titleDocName = configFile.Read("titleDocName", SECTION_PAYMENT_FILE);
            }
            else
            {
                Conf.numberOfLinesToSkipFromTop = Convert.ToInt32(configFile.Read("numberOfLinesToSkipFromTop", SECTION_PAYMENT_FILE));
                Conf.columnDueDate = Convert.ToInt32(configFile.Read("columnDueDate", SECTION_PAYMENT_FILE));
                Conf.columnPayerName = Convert.ToInt32(configFile.Read("columnPayerName", SECTION_PAYMENT_FILE));
                Conf.columnAmount = Convert.ToInt32(configFile.Read("columnAmount", SECTION_PAYMENT_FILE));
                Conf.columnDescription = Convert.ToInt32(configFile.Read("columnDescription", SECTION_PAYMENT_FILE));
                Conf.columnDescriptionDetail = Convert.ToInt32(configFile.Read("columnDescriptionDetail", SECTION_PAYMENT_FILE));
                Conf.columnRegNumber = Convert.ToInt32(configFile.Read("columnRegNumber", SECTION_PAYMENT_FILE));
                Conf.columnInvoiceNumber = Convert.ToInt32(configFile.Read("columnInvoiceNumber", SECTION_PAYMENT_FILE));
                Conf.columnRefNr = Convert.ToInt32(configFile.Read("columnRefNr", SECTION_PAYMENT_FILE));
                Conf.columnDocName = Convert.ToInt32(configFile.Read("columnDocName", SECTION_PAYMENT_FILE));
            }

            Conf.logLevel = Convert.ToInt32(configFile.Read("logLevel", SECTION_GENERAL));
            Conf.failOnAnyError = Convert.ToInt32(configFile.Read("failOnAnyError", SECTION_GENERAL));

            Conf.sellerName = configFile.Read("sellerName", SECTION_COMPANY);
            Conf.sellerAccount = configFile.Read("sellerAccount", SECTION_COMPANY);
            Conf.sellerRegNumber = configFile.Read("sellerRegNumber", SECTION_COMPANY);
            Conf.sellerContractId = configFile.Read("sellerContractId", SECTION_COMPANY);
            Conf.fallBackPaymentDay = Convert.ToInt32(configFile.Read("fallBackPaymentDay", SECTION_COMPANY));

            Conf.regNrMappingFilePath = configFile.Read("RegNrMappingFilePath", SECTION_FILES);
            Conf.paymentsFilePath = configFile.Read("paymentsFilePath", SECTION_FILES);
            Conf.saveToFolderPath = configFile.Read("saveToFolderPath", SECTION_FILES);
            Conf.logFileFolderPath = configFile.Read("logFileFolderPath", SECTION_FILES);

            Conf.mappingFileColumnRegNr = Convert.ToInt32(configFile.Read("mappingFileColumnRegNr", SECTION_REGNR_MAPPING_FILE));
            Conf.mappingFileColumnAccountNr = Convert.ToInt32(configFile.Read("mappingFileColumnAccountNr", SECTION_REGNR_MAPPING_FILE));
            Conf.mappingFileColumnRefNr = Convert.ToInt32(configFile.Read("mappingFileColumnRefNr", SECTION_REGNR_MAPPING_FILE));
        }
        catch (Exception ex)
        {
            Util.reportException(ex, $"Configuration loading failed. Check *{Form1.CONFIG_FILES_EXTENSION} config file.");
        }
    }

    public static void setUpColumnsByTheFirstRowTitles(Dictionary<string, int> firstRowCells)
    {
        if (firstRowCells.ContainsKey(Conf.titleAmount.Trim())) Conf.columnAmount = firstRowCells[Conf.titleAmount.Trim()];
        if (firstRowCells.ContainsKey(Conf.titlePayerName.Trim())) Conf.columnPayerName = firstRowCells[Conf.titlePayerName.Trim()];
        if (firstRowCells.ContainsKey(Conf.titleDueDate.Trim())) Conf.columnDueDate = firstRowCells[Conf.titleDueDate.Trim()];
        if (firstRowCells.ContainsKey(Conf.titleDescription.Trim()))
        {
            Conf.columnDescription = firstRowCells[Conf.titleDescription.Trim()];
        }
        if (firstRowCells.ContainsKey(Conf.titleDescriptionDetail.Trim())) Conf.columnDescriptionDetail = firstRowCells[Conf.titleDescriptionDetail.Trim()];
        if (firstRowCells.ContainsKey(Conf.titleRefNr.Trim())) Conf.columnRefNr = firstRowCells[Conf.titleRefNr.Trim()];
        if (firstRowCells.ContainsKey(Conf.titleRegNumber.Trim())) Conf.columnRegNumber = firstRowCells[Conf.titleRegNumber.Trim()];
        if (firstRowCells.ContainsKey(Conf.titleInvoiceNumber.Trim())) Conf.columnInvoiceNumber = firstRowCells[Conf.titleInvoiceNumber.Trim()];
        if (firstRowCells.ContainsKey(Conf.titleDocName.Trim())) Conf.columnDocName = firstRowCells[Conf.titleDocName.Trim()];
    }
}