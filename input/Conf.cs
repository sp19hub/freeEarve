namespace freeArve;

public class Conf
{
    public static int failOnAnyError { get; set; }
    public static bool deleteLogFileOnStartUp;
    public static bool failOnInvalidBankAccount;
    public static int logLevel { get; set; }
    public static string sellerName { get; set; }
    public static string sellerRegNumber { get; set; }
    public static string sellerAccount;
    public static string sellerContractId;
    public static int fallBackPaymentDay;

    public static string descriptionDetailPrefix;

    public static int determineColumnIndiciesByTitle;
    public static string titleDueDate;
    public static string titlePayerName;
    public static string titleAmount;
    public static string titleDescription;
    public static string titleDescriptionDetail;
    public static string titleRegNumber;
    public static string titleInvoiceNumber;
    public static string titleRefNr;
    public static string titleDocName;


    public static int numberOfLinesToSkipFromTop;
    public static int columnDueDate;
    public static int columnPayerName; // Hack to handle invalid input data: "AIVAR TAMM/KJ" => "AIVAR TAMM" - trailing slash and any chars after it will be cleaned
    public static int columnAmount;
    public static int columnDescription;
    public static int columnDescriptionDetail;
    public static int columnRegNumber; // Isikukood
    public static int columnInvoiceNumber;

    public static int columnRefNr = -1;
    public static int columnDocName = -1;

    // mapping of columns in the regnumbers' file
    public static int mappingFileColumnRegNr = 0;
    public static int mappingFileColumnAccountNr = 1;
    public static int mappingFileColumnRefNr = -1;

    public static string paymentsFilePath; 
    public static string regNrMappingFilePath;
    public static string saveToFolderPath;
    public static string logFileFolderPath;
}