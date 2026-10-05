string[] parkeringsPlatser = new string[101];

int menyVal = 1;

while (true)
{
    Console.Clear();
    Console.WriteLine(menyVal == 1 ? "> Registrera fordon" : "  Registrera fordon");
    Console.WriteLine(menyVal == 2 ? "> Visa parkeringsplatser" : "  Visa parkeringsplatser");
    Console.WriteLine(menyVal == 3 ? "> Flytta fordon" : "  Flytta fordon");
    Console.WriteLine(menyVal == 4 ? "> Ta bort fordon" : "  Ta bort fordon");
    Console.WriteLine(menyVal == 5 ? "> Sök fordon" : "  Sök fordon");

    var knapp = Console.ReadKey(true);

    if (knapp.Key == ConsoleKey.DownArrow && menyVal < 5)
    {
        menyVal++;
    }
    else if (knapp.Key == ConsoleKey.UpArrow && menyVal > 1)
    {
        menyVal--;
    }
    else if (knapp.Key == ConsoleKey.Enter && menyVal == 1)
    {
        RegistreraFordon();
    }
    else if (knapp.Key == ConsoleKey.Enter && menyVal == 2)
    {
        VisaParkeringsPlatser();
    }
    else if (knapp.Key == ConsoleKey.Enter && menyVal == 3)
    {
        
    }
    else if (knapp.Key == ConsoleKey.Enter && menyVal == 4)
    {
        
    }
    else if (knapp.Key == ConsoleKey.Enter && menyVal == 5)
    {
        SökFordon();
    }

}


void RegistreraFordon()
{
    while (true)
    {
        Console.Clear();
        string fordonsTyp = HämtaFordonsTyp();
        string regNr = HämtaRegNr();
        Console.Clear();

        Console.WriteLine($"Du angav: {fordonsTyp.ToUpper()}#{regNr.ToUpper()}");

        Console.Write("\nKontrollera fordonstyp och registreringsnummer. \nOm uppgifterna stämmer tryck 'j' annars 'n' för att börja om. ");
        
        ConsoleKeyInfo knapp = Console.ReadKey(true);
        if (knapp.Key == ConsoleKey.J)
        {
            Console.Clear();
            ParkeraFordon(fordonsTyp, regNr);
            Console.WriteLine("\n\nTryck på valfri tangent för att komma tillbaka till huvudmenyn.");
            Console.ReadKey();
            break;

        }
        else if (knapp.Key == ConsoleKey.N)
        {
            continue;
        }
        else if (knapp.Key != ConsoleKey.J || knapp.Key != ConsoleKey.N)
        {
        Console.WriteLine("\nOgiltigt val.");
        Console.ReadKey();
        }  
    }


}

string HämtaRegNr()
{
    Console.Write("Registreringsnummer: ");
    string regNr = Console.ReadLine();
    // SKAPA IF-SATS HÄR FÖR ATT KONTROLLERA RÄTT DATA FÖR ETT REGISTRERINGSNUMMER !!!
    return regNr;
}

string HämtaFordonsTyp()
{
    while (true)
    {
        Console.Write("Typ av fordon (bil/mc): ");
        string fordonsTyp = Console.ReadLine();
        string bil = "CAR";
        string mc = "MC";

        if (fordonsTyp == "bil")
        {
            fordonsTyp = bil;
            return bil;
        }
        else if (fordonsTyp == "mc")
        {
            fordonsTyp = mc;
            return mc;
        }
        Console.WriteLine("Ogiltig fordonstyp.");
        
    }
}

void ParkeraFordon(string fordonsTyp, string regNr)
{
    for (int i = 1; i < parkeringsPlatser.Length; i++)
    {
        if (parkeringsPlatser[i] == null)
        {
            parkeringsPlatser[i] = fordonsTyp.ToUpper() + "#" + regNr.ToUpper();
            Console.WriteLine($"Fordonet {fordonsTyp.ToUpper()}#{regNr.ToUpper()} har parkerats på plats {i}");
            return;
        }
        else if (parkeringsPlatser[i].StartsWith("MC#") && fordonsTyp == "MC" && !parkeringsPlatser[i].Contains('-'))
        {
            parkeringsPlatser[i] += " - " + fordonsTyp.ToUpper() + "#" + regNr.ToUpper();
            Console.WriteLine($"Fordonet {fordonsTyp.ToUpper()}#{regNr.ToUpper()} har parkerats på plats {i}");
            return;
        }
    }
}

void VisaParkeringsPlatser()
{
    Console.Clear();
    for (int i = 1; i < parkeringsPlatser.Length; i++)
    {
        Console.WriteLine("Parkeringsplats {0}: {1} ", i, parkeringsPlatser[i]);

    }

    Console.ReadKey();
}

void SökFordon()
{
    Console.Clear();
    Console.Write("Ange registreringsnummer: ");
    var sökning = Console.ReadLine();

    bool hittad = false;

    while (true)
    {
        for (int i = 0; i < parkeringsPlatser.Length; i++)
        {
            if (parkeringsPlatser[i] != null && parkeringsPlatser[i].Contains(sökning, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Fordonet {0} står parkerad på parkeringsplats {1}", parkeringsPlatser[i], i);
                hittad = true;
                Console.WriteLine("\nTryck på valfri tangent för att komma tillbaka till huvudmenyn.");
                Console.ReadKey();
                break;
            }
        } 
        if (hittad == false)
        {
            Console.WriteLine("Hittar inget fordon med det registreringsnumret.");
            Console.ReadKey();
            break;
        }
    }
}