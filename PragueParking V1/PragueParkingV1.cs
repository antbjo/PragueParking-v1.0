string[] parkeringsPlatser = new string[101];

VisaMeny();

void RegistreraFordon()
{
    while (true)
    {
        Console.Clear();
        string fordonsTyp = HämtaFordonsTyp();
        string regNr = HämtaRegNr();
        Console.Clear();

        Console.WriteLine($"Du angav: {fordonsTyp.ToUpper()}#{regNr.ToUpper()}");

        Console.Write("\nKontrollera fordonstyp och registreringsnummer. \nOm uppgifterna stämmer tryck 'J' annars 'N' för att börja om. ");


        while (true)
        {
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
                Console.Clear();
                Console.WriteLine($"Du angav: {fordonsTyp.ToUpper()}#{regNr.ToUpper()}");
                Console.Write("\nKontrollera fordonstyp och registreringsnummer. \nOm uppgifterna stämmer tryck 'J' annars 'N' för att börja om. ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOgiltigt val!");
                Console.ResetColor();
                Console.WriteLine("Tryck 'J' eller 'N'");
            } 
        }
    }


}

string HämtaRegNr()
{

    while (true)
    {
        Console.Clear();
        Console.Write("Registreringsnummer: ");
        string regNr = Console.ReadLine().Trim().ToUpper();

        bool korrektRegNr = regNr.Length <= 10 && regNr.Length > 0;

        foreach (var tecken in regNr)
        {
            if (!char.IsLetterOrDigit(tecken))
            {
                korrektRegNr = false;
                break;
            }
        }

        if (!korrektRegNr)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt registreringsnummer!");
            Console.ResetColor();
            Console.WriteLine("\nTryck på valfri tangent för att försöka igen.");
            Console.ReadKey();
        }
        else
        {
            return regNr;
        }
    } 
}

