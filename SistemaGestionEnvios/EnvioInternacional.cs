// La clase EnvioInternacional hereda de Envio

public class EnvioInternacional : Envio
{
    // TODO:
    // definir atributos y propiedades
    // definir constructor
    // aplicar métodos heredados
    public EnvioInternacional(string numeroPaquete, string remitente, string destinatario, string estado, DateTime fechaRegistro, Paquete paquete) : base(numeroPaquete, remitente, destinatario, estado, fechaRegistro, paquete)
    {
    }

    
    public override double CalcularCosto()
    {
        throw new NotImplementedException();
    }

    public override string TipoEnvio()
    {
        throw new NotImplementedException();
    }
}