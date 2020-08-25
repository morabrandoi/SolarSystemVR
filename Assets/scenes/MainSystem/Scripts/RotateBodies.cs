using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBodies : MonoBehaviour
{
    public float rotationalPeriodInDays;
    public bool clockwise;
    private float rotatePerSecond;
    public float daysPerSecond;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;   
    }

    // Update is called once per frame
    void Update()
    {
        rotatePerSecond = 360f * (daysPerSecond) / (rotationalPeriodInDays);
        transform.Rotate(new Vector3(0, (clockwise ? 1 : -1) * rotatePerSecond*Time.deltaTime, 0));

    }
}
