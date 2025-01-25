//==========================================================
// Student Number : S10268880K
// Student Name : Yao Yao
// Partner Name : Atifah 
//==========================================================

using S10268880K_PRG2Assignment;
using System.Runtime.Intrinsics.X86;

Console.WriteLine("\r\nLoading Airlines...\r\n8 Airlines Loaded!\r\nLoading Boarding Gates...\r\n66 Boarding Gates Loaded!\r\nLoading Flights...\r\n30 Flights Loaded!");

//terminal
Terminal terminal5 = new Terminal("Terminal5", new Dictionary<string, Airline>(), new Dictionary<string, Flight>(), new Dictionary<string, BoardingGate>(), new Dictionary<string, double>());
terminal5.GateFees.Add("Base", 300.00);
terminal5.GateFees.Add("DDJB", 300.00);
terminal5.GateFees.Add("CFFT", 150.00);
terminal5.GateFees.Add("LWTT", 500.00);

//feature1
using (StreamReader sr = new StreamReader("airlines.csv"))
{
    sr.ReadLine();
    string? s;
    while ((s = sr.ReadLine()) != null)
    {
        string[] airlinesInfo = s.Split(',');
        Airline airline = new Airline(airlinesInfo[0], airlinesInfo[1], new Dictionary<string, Flight>());
        terminal5.Airlines.Add(airlinesInfo[1], airline);
    }
}
using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    string? s = sr.ReadLine();
    while ((s = sr.ReadLine()) != null)
    {
        string[] gateInfo = s.Split(',');   // name,ddjb,cfft,lwtt
        bool dDJB = Convert.ToBoolean(gateInfo[1]);
        bool cFFT = Convert.ToBoolean(gateInfo[2]);
        bool lWTT = Convert.ToBoolean(gateInfo[3]);
        BoardingGate boardingGate = new BoardingGate(gateInfo[0], cFFT, dDJB, lWTT, null);
        terminal5.BoardingGates.Add(gateInfo[0], boardingGate);
    }
}

//feature 2
using (StreamReader sr = new StreamReader("flights.csv"))
{
    sr.ReadLine();
    string? s;  
    while ((s = sr.ReadLine()) != null)
    {
        string[] flightsInfo = s.Split(',');
        string specialRequestCode = flightsInfo[4].Trim();
        string[] firstEle = flightsInfo[0].Split(" ");
        string code = firstEle[0];

        if (string.IsNullOrEmpty(specialRequestCode))
        {
            DateTime dateTime = Convert.ToDateTime(flightsInfo[3]);
            NORMFlight nORMFlight = new NORMFlight(flightsInfo[0], flightsInfo[1], flightsInfo[2],dateTime,"null");
            terminal5.Flights.Add(flightsInfo[0], nORMFlight);
            terminal5.Airlines[code].Flights.Add(flightsInfo[0],nORMFlight);
        }
        else
        {
            if (flightsInfo[4] == "DDJB")
            {
                DateTime dateTime = Convert.ToDateTime(flightsInfo[3]);
                DDJBFlight dDJBFlight = new DDJBFlight(flightsInfo[0], flightsInfo[1], flightsInfo[2], dateTime, "null", 300.00);
                terminal5.Flights.Add(flightsInfo[0], dDJBFlight);
                terminal5.Airlines[code].Flights.Add(flightsInfo[0], dDJBFlight);
            }
            else if (flightsInfo[4] == "LWTT")
            {
                DateTime dateTime = Convert.ToDateTime(flightsInfo[3]);
                LWTTFlight lWTTFlight = new LWTTFlight(flightsInfo[0], flightsInfo[1], flightsInfo[2], dateTime, "null", 500.00);
                terminal5.Flights.Add(flightsInfo[0], lWTTFlight);
                terminal5.Airlines[code].Flights.Add(flightsInfo[0], lWTTFlight);
            }
            else if (flightsInfo[4] == "CFFT")
            {
                DateTime dateTime = Convert.ToDateTime(flightsInfo[3]);
                CFFTFlight cFFTFlight = new CFFTFlight(flightsInfo[0], flightsInfo[1], flightsInfo[2], dateTime, "null", 150.00);
                terminal5.Flights.Add(flightsInfo[0], cFFTFlight);
                terminal5.Airlines[code].Flights.Add(flightsInfo[0], cFFTFlight);
            }
            else
            {
                Console.WriteLine("Invalid file.");
            }
        }
    }
}

//feature 3
void displayFlights()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    foreach (KeyValuePair<string, Flight> kvp in terminal5.Flights)
    {
        Console.WriteLine(kvp.Value.ToString());
    }
foreach (KeyValuePair<string, Airline> kvp in terminal5.Airlines)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

}


displayFlights();

