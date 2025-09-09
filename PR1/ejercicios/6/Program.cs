// Define el tamano del arreglo
const int TAMANO = 500;

// Crea una instancia de la clase Random para generar numeros aleatorios
Random generadorAleatorio = new Random();

// Declara un arreglo para almacenar los 500 numeros
int[] numerosAleatorios = new int[TAMANO];

// Variables para la suma y el promedio
// Usa 'long' para evitar un posible desbordamiento si los numeros fueran mas grandes
long suma = 0;
double promedio = 0.0;

// Llena el arreglo con numeros aleatorios y calcula la suma
Console.WriteLine("Generando 500 números aleatorios entre 0 y 10...");
for (int i = 0; i < TAMANO; i++)
{
    // Genera un número aleatorio entre 0 y 10 (exclusivo del 11, por lo que es de 0 a 10)
    numerosAleatorios[i] = generadorAleatorio.Next(0, 11);

    // Suma el numero generado a la variable 'suma'
    suma += numerosAleatorios[i];
}

// Convierte la suma a 'double' para que la division sea precisa
promedio = (double)suma / TAMANO;

// Muestra el resultado
Console.WriteLine("Números aleatorios generados. Calculando el promedio...");
Console.WriteLine($"La suma de todos los números es: {suma}");
Console.WriteLine($"El promedio de los 500 números es: {promedio}");
