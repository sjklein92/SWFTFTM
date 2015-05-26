using UnityEngine;
using System.Collections;

public class CatcherCollider : MonoBehaviour {
	public bool isCollide = false;
	void OnTriggerEnter2D(Collider2D other){
		Destroy (other.gameObject);
		isCollide = true;
	}
}
