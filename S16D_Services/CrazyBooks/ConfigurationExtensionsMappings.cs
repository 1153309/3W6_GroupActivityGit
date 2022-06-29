using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Configuration
{
    public static partial class ConfigurationExtensions
    {
        // Dictionnaire des préfixes de nom d'ordinateur avec leur serveur correspondant
        public static Dictionary<string, string> GetNamePrefixesInfos(this IConfiguration config)
        {
            return new Dictionary<string, string>() {
                { "LLBINF", "%ComputerName%\\SQLEXPRESS" }
            };
        }

        // Dictionnaire des noms d'ordinateur avec leur serveur correspondant
        public static Dictionary<string, string> GetComputerNameInfos(this IConfiguration config)
        {
            return new Dictionary<string, string>() {
                { "LPFINFPORT25", "LPFINFPORT25\\SQLEXPRESS" }
            };
        }
    }
}
