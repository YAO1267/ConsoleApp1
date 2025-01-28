﻿//==========================================================
// Student Number : S10268880K
// Student Name : Yao Yao
// Partner Name : Atifah 
// features: 23569
//==========================================================

using S10268880K_PRG2Assignment;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            //DateTime dateTime = Convert.ToDateTime(flightsInfo[3]);
            string dateTime1 = flightsInfo[3]; DateTime dateTime;
            if (DateTime.TryParseExact(dateTime1, "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                dateTime1 = dateTime.ToString("dd/MM/yyyy hh:mm");
            }
            else
            {
                DateTime dateTime2 = Convert.ToDateTime(dateTime1); NORMFlight nORMFlight = new NORMFlight(flightsInfo[0], flightsInfo[1], flightsInfo[2], dateTime2, "null");
                terminal5.Flights.Add(flightsInfo[0], nORMFlight); terminal5.Airlines[code].Flights.Add(flightsInfo[0], nORMFlight);
            }
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
void listBoardingGate()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20}", "Gate Name", "DDJB", "CFFT", "LWTT");
    foreach (KeyValuePair<string, BoardingGate> kvp in terminal5.BoardingGates)
    {
        Console.WriteLine($"{kvp.Key,-15} {kvp.Value.SupportsDDJB,-20} {kvp.Value.SupportsCFFT,-20} {kvp.Value.SupportsLWTT,-20}");
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

        if (terminal5.Flights.ContainsKey(flightN))
        {
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
                else
                {
                    Console.WriteLine("Invalid flight number");
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
                    if (selection == "1")
                    {
                        terminal5.Flights[flightN].Status = "Delayed";
                    }
                    else if (selection == "2")
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
        else
        {
            Console.WriteLine("Invalid flight number. Please try again!");
        }
    } 
}

//feature 6
void addFlight()
{
    while (true) {
        Console.Write("Enter Flight Number: ");
        string flightN = Console.ReadLine();

        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();

        Console.Write("Enter Destination: ");
        string destination = Console.ReadLine();

        Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
        string date = Console.ReadLine();
        DateTime expectedTime = DateTime.ParseExact(date,"dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture);

        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string specialCode = Console.ReadLine();

        string[] flightNumberCode = flightN.Split(" ");
        string airlineCode = flightNumberCode[0];

        if (specialCode == "CFFT")
        {
            CFFTFlight cFFTFlight = new CFFTFlight(flightN, origin, destination, expectedTime, "null", 150.00);
            if (terminal5.Airlines[airlineCode].AddFlight(cFFTFlight))
            {
                Console.WriteLine($"Flight {flightN} has been added!");
                Console.WriteLine("Would you like to add another flight? (Y/N)");
                string option = Console.ReadLine();
                if (option == "N")
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        else if (specialCode == "DDJB")
        {
            DDJBFlight dDJBFlight = new DDJBFlight(flightN, origin, destination, expectedTime, "null", 300.00);
            if (terminal5.Airlines[airlineCode].AddFlight(dDJBFlight))
            {
                Console.WriteLine($"Flight {flightN} has been added!");
                Console.WriteLine("Would you like to add another flight? (Y/N)");
                string option = Console.ReadLine();
                if (option == "N")
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        else if (specialCode == "LWTT")
        {
            LWTTFlight lTTFlight = new LWTTFlight(flightN, origin, destination, expectedTime, "null", 500.00);
            if (terminal5.Airlines[airlineCode].AddFlight(lTTFlight))
            {
                Console.WriteLine($"Flight {flightN} has been added!");
                Console.WriteLine("Would you like to add another flight? (Y/N)");
                string option = Console.ReadLine();
                if (option == "N")
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        else if (specialCode == "None")
        {
            NORMFlight nORMFlight = new NORMFlight(flightN, origin, destination, expectedTime, "null");
            if (terminal5.Airlines[airlineCode].AddFlight(nORMFlight))
            {
                Console.WriteLine($"Flight {flightN} has been added!");
                Console.WriteLine("Would you like to add another flight? (Y/N)");
                string option = Console.ReadLine();
                if (option == "N")
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        else
        {
            Console.WriteLine("Wrong Special Code. Please try again.");
        }
    }
}


//feature 7

void DisplayAirlineFlightDetails()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-25} {1,-20}", "Airline Code", "Airline Name");
    foreach (KeyValuePair<string, Airline> kvp in terminal5.Airlines)
    {
        Console.WriteLine($"{kvp.Key,-20} {kvp.Value.Name,-20} ");
    }
    Console.WriteLine("\nEnter Airline Code : ");
    string airlineCode = Console.ReadLine()?.ToUpper();

    if (terminal5.Airlines.ContainsKey(airlineCode))
    {
        Airline selectedAirline = terminal5.Airlines[airlineCode];

        Console.WriteLine("=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-35}", "Flight Number", "Airline Name","Origin","Destination", "Expected Departure/Arrival Time");

        if (selectedAirline.Flights.Count > 0)
        {
            //foreach (Flight flight in selectedAirline.Flights)
            foreach (KeyValuePair<string, Flight> kvp in selectedAirline.Flights)
            {
                Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-35}",
                                  kvp.Key,
                                  selectedAirline.Name,
                                  kvp.Value.Origin,
                                  kvp.Value.Destination,
                                  kvp.Value.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt"));
            }
        }
        else
        {
            Console.WriteLine("No flights available for this airline.");
        }
    }
    else
    {
        Console.WriteLine("Invalid Airline Code. Please try again.");
    }
}

  //feature 8
  void ModifyFlightDetails()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-25} {1,-20}", "Airline Code", "Airline Name");
    foreach (KeyValuePair<string, Airline> kvp in terminal5.Airlines)
    {
        Console.WriteLine($"{kvp.Key,-20} {kvp.Value.Name,-20} ");
    }
    Console.WriteLine("\nEnter Airline Code : ");
    string airlineCode = Console.ReadLine()?.ToUpper();




    if (terminal5.Airlines.ContainsKey(airlineCode))
    {
        Airline selectedAirline = terminal5.Airlines[airlineCode];

        Console.WriteLine("=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-35}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

        if (selectedAirline.Flights.Count > 0)
        {
            //foreach (Flight flight in selectedAirline.Flights)
            foreach (KeyValuePair<string, Flight> kvp in selectedAirline.Flights)
            {
                Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-35}",
                                  kvp.Key,
                                  selectedAirline.Name,
                                  kvp.Value.Origin,
                                  kvp.Value.Destination,
                                  kvp.Value.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt"));
                            }
            Console.WriteLine("\nChoose an existing Flight to modify or delete: ");
            Console.WriteLine("1. Modify Flight");
            Console.WriteLine("2. Delete Flight");
            Console.WriteLine("Choose an option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine("Enter Flight Number to modify: ");
                string flightNumberToModify = Console.ReadLine()?.ToUpper();

                if (selectedAirline.Flights.ContainsKey(flightNumberToModify))
                {
                    Flight flightToModify = selectedAirline.Flights[flightNumberToModify];

                    Console.WriteLine("Choose the specification to modify: ");
                    Console.WriteLine("1. Modify Basic Information");
                    Console.WriteLine("2. Modify Status");
                    Console.WriteLine("3. Modify Special Request Code");
                    Console.WriteLine("4. Modify Boarding Gate");
                    string modificationOption = Console.ReadLine();

                    if (modificationOption == "1")
                    { }


                }
            }































                    //feature 9
                    void scheduledFlight()
{
    Console.WriteLine("=============================================\r\nFlight Schedule for Changi Airport Terminal 5\r\n=============================================");
    Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-20} {5,-24} {6,-20}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time", "Status", "Boarding Gate");

    List<Flight> flights = new List<Flight>();

    foreach(KeyValuePair<string, Airline> kvp in terminal5.Airlines)
    {
        foreach (KeyValuePair<string, Flight> kvp2 in kvp.Value.Flights)
        {
            Flight flight = kvp2.Value;
            flights.Add(flight);
        }
    }

    

    flights.Sort();

 
    foreach (Flight flight in flights)
    {
        string[] flightCodeANdNum = flight.FlightNumber.Split(" "); 
        string code = flightCodeANdNum[0];
        string airlineN = terminal5.Airlines[code].Name;
        string gate = "Unassigned";
        string status = flight.Status;
        foreach (KeyValuePair<string, BoardingGate> kvp in terminal5.BoardingGates)
        {
            if (kvp.Value.Flight != null && kvp.Value.Flight.FlightNumber == flight.FlightNumber)
            {
                gate = kvp.Key;
            }
        }
        if( status == "null")
        {
            status = "Scheduled";
        }

        Console.WriteLine($"{flight.FlightNumber,-15} {airlineN,-20} {flight.Origin,-20} {flight.Destination,-20} {flight.ExpectedTime,-31} {status,-24} {gate,-16}");
    }
}






























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
            listBoardingGate();
        }
        else if (option == "3")
        {
            assignGate();
        }
        else if (option == "4")
        {
            addFlight();
        }
        else if (option == "5")
        {
            DisplayAirlineFlightDetails();
        }
        else if (option == "6")
        {
        ModifyFlightDetails();
        }
        else if (option == "7")
        {
            scheduledFlight();
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

