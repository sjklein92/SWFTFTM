using UnityEngine;
using System.Collections;

public abstract class GameScript : MonoBehaviour {

	public float pitchAdjust;
	public float totalTime;
	public int difficulty;

	public string instruction;	
	public bool isWin;

	public virtual void GameLoad(){
		instruction = "Stuff";
	}

	public virtual void GameStart () {
		
	}

	public virtual void GameUpdate () {
		totalTime -= Time.deltaTime;
		if(totalTime <= 0)
			Terminate ();
	}

	public virtual void GameFixedUpdate(){

	}

	public virtual void Terminate(){
		Destroy (transform.root.gameObject);
	}
}
