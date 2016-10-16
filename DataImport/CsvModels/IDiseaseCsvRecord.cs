using Remote.DbModels;

namespace DataImport.CsvModels
{
    public interface IDiseaseCsvRecord
    {
        DiseaseRecord ToDiseaseRecord();
    }
}
