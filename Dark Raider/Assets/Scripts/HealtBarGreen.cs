using UnityEngine;
using System.Collections;

public class HealtBarGreen : MonoBehaviour {


	private RectTransform theTransform = null;

	public float maxSpeed = 10.0f;


	void Awake(){
		theTransform = GetComponent<RectTransform> ();
	} 
	// Use this for initialization
	void Start () {
		if (PleyerController.player != null) {
			theTransform.sizeDelta = new Vector2(
				Mathf.Clamp(PleyerController.Health, 0, 100),
				theTransform.sizeDelta.y
				);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float healthUpdate = 0.0f;

		if (PleyerController.player != null) {
			healthUpdate = Mathf.MoveTowards (theTransform.rect.width, PleyerController.Health, maxSpeed);

			theTransform.sizeDelta = new Vector2 (
				Mathf.Clamp(PleyerController.Health, 0, 100),
				theTransform.sizeDelta.y
				);
		}
	}
}
