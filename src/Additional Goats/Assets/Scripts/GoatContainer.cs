using UnityEngine;
using System.Collections;

public class GoatContainer : MonoBehaviour {

	public GoatDnD currentGoat;

	public Main m;

	public Transform gridTrans;

	public UIGrid myGrid;

	public GameObject emptySprites;
	public GameObject fullSprites;

	public void Awake () {
		if (myGrid == null)
			myGrid = GetComponent<UIGrid>();
		if (gridTrans == null)
			gridTrans = myGrid.transform;
		if (m == null)
			m = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
	}

	public void Clear (bool delete) {
		m.totalGoats--;
		if (delete) {
			NGUITools.Destroy(currentGoat.gameObject);
		} else {
			currentGoat = null;
		}
		emptySprites.SetActive(true);
		fullSprites.SetActive(false);
	}

	public void Clear () {
		Clear(true);
	}

	public void Assign (GoatDnD goat) {
		if (Occupied())
			Clear();
		currentGoat = goat;
		currentGoat.stall = this;
		m.totalGoats++;
		//Debug.Log(goat.transform.localScale);
		goat.transform.parent = gridTrans;
		goat.transform.localScale = new Vector3(1f, 1f, 1f);
		myGrid.Reposition();
		emptySprites.SetActive(false);
		fullSprites.SetActive(true);
		//Debug.Log(goat.transform.localScale);
	}

	public bool Occupied () {
		return currentGoat != null;
	}
}
