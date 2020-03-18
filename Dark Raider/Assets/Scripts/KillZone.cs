using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	public float damage = 100.0f;

	void OnTiggerStay2D(Collider2D otherCollider){
	
		if (!otherCollider.CompareTag ("Player"))
			return;


		if (PleyerController.player != null) {
		
			PleyerController.Health -= damage * Time.deltaTime;
		}
	}
}
