using System.Collections.Concurrent;

namespace LambdasAndLinq;

public class Program
{
    static void Main(string[] args)
    {
        // LINQ = Language Integrated Queries
        var nums = new List<int> { 3, 7, 1, 2, 8, 3, 0, 4, 5 };

        var numsCount = nums.Count();

        //int countEven = 0;
        //foreach(int num in nums)
        //{
        //    if (IsEven(num)) countEven++;
        //}

        int countEven = nums.Count(IsEven); //Passing in IsEven (reference), not calling it

        List<Person> people = new List<Person> {
            new Person { Name = "Cathy", Age = 40, City = "Ottawa"},
            new Person { Name = "Nish", Age = 55,City = "Birmingham"},
            new Person { Name = "Martin", Age = 20, City = "London"}
        };

        var countYoungPeople = people.Count(isYoung);

        //Anonymous method using delegates

        int countDEven = nums.Count(delegate (int num) { return num % 2 == 0; });

        //Lambda Expressions
        // given something => return something
        int sumOfSquares = nums.Sum(x => x * x); // => is the operator and the whole thing is the expression

        int countLEven = nums.Count(n => n % 2 == 0); //n is declared in the given section of the lambda expression

        //Linq and lambda
        var peopleInLondonQuery = people.Where(p => p.City == "London").ToList(); //This is the query to get the people from london

        var peopleByAge = people.OrderBy(p => p.Age);

        foreach(var person in peopleByAge)
        {
            Console.WriteLine(person);
        }

        var namesOfThoseOver20 = people.Where(p => p.Age > 20).Select(p => p.Name).ToList(); //Where and Select are LINQ query terms

        string newString = ModifyString("Hello World", s => s.Replace(" ", "_").ToUpper()); //The lambda expression is strModify (method)
    }

    private static string ModifyString(string str, Func<string, string> strModify) //Func<input parameter, output parameter>
    {
        return strModify(str);
    }

    private static int Square(int x)
    {
        return x * x;
    }

    private static bool IsEven(int num)
    {
        return num % 2 == 0;
    }

    private static bool isYoung(Person p)
    {
        return p.Age < 30;
    }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public override string ToString()
    {
        return $"{Name} - {City} - {Age}";
    }
}