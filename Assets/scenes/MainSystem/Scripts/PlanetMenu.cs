using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public Text name = null;
    public Text fact = null;
    public Text factNum = null;
    public Text info = null;
    public Image planetImage = null;
    public PlanetInfo currentPlanet = null;
    public GameObject menu;
    public GameObject reticle;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePlanetMenu(PlanetInfo planet)
    {
        if (planet == currentPlanet && menu.activeSelf == true)
        {
            CloseUI();
            return;
        }

        this.menu.SetActive(false);
        this.name.text = planet.name.ToString();

        float numberOfFacts = (float)planet.facts.Count;
        int randomNum = (int)Random.Range(0f, numberOfFacts);
        this.factNum.text = "Did you know? #" +(randomNum+1).ToString();
        this.fact.text = planet.facts.ToArray()[randomNum] + "\n\nSource: NASA";
        this.info.text = planet.GetInfo();
        this.planetImage.material = planet.planetImage;
        this.menu.SetActive(true);
        currentPlanet = planet;
        reticle.SetActive(false);
    }

    public void CloseUI()
    {
        menu.SetActive(false);
        reticle.SetActive(true);
    }
}
