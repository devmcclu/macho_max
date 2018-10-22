using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMacho : MonoBehaviour {

    //Macho values
    public int startingMacho = 0;
    public int currentMacho;
    //Slider with macho value
    public Slider machoSlider;
    //Multiplier that changes values of health, damage, and speed
    public int machoMutli = 2;
    //Check for macho mode on
    public bool isMacho = false;
    //macho timer
    public float timer = 0f;
    public Color flashColour = new Color(0f, 1f, 0f, 0.1f);
    public float flashSpeed = 5f;
    public Image damageImage;

    //GameObject gunBarrelEnd;
    //PlayerShooting playerShooting;

    void Awake()
    {
        currentMacho = startingMacho;
        //gunBarrelEnd = GetComponent<GunBarrelEnd>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetButton("Action") && currentMacho == 100 && timer != 30.0f && isMacho == false)
        {
            CheckMacho();
        }

        if(isMacho == true)
        {
            timer += Time.deltaTime;
            if (currentMacho > 0)
            {
                currentMacho -= 1; //* Mathf.RoundToInt(Time.deltaTime);
                //print(Time.deltaTime);
            }
            damageImage.color = flashColour;
            machoSlider.value = currentMacho;

        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        //Turns timer off if Macho time is over (30 seconds)
        if (timer > 30.0f)
        {
            isMacho = false;
            timer = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        /*if (isMacho == true)
        {
            timer += Time.deltaTime;
            if (currentMacho > 0)
            {
                currentMacho -= 1;
            }
            //print(Time.deltaTime);
            machoSlider.value = currentMacho;
        }*/
    }

    public void AddMacho (int amount)
    {
        if (isMacho == false)
        {
            currentMacho += amount;
        }
        
        if (currentMacho > 100)
        {
            currentMacho = 100;
        }

        machoSlider.value = currentMacho;
    }
    //Changes macho to
    public void CheckMacho ()
    {
        isMacho = true;
    }
}
