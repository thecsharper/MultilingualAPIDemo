using Microsoft.Extensions.Localization;
using System.Reflection;

namespace MultilingualAPIDemo.Locale
{
    public class ResourceLocalizer : IResourceLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public ResourceLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.Assembly.FullName!);
            _localizer = factory.Create("SharedResource", assemblyName.Name!);
        }

        public string Localize(string key)
        {
            return _localizer[key];
        }
    }
}
