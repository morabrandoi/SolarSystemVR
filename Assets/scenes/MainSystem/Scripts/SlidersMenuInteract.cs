using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// binds variables related to the scaling of a planet for each planet
public class SlidersMenuInteract : MonoBehaviour
{
    public GameObject slidersMenu;
    public GameObject celestialBodies;
    private Dictionary<string, PlanetScaling> Scalings;

    readonly struct PlanetScaling
    {
        public string Name { get; }  // name of the planet
        public string ScalesDistanceTo { get; }  // name of planet planet scales relative to (e.g. moon->earth,  venus->sun)
        public float DistanceScale { get; }
        public float SizeScale { get; }

        public PlanetScaling(string name, string scalesDistanceTo, float distanceScale, float sizeScale)
        {
            this.Name = name;
            this.ScalesDistanceTo = scalesDistanceTo;
            this.DistanceScale = distanceScale;
            this.SizeScale = sizeScale;
        }

    }

    void Start()
    {
        // All the scaling information for the planets
        Scalings = new Dictionary<string, PlanetScaling>
        {
            {"The Sun", new PlanetScaling("The Sun", "The Sun", 0f, 285f) },
            {"Mercury", new PlanetScaling("Mercury", "The Sun", 1f, 1f) },
            {"Venus", new PlanetScaling("Venus", "The Sun", 1.87f, 2.48f) },
            {"Earth", new PlanetScaling("Earth", "The Sun", 2.58f, 2.61f) },
            {"Mars", new PlanetScaling("Mars", "The Sun", 3.94f, 1.39f) },
            {"Jupiter", new PlanetScaling("Jupiter", "The Sun", 13.4f, 28.66f) },
            {"Saturn", new PlanetScaling("Saturn", "The Sun", 24.76f, 23.87f) },
            {"Uranus", new PlanetScaling("Uranus", "The Sun", 49.58f, 10.40f) },
            {"Neptune", new PlanetScaling("Neptune", "The Sun", 77.63f, 10.09f) },
            {"The Moon", new PlanetScaling("The Moon", "Earth", 0.01f, 0.71f) }

        };

        // start everything at pretty scale size and distance

        HandleDistanceSlider();
        HandleSizeSlider();

    }

    // Update is called once per frame
    void Update()
    {
        // Toggle menu with M
        if (Input.GetKeyDown(KeyCode.M))
        {   
            // toggle menu's active status active = !active
            slidersMenu.SetActive(!slidersMenu.activeSelf);
        }
    }

    public void HandleTeleportButton(){
        // get dropdown
        TMPro.TMP_Dropdown teleportDropdown = slidersMenu.GetComponentInChildren<TMPro.TMP_Dropdown>();

        // Get name of planet selected in dropdown
        string nameOfPlanet = teleportDropdown.options[teleportDropdown.value].text;

        // get planet GameObject with the nameOfPlanet
        GameObject planet = celestialBodies.transform.Find(nameOfPlanet).gameObject;

        // teleport to said planet
        this.transform.position = planet.transform.position;
        print("equal positions? " + (this.transform.position == planet.transform.position).ToString());

        // rotate to face planet
        this.transform.LookAt(planet.transform, Vector3.up);
    }

    void ScaleDistanceFrom(Transform pinned, Transform moving, float factor)
    {
        // Scaling distance of moving object to the pinned one
        // newPos = pinned + (factor)(moving - pinned)
        Vector3 pPos = pinned.position;
        Vector3 mPos = moving.position;
        moving.position = pPos + (factor * (mPos - pPos).normalized);
    }

    public void HandleDistanceSlider()
    {
        // getting value of slider
        GameObject slider = slidersMenu.transform.Find("Main/DistanceSubMenu/Slider").gameObject;
        
        float sliderValue = slider.GetComponent<Slider>().value;
        
        foreach (PlanetScaling infoStruct in Scalings.Values) {
            // Get Planet
            GameObject planet = GameObject.Find(infoStruct.Name);

            // Get Planet pin
            GameObject pin = GameObject.Find(infoStruct.ScalesDistanceTo);

            // Scale
            ScaleDistanceFrom(pin.transform, planet.transform, sliderValue * infoStruct.DistanceScale);
        }

        // update text
        string pathToDistanceText = "Player/OVRCameraRig/TrackingSpace/" +
                                    "LeftHandAnchor/LeftControllerAnchor/" +
                                    "SlidersMenu/Main/DistanceSubMenu/ScaleText";
        TMPro.TextMeshProUGUI distanceText = GameObject.Find(pathToDistanceText).GetComponent<TMPro.TextMeshProUGUI>();
        distanceText.text = "Game at 1:" + ((int)(57910000f / sliderValue)).ToString() + " scale";
        // 57910000 is the distance of mercury to the sun in meters

    }

