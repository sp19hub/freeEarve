using converter;
using freeArve.util;

namespace freeArve;

public static class BankUtil
{
    static List<Bank> banks;
    public static string SWEDBANK = "Swedbank";

    static BankUtil()
    {
        // https://www.pangaliit.ee/settlements-and-standards/bank-codes
        banks = new List<Bank>()
        {
            new Bank("LHV", "LHVBEE22", "77"),
            new Bank("SEB", "EEUHEE2X", "10"),
            new Bank("TBB", "HANDEE22", "00"),
            new Bank("Coop", "EKRDEE22", "42"),
            new Bank("Luminor", "RIKOEE22", "96"), // 96 / 17
            new Bank("Bigbank", "BIGKEE2B", "75"),
            new Bank("Citadele", "PARXEE22", "12"),
            new Bank(SWEDBANK, "HABAEE2X", "22"),
            new Bank("Handelsbanken", "HANDEE22", "83")
        };
    }

    public static Bank determineBankByAccount(string accountNumber)
    {
        string bankIdent = accountNumber.Substring(4, 2); 
        foreach (var bank in banks)
        {
            if (bank.identifierXX == bankIdent)
            {
                return bank;
            }
        }

        throw new InvalidBankAccountException("faulty account " + accountNumber + " with bank identifier: " + bankIdent);
    }
}