// Declara el array para almacenar 5 numeros enteros
int[] numeros = new int[5];

// Inicializa una variable para almacenar la suma de los numeros.
int suma = 0;

// Carga los numeros usando un bucle
for (int i = 0; i < numeros.Length; i++)
{
    Console.Write($"Ingresa el numero {i + 1}: ");
    numeros[i] = Convert.ToInt32(Console.ReadLine());
}

// Agrega el valor de cada elemento a la variable 'suma'
for (int i = 0; i < numeros.Length; i++)
{
    suma += numeros[i];
}

// Convertir a double para asegurar que la division sea precisa
double promedio = (double)suma / numeros.Length;

// Mostar resultados
Console.WriteLine($"La suma de los números es: {suma}");
Console.WriteLine($"El promedio de los números es: {promedio}");
