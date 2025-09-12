using CsvHelper;
using System.Text.Json;
using OrchardCore.Workflows.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Workflows.Export.Services
{
    public class ExportService : IExportService
    {
        #region Implementation

        public async Task<Stream> GetExportFileAsStreamAsync(IEnumerable<Workflow> instances)
        {
            var rows = instances.Select(x => GetOutput(x)).ToList();
            var headers = GetHeaders(rows);

            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            await WriteHeadersAsync(csvWriter, headers);
            await WriteRowsAsync(csvWriter, headers, rows);

            await csvWriter.FlushAsync();
            await streamWriter.FlushAsync();

            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        public IDictionary<string, string> GetOutput(Workflow instance)
        {
            var result = (IDictionary<string, string>)new Dictionary<string, string>();

            if (instance == null)
            {
                return result;
            }

            result.Add(ExportConstants.CreatedAtUTCColumnName, instance.CreatedUtc.ToString());

            if (instance.State == null)
            {
                return result;
            }

            // Get the raw JSON string for "Output"
            var outputJson = instance.State["Output"]?.ToString();

            if (string.IsNullOrWhiteSpace(outputJson))
            {
                return result;
            }

            // Parse with System.Text.Json
            IDictionary<string, string> outputDict;
            try
            {
                outputDict = JsonSerializer.Deserialize<Dictionary<string, string>>(outputJson);
            }
            catch
            {
                outputDict = new Dictionary<string, string>();
            }

            // Merge dictionaries
            return new[] { result, outputDict }.SelectMany(dict => dict)
                         .ToLookup(pair => pair.Key, pair => pair.Value)
                         .ToDictionary(group => group.Key, group => group.First());
        }

        #endregion Implementation

        #region Private Methods

        private static IList<string> GetHeaders(IList<IDictionary<string, string>> rows)
        {
            return rows.SelectMany(x => x.Keys).Distinct().ToList();
        }

        private static async Task WriteHeadersAsync(CsvWriter csvWriter, IList<string> headers)
        {
            foreach (var header in headers)
            {
                csvWriter.WriteField(header);
            }
            await csvWriter.NextRecordAsync();
        }

        private static async Task WriteRowsAsync(CsvWriter csvWriter, IList<string> headers, List<IDictionary<string, string>> rows)
        {
            foreach (var row in rows)
            {
                foreach (var header in headers)
                {
                    csvWriter.WriteField(row.ContainsKey(header) ? row[header] ?? "" : "");
                }
                await csvWriter.NextRecordAsync();
            }
        }

        #endregion Private Methods
    }
}
