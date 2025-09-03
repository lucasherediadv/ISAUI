using System;

public class TotalVentasVendedor
{
    public static void Main(string[] args)
    {
        // Declara los dos vectores de 10 elementos
        int[] codigoVendedor = new int[10];
        decimal[] importeVenta = new decimal[10];

        Console.WriteLine("---Carga de ventas ---");

        // Carga los vectores con los datos ingresados por teclado
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"\nRegistro de venta N° {i + 1}");

            // Solicitar y guardar codigo del vendedor
            Console.Write("ingrese el codigo del vendedor: ");
            if (!int.TryParse(Console.ReadLine(), out codigoVendedor[i]))
            {
                Console.WriteLine("Error: Ingrese un numero entero valido.");
                return;
            }

            // Pide i guarda el importe de la venta
            Console.Write("ingrese el importe de la venta: ");
            if (!decimal.TryParse(Console.ReadLine(), out importeVenta[i]))
            {
                Console.WriteLine("Error: Ingrese un numero valido para el importe");
                return;
            }
        }

        // Pide el codigo del venededor a buscar y calcula el total
        Console.WriteLine("\n--- Busqueda de ventas ---");
        Console.WriteLine("Ingrese el codigo de vendedor a buscar: ");
        int codigoBuscado;
        if (!int.TryParse(Console.ReadLine(), out codigoBuscado))
        {
            Console.WriteLine("Error: Ingrese un numero entero valido.");
            return;
        }

        decimal totalVentas = 0;

        // Recorre los vectores para sumar todas las ventas del vendedor
        // buscado
        for (int i = 0; i < 10; i++)
        {
            if (codigoVendedor[i] == codigoBuscado)
            {
                totalVentas += importeVenta[i];
            }
        }

        Console.WriteLine($"\nEl total de ventas para el vendedor {codigoBuscado} es: ${totalVentas}");
    }
}
