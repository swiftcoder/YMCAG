using UnityEngine;
using System.Collections;

public class HumanResources : MonoBehaviour {

    public int villagers = 100; // start with 100 villagers
    
    public int sanity = 100; // start out entirely sane

    public UILabel villagerDisplay;
    public UILabel sanityDisplay;
    
    public void ChangeVillagers (int delta) {
    	villagers += delta;
    	if (villagers < 0)
    		villagers = 0;
    	villagerDisplay.text = "" + villagers;
    }

    public void ChangeSanity (int delta) {
    	sanity += delta;
    	if (sanity < 0)
    		sanity = 0;
    	sanityDisplay.text = "" + sanity;
    }

    public void onGodsAppeased() {
        Debug.Log("The gods appreciate your sacrifice");
        ChangeSanity(-10);
    }
    public void onGodsAngered() {
        Debug.Log("The gods spit upon your feeble offering");
        ChangeVillagers(-10);
    }
}
