// ConversorNumerico.cs
// Esta clase estática contiene toda la lógica para la conversión de números.
// Realiza la validación de la entrada y los cálculos de conversión entre bases.

using System;
using System.Text;
using System.Globalization;
using System.Numerics;

/// <summary>
/// Enumeración para representar los sistemas numéricos soportados.
/// </summary>
public enum SistemaNumerico
{
    Decimal = 1,
    Binario = 2,
    Octal = 3,
    Hexadecimal = 4
}

public static class ConversorNumerico
{
    private const int PRECISION_FRACCIONARIA = 16;
    private const string DIGITOS_HEX = "0123456789ABCDEF";

    /// <summary>
    /// Valida que una cadena de texto sea un número válido para un sistema numérico dado.
    /// </summary>
    /// <param name="numeroStr">La cadena de texto a validar.</param>
    /// <param name="sistema">El sistema numérico de referencia.</param>
    /// <param name="mensajeError">Un parámetro de salida que contiene el mensaje de error si la validación falla.</param>
    /// <returns>Verdadero si la validación es exitosa; de lo contrario, falso.</returns>
    public static bool ValidarNumeroParaBase(string numeroStr, SistemaNumerico sistema, out string mensajeError)
    {
        mensajeError = string.Empty;

        if (string.IsNullOrEmpty(numeroStr))
        {
            mensajeError = "La entrada no puede estar vacía.";
            return false;
        }

        numeroStr = numeroStr.Replace(',', '.');
        bool esNegativo = numeroStr.StartsWith('-');

        // Si esNegativo es verdadero, entonces quita el primer carácter de numeroStr
        // y guarda el resto en numeroSinSigno.
        // Si no, guarda numeroStr tal como esta.
        string numeroSinSigno = esNegativo ? numeroStr.Substring(1) : numeroStr;

        if (sistema == SistemaNumerico.Decimal)
        {
            if (!double.TryParse(numeroSinSigno, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                mensajeError = "El número decimal ingresado no tiene un formato válido.";
                return false;
            }
            return true;
        }

        int baseOrigen = ObtenerBasePorSistema(sistema);
        string[] partes = numeroSinSigno.Split('.');

        if (partes.Length > 2)
        {
            mensajeError = "El número tiene más de un punto decimal.";
            return false;
        }

        string parteEnteraStr = partes[0];

        // Si partes.Length tieen mas de un elemento, se toma el segundo indice,
        // la parte fraccionaria, si no hay parte fraccionaria, se asigna una
        // cadena vacia
        string parteFraccionariaStr = partes.Length > 1 ? partes[1] : "";

        // Se recorre cada carácter de la parte entera y fraccionaria del número,
        // verificando que esté dentro del conjunto de dígitos válidos (DIGITOS_HEX)
        // y que su valor no exceda la base del sistema (baseOrigen).
        foreach (char c in parteEnteraStr + parteFraccionariaStr)
        {
            if (DIGITOS_HEX.IndexOf(char.ToUpper(c)) == -1 || DIGITOS_HEX.IndexOf(char.ToUpper(c)) >= baseOrigen)
            {
                mensajeError = $"El dígito '{c}' no es válido para el sistema {sistema}.";
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Convierte un número de un sistema a otro.
    /// </summary>
    /// <param name="numeroStr">El número a convertir en formato de cadena.</param>
    /// <param name="origen">El sistema numérico de origen.</param>
    /// <param name="destino">El sistema numérico de destino.</param>
    /// <returns>El número convertido en formato de cadena.</returns>
    public static string Convertir(string numeroStr, SistemaNumerico origen, SistemaNumerico destino)
    {
        numeroStr = numeroStr.Replace(',', '.');
        double numeroDecimal = ConvertirADecimal(numeroStr, origen);
        string resultado = ConvertirDesdeDecimalConSigno(numeroDecimal, destino);
        return resultado.Replace('.', ',');
    }

    /// <summary>
    /// Convierte un número de cualquier base a su representación decimal.
    /// </summary>
    /// <param name="numeroStr">El número a convertir.</param>
    /// <param name="sistemaOrigen">El sistema numérico de origen.</param>
    /// <returns>El valor decimal del número.</returns>
    private static double ConvertirADecimal(string numeroStr, SistemaNumerico sistemaOrigen)
    {
        bool esNegativo = numeroStr.StartsWith('-');
        string numeroSinSigno = esNegativo ? numeroStr.Substring(1) : numeroStr;

        if (sistemaOrigen == SistemaNumerico.Decimal)
        {
            return esNegativo ? -double.Parse(numeroSinSigno, CultureInfo.InvariantCulture) : double.Parse(numeroSinSigno, CultureInfo.InvariantCulture);
        }

        int baseOrigen = ObtenerBasePorSistema(sistemaOrigen);
        string[] partes = numeroSinSigno.Split('.');
        string parteEnteraStr = partes[0];
        string parteFraccionariaStr = partes.Length > 1 ? partes[1] : "";

        double valorEntero = 0;
        long potencia = 1;

				// A continuación, el bucle 'for' que procesa la parte entera.
				// La conversión se basa en la suma de cada dígito multiplicado por la potencia de la base.
				// Para la parte entera, se recorre el número de derecha a izquierda.
				// Por ejemplo, para el número binario 101:
				// 1. Primer dígito (el último '1'): Se multiplica por 2^0 (potencia = 1).
				// 2. Segundo dígito ('0'): Se multiplica por 2^1 (potencia se convierte en 2).
				// 3. Tercer dígito (el primer '1'): Se multiplica por 2^2 (potencia se convierte en 4).
				// La suma de estos productos (1*1 + 0*2 + 1*4) da el resultado decimal 5.
        for (int i = parteEnteraStr.Length - 1; i >= 0; i--)
        {
            int valorDigito = ObtenerValorDeDigito(parteEnteraStr[i]);
            valorEntero += valorDigito * potencia;
            potencia *= baseOrigen;
        }

        double valorFraccionario = 0;
        double factor = 1.0 / baseOrigen;

				// A continuación, el bucle 'for' que procesa la parte fraccionaria.
				// La conversión se basa en la suma de cada dígito fraccionario
				// multiplicado por las potencias negativas de la base.
				// Por ejemplo, para el número binario 0.101:
				// 1. Primer dígito ('1'): Se multiplica por 2^-1 (factor = 0.5).
				// 2. Segundo dígito ('0'): Se multiplica por 2^-2 (factor = 0.25).
				// 3. Tercer dígito ('1'): Se multiplica por 2^-3 (factor = 0.125).
				// La suma de estos productos (1*0.5 + 0*0.25 + 1*0.125) da el resultado decimal 0.625.
				for (int i = 0; i < parteFraccionariaStr.Length; i++)
        {
            int valorDigito = ObtenerValorDeDigito(parteFraccionariaStr[i]);
            valorFraccionario += valorDigito * factor;
            factor /= baseOrigen;
        }

        double resultado = valorEntero + valorFraccionario;
        return esNegativo ? -resultado : resultado;
    }

    /// <summary>
    /// Convierte un número decimal a un sistema de destino, manejando el signo negativo.
    /// </summary>
    /// <param name="numeroDecimal">El número decimal a convertir.</param>
    /// <param name="sistemaDestino">El sistema numérico de destino.</param>
    /// <returns>La representación del número en el sistema de destino.</returns>
    private static string ConvertirDesdeDecimalConSigno(double numeroDecimal, SistemaNumerico sistemaDestino)
    {
        if (numeroDecimal == 0)
        {
            return "0";
        }

        bool esNegativo = numeroDecimal < 0;
        double numeroPositivo = Math.Abs(numeroDecimal);

        string resultado = ConvertirDesdeDecimal(numeroPositivo, sistemaDestino);

        return esNegativo ? "-" + resultado : resultado;
    }

    /// <summary>
    /// Convierte un número decimal positivo a un sistema numérico de destino.
    /// </summary>
    /// <param name="numeroDecimal">El número decimal positivo a convertir.</param>
    /// <param name="sistemaDestino">El sistema numérico de destino.</param>
    /// <returns>La representación del número en el sistema de destino.</returns>
    private static string ConvertirDesdeDecimal(double numeroDecimal, SistemaNumerico sistemaDestino)
    {
        int baseDestino = ObtenerBasePorSistema(sistemaDestino);
        if (baseDestino == 10)
        {
            return numeroDecimal.ToString(CultureInfo.InvariantCulture);
        }

        long parteEntera = (long)Math.Truncate(numeroDecimal);
        double parteFraccionaria = numeroDecimal - parteEntera;

        var resultadoEntero = new StringBuilder();
        if (parteEntera == 0)
        {
            resultadoEntero.Append('0');
        }
        else
        {
            while (parteEntera > 0)
            {
                resultadoEntero.Insert(0, DIGITOS_HEX[(int)(parteEntera % baseDestino)]);
                parteEntera /= baseDestino;
            }
        }

        if (parteFraccionaria > 0)
        {
            resultadoEntero.Append('.');
            for (int i = 0; i < PRECISION_FRACCIONARIA && parteFraccionaria > 0; i++)
            {
                parteFraccionaria *= baseDestino;
                int digito = (int)Math.Truncate(parteFraccionaria);
                resultadoEntero.Append(DIGITOS_HEX[digito]);
                parteFraccionaria -= digito;
            }
        }
        return resultadoEntero.ToString();
    }

    /// <summary>
    /// Obtiene el valor numérico de un dígito en base 16.
    /// </summary>
    /// <param name="digito">El carácter que representa el dígito.</param>
    /// <returns>El valor numérico del dígito.</returns>
    private static int ObtenerValorDeDigito(char digito)
    {
        return DIGITOS_HEX.IndexOf(char.ToUpper(digito));
    }

    /// <summary>
    /// Obtiene el valor de la base numérica a partir de una enumeración.
    /// </summary>
    /// <param name="sistema">La enumeración del sistema numérico.</param>
    /// <returns>El valor entero de la base.</returns>
    private static int ObtenerBasePorSistema(SistemaNumerico sistema)
    {
        switch (sistema)
        {
            case SistemaNumerico.Binario:
                return 2;
            case SistemaNumerico.Octal:
                return 8;
            case SistemaNumerico.Decimal:
                return 10;
            case SistemaNumerico.Hexadecimal:
                return 16;
            default:
                throw new ArgumentException("Sistema numérico no soportado.");
        }
    }
}
