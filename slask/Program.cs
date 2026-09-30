string[] parkeringsPlatser = new string[100];

int menyVal = 1;

while (true)
{
    Console.Clear();
    Console.WriteLine(menyVal == 1 ? "> Registrera fordon" : "  Registrera fordon");
    Console.WriteLine(menyVal == 2 ? "> Flytta fordon" : "  Flytta fordon");
    Console.WriteLine(menyVal == 3 ? "> Ta bort fordon" : "  Ta bort fordon");
    Console.WriteLine(menyVal == 4 ? "> Sök fordon" : "  Sök fordon");

    var knapp = Console.ReadKey(true);

    if (knapp.Key == ConsoleKey.DownArrow && menyVal < 4)
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

}


void RegistreraFordon()
{
    bool korrektData = false;

    while (true)
    {
        Console.Clear();
        Console.Write("Typ av fordon (bil/mc): ");
        string fordonsTyp = Console.ReadLine();

        Console.Write("Registreringsnummer: ");
        string regNr = Console.ReadLine();

        if (fordonsTyp == "bil")
        {
            Console.Clear();
            korrektData = true;
            Console.WriteLine($"Du angav: {fordonsTyp.ToUpper() + '#'}{regNr.ToUpper()}");
        }
        else if (fordonsTyp == "mc")
        {
            Console.Clear();
            korrektData = true;
            Console.WriteLine($"Du angav: {fordonsTyp.ToUpper() + '#'}{regNr.ToUpper()}");
        }
        else if (fordonsTyp != "bil" || fordonsTyp != "mc")
        {
            Console.WriteLine("Ogiltig fordonstyp");
        }

        Console.Write("\nKontrollera fordonstyp och registreringsnummer. \nOm uppgifterna stämmer tryck 'j' annars 'n' för att börja om. ");
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        if (keyInfo.Key == ConsoleKey.J && korrektData == true)
        {
            Console.Clear();
            parkeringsPlatser[1] = fordonsTyp + regNr;
            Console.WriteLine($"{fordonsTyp.ToUpper() + '#'}{regNr.ToUpper()} har parkerats på parkeringsplats 1.");
            Console.ReadKey();
            break;

        }
        else if (keyInfo.Key == ConsoleKey.N)
        {

        }
        else if (keyInfo.Key != ConsoleKey.J || keyInfo.Key != ConsoleKey.N)
        {
            Console.WriteLine("\nOgiltigt val.");
            Console.ReadKey();
        }

    }


}

void VisaParkeringsPlatser()
{
    for (int i = 1; i < parkeringsPlatser.Length; i++)
    {
        Console.WriteLine("Parkeringsplats: {0}", i);
    }

    Console.ReadKey();
}