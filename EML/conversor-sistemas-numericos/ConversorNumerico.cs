// ConversorNumerico.cs
// Esta clase estática contiene toda la lógica para la conversión de números.
// Realiza la validación de la entrada y los cálculos de conversión entre bases.

using System;
using System.Text;

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
    /// <exception cref="FormatException">Se lanza si el número no tiene el formato correcto.</exception>
    /// <exception cref="ArgumentException">Se lanza si el número contiene dígitos inválidos para la base.</exception>
    public static void ValidarNumeroParaBase(string numeroStr, SistemaNumerico sistema)
    {
        // Si el sistema es decimal, usa TryParse para la validación.
        if (sistema == SistemaNumerico.Decimal)
        {
            if (!double.TryParse(numeroStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                throw new FormatException("El número decimal ingresado no tiene un formato válido.");
            }
            return;
        }

        int baseOrigen = GetBaseFromSistema(sistema);

        // Separa el signo negativo para la validación.
        bool esNegativo = numeroStr.StartsWith('-');
        string numeroSinSigno = esNegativo ? numeroStr.Substring(1) : numeroStr;

        // Comprueba si solo se ingresó el signo negativo.
        if (esNegativo && numeroSinSigno.Length == 0)
        {
            throw new ArgumentException("No se puede ingresar solo un signo.");
        }

        // Comprueba si hay un signo en medio del número.
        if (numeroSinSigno.IndexOf('-') != -1)
        {
            throw new ArgumentException("El signo '-' solo puede aparecer al principio del número.");
        }

        // Valida que cada dígito sea válido para la base.
        foreach (char c in numeroSinSigno)
        {
            if (c == '.' || c == ',') continue;
            ObtenerValorDeDigito(c, baseOrigen, sistema);
        }
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
    /// <exception cref="FormatException">Se lanza si el formato del número es incorrecto.</exception>
    private static double ConvertirADecimal(string numeroStr, SistemaNumerico sistemaOrigen)
    {
        bool esNegativo = numeroStr.StartsWith('-');
        string numeroSinSigno = esNegativo ? numeroStr.Substring(1) : numeroStr;

        if (sistemaOrigen == SistemaNumerico.Decimal)
        {
            if (double.TryParse(numeroSinSigno, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double res))
                return esNegativo ? -res : res;
            throw new FormatException("El número decimal ingresado no tiene un formato válido.");
        }

        int baseOrigen = GetBaseFromSistema(sistemaOrigen);
        string[] partes = numeroSinSigno.Split('.');
        string parteEnteraStr = partes[0];
        string parteFraccionariaStr = partes.Length > 1 ? partes[1] : "";

        double valorEntero = 0;
        long potencia = 1;

        for (int i = parteEnteraStr.Length - 1; i >= 0; i--)
        {
            int valorDigito = ObtenerValorDeDigito(parteEnteraStr[i], baseOrigen, sistemaOrigen);
            valorEntero += valorDigito * potencia;
            potencia *= baseOrigen;
        }

        double valorFraccionario = 0;
        double factor = 1.0 / baseOrigen;
        for (int i = 0; i < parteFraccionariaStr.Length; i++)
        {
            int valorDigito = ObtenerValorDeDigito(parteFraccionariaStr[i], baseOrigen, sistemaOrigen);
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
        int baseDestino = GetBaseFromSistema(sistemaDestino);
        if (baseDestino == 10)
        {
            return numeroDecimal.ToString(System.Globalization.CultureInfo.InvariantCulture);
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
    /// Obtiene el valor numérico de un dígito en una base específica.
    /// </summary>
    /// <param name="digito">El carácter que representa el dígito.</param>
    /// <param name="baseNum">La base numérica.</param>
    /// <param name="sistema">El sistema numérico.</param>
    /// <returns>El valor numérico del dígito.</returns>
    /// <exception cref="ArgumentException">Se lanza si el dígito no es válido para la base dada.</exception>
    private static int ObtenerValorDeDigito(char digito, int baseNum, SistemaNumerico sistema)
    {
        int valor = DIGITOS_HEX.IndexOf(char.ToUpper(digito));
        if (valor == -1 || valor >= baseNum)
        {
            throw new ArgumentException($"El dígito '{digito}' no es válido para el sistema {sistema}.");
        }
        return valor;
    }

    /// <summary>
    /// Obtiene el valor de la base numérica a partir de una enumeración.
    /// </summary>
    /// <param name="sistema">La enumeración del sistema numérico.</param>
    /// <returns>El valor entero de la base.</returns>
    /// <exception cref="ArgumentException">Se lanza si el sistema numérico no es soportado.</exception>
    private static int GetBaseFromSistema(SistemaNumerico sistema)
    {
        switch (sistema)
        {
            case SistemaNumerico.Binario: return 2;
            case SistemaNumerico.Octal: return 8;
            case SistemaNumerico.Decimal: return 10;
            case SistemaNumerico.Hexadecimal: return 16;
            default: throw new ArgumentException("Sistema numérico no soportado.");
        }
    }
}
