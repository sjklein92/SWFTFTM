using UnityEngine;
using System.Collections;

public class CoinCollider : MonoBehaviour {

	void OnTriggerEnter2D(){
		Destroy (transform.root.gameObject);//Destroys gameObject holding this script
	}
}
