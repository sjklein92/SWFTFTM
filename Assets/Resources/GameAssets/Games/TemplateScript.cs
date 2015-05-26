using UnityEngine;
using System.Collections;

public class TemplateScript : GameScript {

	//Given by MasterScript
	//public float pitchAdjust;//Set the pitch of any sounds/music you play to this value
	//public float totalTime;//The amount of time your game will last in seconds. Terminate is called when this runs out. Use this to time any movement.
	//public int difficulty;//How hard your game should be. Will be either 0 (easy), 1 (medium), or 2 (hard).
	
	//Retrieved by MasterScript
	//public string instruction;//Needed by master script	
	//public bool isWin;//isWin checked by MasterScript at end of microgame

	//You don't need to instantiate any of the variables commented out above to access them.

	//Add any other variables you might need here
	
	// Use this for loading assets, but not instantiating them. Called from MasterScript
	public override void GameLoad(){
		instruction = "Stuff";//Instruction needs to be set here
	}
	
	// Use this for initialization, instantiating assets. Called from MasterScript
	public override void GameStart () {

	}
	
	// GameUpdate is called once per frame from MasterScript
	public override void GameUpdate () {

		//Put game logic here

		totalTime -= Time.deltaTime;//Runs down timer
		if(totalTime <= 0)
			Terminate ();//When timer runs out, calls terminate
	}

	//GameFixedUpdate is called every time a physics step happens from GameScript
	public virtual void GameFixedUpdate(){
		
	}

	//Use this to destroy any assets you've created, or return any changed global settings to their defaults
	public override void Terminate(){
		Destroy (transform.root.gameObject);//Destroys gameObject holding this script
	}
}
