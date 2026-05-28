using System;
using System.Collections.Generic;

public class Paquete
{
    // atributos 
    
    private string _CodigoPaquete;
    private string _Contenido;
    private bool _EsFragil;
    private decimal _ValorDeclarado; // valor monetario declarado por el cliente, en caso de pérdida o daño, se reembolsa este valor.
    private string _TipoPaquete;  // pequeño, mediano, grande, monetario, electrónico, perecedero, etc.
    private double _Peso; // en kg
    private double _Largo;
    private double _Alto;
    private double _Ancho;

    // Constructor
    public Paquete(string codigoPaquete, string contenido, bool esFragil, decimal valorDeclarado, string tipoPaquete, double peso, double largo, double alto, double ancho)
    {
        CodigoPaquete = codigoPaquete;
        Contenido = contenido;
        EsFragil = esFragil;
        ValorDeclarado = valorDeclarado;
        TipoPaquete = tipoPaquete;
        Peso = peso;
        Largo = largo;
        Alto = alto;
        Ancho = ancho;
    }


    // propiedades (aún faltan validar)
    public string CodigoPaquete
    {
        get { return _CodigoPaquete; }
        set { _CodigoPaquete = value; }
    }

    public string Contenido
    {
        get { return _Contenido; }
        set { _Contenido = value; }
    }
    
    public bool EsFragil
    {
        get { return _EsFragil; }
        set { _EsFragil = value; }
    }

    public decimal ValorDeclarado
    {
        get { return _ValorDeclarado; }
        set { _ValorDeclarado = value; }
    }

    public string TipoPaquete
    {
        get { return _TipoPaquete; }
        set { _TipoPaquete = value; }
    }

    public double Peso
    {
        get { return _Peso; }
        set { _Peso = value; }
    }

    public double Largo
    {
        get { return _Largo; }
        set { _Largo = value; }
    }

    public double Alto
    {
        get { return _Alto; }
        set { _Alto = value; }
    }

    public double Ancho
    {
        get { return _Ancho; }
        set { _Ancho = value; }
    }   


    // Métodos

    public double CalcularVolumen()
    {
        return Alto * Ancho * Largo;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"Codigo           : {CodigoPaquete}");
        Console.WriteLine($"Contenido        : {Contenido}");
        Console.WriteLine($"Valor Declarado  : {ValorDeclarado}");
        Console.WriteLine($"Tipo Paquete     : {TipoPaquete}");
        Console.WriteLine($"Peso             : {Peso}");
        Console.WriteLine($"Fragil           : {(EsFragil ? "Sí" : "No")}");
    }

    public decimal CalcularCostoBase()
    {
        decimal costo = (decimal)Peso * 10;

        if (EsFragil)
        {
            costo += 50;
        }

        return costo;
    }

    public string CategoriaPeso()
    {
        return "falta";
    }
}

    /*decimal valorBase = (decimal)(Peso * 10); // $10 por kg
        if (EsFragil)
        {
            valorBase += 50; // Cargo adicional por fragilidad
        }*/