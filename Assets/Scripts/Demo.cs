using System;

using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public static void Main()
    {
        //create a Dictionary with string key and int value pairs
        Dictionary<string, int> cityPopulation = new Dictionary<string, int>();

        cityPopulation.Add("Tokyo", 3800000);
        cityPopulation.Add("Dehli", 25700000);
        cityPopulation.Add("Shanghai", 23700000);
        cityPopulation.Add("San Paulo", 21100000);
        cityPopulation.Add("Mexico City", 2100000);

        //read all the data
        Console.WriteLine("City Population");

        foreach(KeyValuePair<string, int> city in cityPopulation)
        {
            Console.WriteLine("City: " + city.Key + ", Population: ", city.Value);
        }
    }
}
