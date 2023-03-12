using System.Globalization;

namespace SequenceParser
{
    static class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            
            string clipboardText = TextCopy.ClipboardService.GetText() ?? "";
            SequenceCodeGenerator sequenceCodeGenerator = new SequenceCodeGenerator(clipboardText);
            TextCopy.ClipboardService.SetText(sequenceCodeGenerator.GenerateFinalCode());
        }
    }
}