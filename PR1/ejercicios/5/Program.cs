// Define el tamaño de los arreglos
const int TAMANO = 3;

// Declara los arreglos para almacenar la informacion
string[] documentos = new string[TAMANO];
string[] nombres = new string[TAMANO];
string[] apellidos = new string[TAMANO];

// Carga los datos de las personas
for (int i = 0; i < TAMANO; i++)

        {
    // Pide documento
    Console.WriteLine($"--- Carga de datos de la persona {i + 1} ---");
    Console.Write("Ingrese el número de documento: ");
    documentos[i] = Console.ReadLine();

    // Pide nombre
    Console.Write("Ingrese el nombre: ");
    nombres[i] = Console.ReadLine();

    // Pide apellido
    Console.Write("Ingrese el apellido: ");
    apellidos[i] = Console.ReadLine();
    Console.WriteLine();
}

// Pide al usuario el documento a buscar
Console.WriteLine("--- Búsqueda de datos ---");
Console.Write("Ingrese el número de documento a buscar: ");
string documentoBuscado = Console.ReadLine();

// Recorre los arreglos para encontrar el documento
bool encontrado = false;
for (int i = 0; i < TAMANO; i++)
{
    // Compara el documento buscado con el que está en el arreglo
    // 'String.Equals' es una forma segura de comparar cadenas de texto
    if (documentos[i].Equals(documentoBuscado))
    {
        Console.WriteLine("\n¡Documento encontrado!");
        Console.WriteLine($"Nombre: {nombres[i]}");
        Console.WriteLine($"Apellido: {apellidos[i]}");
        encontrado = true; // Cambia la variable a 'true' porque encuentra el dato.
        break; // Usa 'break' para salir del bucle una vez que encuentra la persona.
    }
}

// Maneja el caso en que el documento no fue encontrado
if (!encontrado)
{
    Console.WriteLine("\nEl documento ingresado no se encontró en la base de datos.");
}
