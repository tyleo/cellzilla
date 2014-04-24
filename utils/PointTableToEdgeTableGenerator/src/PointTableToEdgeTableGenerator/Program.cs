using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PointTableToEdgeTableGenerator
{
    public static class Program
    {
        public static void Main()
        {
            File.WriteAllText("PointFlagsToEdgeFlagsTable.txt", CreatePointFlagsToEdgeFlagsTable());
            File.WriteAllText("PointFlagsToEdgeIndicesTable.txt", CreatePointFlagsToEdgeIndicesTable());
        }

        private static string CreatePointFlagsToEdgeFlagsTable()
        {
            var tableStringBuilder = new StringBuilder();

            tableStringBuilder
                .Append('{')
                .AppendLine();

            foreach (var @byte in GetAllBytes())
            {
                var thisEdgeFlag = PointTableToEdgeTableProvider.GetEdgeFlagsFromPointFlags((PointFlags)@byte);

                tableStringBuilder
                    .Append('\t');

                foreach (var edgeFlag in GetAllEdgeFlags())
                {
                    tableStringBuilder
                        .Append("EdgeFlags.").Append(thisEdgeFlag.HasFlag(edgeFlag) ? edgeFlag.ToShortString() : "None  ").Append(" | ");
                }

                tableStringBuilder
                    .Remove(tableStringBuilder.Length - 3, 3)
                    .Append(',')
                    .AppendLine();
            }

            tableStringBuilder
                .Remove(tableStringBuilder.Length - 4, 4)
                .AppendLine()
                .Append('}')
                .AppendLine();

            return tableStringBuilder.ToString();
        }

        private static IEnumerable<EdgeFlags> GetAllEdgeFlags()
        {
            foreach(var @int in Enumerable.Range(0, 12))
            {
                yield return (EdgeFlags)(1 << @int);
            }
        }

        private static string CreatePointFlagsToEdgeIndicesTable()
        {
            var tableStringBuilder = new StringBuilder();

            tableStringBuilder.Append('{')
                .AppendLine();

            foreach (var @byte in GetAllBytes())
            {
                var theseEdgeIndices = PointTableToEdgeTableProvider.GetEdgeIndicesFromPointFlags((PointFlags)@byte);

                tableStringBuilder
                    .Append("\t new EdgeIndex[] { ");

                foreach (var thisEdgeIndex in theseEdgeIndices)
                {
                    tableStringBuilder.Append("EdgeIndex.").Append(thisEdgeIndex.ToShortString()).Append(", ");
                }

                if (theseEdgeIndices.Any())
                {
                    tableStringBuilder
                        .Remove(tableStringBuilder.Length - 2, 2)
                        .Append(" },");
                }
                else
                {
                    tableStringBuilder
                    .Remove(tableStringBuilder.Length - 1, 1)
                    .Append("},");
                }

                tableStringBuilder
                    .AppendLine();
            }

            tableStringBuilder.Remove(tableStringBuilder.Length - 3, 3)
                .AppendLine()
                .Append('}')
                .AppendLine();

            return tableStringBuilder.ToString();
        }

        private static IEnumerable<byte> GetAllBytes()
        {
            foreach (var @byte in Enumerable.Range(byte.MinValue, byte.MaxValue + 1))
            {
                yield return (byte)@byte;
            }
        }
    }
}
