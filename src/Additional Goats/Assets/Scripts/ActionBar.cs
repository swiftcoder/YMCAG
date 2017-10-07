/*
using UnityEngine;
using System.Collections;

public class ActionBar : MonoBehaviour {

    public CreatureFactory creatureFactory;
    
    public readonly int actionCount = 5;

    public GameObject creaturePrefab;
    
    public Creature[] actions;
    
	// Use this for initialization
	void Start () {
        actions = new Creature[actionCount];
        
       for (int i = 0; i < actionCount; ++i) {
            Creature action = creatureFactory.createBaseCreature();
            actions[i] = action;
            
            GameObject creature = (GameObject)Instantiate(creaturePrefab, Vector3.zero, Quaternion.identity);
            creature.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = action.sprites[0];
            creature.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = action.sprites[1];
            creature.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = action.sprites[2];
            creature.transform.parent = transform;
            creature.transform.localPosition = new Vector2(-((actionCount/2)*2) + i*2, 0);
       }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
*/