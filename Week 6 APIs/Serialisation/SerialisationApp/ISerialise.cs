namespace SerialisationApp;

public interface ISerialise
{
    public void SerialiseToFile<T>(string filePath, T item);

    public T DeserialiseFromFile<T>(string filePath);
}
