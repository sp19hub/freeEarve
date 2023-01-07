# freeEarve
C# implementation of Excel file(s) converter to Estonian e-invoice (Swedbank dialect) e-arve file.

The program can produce e-arve file in Swedbank e-earve dialict taking data from 2 Excel files:
1) payment data: regNumber, amount, details
2) mapping by regNumber to account number, name.
The 2nd file could be configured (chosen) to be same as the 1st one. In that case all required payments' information must be presented in the single Excel file.

Prerequisites:
.NET
Concluded e-invoice agreement in Swedbank Business inteernet bank.

See *.earve.ini sample configuration files and modify them, specifying Swedbank's e-invoice agreement number and other credentials.
Once the file is produced, it can be uploaded to Swedbank internet bank e-invoices page.
