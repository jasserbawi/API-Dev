using Newtonsoft.Json;

namespace SerialisationApp;

public class Program
{
    private static readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private static ISerialise _serialiser;
    static void Main(string[] args)
    {
        //Trainee Jasser = new Trainee() { FirstName = "Jasser", LastName = "Bawi", SpartaNo = 7 };

        _serialiser = new BinarySerialiser();

        //serialiser.SerialiseToFile<Trainee>($"{_path}/SpartaGlobal/BinaryJasser.bin", Jasser);

        Trainee Jasser = _serialiser.DeserialiseFromFile<Trainee>($"{_path}/SpartaGlobal/BinaryJasser.bin");

        _serialiser = new XMLSerialiser();

        //_serialiser.SerialiseToFile<Trainee>($"{_path}/SpartaGlobal/XMLJasser.XML", Jasser);

        Jasser = _serialiser.DeserialiseFromFile<Trainee>($"{_path}/SpartaGlobal/BinaryJasser.XML");

        Course eng134 = new Course()
        {
            Title = "Engineering 134",
            Subject = "C# SDET",
            StartDate = new DateTime(2022, 11, 28)
        };

        eng134.AddTrainee(Jasser);
        eng134.AddTrainee(new Trainee() { FirstName = "Faisal", LastName = "Khalaf", SpartaNo = 47 });
        eng134.AddTrainee(new Trainee() { FirstName = "Mehdi", LastName = "Hamdi", SpartaNo = 5 });

        //_serialiser.SerialiseToFile<Course>($"{_path}/SpartaGlobal/XMLCourse.XML", eng134);

        _serialiser = new JSONSerialiser();

        _serialiser.SerialiseToFile<Course>($"{_path}/SpartaGlobal/JSONCourse.json", eng134);
    }
}

[Serializable]
public class Trainee
{
    public string? FirstName { get; init; } // ? means FirstName can hold null
    public string? LastName { get; init; }
    public int? SpartaNo { get; init; }

    [JsonIgnore] // ignore when we type it second time around
    public string FullName => $"{FirstName} {LastName}";
    public override string ToString()
    {
        return $"{SpartaNo} - {FullName}";
    }
}

[Serializable]
public class Course
{
    public string Subject { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public List<Trainee> Trainees { get; } = new List<Trainee>();
    [field: NonSerialized]
    private readonly DateTime _lastRead;
    public Course()
    {
        _lastRead = DateTime.Now;
    }
    public void AddTrainee(Trainee newTrainee)
    {
        Trainees.Add(newTrainee);
    }
}