using UnityEngine;
using System.Collections;

public class GameScript3 : GameScript {
	
	//Given by MasterScript
	//public float pitchAdjust;//Set the pitch of any sounds/music you play to this value
	//public float totalTime;//The amount of time your game will last in seconds. Terminate is called when this runs out. Use this to time any movement.
	//public int difficulty;//How hard your game should be. Will be either 0 (easy), 1 (medium), or 2 (hard).
	
	//Local
	public GameObject shieldGuy, fireball, background, song;
	//public string instruction;//Needed by master script	
	//public bool isWin;//isWin checked by MasterScript at end of microgame
	
	//Add any other variables you might need here
	GameObject myShieldGuy, myBackground, mySong;
	public enum Direction {up, down, left, right};
	public Direction playerDirection;
	Direction[] fireballDirections;
	GameObject[] fireballs;
	float[] waitTime;
	float[] transTime;
	float transConstant;
	Vector3 upPos, downPos, leftPos, rightPos, playerPos;
	Animator anim;
	
	// Use this for loading assets, but not instantiating them. Called from MasterScript
	public override void GameLoad(){
		instruction = "Block!";//Instruction needs to be set here
		playerDirection = Direction.down;
		fireballDirections = new Direction[difficulty + 1];
		fireballs = new GameObject[difficulty + 1];
		waitTime = new float[difficulty + 1];
		transTime = new float[difficulty + 1];
		isWin = true;
		upPos = new Vector3(0, 4, 0);
		downPos = new Vector3(0, -4, 0);
		leftPos = new Vector3(-5, 0, 0);
		rightPos = new Vector3(5, 0, 0);
		playerPos = new Vector3(0, 0, 0);
		transConstant = totalTime/3;
	}
	
	// Use this for initialization, instantiating assets. Called from MasterScript
	public override void GameStart () {
		myShieldGuy = (GameObject) Instantiate (shieldGuy);
		myShieldGuy.transform.rotation = Quaternion.identity;
		anim = myShieldGuy.GetComponent<Animator>();
		myBackground = (GameObject) Instantiate (background);

		for (int i = 0; i < fireballDirections.Length; i++){
			int temp = Random.Range (0, 4);
			if(temp == 0){
				fireballDirections[i] = Direction.down;
				fireballs[i] = (GameObject)Instantiate (fireball, upPos, Quaternion.identity);
				fireballs[i].transform.Rotate (new Vector3(0, 0, 0));
			}
			else if (temp == 1){
				fireballDirections[i] = Direction.up;
				fireballs[i] = (GameObject)Instantiate (fireball, downPos, Quaternion.identity);
				fireballs[i].transform.Rotate (new Vector3(0, 0, 180));
			}
			else if(temp == 2){
				fireballDirections[i] = Direction.right;
				fireballs[i] = (GameObject)Instantiate (fireball, leftPos, Quaternion.identity);
				fireballs[i].transform.Rotate (new Vector3(0, 0, 90));
			}
			else{
				fireballDirections[i] = Direction.left;
				fireballs[i] = (GameObject)Instantiate (fireball, rightPos, Quaternion.identity);
				fireballs[i].transform.Rotate (new Vector3(0, 0, -90));
			}
			waitTime [i] = (i)*totalTime/4 + totalTime*(1.0f/8.0f);
			transTime[i] = totalTime/3;
		}

		mySong = (GameObject)Instantiate (song);
		mySong.GetComponent<AudioSource>().pitch= pitchAdjust;
		mySong.GetComponent<AudioSource>().Play ();
	}
	
	// GameUpdate is called once per frame from MasterScript
	public override void GameUpdate () {
		//Put game logic here
		if(isWin){
			if(Input.GetKeyDown("up")){
				playerDirection = Direction.up;
				myShieldGuy.transform.rotation = Quaternion.identity;
				myShieldGuy.transform.Rotate (new Vector3(0,0,180));
			}

			else if(Input.GetKeyDown("down")){
				playerDirection = Direction.down;
				myShieldGuy.transform.rotation = Quaternion.identity;
			}

			else if(Input.GetKeyDown("left")){
				playerDirection = Direction.left;
				myShieldGuy.transform.rotation = Quaternion.identity;
				myShieldGuy.transform.Rotate (new Vector3(0,0,-90));
			}

			else if(Input.GetKeyDown("right")){
				playerDirection = Direction.right;
				myShieldGuy.transform.rotation = Quaternion.identity;
				myShieldGuy.transform.Rotate (new Vector3(0,0,90));
			}

			for(int i = 0; i < fireballs.Length; i++){
				if (fireballs[i] != null){
					if(waitTime[i] > 0){
						waitTime[i] -= Time.deltaTime;
					}
					else{
						if(fireballDirections[i] == Direction.down)
							fireballs[i].transform.position = Vector3.Lerp (upPos, playerPos, (transConstant - transTime[i])/transConstant);
						else if(fireballDirections[i] == Direction.up)
							fireballs[i].transform.position = Vector3.Lerp (downPos, playerPos, (transConstant - transTime[i])/transConstant);
						else if(fireballDirections[i] == Direction.right)
							fireballs[i].transform.position = Vector3.Lerp (leftPos, playerPos, (transConstant - transTime[i])/transConstant);
						else
							fireballs[i].transform.position = Vector3.Lerp (rightPos, playerPos, (transConstant - transTime[i])/transConstant);
						if(transTime[i] > 0)
							transTime[i] -= Time.deltaTime;
					}

					if(fireballs[i].GetComponent<FireballCollider>().isCollide){
						if((fireballDirections[i] == Direction.down && playerDirection == Direction.up) ||
						   (fireballDirections[i] == Direction.up && playerDirection == Direction.down) ||
						   (fireballDirections[i] == Direction.left && playerDirection == Direction.right) ||
						   (fireballDirections[i] == Direction.right && playerDirection == Direction.left)){
							
						}
						else{
							isWin = false;
							anim.SetBool("isAlive", false);
						}
						Destroy (fireballs[i]);
					}
				}
				}
		}

		totalTime -= Time.deltaTime;//Runs down timer
		if(totalTime <= 0)
			Terminate ();//When timer runs out, calls terminate
	}
	
	//Use this to destroy any assets you've created
	public override void Terminate(){
		Destroy (transform.root.gameObject);//Destroys gameObject holding this script
		Destroy (myBackground);
		Destroy (myShieldGuy);
		Destroy (mySong);
		for(int i = 0; i < fireballs.Length; i++){
			Destroy(fireballs[i]);
		}
	}
}