using UnityEngine;
using System.Collections;

public class ScaleCollider : MonoBehaviour {

	public BoxCollider myCol;

	private int oldWidth;

	public void Update () {

		if (oldWidth != Screen.width) {

			//Debug.Log(Screen.width);
			oldWidth = Screen.width;
			float multiplier = Screen.width/8f;
			//Debug.Log(multiplier + ", " + Screen.width%16); // 220x100 = 100x scale.
			myCol.size = new Vector3(multiplier*2f, multiplier*3f, 1f);

		}
	}

}
