using UnityEngine;
using System.Collections;

public class Celestial : MonoBehaviour {

    public GameObject sun, moon, skyDay, townNight;
    public float dayLength = 3;
    
     float dayCounter = 1, dayAlpha = 1, dayMax = 1; 

    public Color nightTint;

    public SpriteRenderer[] tintSubjects;
        
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
     	dayCounter += Time.deltaTime / dayLength;
        
        if (dayCounter >= dayMax) {
            dayCounter = dayMax;
        }
        
     	dayAlpha = Mathf.Cos(dayCounter * Mathf.PI * 2) * 0.6f + 0.6f;
      	skyDay.GetComponent<SpriteRenderer>().color = new Color(1,1,1,dayAlpha);
      	townNight.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1-dayAlpha);

        Color currentColor = Color.Lerp(nightTint, Color.white, dayAlpha);
      	for (int i = 0; i < tintSubjects.Length; i++)
       		tintSubjects[i].color = currentColor;

        if (dayCounter >= dayMax) {
            return;
        }
        
	  	sun.transform.RotateAround(transform.position, Vector3.forward,  Time.deltaTime * 365f / dayLength);
	  	moon.transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * 365f / dayLength);
    }
    
    public void itsANewDay() {
        dayCounter = 0;
        dayMax = 1;
    }
    
    public void nightTimeIsTheRightTIme() {
        dayCounter = 0;
        dayMax = 0.5f;
    }
}
