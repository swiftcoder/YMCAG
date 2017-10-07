using UnityEngine;
using System.Collections;

// Script that does all of the turn execution.  Placeholder for testing UI scripts.

public class Main : MonoBehaviour {

	public GoatContainer[] stable;
	public int totalGoats = 0;
	public int maxGoats; // Never, ever, ever increase this over the stable size.
	public int testGoats;

	public GameObject goatFab;

	public CreatureFactory fac;
	public Divinities divinities;
	public HumanResources resources;

    public SoundEffects soundEffects;
    public Celestial celestial;
    
    public GameObject  inGameUI;
    public SpriteRenderer victoryScreen, defeatScreen, ggjSplash;
    
    public Transform gameboardMovement;
    
    bool inGGJSplashScreen = true, doneFading = false, inSplashScreen = false, doneSliding = false;
    float timeSinceStarted = 0.0f;
    Vector3 start, end;
    
    
	public void Start () {
		totalGoats = 0;
		for (int i = 0; i < testGoats; i++)
			AddGoat(NewGoat());

        start = gameboardMovement.localPosition;
        end = new Vector3(0,0,0);
    }
    
    public void FixedUpdate() {
        if (inGGJSplashScreen) {
            if (Input.anyKeyDown) {
                inGGJSplashScreen = false;
            } else {
                return;
            }
        }
        
        if (!inGGJSplashScreen && !doneFading) {
            timeSinceStarted += Time.deltaTime;
            ggjSplash.color = new Color(1, 1, 1, 1 - timeSinceStarted);
            
            if (timeSinceStarted >= 1.0f) {
                doneFading = true;
                inSplashScreen = true;
                ggjSplash.gameObject.SetActive(false);
                soundEffects.startMusic();
                timeSinceStarted = 0.0f;
            } else {
                return;
            }
        }
        
        if (inSplashScreen) {
            if (Input.anyKeyDown) {
                inSplashScreen = false;
            }
        }
        
        if (!inSplashScreen && !doneSliding) {
            timeSinceStarted += Time.deltaTime;
            gameboardMovement.localPosition = Vector3.Lerp(start, end, timeSinceStarted / 2.0f);
            
            if (timeSinceStarted >= 2.0f) {
                gameboardMovement.localPosition = end;
                doneSliding = true;
                beginInteraction();
            }
        }
    }


    private void beginInteraction() {
        inGameUI.SetActive(true);
        divinities.makeDemand();
    }
    
	public void Sacrifice (GoatDnD sac) {
		if (totalGoats <= 2) {
			Debug.Log("YOU MUST CONSTRUCT ADDITIONAL GOATS.");
			return;
		}
		//Debug.Log("Sacrificing " + sac);
		sac.gameObject.SetActive(false); // Prevents the tooltip from popping up after destruction.
		sac.stall.Clear();

		if (divinities.onSacrifice(sac.genes)) {
            soundEffects.playPleasedSound();
        } else {
            soundEffects.playAngeredSound();
        }
		Turn();
	}

	public void Cross (GoatDnD goatA, GoatDnD goatB) {
		if (totalGoats < maxGoats) {
			GoatDnD temp = GameObject.Instantiate(goatFab).GetComponent<GoatDnD>();
			temp.genes = fac.breedCreatures(goatA.genes, goatB.genes);
			//temp.SetPop(Random.Range(0, 5));
			//temp.SetSan(Random.Range(0, 5));
			temp.InitStats();
			for (int i = 0; i < 3; i++) {
				temp.sprites[i].sprite2D = temp.genes.parts[i].sprite;
			}
			AddGoat(temp);
            soundEffects.playCreatureSound();
   		} else {
			Debug.Log("SACRIFICE GOATS BEFORE CONSTRUCTING ADDITIONAL GOATS");
		}
	}

	public void AddGoat (GoatDnD goat) {
		for (int i = 0; i < maxGoats; i++) {
			if (!stable[i].Occupied()) {
				stable[i].Assign(goat);
				break;
			}
		}
	}

	public GoatDnD NewGoat () {
		
		GoatDnD temp = GameObject.Instantiate(goatFab).GetComponent<GoatDnD>();
		temp.genes = fac.createBaseCreature();
		//temp.SetPop(Random.Range(0, 5));
		//temp.SetSan(Random.Range(0, 5));
		temp.InitStats();
		for (int i = 0; i < 3; i++) {
			temp.sprites[i].sprite2D = temp.genes.parts[i].sprite;
		}
		return temp;
	}

	public void Turn () {
		int pop = 0;
		int san = 0;
		foreach (GoatContainer currentStall in stable) {
			if (currentStall.Occupied()) {
				pop -= currentStall.currentGoat.consumption;
				san -= currentStall.currentGoat.horror;
			}
		}
		if (pop != 0)
			resources.ChangeVillagers(pop);
		if (san != 0)
			resources.ChangeSanity(san);

		// Here is where a victory check is, which should pause the game and probably pop up a menu when triggered.
		if (resources.villagers > 0 && resources.sanity > 0) {
			// Game continuing case.
			divinities.makeDemand();
            celestial.itsANewDay();
		} else {
			// Game ending case.
			if (resources.sanity == 0) {
				Debug.Log("VICTORY - REVEL IN BLISSFUL INSANITY");
                victoryScreen.gameObject.SetActive(true);
                inGameUI.SetActive(false);
                soundEffects.playWinSound();
			} else {
				Debug.Log("GAME OVER - ALL HAVE PERISHED");
                celestial.nightTimeIsTheRightTIme();
                defeatScreen.gameObject.SetActive(true);
                inGameUI.SetActive(false);
                soundEffects.playLoseSound();
			}
			
		}
	}

}
