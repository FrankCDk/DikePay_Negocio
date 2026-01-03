namespace DikePay.Modules.Auth.Application
{
    /// <summary>
    /// Clase ancla para que MediatR y otros servicios puedan localizar este ensamblado
    /// sin necesidad de referenciar tipos volátiles (como Handlers).
    /// </summary>
    public static class AssemblyReference
    {
        public static readonly System.Reflection.Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
