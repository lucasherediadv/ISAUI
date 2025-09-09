const int NUMERO_DE_DIAS = 4;

// Declara los arreglos para las temperaturas
int[] tempMinimas = new int[NUMERO_DE_DIAS];
int[] tempMaximas = new int[NUMERO_DE_DIAS];

// Declara variables para las temperaturas extremas
int minimaGlobal = 0;
int maximaGlobal = 0;

Console.WriteLine("--- Registro de Temperaturas ---");

// Bucle para cargar los datos y encontrar los valores extremos
for (int i = 0; i < NUMERO_DE_DIAS; i++)
{
    Console.WriteLine($"\nRegistro del día {i + 1}:");

    Console.Write("Ingrese la temperatura mínima: ");
    tempMinimas[i] = Convert.ToInt32(Console.ReadLine());

    Console.Write("Ingrese la temperatura máxima: ");
    tempMaximas[i] = Convert.ToInt32(Console.ReadLine());

    // En la primera iteracion, inicializa los valores globales con los del primer dia
    if (i == 0)
    {
        minimaGlobal = tempMinimas[i];
        maximaGlobal = tempMaximas[i];
    }
    else
    {
        // Compara las temperaturas de los dias siguientes con los valores globales
        if (tempMinimas[i] < minimaGlobal)
        {
            minimaGlobal = tempMinimas[i];
        }
        if (tempMaximas[i] > maximaGlobal)
        {
            maximaGlobal = tempMaximas[i];
        }
    }
}

// Muestra los resultados
Console.WriteLine("\n--- Resultados ---");
Console.WriteLine($"La temperatura mínima de los {NUMERO_DE_DIAS} días es: {minimaGlobal}");
Console.WriteLine($"La temperatura máxima de los {NUMERO_DE_DIAS} días es: {maximaGlobal}");
