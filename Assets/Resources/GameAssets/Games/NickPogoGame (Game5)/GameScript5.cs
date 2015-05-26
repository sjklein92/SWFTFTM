using UnityEngine;
using System.Collections;

public class GameScript5 : GameScript {
	
	//Given by MasterScript
	//public float pitchAdjust;//Set the pitch of any sounds/music you play to this value
	//public float totalTime;//The amount of time your game will last in seconds. Terminate is called when this runs out. Use this to time any movement.
	//public int difficulty;//How hard your game should be. Will be either 0 (easy), 1 (medium), or 2 (hard).
	
	//Retrieved by MasterScript
	//public string instruction;//Needed by master script	
	//public bool isWin;//isWin checked by MasterScript at end of microgame
	
	//You don't need to instantiate any of the variables commented out above to access them.
	
	//Add any other variables you might need here
	public GameObject pogoGuy, coin, sky, ground1, ground2, ground3, walls;

	GameObject myPogoGuy, myCoin1, myCoin2, myCoin3, mySky, myGround, myWalls;
	
	// Use this for loading assets, but not instantiating them. Called from MasterScript
	public override void GameLoad(){
		instruction = "Get all coins!";//Instruction needs to be set here
		isWin = false;
	}
	
	// Use this for initialization, instantiating assets. Called from MasterScript
	public override void GameStart () {
		myPogoGuy = (GameObject) Instantiate (pogoGuy);
		mySky = (GameObject) Instantiate (sky);
		myWalls = (GameObject) Instantiate (walls);
		myCoin1 = (GameObject) Instantiate (coin);
		myCoin2 = (GameObject) Instantiate (coin);
		myCoin3 = (GameObject) Instantiate (coin);
		if(difficulty == 0){
			myGround = (GameObject) Instantiate (ground1);
			myCoin1.transform.position = new Vector3(-2, 0, 0);
			myCoin2.transform.position = new Vector3(0, 0, 0);
			myCoin3.transform.position = new Vector3(2, 0, 0);
		}
		else if(difficulty == 1){
			myGround = (GameObject) Instantiate (ground3);
			myCoin1.transform.position = new Vector3(-2, 0, 0);
			myCoin2.transform.position = new Vector3(0, 1, 0);
			myCoin3.transform.position = new Vector3(2, 2, 0);
		}
		else{
			myGround = (GameObject) Instantiate (ground2);
			myCoin1.transform.position = new Vector3(-2, 0, 0);
			myCoin2.transform.position = new Vector3(0, -1.5f, 0);
			myCoin3.transform.position = new Vector3(2, 0, 0);
		}

	}
	
	// GameUpdate is called once per frame from MasterScript
	public override void GameUpdate () {
		if(myCoin1 == null && myCoin2 == null && myCoin3 == null)
			isWin = true;
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
		Destroy (myPogoGuy);
		Destroy (mySky);
		Destroy (myGround);
		Destroy (myWalls);
		Destroy (myCoin1);
		Destroy (myCoin2);
		Destroy (myCoin3);
	}
}