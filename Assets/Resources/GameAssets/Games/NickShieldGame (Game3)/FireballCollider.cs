using UnityEngine;
using System.Collections;

public class FireballCollider : MonoBehaviour {
	public bool isCollide = false;

	void OnTriggerEnter2D(Collider2D other){
		isCollide = true;
	}
}
