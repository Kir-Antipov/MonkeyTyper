using MonkeyTyper.Core.Data;
using MonkeyTyper.Core.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonkeyTyper.Plugins.DataReaders.DSV
{
    /// <summary>
    /// Provides access to DSV files (e.g., CSV, TSV and others).
    /// </summary>
    /// <remarks>
    /// This implementation doesn't support line breaks within double quotes.
    /// </remarks>
    [Service(Lifetime = ServiceLifetime.Scoped, ServiceType = typeof(IDataReader), SettingsType = typeof(DSVReaderSettings))]
    public sealed class SinglelineDSVReader : DataReader
    {
        #region Var
        /// <summary>
        /// DSV parser settings.
        /// </summary>
        public DSVReaderSettings Settings { get; }

        /// <inheritdoc/>
        public override object? this[string name] => Current[name];
        
        /// <inheritdoc/>
        public override object? this[int i] => Current[i];

        /// <inheritdoc/>
        public override IReadOnlyList<string> PropertyNames => ColumnNames;
        
        /// <inheritdoc/>
        public override int PropertyCount => ColumnNames.Count;
        
        /// <inheritdoc/>
        public override int Count => EntriesCount;


        private IDataRecord Current { get; set; } = Empty;

        private FileStream? FileStream { get; set; } = null;

        private StreamReader? Reader { get; set; } = null;

        private IReadOnlyList<string> ColumnNames { get; set; } = Array.Empty<string>();

        private int EntriesCount { get; set; } = -1;

        private Regex ActiveParser { get; set; } = DefaultParser;

        private bool TrimWhitespace { get; set; } = true;


        private static readonly Regex DefaultParser = ImplementDSVParser(true, ',', ';', '\t');
        private static readonly Regex CSVParser = ImplementDSVParser(true, ',');
        private static readonly Regex SSVParser = ImplementDSVParser(true, ';');
        private static readonly Regex TSVParser = ImplementDSVParser(true, '\t');
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="SinglelineDSVReader"/> class.
        /// </summary>
        /// <param name="settings">DSV parser settings.</param>
        public SinglelineDSVReader(DSVReaderSettings settings) => Settings = settings ?? throw new ArgumentNullException(nameof(settings));

        private static Regex ImplementDSVParser(bool compiled, params char[] separators)
        {
            RegexOptions options = RegexOptions.Singleline;
            if (compiled)
                options |= RegexOptions.Compiled;
            string orPattern = string.Join("|", separators);
            string anyPattern = string.Concat(separators);
            Regex regex = new Regex($"(?<=^|{orPattern})(?:(?:\"(?<quoted_value>(?:[^\"]|(?:\"\"))*)\")|(?<value>[^\"{anyPattern}]*))(?={orPattern}|$)", options);
            return regex;
        }
        #endregion

        #region Functions
        /// <inheritdoc/>
        public override async Task<bool> NextResultAsync()
        {
            if (Reader is { })
                return false;

            string filename = Settings.Filename;
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("The file path wasn't specified.");

            if (!File.Exists(filename))
                throw new ArgumentException($"The specified file doesn't exist: \"{filename}\".");

            EntriesCount = Math.Max(File.ReadLines(filename).Count() - 1, 0);
            FileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            Reader = new StreamReader(FileStream, detectEncodingFromByteOrderMarks: true);
            ActiveParser = Settings.Separators switch
            {
                "" => DefaultParser,
                "," => CSVParser,
                "\t" => TSVParser,
                ";" => SSVParser,
                null => DefaultParser,
                string separators => ImplementDSVParser(false, separators.ToCharArray())
            };
            TrimWhitespace = Settings.TrimEntries;
            ColumnNames = Array.AsReadOnly(await ReadRowAsync() ?? Array.Empty<string>());

            return true;
        }

        /// <inheritdoc/>
        public override async Task<bool> ReadAsync()
        {
            if (Reader is null)
                return false;

            if (!(await ReadRowAsync() is string[] values))
                return false;

            IEnumerable<KeyValuePair<string, object?>> pairs = ColumnNames.Select((x, i) => new KeyValuePair<string, object?>(x, values.ElementAtOrDefault(i)));
            Current = From(pairs);
            return true;
        }

        private async Task<string[]?> ReadRowAsync()
        {
            if (Reader is null)
                return null;

            if (!(await Reader.ReadLineAsync() is string row))
                return null;

            return ActiveParser.Matches(row).OfType<Match>().Select(match =>
            {
                string value = match.Groups["value"].Value;
                if (string.IsNullOrEmpty(value))
                    value = match.Groups["quoted_value"].Value.Replace("\"\"", "\"");
                if (TrimWhitespace)
                    value = value.Trim();

                return value;
            })
            .ToArray();
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            Reader?.Dispose();
            FileStream?.Dispose();
            Reader = null;
            FileStream = null;
            ColumnNames = Array.Empty<string>();
            EntriesCount = -1;
            ActiveParser = DefaultParser;
        }

        /// <inheritdoc/>
        public override async Task ResetAsync()
        {
            Reader?.Dispose();
#if NETSTANDARD2_1
            await (FileStream?.DisposeAsync() ?? default);
#else
            FileStream?.Dispose();
            await Task.CompletedTask;
#endif
            Reader = null;
            FileStream = null;
            ColumnNames = Array.Empty<string>();
            EntriesCount = -1;
            TrimWhitespace = true;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Reset();
        }

        /// <inheritdoc/>
        public override ValueTask DisposeAsync() => new ValueTask(ResetAsync());
#endregion
    }
}
