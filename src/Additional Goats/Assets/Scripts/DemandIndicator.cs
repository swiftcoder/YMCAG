using UnityEngine;
using System.Collections;

public class DemandIndicator : MonoBehaviour {

	public UI2DSprite[] components;

	public CreatureFactory partList;

	public Part currentDemand;

	public void SetDemand (Part d) {
		currentDemand = d;
		// Reset
		components[0].sprite2D = partList.heads[0].sprite;
		components[1].sprite2D = partList.bodies[0].sprite;
		components[2].sprite2D = partList.tails[0].sprite;

		// Show current demand
		for (int i = 0; i < components.Length; i++) {
			if (i == currentDemand.type) {
				components[i].sprite2D = currentDemand.sprite;
				components[i].color = Color.white;
			} else {
				components[i].color = new Color(1f, 1f, 1f, 0.25f);
			}
		}
		
	}
}
