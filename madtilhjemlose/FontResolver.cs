using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose
{
    public class FontResolver : IFontResolver
    {
        public byte[]? GetFont(string faceName)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(FontResolver));
            string name = assembly.GetManifestResourceNames().First(x => x.Contains("Regular.ttf"));
            using Stream s = assembly.GetManifestResourceStream(name);
            using MemoryStream mem = new();
            s.CopyTo(mem);
            return mem.ToArray();
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return new FontResolverInfo("test");
        }
    }
}
