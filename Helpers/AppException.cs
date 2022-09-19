namespace UsuariosEscolaridade.Helpers;

using System.Globalization;

// Excessão customizada para jogar mensagens específicas
public class AppException : Exception
{
    public AppException() : base() {}

    public AppException(string message) : base(message) { }

    public AppException(string message, params object[] args) 
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}