string HämtaFordonsTyp()
{
    while (true)
    {
        Console.Clear();
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
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ogiltig fordonstyp!");
        Console.ResetColor();
        Console.WriteLine("\nTryck på valfri tangent för att försöka igen.");
        Console.ReadKey();

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
        else if (parkeringsPlatser[i].StartsWith("MC#") && fordonsTyp == "MC" && !parkeringsPlatser[i].Contains('|'))
        {
            parkeringsPlatser[i] += "|" + fordonsTyp.ToUpper() + "#" + regNr.ToUpper();
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
    var sökFordon = Console.ReadLine().ToUpper();

    while (true)
    {
        for (int i = 1; i < parkeringsPlatser.Length; i++)
        {
            if (parkeringsPlatser[i] != null && parkeringsPlatser[i].Contains("#" + sökFordon))
            {
                Console.WriteLine("Fordonet {0} står parkerad på parkeringsplats {1}.", parkeringsPlatser[i], i);
                Console.WriteLine("\nTryck på valfri tangent för att komma tillbaka till huvudmenyn.");
                Console.ReadKey();
                return;
            }
        }
        Console.WriteLine("Hittar inget fordon med det registreringsnumret.");
        Console.WriteLine("\nTryck på valfri tangent för att komma tillbaka till huvudmenyn.");
        Console.ReadKey();
        return;
    }
}

void FlyttaFordon()
{
    while (true)
    {
        Console.Clear();
        Console.Write("Ange registreringnumret för det fordon som du vill flytta: ");
        string registreringsNummer = Console.ReadLine().ToUpper();

        int plats = -1;
        string fordonet = null;
        string kvar = null;

        for (int i = 0; i < parkeringsPlatser.Length; i++)
        {
            if (parkeringsPlatser[i] == null) continue;

            string[] fordonPåPlatsen = parkeringsPlatser[i].Split('|');

            for (int j = 0; j < fordonPåPlatsen.Length; j++)
            {
                string f = fordonPåPlatsen[j];
                if (f.Contains("#" + registreringsNummer))
                {
                    plats = i;
                    fordonet = f;

                    if (fordonPåPlatsen.Length == 2)
                    {
                        kvar = fordonPåPlatsen[1 - j];
                    }
                    break;
                }
            }
            if (fordonet != null) break;
        }

        if (fordonet == null)
        {
            Console.WriteLine("Fordonet med det registreringsnumret hittades inte.");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Fordonet {0} står på parkeringsplats {1}", fordonet, plats);
        Console.Write("\nAnge parkeringsplatsen som du vill flytta fordonet till (1-100): ");

        if (!int.TryParse(Console.ReadLine(), out int nyPlats) || nyPlats < 1 || nyPlats > 100)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltig parkeringsplats!");
            Console.ResetColor();
            Console.Write("Tryck på valfri tangent för att försöka igen.");
            Console.ReadKey();
            continue;
        }

        bool ledig = parkeringsPlatser[nyPlats] == null;
        bool delaPlats = fordonet.StartsWith("MC#") && parkeringsPlatser[nyPlats] != null && parkeringsPlatser[nyPlats].StartsWith("MC#") && !parkeringsPlatser[nyPlats].Contains('|');

        if (ledig)
        {
            parkeringsPlatser[nyPlats] = fordonet;
        }
        else if (delaPlats)
        {
            parkeringsPlatser[nyPlats] += "|" + fordonet;
        }
        else
        {
            Console.WriteLine("Parkeringsplatsen är upptagen.");
            Console.Write("Tryck på valfri tangent för att välja en ny plats.");
            Console.ReadKey();
            continue;
        }

        parkeringsPlatser[plats] = kvar;
        Console.Clear();
        Console.WriteLine("Fordonet {0} har flyttats till parkeringsplats {1}", fordonet, nyPlats);
        Console.Write("Tryck på valfri tangent för att komma tillbaka till huvudmenyn.");
        Console.ReadKey();
        return;
    }


}


void VisaMeny()
{
    int menyVal = 1;

    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== PRAGUE PARKING V1 ===\n");
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
            FlyttaFordon();
        }
        else if (knapp.Key == ConsoleKey.Enter && menyVal == 4)
        {
            TaBortFordon();
        }
        else if (knapp.Key == ConsoleKey.Enter && menyVal == 5)
        {
            SökFordon();
        }
    }
}

void TaBortFordon()
{
    while (true)
    {
        Console.Clear();
        Console.Write("Ange registreringsnummer för fordonet du vill ta bort: ");
        string registreringsNummer = Console.ReadLine().ToUpper();

        int plats = -1;
        string fordon = null;
        string kvar = null;

        for (int i = 0; i < parkeringsPlatser.Length; i++)
        {
            if (parkeringsPlatser[i] == null) continue;

            string[] fordonPåPlatsen = parkeringsPlatser[i].Split('|');

            for (int j = 0; j < fordonPåPlatsen.Length; j++)
            {
                string f = fordonPåPlatsen[j];
                if (f.Contains("#" + registreringsNummer))
                {
                    plats = i;
                    fordon = f;

                    if (fordonPåPlatsen.Length == 2)
                    {
                        kvar = fordonPåPlatsen[1 - j];
                        break;
                    }
                }
                if (fordon != null) break;
            }

            if (fordon == null)
            {
                Console.WriteLine("Hittar inget fordon med det registreringsnumret.");
                Console.WriteLine("\n\nTryck på valfri tangent för att komma tillbaka till huvudmenyn.");
                Console.ReadKey();
                return;
            }
            else
            {
                parkeringsPlatser[plats] = kvar;
                Console.WriteLine("Fordonet {0} borttaget.", fordon);
                Console.ReadKey();
                return;
            }
        }
    }
}

//void TillHuvudMeny()
//{
//    Console.WriteLine("\n\n\nTryck på ESC för komma tillbaka till huvudmenyn.");               // STRUNTADE I DENNA DÅ DET BLEV FÖR KRÅNGLIGT! :-)
//    var knapp = Console.ReadKey();

//    if (knapp.Key == ConsoleKey.Escape)
//    {
//        VisaMeny();
//    }
//}
