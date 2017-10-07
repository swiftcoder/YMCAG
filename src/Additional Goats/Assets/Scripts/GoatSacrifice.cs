using UnityEngine;
using System.Collections;

// Attach this script to the altar.  When a goat is dragged to the altar, this will call up the main turn execution script.

public class GoatSacrifice : MonoBehaviour {

	public Main m;

	public void Awake () {
		if (m == null)
			m = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
	}

	public void Sacrifice (GoatDnD sac) {
		m.Sacrifice(sac);
	}
}
