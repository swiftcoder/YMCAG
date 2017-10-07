using UnityEngine;
using System.Collections;

// Goat drag and drop.  

public class GoatDnD : UIDragDropItem {

	protected GoatDnD originalGoat; // Store the original goat, not just the drag and drop clone.
	public GameObject goatContainer; // Store your container, for reference.

	public Main m;

	public GoatContainer stall;

	public GameObject tooltip;

	public UILabel pop;
	public UILabel san;

	public int consumption;
	public int horror;

	public UI2DSprite[] sprites;

	public Creature genes;

	public void OnHover(bool selected) {
		tooltip.SetActive(selected);
	}

	public void InitStats () {
		consumption = 0;
		horror = 0;
		foreach (Part currentPart in genes.parts) {
			consumption += currentPart.consumption;
			horror += currentPart.madness;
		}
		pop.text = "" + consumption;
		san.text = "" + horror;
	}

	public void SetPop (int c) {
		consumption = c;
		pop.text = "" + c;
	}

	public void SetSan (int h) {
		horror = h;
		san.text = "" + h;
	}

	protected override void OnDragDropRelease (GameObject surface) {
		if (surface != null) {
			if (surface.tag != "Root") { // I feel bad that this is my solution, frankly.

				GoatSacrifice sac = surface.GetComponent<GoatSacrifice>();
				if (sac != null)
					sac.Sacrifice(originalGoat);

				GoatDnD mate = surface.GetComponentInChildren<GoatDnD>();

				if (mate != null && mate != originalGoat) {
					if (m == null)
						m = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
					m.Cross(originalGoat, mate);
				} else {
					GoatContainer newhouse = surface.GetComponent<GoatContainer>();
					if (newhouse != null) {
						// Move to new stall.
						stall.Clear(false);
						newhouse.Assign(originalGoat);

					}
				}
			}

		}
		OnDragDropEnd();
		NGUITools.Destroy(gameObject);
	}

	protected override void OnClone (GameObject original) {
		originalGoat = original.GetComponent<GoatDnD>();
		foreach (UI2DSprite current in sprites)
			current.depth += 5;
		tooltip.SetActive(false);
	}

}
