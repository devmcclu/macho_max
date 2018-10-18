using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMacho : MonoBehaviour {

    public int startingMacho = 0;
    public int currentMacho;
    public Slider machoSlider;


    void Awake()
    {
        currentMacho = startingMacho;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AddMacho (int amount)
    {
        currentMacho += amount;

        if (currentMacho > 100)
        {
            currentMacho = 100;
        }

        machoSlider.value = currentMacho;
    }
}
