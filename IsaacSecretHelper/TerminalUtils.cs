using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsaacSecretHelper
{
    public static class TerminalUtils
    {
        public static void WriteTable<T>(IEnumerable<T> objects, int resizeCol, params Func<T, string>[] columns)
        {
            var widths = GetColumnWidths(objects, resizeCol, columns);
            var lines = new StringBuilder();
            foreach (var obj in objects)
                WriteRow(lines, obj, columns, widths);
            
            Console.WriteLine(lines.ToString());
        }

        private static void WriteRow<T>(StringBuilder lines, T obj, Func<T, string>[] columns, int[] widths)
        {
            var texts = columns.Select(c => c(obj)).ToArray();
            var rows = texts.Select((t, i) => Math.Ceiling((float) t.Length / widths[i])).Max();

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns.Length; column++)
                {
                    var text = columns[column](obj);
                    WriteCell(lines, row, column, text, widths);
                }

                lines.AppendLine();
            }
        }

        private static void WriteCell(StringBuilder output, int row, int column, string text, int[] widths)
        {
            for (var i = 0; i < widths[column]; i++)
            {
                var index = row * widths[column] + i;
                output.Append(index < text.Length ? text[index] : ' ');
            }

            if (column < widths.Length - 1)
                output.Append(" | ");
        }

        private static int[] GetColumnWidths<T>(IEnumerable<T> objects, int resizeCol, Func<T, string>[] columns)
        {
            var maxWidths = GetColumnMaxWidths(objects, columns);
            var widths = new List<int>();
            var remainingWidth = Console.WindowWidth - 1;

            for (var i = 0; i < columns.Length; i++)
            {
                if (i > 0)
                    remainingWidth -= 3;
                
                if (i == resizeCol)
                {
                    widths.Add(-1);
                    continue;
                }

                widths.Add(maxWidths[i]);
                remainingWidth -= widths[i];
            }

            widths[resizeCol] = Math.Min(maxWidths[resizeCol], remainingWidth);
            return widths.ToArray();
        }

        private static int[] GetColumnMaxWidths<T>(IEnumerable<T> objects, Func<T, string>[] columns)
        {
            return columns.Select(column => objects.Max(o => column(o).Length)).ToArray();
        }
    }
}