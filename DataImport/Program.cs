using DataImport.CsvModels.CsvMaps;
using Remote.DbModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DataImport
{
    class Program
    {
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
       
        static void Main(string[] args)
        {
            ClearDatabase();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            StartImportProcess();
            watch.Stop();
            float timeToImport = watch.Elapsed.Seconds;
            int recordsImported;

            using (DataContext context = new DataContext())
            {
                 recordsImported = context.DiseaseRecords.Count();
            }

            Console.WriteLine($"{recordsImported} disease records imported in {timeToImport} sceonds");
        }

        private static void ClearDatabase()
        {
            if (File.Exists("DiseaseAndClimateDb.sdf"))
                File.Delete("DiseaseAndClimateDb.sdf");
        }

        private static void StartImportProcess()
        {
            using (DataContext context = new DataContext())
            {
                DiseaseImporter importer = new DiseaseImporter(context);

                importer.Import<BabesiosisMap>(Diseases.Babesiosis, Files[0]);
                importer.Import<BabesiosisMap>(Diseases.Babesiosis, Files[1]);
                importer.Import<BabesiosisMap>(Diseases.Babesiosis, Files[2]);

                importer.Import<ChlamydiaMap>(Diseases.Chlamydia, Files[3]);
                importer.Import<ChlamydiaMap>(Diseases.Chlamydia, Files[4]);
                importer.Import<ChlamydiaMap>(Diseases.Chlamydia, Files[5]);

                importer.Import<CoccidioidomycosisMap>(Diseases.Coccidioidomycosis, Files[3]);
                importer.Import<CoccidioidomycosisMap>(Diseases.Coccidioidomycosis, Files[4]);
                importer.Import<CoccidioidomycosisMap>(Diseases.Coccidioidomycosis, Files[5]);

                importer.Import<CryptosporidiosisMap>(Diseases.Cryptosporidiosis, Files[6]);
                importer.Import<CryptosporidiosisMap>(Diseases.Cryptosporidiosis, Files[7]);
                importer.Import<CryptosporidiosisMap>(Diseases.Cryptosporidiosis, Files[8]);

                importer.Import<DengueFeverMap>(Diseases.DengueFever, Files[6]);
                importer.Import<DengueFeverMap>(Diseases.DengueFever, Files[7]);
                importer.Import<DengueFeverMap>(Diseases.DengueFever, Files[8]);

                importer.Import<DengueHemorrhagicFeverMap>(Diseases.DengueHemorrhagicFever, Files[6]);
                importer.Import<DengueHemorrhagicFeverMap>(Diseases.DengueHemorrhagicFever, Files[7]);
                importer.Import<DengueHemorrhagicFeverMap>(Diseases.DengueHemorrhagicFever, Files[8]);

                importer.Import<EhrlichiosisMap>(Diseases.Ehrlichiosis, Files[9]);
                importer.Import<EhrlichiosisMap>(Diseases.Ehrlichiosis, Files[10]);
                importer.Import<EhrlichiosisMap>(Diseases.Ehrlichiosis, Files[11]);

                importer.Import<AnaplasmosisMap>(Diseases.Anaplasmosis, Files[9]);
                importer.Import<AnaplasmosisMap>(Diseases.Anaplasmosis, Files[10]);
                importer.Import<AnaplasmosisMap>(Diseases.Anaplasmosis, Files[11]);

                importer.Import<GiardiasisMap>(Diseases.Giardiasis, Files[12]);
                importer.Import<GiardiasisMap>(Diseases.Giardiasis, Files[13]);
                importer.Import<GiardiasisMap>(Diseases.Giardiasis, Files[14]);

                importer.Import<GonorrheaMap>(Diseases.Gonorrhea, Files[12]);
                importer.Import<GonorrheaMap>(Diseases.Gonorrhea, Files[13]);
                importer.Import<GonorrheaMap>(Diseases.Gonorrhea, Files[14]);

                importer.Import<HaemophilusInfluenzaMap>(Diseases.HaemophilusInfluenza, Files[12]);
                importer.Import<HaemophilusInfluenzaMap>(Diseases.HaemophilusInfluenza, Files[13]);
                importer.Import<HaemophilusInfluenzaMap>(Diseases.HaemophilusInfluenza, Files[14]);

                importer.Import<HepatitisAMap>(Diseases.HepatitisA, Files[15]);
                importer.Import<HepatitisAMap>(Diseases.HepatitisA, Files[16]);
                importer.Import<HepatitisAMap>(Diseases.HepatitisA, Files[17]);

                importer.Import<HepatitisBMap>(Diseases.HepatitisB, Files[15]);
                importer.Import<HepatitisBMap>(Diseases.HepatitisB, Files[16]);
                importer.Import<HepatitisBMap>(Diseases.HepatitisB, Files[17]);

                importer.Import<HepatitisCMap>(Diseases.HepatitisC, Files[15]);
                importer.Import<HepatitisCMap>(Diseases.HepatitisC, Files[16]);
                importer.Import<HepatitisCMap>(Diseases.HepatitisC, Files[17]);

                importer.Import<InvasivePneumococcalMap>(Diseases.InvasivePneumococcal, Files[18]);
                importer.Import<InvasivePneumococcalMap>(Diseases.InvasivePneumococcal, Files[19]);
                importer.Import<InvasivePneumococcalMap>(Diseases.InvasivePneumococcal, Files[20]);

                importer.Import<LegionellosisMap>(Diseases.Legionellosis, Files[18]);
                importer.Import<LegionellosisMap>(Diseases.Legionellosis, Files[19]);
                importer.Import<LegionellosisMap>(Diseases.Legionellosis, Files[20]);

                importer.Import<LymeDiseaseMap>(Diseases.LymeDisease, Files[21]);
                importer.Import<LymeDiseaseMap>(Diseases.LymeDisease, Files[22]);
                importer.Import<LymeDiseaseMap>(Diseases.LymeDisease, Files[23]);

                importer.Import<MalariaMap>(Diseases.Malaria, Files[21]);
                importer.Import<MalariaMap>(Diseases.Malaria, Files[22]);
                importer.Import<MalariaMap>(Diseases.Malaria, Files[23]);

                importer.Import<MeningococcalMap>(Diseases.Meningococcal, Files[21]);
                importer.Import<MeningococcalMap>(Diseases.Meningococcal, Files[22]);
                importer.Import<MeningococcalMap>(Diseases.Meningococcal, Files[23]);

                importer.Import<MumpsMap>(Diseases.Mumps, Files[24]);
                importer.Import<MumpsMap>(Diseases.Mumps, Files[25]);
                importer.Import<MumpsMap>(Diseases.Mumps, Files[26]);

                importer.Import<PertussisMap>(Diseases.Pertussis, Files[24]);
                importer.Import<PertussisMap>(Diseases.Pertussis, Files[25]);
                importer.Import<PertussisMap>(Diseases.Pertussis, Files[26]);

                importer.Import<RabiesMap>(Diseases.Rabies, Files[24]);
                importer.Import<RabiesMap>(Diseases.Rabies, Files[25]);
                importer.Import<RabiesMap>(Diseases.Rabies, Files[26]);

                importer.Import<SamonellosisMap>(Diseases.Salmonellosis, Files[27]);
                importer.Import<SamonellosisMap>(Diseases.Salmonellosis, Files[28]);
                importer.Import<SamonellosisMap>(Diseases.Salmonellosis, Files[29]);

                importer.Import<ShigaToxinMap>(Diseases.ShigaToxin, Files[30]);
                importer.Import<ShigaToxinMap>(Diseases.ShigaToxin, Files[31]);
                importer.Import<ShigaToxinMap>(Diseases.ShigaToxin, Files[32]);

                importer.Import<ShigellosisMap>(Diseases.Shigellosis, Files[30]);
                importer.Import<ShigellosisMap>(Diseases.Shigellosis, Files[31]);
                importer.Import<ShigellosisMap>(Diseases.Shigellosis, Files[32]);

                importer.Import<SpottedFeverRickettsiosisMap>(Diseases.SpottedFeverRickettsiosis, Files[33]);
                importer.Import<SpottedFeverRickettsiosisMap>(Diseases.SpottedFeverRickettsiosis, Files[34]);
                importer.Import<SpottedFeverRickettsiosisMap>(Diseases.SpottedFeverRickettsiosis, Files[35]);

                importer.Import<SyphilisMap>(Diseases.Syphilis, Files[33]);
                importer.Import<SyphilisMap>(Diseases.Syphilis, Files[34]);
                importer.Import<SyphilisMap>(Diseases.Syphilis, Files[35]);

                importer.Import<VaricellaMap>(Diseases.Varicella, Files[36]);
                importer.Import<VaricellaMap>(Diseases.Varicella, Files[37]);
                importer.Import<VaricellaMap>(Diseases.Varicella, Files[38]);

                importer.Import<WestNileVirusDiseaseMap>(Diseases.WestNileVirus, Files[36]);
                importer.Import<WestNileVirusDiseaseMap>(Diseases.WestNileVirus, Files[39]);
                importer.Import<WestNileVirusDiseaseMap>(Diseases.WestNileVirus, Files[40]);
            }
        }
    }
}
