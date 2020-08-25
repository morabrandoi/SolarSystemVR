using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    public string name;
    public List<string> facts;
    public double mass;
    public double diameter;
    public float surfaceGravity;
    public float lengthOfDay;
    public int meanSurfaceTemperature;
    public Material planetImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public string GetInfo()
    {
        return 
            "Mass: " + mass + " kg\n" +
            "Diameter: " + diameter + " km\n" +
            "Surface gravity: " + surfaceGravity + " m/s^2\n" +
            "Length of day: " + lengthOfDay + " days\n" +
            "Mean surface temperature: " + meanSurfaceTemperature + " °C";
    }
}
