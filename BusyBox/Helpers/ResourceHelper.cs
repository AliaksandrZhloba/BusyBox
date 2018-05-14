using System;
using System.IO;
using System.Reflection;
using System.Windows;


namespace BusyBox.Helpers
{
    public static class ResourceHelper
    {
        public static T Get<T>(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("BusyBox.ResourceDictionary.xaml"))
            {
                var reader = new System.Windows.Markup.XamlReader();
                var resDictionary = (ResourceDictionary)reader.LoadAsync(stream);

                return (T)resDictionary[resourceName];
            }
        }
    }
}