    public void HandleDistanceRealisticButton()
    {
        //foreach (PlanetScaling infoStruct in Scalings.Values)
        //{
        //    // Get Planet
        //    GameObject planet = GameObject.Find(infoStruct.Name);

        //    // Get Planet pin
        //    GameObject pin = GameObject.Find(infoStruct.ScalesDistanceTo);

        //    // Scale
        //    ScaleDistanceFrom(pin.transform, planet.transform, 57910000f * infoStruct.DistanceScale);
        //}

        // update text
        string pathToDistanceText = "Player/OVRCameraRig/TrackingSpace/" +
                                    "LeftHandAnchor/LeftControllerAnchor/" +
                                    "SlidersMenu/Main/DistanceSubMenu/ScaleText";
        TMPro.TextMeshProUGUI distanceText = GameObject.Find(pathToDistanceText).GetComponent<TMPro.TextMeshProUGUI>();
        distanceText.text = "Not possible to show. Space is really big";
    }

    public void HandleDistancePrettyButton()
    {
        int i = 0;
        foreach (PlanetScaling infoStruct in Scalings.Values)
        {
            
            if (infoStruct.Name == "The Moon")
            {
                continue;
            }

            // Get Planet
            GameObject planet = GameObject.Find(infoStruct.Name);

            // Get Planet pin
            GameObject pin = GameObject.Find(infoStruct.ScalesDistanceTo);
            ScaleDistanceFrom(pin.transform, planet.transform, i * 150);

            i += 1;
        }

        string pathToDistanceText = "Player/OVRCameraRig/TrackingSpace/" +
                                    "LeftHandAnchor/LeftControllerAnchor/" +
                                    "SlidersMenu/Main/DistanceSubMenu/ScaleText";
        TMPro.TextMeshProUGUI distanceText = GameObject.Find(pathToDistanceText).GetComponent<TMPro.TextMeshProUGUI>();
        distanceText.text = "Distances not to scale";
    }

    private void ScaleSize(Transform planet, float factor) {
        // Scale size by sliderValue and scalingFactor
        Vector3 scaleBefore = planet.transform.localScale;

        // Finding minimum component of vector and dividing it from the vector so that one of the components of the vector is 1
        // result = (size / MinComponentOf(size)) * (sliderValue * infoStruct.SizeScale)

        float minComponent = Mathf.Min(Mathf.Abs(scaleBefore[0]), Mathf.Abs(scaleBefore[1]), Mathf.Abs(scaleBefore[2]));
        Vector3 minimizedVector = scaleBefore * (1 / minComponent);
        planet.transform.localScale = minimizedVector * factor;
    }

    public void HandleSizeSlider()
    {
        GameObject slider = slidersMenu.transform.Find("Main/SizeSubMenu/Slider").gameObject;
        float sliderValue = slider.GetComponent<Slider>().value;
        foreach (PlanetScaling infoStruct in Scalings.Values)
        {
            // Get Planet
            GameObject planet = GameObject.Find(infoStruct.Name);
            // Scale
            ScaleSize(planet.transform, sliderValue * infoStruct.SizeScale);
        }

        // update text
        string pathToSizeText = "Player/OVRCameraRig/TrackingSpace/" +
                                    "LeftHandAnchor/LeftControllerAnchor/" +
                                    "SlidersMenu/Main/SizeSubMenu/ScaleText";
        TMPro.TextMeshProUGUI sizeText = GameObject.Find(pathToSizeText).GetComponent<TMPro.TextMeshProUGUI>();
        sizeText.text = "Game at 1:" + ((int)(4879400f / sliderValue)).ToString() + " scale";
    }

    public void HandleSizePrettyButton()
    {
        foreach (PlanetScaling infoStruct in Scalings.Values)
        {
            // Get Planet
            GameObject planet = GameObject.Find(infoStruct.Name);
            // Scale
            ScaleSize(planet.transform, ((128f + infoStruct.SizeScale) / 2f) );
        }

        string pathToSizeText = "Player/OVRCameraRig/TrackingSpace/" +
                                    "LeftHandAnchor/LeftControllerAnchor/" +
                                    "SlidersMenu/Main/SizeSubMenu/ScaleText";
        TMPro.TextMeshProUGUI sizeText = GameObject.Find(pathToSizeText).GetComponent<TMPro.TextMeshProUGUI>();
        sizeText.text = "Sizes not scaled";
    }

    public void HandleSizeRealisticButton()
    {
        // update text
        string pathToSizeText = "Player/OVRCameraRig/TrackingSpace/" +
                                    "LeftHandAnchor/LeftControllerAnchor/" +
                                    "SlidersMenu/Main/SizeSubMenu/ScaleText";
        TMPro.TextMeshProUGUI sizeText = GameObject.Find(pathToSizeText).GetComponent<TMPro.TextMeshProUGUI>();
        sizeText.text = "Not possible";
    }
}
