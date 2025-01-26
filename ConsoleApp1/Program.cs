//==========================================================
// Student Number : S10268880K
// Student Name : Yao Yao
// Partner Name : Atifah 
// features: 23569
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
    Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-20}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (KeyValuePair<string, Flight> kvp in terminal5.Flights)
    {
        string flightN = kvp.Key;
        string[] numAndCode = flightN.Split(' ');
        string code = numAndCode[0];
        string airlineN = terminal5.Airlines[code].Name;
        Console.WriteLine($"{kvp.Key,-15} {airlineN,-20} {kvp.Value.Origin,-20} {kvp.Value.Destination,-20}{kvp.Value.ExpectedTime}");
    }
}

// feature 4
void ListAllBoardingGates()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine("Gate Name\tDDJB\tCFFT\tLWTT");
    Console.WriteLine("==========================================");

    // Iterate through all Boarding Gates in Terminal 5

    foreach (var gate in terminal5.BoardingGates.Values)
    {
        // Print the gate name and special request codes
        Console.Write($"{gate.GateName}\t");
        Console.Write($"{gate.SupportsDDJB}\t");
        Console.Write($"{gate.SupportsCFFT}\t");
        Console.WriteLine($"{gate.SupportsLWTT}");
    }
}


//feature 5
void assignGate()
{
    while (true)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Assign a Boarding Gate to a Flight");
        Console.WriteLine("=============================================");
        Console.WriteLine("Enter Flight Number:");
        string flightN = Console.ReadLine();
        Console.WriteLine("Enter Boarding Gate Name:");
        string gateN = Console.ReadLine();
        BoardingGate boardingGate = terminal5.BoardingGates[gateN];
        if (terminal5.AddBoardingGate(boardingGate))
        {
            string? specialCode = null;
            if (terminal5.Flights[flightN] is NORMFlight)
            {
                specialCode = "None";
            }
            else if (terminal5.Flights[flightN] is LWTTFlight)
            {
                specialCode = "LWTT";
            }
            else if (terminal5.Flights[flightN] is DDJBFlight)
            {
                specialCode = "DDJB";
            }
            else if (terminal5.Flights[flightN] is CFFTFlight)
            {
                specialCode = "CFFT";
            }
            Console.WriteLine($"Flight Number: {flightN}");
            Console.WriteLine($"Origin: {terminal5.Flights[flightN].Origin}");
            Console.WriteLine($"Destination: {terminal5.Flights[flightN].Destination}");
            Console.WriteLine($"Expected Time: {terminal5.Flights[flightN].ExpectedTime}");
            Console.WriteLine($"Special Request Code: {specialCode}");
            Console.WriteLine($"Boarding Gate Name: {gateN}");
            Console.WriteLine($"Supports DDJB: {terminal5.BoardingGates[gateN].SupportsDDJB}");
            Console.WriteLine($"Supports CFFT: {terminal5.BoardingGates[gateN].SupportsCFFT}");
            Console.WriteLine($"Supports LWTT: {terminal5.BoardingGates[gateN].SupportsLWTT}");
            Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
            string confirmation = Console.ReadLine();
            if (confirmation == "Y")
            {
                Console.WriteLine("1. Delayed");
                Console.WriteLine("2. Boarding");
                Console.WriteLine("3. On Time");
                Console.WriteLine("Please select the new status of the flight:");
                string selection = Console.ReadLine();
                if(selection == "1")
                {
                    terminal5.Flights[flightN].Status = "Delayed";
                }
                else if(selection == "2")
                {
                    terminal5.Flights[flightN].Status = "Boarding";
                }
                else if (selection == "2")
                {
                    terminal5.Flights[flightN].Status = "On Time";
                }
                terminal5.BoardingGates[gateN].Flight = terminal5.Flights[flightN];
                Console.WriteLine(terminal5.BoardingGates[gateN].Flight);
                Console.WriteLine($"Flight {flightN} has been assigned to Boarding Gate {gateN}!");
                break;
            }
            else
            {
                terminal5.BoardingGates[gateN].Flight = terminal5.Flights[flightN];
                terminal5.Flights[flightN].Status = "On Time";
                Console.WriteLine($"Flight {flightN} has been assigned to Boarding Gate {gateN}!");
                break;
            }
        }
        else
        {
            Console.WriteLine("Boarding Gate is already assigned. Please try again!");
        }
    } 
}

//feature 6






















//feature 7

void DisplayAirlineFlightDetails(Dictionary<string, Airline> airlines)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    // Display the list of airline codes and names
    foreach (var airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code}: {airline.Name}");
    }

    // Prompt the user for an airline code
    Console.Write("\nEnter the 2-Letter Airline Code (e.g., SQ, MH): ");
    string airlineCode = Console.ReadLine()?.ToUpper();

    // Retrieve the airline
    if (!airlines.TryGetValue(airlineCode, out Airline selectedAirline))
    {
        Console.WriteLine("Invalid Airline Code. Please try again.");
        return;
    }

    // Display the flights for the selected airline
    Console.WriteLine($"\nFlight Details for {selectedAirline.Name}:");
    Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-15} {5,-10}", 
        "Flight Number", "Origin", "Destination", "Expected Time", "Special Code", "Gate");

    foreach (var flight in selectedAirline.Flights.Values)
    {
        string gateName = flight.BoardingGate != null ? flight.BoardingGate.GateName : "Unassigned";
        Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-15} {5,-10}", 
            flight.Number, flight.Origin, flight.Destination, 
            flight.ExpectedTime, flight.SpecialRequestCode, gateName);
    }
}



    //feature 8































    //feature 9































    //advanced feature 1











    //advanced feature 2























    //main 
    while (true)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
    Console.WriteLine("Please select your option:");
    string option = Console.ReadLine();
    if (option == "1")
    {
        displayFlights();
    }
    else if (option == "2")
    {

    }
    else if (option == "3")
    {
        assignGate();
    }
    else if (option == "4")
    {

    }
    else if (option == "5")
    {

    }
    else if (option == "6")
    {

    }
    else if (option == "7")
    {
        DisplayAirlineFlightDetails(Dictionary<string, Airline> airlines);
    }
    else if (option == "0")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else
    {
        Console.WriteLine("Invalid Option Number. Please try again.");
    }
}
