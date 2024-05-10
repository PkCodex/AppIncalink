using PdfSharp.Fonts;
namespace AppIncalink.Datos
{
    public class CustomFontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            // Implementa la lógica para cargar y devolver la fuente en función del nombre de la fuente (faceName).
            // Aquí puedes cargar la fuente desde un archivo, una base de datos, o cualquier otro origen.
            // A modo de ejemplo, cargaré la fuente Verdana desde el sistema de archivos.
            if (faceName == "Verdana-Bold")
            {
                // Lee el archivo de la fuente desde el sistema de archivos
                byte[] fontBytes = File.ReadAllBytes("ruta/a/tu/fuente/Verdana-Bold.ttf");
                return fontBytes;
            }
            // Si no se encuentra la fuente, devuelve null
            return null;
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Implementa la lógica para resolver el tipo de fuente según el nombre de la familia de fuentes y los estilos (negrita, cursiva).
            // Aquí puedes devolver información sobre la fuente, como su nombre y su estilo.
            // A modo de ejemplo, devolveré información sobre la fuente Verdana.
            if (familyName.Equals("Verdana", StringComparison.OrdinalIgnoreCase) && isBold)
            {
                // Devuelve información sobre la fuente Verdana-Bold
                return new FontResolverInfo("Verdana-Bold");
            }
            // Si no se encuentra la fuente, devuelve null
            return null;
        }
    }
}
