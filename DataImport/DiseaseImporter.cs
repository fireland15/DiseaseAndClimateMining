using CsvHelper;
using CsvHelper.Configuration;
using DataImport.CsvModels;
using Remote.DbModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImport
{
    public class DiseaseImporter
    {
        private DataContext _db;
        private Stopwatch _timer;

        public DiseaseImporter(DataContext db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db), "The db must not be null");

            _db = db;

            _timer = new Stopwatch();
        }

        /// <summary>
        /// Reads records from the file specified by <code>filename</code> and uses the TClassMap generic paramter to create DiseaseRecord objects.
        /// Saves the imported DiseaseRecord objects to the data context and sets their DiseaseName attribute to <code>disease</code>.
        /// </summary>
        /// <typeparam name="TClassMap">The map class to be used to map csv attributes to a class</typeparam>
        /// <param name="disease">The disease of the imported records.</param>
        /// <param name="filename">The filename of the file to be imported.</param>
        public void Import<TClassMap>(string disease, string filename) where TClassMap : CsvClassMap
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("filename must be specified");
            if (string.IsNullOrWhiteSpace(disease))
                throw new ArgumentException("disease must be specified");

            Console.WriteLine($"Importing {disease} from {filename}");

            _timer.Start();
            var newRecordsCount = 0;

            using (TextReader txtRdr = File.OpenText(filename))
            {
                CsvReader csvRdr = new CsvReader(txtRdr);
                csvRdr.Configuration.RegisterClassMap<TClassMap>();
                IEnumerable<CsvDiseaseRecord> records = csvRdr.GetRecords<CsvDiseaseRecord>();

                var count = _db.DiseaseRecords.Count();
                _db.DiseaseRecords.AddRange(records.Select(x => ConvertToDiseaseRecord(disease, x)));
                _db.SaveChangesAsync();
                newRecordsCount = _db.DiseaseRecords.Count() - count;
            }

            _timer.Stop();

            Console.WriteLine($"\t{_timer.ElapsedMilliseconds}ms {newRecordsCount} {disease} records imported");
            _timer.Reset();
        }

        private static DiseaseRecord ConvertToDiseaseRecord(string disease, CsvDiseaseRecord record)
        {
            return new DiseaseRecord
            {
                DiseaseName = disease,
                Location = record.Location,
                Year = record.Year,
                Week = Convert.ToInt32(record.Week != string.Empty ? record.Week : "0"),
                NewInfections = Convert.ToInt32(record.NewInfections != string.Empty ? record.NewInfections : "0")
            };
        }
    }
}
