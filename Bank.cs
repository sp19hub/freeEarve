using Microsoft.Office.Interop.Excel;

namespace converter;

public class Bank
{
    public string name;
    public string SWIFT;
    public string identifierXX;

    public Bank(string name, string swift, string identifierXX)
    {
        this.name = name;
        this.SWIFT = swift;
        this.identifierXX = identifierXX;
    }
}
/*
Coop Pank aktsiaselts	EKRDEE22	42
Eesti Pank	EPBEEE2X	16
AS SEB Pank	EEUHEE2X	10
Swedbank AS	HABAEE2X	22
Luminor Bank AS	RIKOEE22	96 / 17
AS Citadele banka Eesti filiaal	PARXEE22	12
Svenska Handelsbanken AB Eesti filiaal	HANDEE22	83
AS TBB pank	TABUEE22	00
OP Corporate Bank plc Eesti filiaal	OKOYEE2X	51
AS LHV Pank	LHVBEE22	77
Bigbank AS	BIGKEE2B	75
 */