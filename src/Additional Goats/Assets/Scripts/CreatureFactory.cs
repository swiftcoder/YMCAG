using UnityEngine;

[System.Serializable]
public class Part {
    public Sprite sprite;
    public string demand; // what godly demand the part will satisfy // Going to have to change this to an int index, since I'm aiming for showing demand rather than telling.
    public int consumption; // How much population this part eats each turn
    public int madness; // how much sanity reduction the part causes
    public int type; // head, body, or tail.
    
    public Part() {}
}

[System.Serializable]
public class Creature {
    public Part[] parts;
    
    public Creature() {
        parts = new Part[3];        
    }
}

public class CreatureFactory : MonoBehaviour {

    public readonly int headIndex = 0, bodyIndex = 1, tailIndex = 2;

    public Part[] heads;
    public Part[] bodies;
    public Part[] tails;
    
    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    
    }
    
    public Creature createBaseCreature() {
        
        Creature creature = new Creature();
        creature.parts[0] = heads[0];
        creature.parts[1] = bodies[0];
        creature.parts[2] = tails[0];
        
        return creature;
    }
    
    public Creature breedCreatures(Creature lhs, Creature rhs) {
        Creature child = new Creature();
        
        // Randomly mix the parent DNA
        for (int i = 0; i < 3; ++i) {
            child.parts[i] = Random.value < 0.5f ? lhs.parts[i] : rhs.parts[i];
        }
        
        // Add a recessive gene
        int which = Random.Range(0, 3);
        child.parts[which] = getRandomPart(which);
        
        return child;
    }
    
    private Part getRandomPart(int type) {
        Part[][] parts = new Part[3][] {
            heads, bodies, tails
        };
        return parts[type][Random.Range(1, parts[type].Length)];
    }
    
    public Part getArbitraryDemand() {
        return getRandomPart(Random.Range(0, 3));
    }
}