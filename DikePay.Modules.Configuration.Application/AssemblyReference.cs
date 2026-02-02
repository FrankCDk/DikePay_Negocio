using System.Reflection;

namespace DikePay.Modules.Configuration.Application
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
