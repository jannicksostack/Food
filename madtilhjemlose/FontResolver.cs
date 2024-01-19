using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose
{
    // I App.xaml.cs sætter vi GlobalFontSettings.FontResolver til denne FontResolver implementation
    public class FontResolver : IFontResolver
    {
        public byte[]? GetFont(string faceName)
        {
            // Først lavede jeg en Embedded Resource i csproj filen
            // <EmbeddedResource Include="Resources\Fonts\*" />
            // Det kopier alle filer i Resources\Fonts mappen ind i vores Assembly

            // Har får jeg fat i vores Assembly
            Assembly assembly = Assembly.GetAssembly(typeof(FontResolver));
            // Så gar jeg i gennem alle resource navne og finder den første der indeholder "Regular.ttf"
            string name = assembly.GetManifestResourceNames().First(x => x.Contains("Regular.ttf"));
            // Så får jeg fat i denne resource som en Stream. 
            using Stream s = assembly.GetManifestResourceStream(name);
            // Da jeg skal bruge dataen som et byte array, kopier jeg dataen til en MemoryStream
            using MemoryStream mem = new();
            s.CopyTo(mem);
            // og kalder metoden ToArray
            return mem.ToArray();
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Når man kalder en XFont constructor, bliver denne metode kaldt
            // Senere bliver GetFont kaldt med faceName = "test"
            // I denne implementation bruger vi i familyName til noget, så det vil være den samme font der bliver loadet
            return new FontResolverInfo("test");
        }
    }
}
