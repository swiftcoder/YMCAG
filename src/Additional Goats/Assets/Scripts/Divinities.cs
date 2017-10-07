using UnityEngine;
using System.Collections;

public class Divinities : MonoBehaviour {

    public CreatureFactory creatureFactory;
    public HumanResources humanResources;

    public DemandIndicator[] deities;

    public void Start() {
        //InvokeRepeating("makeDemand", 5, 5);
    }
    
    public void makeDemand() {
        foreach (DemandIndicator current in deities)
            current.SetDemand(creatureFactory.getArbitraryDemand());
        
        Debug.Log("The gods demand " + deities[0].currentDemand.demand + " and " + deities[1].currentDemand.demand + "!!!");
    }
    
    public bool onSacrifice(Creature creature) {
        int satisfied = 0;
        foreach (DemandIndicator current in deities) {
            for (int i = 0; i < 3; ++i) {
                if (creature.parts[i].demand == current.currentDemand.demand) {
                    satisfied++;
                }
            }
        }
        
        if (satisfied >= 1) {
            humanResources.onGodsAppeased();
            return true;
        } else {
            humanResources.onGodsAngered();
            return false;
        }
    }
}
