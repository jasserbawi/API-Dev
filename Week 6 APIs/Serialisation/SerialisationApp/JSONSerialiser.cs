using Newtonsoft.Json;

namespace SerialisationApp;

internal class JSONSerialiser : ISerialise
{    
    public void SerialiseToFile<T>(string filePath, T item)
    {
        string jsonString = JsonConvert.SerializeObject(item);

        File.WriteAllText(filePath, jsonString);
    }

    public T DeserialiseFromFile<T>(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        T item = JsonConvert.DeserializeObject<T>(jsonString);

        return item;
    }
}
