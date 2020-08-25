using System.Collections.Generic;
using System.Collections;
using OVRTouchSample;
using UnityEngine;
using VR = UnityEngine.VR;
using static UnityEngine.Input;


public class PlanetInteract : MonoBehaviour
{
    public PlanetMenu planetMenu;
    [SerializeField] Camera camera;
    public float speed = 50f;
    private RaycastHit hit;

    void Start()
    {
        
    }

    void Update()
    {
        OVRInput.Update();
        // If player clicks _____ button, then ______
        //if (Input.GetKey(KeyCode.Space) || OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) != 0f || OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) != 0f)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("user fired raycast");
            PlanetInfo planet = FireRaycastToFindPlanet();
            if (planet != null)
            {
                Debug.Log(planet.name);
                planetMenu.UpdatePlanetMenu(planet);
            }
            else
            {
                planetMenu.CloseUI();

            }
        }
        
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + -Camera.main.transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + -Camera.main.transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Camera.main.transform.right * speed * Time.deltaTime;
        }
    }

    private PlanetInfo FireRaycastToFindPlanet()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            PlanetInfo planet = hit.transform.gameObject.GetComponent<PlanetInfo>();
            if (planet != null)
            {
                return planet;
            }
        }
        return null;
    }
}