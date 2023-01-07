using ExcelDataReader;
using freeArve.service;
using freeArve.util;

namespace freeArve;

public class InvoicesExcelReader
{
    const string DUE_DATE_FORMAT = "dd.MM.yyyy";

    private IExcelDataReader reader;
    private string paymentFileName;
    private int rowIdx = 0;
    private object Excel;

    public InvoicesExcelReader(string paymentFileName)
    {
        this.paymentFileName = paymentFileName;
    }


    public List<InputRow> readInPaymentsFile()
    {
        List<InputRow> result = new List<InputRow>();

        /*
        Microsoft.Office.Interop.Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbooks wbs = ex.Workbooks;

        wbs.OpenText(paymentFileName,
            DataType: Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited,
            Tab: true);
        */


        using (var stream = File.Open(paymentFileName, FileMode.Open, FileAccess.Read))
        {
            using (reader = ExcelReaderFactory.CreateReader(stream))
            {
                rowIdx = 0;

                while (reader.Read())
                {
                    if (rowIdx == 0 && Conf.determineColumnIndiciesByTitle == 1)
                    {
                        Dictionary<string, int> firstRowCells = new Dictionary<string, int>();
                        for (int i = 0; i < reader.FieldCount; i++) {
                            string title = getCellOrEmptyString(i);
                            if (!string.IsNullOrEmpty(title))
                            {
                                firstRowCells.TryAdd(title, i);
                                Log.debug($"Mapped title {title} to column {i}");
                            }
                        }
                        Log.info($"Read {reader.FieldCount} columns titles from the 1st row.");

                        ConfLoader.setUpColumnsByTheFirstRowTitles(firstRowCells);
                        rowIdx++;
                        continue;
                    }


                    if (++rowIdx > Conf.numberOfLinesToSkipFromTop && reader.GetValue(Conf.columnAmount) != null && getCellOrEmptyString(Conf.columnPayerName) != null)
                    {
                        InputRow inputRow = new InputRow()
                        {
                            rowNumberInSourceFile = rowIdx,
                            amount = getCellAsDecimal(Conf.columnAmount),
                            refNumber = getCellOrEmptyString(Conf.columnRefNr),
                            documentName = getCellOrEmptyString(Conf.columnDocName),
                            payerName = getCellOrEmptyString(Conf.columnPayerName),
                            payerRegNumber = getCellOrEmptyString(Conf.columnRegNumber),
                            dueDate = makeDueDateFallingBackOnDefaultDay(Conf.columnDueDate),
                            description = getCellOrEmptyString(Conf.columnDescription),
                            descriptionDetail = getCellOrEmptyString(Conf.columnDescriptionDetail),
                            invoiceNumber = getCellOrEmptyString(Conf.columnInvoiceNumber),
                        };
                        result.Add(inputRow);
                    }
                }
            }
        }

        return result;
    }

    private DateTime makeDueDateFallingBackOnDefaultDay(int columnIndex)
    {
        if (columnIndex >= 0)
        {
            string value = getCellOrEmptyString(columnIndex);
            if (value != "")
            {
                try
                {
                    DateTime dateTime = DateTime.ParseExact(value, DUE_DATE_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
                    if (dateTime > DateTime.Now)
                    {
                        return dateTime;
                    } else
                    {
                        Log.debug($"Provided dueDate {dateTime} is in the past.");
                    }
                }
                catch (Exception handledByFallback) {
                    Log.debug(handledByFallback.ToString());
                }
            }
        }
        DateTime result = Util.makeDefaultDueDate();
        Log.error($"DueDate not found on row {rowIdx} column={columnIndex}. Falling back on default payment day {Conf.fallBackPaymentDay}: {result}");
        return result;
    }


    private double getCellAsDouble(int columnIndex)
    {
        if (columnIndex >= 0)
        {
            string value = getCellOrEmptyString(columnIndex);
            if (value != "")
            {
                return Convert.ToDouble(value);
            }
        }
        return 0;
    }

    private decimal getCellAsDecimal(int columnIndex)
    {
        if (columnIndex >= 0)
        {
            string value = getCellOrEmptyString(columnIndex);
            if (value != "")
            {
                return Convert.ToDecimal(value);
            }
        }
        return 0;

    }

    private int getCellAsInt(int columnIndex)
    {
        if (columnIndex >= 0) {
            string value = getCellOrEmptyString(columnIndex);
            if (value != "")
            {
                return Convert.ToInt32(value);
            }
        }
        return 0;
    }

    
    private string getCellOrEmptyString(int columnIndex)
    {
        try
        {
            // -1 disables the look up
            if (columnIndex >= 0) {
                Log.debug($"[{rowIdx - 1}:{columnIndex}] = '{reader.GetValue(columnIndex)}'");
                if (reader.GetValue(columnIndex) != null)
                {
                    return reader.GetValue(columnIndex).ToString().Trim();
                }
            }
        } catch (Exception e)
        {
            Log.error($"Unexpected type on {rowIdx}:{columnIndex}: '{reader.GetValue(columnIndex)}'. {e.Message}");
            Log.error(e.Message);
        }
        return "";
    }

}