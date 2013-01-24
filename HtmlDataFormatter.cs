using DiffPlex.DiffBuilder.Model;
using RazorEngine;
using System.IO;

namespace FileDiff
{
    public class HtmlDataFormatter
    {
        public string Format(SideBySideDiffModel diffResult)
        {
            var dataInfo = new[] { diffResult.OldText.Lines, diffResult.NewText.Lines };
            var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string templateText;
            using (var templateStream = new StreamReader(string.Format(@"{0}\Templates\email.template", dir)))
                templateText = templateStream.ReadToEnd();
            
            return Razor.Parse(templateText, new { data = dataInfo });
        }
    }
}
