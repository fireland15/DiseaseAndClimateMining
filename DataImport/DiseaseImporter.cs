using CsvHelper;
using CsvHelper.Configuration;
using DataImport.CsvModels;
using DataImport.CsvModels.CsvMaps;
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
        private List<Location> _states;

        public DiseaseImporter(DataContext db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db), "The db must not be null");

            _db = db;

            _states = _db.Locations.ToList();// caches locations in the class so that there are fewer db calls. Makes importing faster.

            _timer = new Stopwatch();
        }

        public void ImportAllDiseases()
        {
            Import<BabesiosisMap>(Diseases.Babesiosis, Files[0]);
            Import<BabesiosisMap>(Diseases.Babesiosis, Files[1]);
            Import<BabesiosisMap>(Diseases.Babesiosis, Files[2]);

            Import<ChlamydiaMap>(Diseases.Chlamydia, Files[3]);
            Import<ChlamydiaMap>(Diseases.Chlamydia, Files[4]);
            Import<ChlamydiaMap>(Diseases.Chlamydia, Files[5]);

            Import<CoccidioidomycosisMap>(Diseases.Coccidioidomycosis, Files[3]);
            Import<CoccidioidomycosisMap>(Diseases.Coccidioidomycosis, Files[4]);
            Import<CoccidioidomycosisMap>(Diseases.Coccidioidomycosis, Files[5]);

            Import<CryptosporidiosisMap>(Diseases.Cryptosporidiosis, Files[6]);
            Import<CryptosporidiosisMap>(Diseases.Cryptosporidiosis, Files[7]);
            Import<CryptosporidiosisMap>(Diseases.Cryptosporidiosis, Files[8]);

            Import<DengueFeverMap>(Diseases.DengueFever, Files[6]);
            Import<DengueFeverMap>(Diseases.DengueFever, Files[7]);
            Import<DengueFeverMap>(Diseases.DengueFever, Files[8]);

            Import<DengueHemorrhagicFeverMap>(Diseases.DengueHemorrhagicFever, Files[6]);
            Import<DengueHemorrhagicFeverMap>(Diseases.DengueHemorrhagicFever, Files[7]);
            Import<DengueHemorrhagicFeverMap>(Diseases.DengueHemorrhagicFever, Files[8]);

            Import<EhrlichiosisMap>(Diseases.Ehrlichiosis, Files[9]);
            Import<EhrlichiosisMap>(Diseases.Ehrlichiosis, Files[10]);
            Import<EhrlichiosisMap>(Diseases.Ehrlichiosis, Files[11]);

            Import<AnaplasmosisMap>(Diseases.Anaplasmosis, Files[9]);
            Import<AnaplasmosisMap>(Diseases.Anaplasmosis, Files[10]);
            Import<AnaplasmosisMap>(Diseases.Anaplasmosis, Files[11]);

            Import<GiardiasisMap>(Diseases.Giardiasis, Files[12]);
            Import<GiardiasisMap>(Diseases.Giardiasis, Files[13]);
            Import<GiardiasisMap>(Diseases.Giardiasis, Files[14]);

            Import<GonorrheaMap>(Diseases.Gonorrhea, Files[12]);
            Import<GonorrheaMap>(Diseases.Gonorrhea, Files[13]);
            Import<GonorrheaMap>(Diseases.Gonorrhea, Files[14]);

            Import<HaemophilusInfluenzaMap>(Diseases.HaemophilusInfluenza, Files[12]);
            Import<HaemophilusInfluenzaMap>(Diseases.HaemophilusInfluenza, Files[13]);
            Import<HaemophilusInfluenzaMap>(Diseases.HaemophilusInfluenza, Files[14]);

            Import<HepatitisAMap>(Diseases.HepatitisA, Files[15]);
            Import<HepatitisAMap>(Diseases.HepatitisA, Files[16]);
            Import<HepatitisAMap>(Diseases.HepatitisA, Files[17]);

            Import<HepatitisBMap>(Diseases.HepatitisB, Files[15]);
            Import<HepatitisBMap>(Diseases.HepatitisB, Files[16]);
            Import<HepatitisBMap>(Diseases.HepatitisB, Files[17]);

            Import<HepatitisCMap>(Diseases.HepatitisC, Files[15]);
            Import<HepatitisCMap>(Diseases.HepatitisC, Files[16]);
            Import<HepatitisCMap>(Diseases.HepatitisC, Files[17]);

            Import<InvasivePneumococcalMap>(Diseases.InvasivePneumococcal, Files[18]);
            Import<InvasivePneumococcalMap>(Diseases.InvasivePneumococcal, Files[19]);
            Import<InvasivePneumococcalMap>(Diseases.InvasivePneumococcal, Files[20]);

            Import<LegionellosisMap>(Diseases.Legionellosis, Files[18]);
            Import<LegionellosisMap>(Diseases.Legionellosis, Files[19]);
            Import<LegionellosisMap>(Diseases.Legionellosis, Files[20]);

            Import<LymeDiseaseMap>(Diseases.LymeDisease, Files[21]);
            Import<LymeDiseaseMap>(Diseases.LymeDisease, Files[22]);
            Import<LymeDiseaseMap>(Diseases.LymeDisease, Files[23]);

            Import<MalariaMap>(Diseases.Malaria, Files[21]);
            Import<MalariaMap>(Diseases.Malaria, Files[22]);
            Import<MalariaMap>(Diseases.Malaria, Files[23]);

            Import<MeningococcalMap>(Diseases.Meningococcal, Files[21]);
            Import<MeningococcalMap>(Diseases.Meningococcal, Files[22]);
            Import<MeningococcalMap>(Diseases.Meningococcal, Files[23]);

            Import<MumpsMap>(Diseases.Mumps, Files[24]);
            Import<MumpsMap>(Diseases.Mumps, Files[25]);
            Import<MumpsMap>(Diseases.Mumps, Files[26]);

            Import<PertussisMap>(Diseases.Pertussis, Files[24]);
            Import<PertussisMap>(Diseases.Pertussis, Files[25]);
            Import<PertussisMap>(Diseases.Pertussis, Files[26]);

            Import<RabiesMap>(Diseases.Rabies, Files[24]);
            Import<RabiesMap>(Diseases.Rabies, Files[25]);
            Import<RabiesMap>(Diseases.Rabies, Files[26]);

            Import<SamonellosisMap>(Diseases.Salmonellosis, Files[27]);
            Import<SamonellosisMap>(Diseases.Salmonellosis, Files[28]);
            Import<SamonellosisMap>(Diseases.Salmonellosis, Files[29]);

            Import<ShigaToxinMap>(Diseases.ShigaToxin, Files[30]);
            Import<ShigaToxinMap>(Diseases.ShigaToxin, Files[31]);
            Import<ShigaToxinMap>(Diseases.ShigaToxin, Files[32]);

            Import<ShigellosisMap>(Diseases.Shigellosis, Files[30]);
            Import<ShigellosisMap>(Diseases.Shigellosis, Files[31]);
            Import<ShigellosisMap>(Diseases.Shigellosis, Files[32]);

            Import<SpottedFeverRickettsiosisMap>(Diseases.SpottedFeverRickettsiosis, Files[33]);
            Import<SpottedFeverRickettsiosisMap>(Diseases.SpottedFeverRickettsiosis, Files[34]);
            Import<SpottedFeverRickettsiosisMap>(Diseases.SpottedFeverRickettsiosis, Files[35]);

            Import<SyphilisMap>(Diseases.Syphilis, Files[33]);
            Import<SyphilisMap>(Diseases.Syphilis, Files[34]);
            Import<SyphilisMap>(Diseases.Syphilis, Files[35]);

            Import<VaricellaMap>(Diseases.Varicella, Files[36]);
            Import<VaricellaMap>(Diseases.Varicella, Files[37]);
            Import<VaricellaMap>(Diseases.Varicella, Files[38]);

            Import<WestNileVirusDiseaseMap>(Diseases.WestNileVirus, Files[36]);
            Import<WestNileVirusDiseaseMap>(Diseases.WestNileVirus, Files[39]);
            Import<WestNileVirusDiseaseMap>(Diseases.WestNileVirus, Files[40]);
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

                _db.DiseaseRecords
                    .AddRange(records.Where(x => _states.Any(y => y.Name.Equals(x.Location, StringComparison.CurrentCultureIgnoreCase)))
                                     .Select(x => ConvertToDiseaseRecord(disease, x)));
                _db.SaveChanges();

                newRecordsCount = _db.DiseaseRecords.Count() - count;
            }

            _timer.Stop();

            Console.WriteLine($"\t{_timer.ElapsedMilliseconds}ms {newRecordsCount} {disease} records imported");
            _timer.Reset();
        }

        private DiseaseRecord ConvertToDiseaseRecord(string disease, CsvDiseaseRecord record)
        {
            return new DiseaseRecord
            {
                DiseaseName = disease,
                Location = _states.Single(x => x.Name.Equals(record.Location, StringComparison.CurrentCultureIgnoreCase)),
                Year = record.Year,
                Week = Convert.ToInt32(record.Week != string.Empty ? record.Week : "0"),
                NewInfections = Convert.ToInt32(record.NewInfections != string.Empty ? record.NewInfections : "0")
            };
        }

        private static string[] Files =
        {
            "../../../Data/CDC/Babesiosis_2014.csv",
            "../../../Data/CDC/BabesiosisToCampylobacteriosis_2015.csv",
            "../../../Data/CDC/BabesiosisToCampylobacteriosis_2016.csv",

            "../../../Data/CDC/ChlamydiaToCoccidioidomycosis_2014.csv",
            "../../../Data/CDC/ChlamydiaToCoccidioidomycosis_2015.csv",
            "../../../Data/CDC/ChlamydiaToCoccidioidomycosis_2016.csv",

            "../../../Data/CDC/CryptosporidiosisToDengue_2014.csv",
            "../../../Data/CDC/CryptosporidiosisToDengue_2015.csv",
            "../../../Data/CDC/CryptosporidiosisToDengue_2016.csv",

            "../../../Data/CDC/EhrlichiosisAnaplasmosisDisease_2014.csv",
            "../../../Data/CDC/EhrlichiosisAnaplasmosisDisease_2015.csv",
            "../../../Data/CDC/EhrlichiosisAnaplasmosisDisease_2016.csv",

            "../../../Data/CDC/GiardiasisToHaemophilusInfluenza_2014.csv",
            "../../../Data/CDC/GiardiasisToHaemophilusInfluenza_2015.csv",
            "../../../Data/CDC/GiardiasisToHaemophilusInfluenza_2016.csv",

            "../../../Data/CDC/HepatitisViralAcute_2014.csv",
            "../../../Data/CDC/HepatitisViralAcute_2015.csv",
            "../../../Data/CDC/HepatitisViralAcute_2016.csv",

            "../../../Data/CDC/InvasivePneumococcalToLegionellosis_2014.csv",
            "../../../Data/CDC/InvasivePneumococcalToLegionellosis_2015.csv",
            "../../../Data/CDC/InvasivePneumococcalToLegionellosis_2016.csv",

            "../../../Data/CDC/LymeDiseaseToMeningococcal_2014.csv",
            "../../../Data/CDC/LymeDiseaseToMeningococcal_2015.csv",
            "../../../Data/CDC/LymeDiseaseToMeningococcal_2016.csv",

            "../../../Data/CDC/MumpsToRabies_2014.csv",
            "../../../Data/CDC/MumpsToRabies_2015.csv",
            "../../../Data/CDC/MumpsToRabies_2016.csv",

            "../../../Data/CDC/Salmonellosis_2014.csv",
            "../../../Data/CDC/RubellaToSalmonellosis_2015.csv",
            "../../../Data/CDC/RubellaToSalmonellosis_2016.csv",

            "../../../Data/CDC/ShigaToxinToShigellosis_2014.csv",
            "../../../Data/CDC/ShigaToxinToShigellosis_2015.csv",
            "../../../Data/CDC/ShigaToxinToShigellosis_2016.csv",

            "../../../Data/CDC/SpottedFeverRickettsiosisToSyphillis_2014.csv",
            "../../../Data/CDC/SpottedFeverRickettsiosisToSyphillis_2015.csv",
            "../../../Data/CDC/SpottedFeverRickettsiosisToSyphillis_2016.csv",

            "../../../Data/CDC/VaricellaToWestNileVirusDisease_2014.csv",
            "../../../Data/CDC/TetanusToVibriosis_2015.csv",
            "../../../Data/CDC/TetanusToVibriosis_2016.csv",

            "../../../Data/CDC/WestNileVirusDisease_2015.csv",
            "../../../Data/CDC/WestNileVirusDisease_2016.csv"
        };
    }
}
