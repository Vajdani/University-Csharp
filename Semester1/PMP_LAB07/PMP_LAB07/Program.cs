//#region Task 1
//Mole mole = new();
//int min = 0;
//int max = 25;
//while (true)
//{
//    mole.Hide(min, max);
//    Console.Write($"Találja ki, hogy hová bújt a vakond! ({min}-{max}) ");
//    int guess = int.Parse(Console.ReadLine()!);

//    if (guess == mole.Position)
//    {
//        Console.WriteLine("Megtaláltad a vakondot!");
//        break;
//    }

//    Console.WriteLine("Téves! Próbáld újra!");
//}

//Console.ReadKey();
//#endregion

//#region Task 2
//GroundControl gctrl1 = new();
//gctrl1.AddFlight(new("LH1337", "STD", new(2024, 10, 20, 11, 10, 0)));
//gctrl1.AddFlight(new("W62221", "STD", new(2024, 10, 20, 11, 50, 0)));
//gctrl1.AddFlight(new("FR3586", "STD", new(2024, 10, 20, 16, 05, 0)));

//GroundControl gctrl2 = new();
//gctrl2.AddFlight(new("LH1337", "ETD", new(2024, 10, 20, 11, 10, 0), 30));
//Flight cancelled = new("W62221", "STD", new(2024, 10, 20, 11, 50, 0));
//cancelled.Cancel();
//gctrl2.AddFlight(cancelled);
//gctrl2.AddFlight(new("FR3586", "STD", new(2024, 10, 20, 16, 05, 0)));

//gctrl1.DisplayFlightData();
//Console.WriteLine();
//gctrl2.DisplayFlightData();
//#endregion

#region Task 3
int[] pointLimits = [0, 50, 62, 74, 86];
Console.Write("Adja meg a gyűjtemény méretét! ");
int resultCount = int.Parse(Console.ReadLine()!);

Console.WriteLine("Sikeres vizsgák:");
ExamResult[] examResults = new ExamResult[resultCount];
float average = 0;
int maxId = -1;
for (int i = 0; i < resultCount; i++)
{
    ExamResult examResult = new();
    if (examResult.Passed)
    {
        Console.WriteLine($"{examResult.NeptunId} - {examResult.Result}({examResult.Grade(pointLimits)})");
    }

    average += examResult.Result;
    if (maxId == -1 || examResults[maxId].Result < examResult.Result)
    {
        maxId = i;
    }

    examResults[i] = examResult;
}

Console.WriteLine($"Átlag pontszám: {average/resultCount}");
Console.WriteLine($"Legjobb eredmény: {examResults[maxId].NeptunId}");
#endregion



class Mole {
    private int position;
    public int Position {
        get => position;
    }

    public void TurnUp()
    {
        Console.WriteLine("M", position);
    }

    public void Hide(int min, int max)
    {
        position = Random.Shared.Next(min, max + 1);
        //Console.Clear();
    }
}



enum FlightStatus
{
    Scheduled,
    Delayed,
    Canceled
}

class Flight
{
    string flightId;
    string destination;
    DateTime departure;
    int delayMinutes;
    FlightStatus status;

    public string FlightId { get => flightId; }
    public string Destination { get => destination; }
    public DateTime Departure { get => departure; }
    public int DelayMinutes { get => delayMinutes; }
    public FlightStatus Status { get => status; }

    public string AllData
    {
        get {
            string statusString = "";
            switch (status)
            {
                case FlightStatus.Scheduled:
                    statusString = "on time.";
                    break;
                case FlightStatus.Delayed:
                    statusString = $"delayed by {delayMinutes} minutes.";
                    break;
                case FlightStatus.Canceled:
                    return $"Flight {flightId} is canceled.";
            }

            return $"Flight {flightId} is {statusString} ({destination} {EstimatedDeparture():yyyy.\tMM.\tdd.\tHH:mm:ss})";
        }
    } 

    public Flight(string flightId, string destination, DateTime departure, int delayMinutes)
    {
        this.flightId = flightId;
        this.destination = destination;
        this.departure = departure;
        this.delayMinutes = delayMinutes;
        UpdateStatus();
    }

    public Flight(string flightId, string destination, DateTime departure)
    {
        this.flightId = flightId;
        this.destination = destination;
        this.departure = departure;
        this.delayMinutes = 0;
        UpdateStatus();
    }

    public void Delay(int delayMinutes)
    {
        this.delayMinutes = delayMinutes;
    }

    public void Cancel()
    {
        status = FlightStatus.Canceled;
    }

    public void UpdateStatus(FlightStatus status)
    {
        this.status = status;
    }

    public void UpdateStatus()
    {
        if (delayMinutes > 0)
        {
            status = FlightStatus.Delayed;
        }
        else
        {
            status = FlightStatus.Scheduled;
        }
    }

    public DateTime EstimatedDeparture()
    {
        return departure.AddMinutes(delayMinutes);
    }
}

class GroundControl
{
    DateTime now;
    List<Flight> flights = [];

    public void AddFlight(Flight flight)
    {
        flights.Add(flight);
    }

    public float AverageDelay()
    {
        int count = 0;
        int delay = 0;
        foreach (Flight flight in flights)
        {
            if (flight.Status != FlightStatus.Canceled)
            {
                count++;
                delay += flight.DelayMinutes;
            }
        }

        if (count == 0)
        {
            return 0;
        }

        return delay/(float)count;
    }

    public void DisplayFlightData()
    {
        foreach (Flight flight in flights)
        {
            Console.WriteLine(flight.AllData);
        }

        Console.WriteLine($"Average delay is {Math.Round(AverageDelay())} minutes");
    }
}



enum ExamGrade
{
    Elégtelen = 0,
    Elégséges = 1,
    Közepes   = 2,
    Jó        = 3,
    Jeles     = 4
}

class ExamResult
{
    string neptunId = "";
    int result;

    public string NeptunId {
        get => neptunId;
        set {
            if (value.Length != 6)
            {
                throw new Exception($"Neptun kód mérete nem felel meg! ({value.Length})");
            }

            neptunId = value;
        }
    }
    public int Result
    {
        get => result;
        set { 
            if (value < 0)
            {
                throw new Exception("A vizsgaeredmény nem lehet kevesebb mint 0!");
            }

            if (value > 100)
            {
                throw new Exception("A vizsgaeredmény nem lehet több mint 100!");
            }

            result = value;
        }
    }

    public bool Passed => result >= 50;

    public ExamResult(string neptunId, int result)
    {
        NeptunId = neptunId;
        Result = result;
    }

    public ExamResult()
    {
        string neptun = "";
        List<int> chars = [.. Enumerable.Range(48, 10)];
        chars.AddRange(Enumerable.Range(65, 26));

        neptun += (char)chars[Random.Shared.Next(9, chars.Count)];
        for (int i = 0; i < 5; i++)
        {
            neptun += (char)chars[Random.Shared.Next(0, chars.Count)];
        }

        NeptunId = neptun;
        Result = Random.Shared.Next(0, 101);
    }

    public ExamGrade Grade(int[] pointLimits)
    {
        if (pointLimits.Length != 5)
        {
            throw new Exception("A ponthatár tömbnek 5 ponthatárt kell tartalmaznia!");
        }

        for (int i = 4; i > 0; i--)
        {
            if (result >= pointLimits[i])
            {
                return (ExamGrade)i;
            }
        }

        return ExamGrade.Elégtelen;
    }
}