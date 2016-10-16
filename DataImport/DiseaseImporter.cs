using CsvHelper;
using CsvHelper.Configuration;
using DataImport.CsvModels;
using Remote.DbModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataImport
{
    public class DiseaseImporter
    {
        private DataContext db;

        public DiseaseImporter()
        {
            if (File.Exists(@"DiseaseAndClimateDb.sdf"))
                File.Delete(@"DiseaseAndClimateDb.sdf");

            db = new DataContext();

            Console.WriteLine(db.DiseaseRecords.Count());
        }

        public void Import<TClass, TClassMap>(string filename) where TClassMap : CsvClassMap where TClass : IDiseaseCsvRecord
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("filename must be specified");

            using (TextReader txtRdr = File.OpenText(filename))
            {
                CsvReader csvRdr = new CsvReader(txtRdr);
                csvRdr.Configuration.RegisterClassMap<TClassMap>();
                IEnumerable<TClass> records = csvRdr.GetRecords<TClass>();

                db.DiseaseRecords.AddRange(records.Select(x => x.ToDiseaseRecord()));
                db.SaveChanges();
            }

            Console.WriteLine(db.DiseaseRecords.Count());
        }

        public void Import2<TClassMap>(string disease, string filename) where TClassMap : CsvClassMap
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("filename must be specified");
            if (string.IsNullOrWhiteSpace(disease))
                throw new ArgumentException("disease must be specified");

            using (TextReader txtRdr = File.OpenText(filename))
            {
                CsvReader csvRdr = new CsvReader(txtRdr);
                csvRdr.Configuration.RegisterClassMap<TClassMap>();
                IEnumerable<CsvDiseaseRecord> records = csvRdr.GetRecords<CsvDiseaseRecord>();

                

                db.DiseaseRecords.AddRange(records.Select(x => ConvertToDiseaseRecord(disease, x)));
                db.SaveChanges();
            }

            Console.WriteLine(db.DiseaseRecords.Count());
        }

        public static DiseaseRecord ConvertToDiseaseRecord(string disease, CsvDiseaseRecord record)
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
