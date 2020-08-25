using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolveBodies : MonoBehaviour
{
    public GameObject centerOfOrbit;
    public float daysPerSecond;
    public float periodTimeInDays;
    private float degreesPerSecond;  
    private Vector3 axisOfRotation;
    private float orbitDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        axisOfRotation = Vector3.up;
        orbitDistance = (centerOfOrbit.transform.position - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 centerOrbitPos = centerOfOrbit.transform.position;
        // Before that should be same after
        Quaternion rotationBefore = transform.rotation;
        
        degreesPerSecond = 360f * (daysPerSecond) / (periodTimeInDays);
        transform.RotateAround(centerOrbitPos, axisOfRotation, (-degreesPerSecond * Time.deltaTime));

        if (centerOfOrbit.name != "The Sun")
        {
            transform.position = centerOrbitPos + (transform.position - centerOrbitPos).normalized * orbitDistance;
        }
        transform.rotation = rotationBefore;
    }
}
