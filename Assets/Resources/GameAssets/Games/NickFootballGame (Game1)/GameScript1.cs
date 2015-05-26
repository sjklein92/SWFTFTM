using UnityEngine;
using System.Collections;

public class GameScript1 : GameScript {
	
	//public float pitchAdjust;
	//public float totalTime;
	//public int difficulty;
	
	//public string instruction;	
	//public bool isWin;

	public GameObject thrower, catcher, background, football, reticule, song;
	GameObject myThrower, myCatcher, myBackground, myFootball, myReticule, mySong;
	GameObject fakeCatcher;
	float waitTime, transTime, fakeWaitTime, fakeTransTime;
	Vector3 footballVel;
	Vector3 leftEnd, rightEnd;
	Animator throwAnim, catchAnim, fakeCatchAnim;
	
	public override void GameLoad(){
		instruction = "Pass!";
		isWin = false;
		rightEnd = new Vector3(4.630537f, -0.5819778f, 0);
		leftEnd = new Vector3(-4.630537f, -0.5819778f, 0);
	}
	
	public override void GameStart () {
		myThrower = (GameObject) Instantiate (thrower);
		myReticule = (GameObject) Instantiate(reticule);
		myBackground = (GameObject) Instantiate(background);
		mySong = (GameObject) Instantiate (song);
		mySong.GetComponent<AudioSource>().pitch = pitchAdjust;
		mySong.GetComponent<AudioSource>().Play ();

		if(difficulty == 2){
			myCatcher = (GameObject) Instantiate(catcher);
			myCatcher.transform.position = leftEnd;
			myCatcher.transform.localScale +=new Vector3(-2,0,0);

			waitTime = totalTime/2;
			transTime = totalTime/2;

			fakeCatcher = (GameObject) Instantiate (catcher);
			fakeWaitTime = 0;
			fakeTransTime = totalTime;
		}

		else{
			myCatcher = (GameObject) Instantiate (catcher);
			if(difficulty == 0){
				waitTime = 0;
				transTime = totalTime;
			}
			else{
				waitTime = totalTime/2;
				transTime = totalTime/2;
			}
		}

		throwAnim = myThrower.GetComponent<Animator>();
		catchAnim = myCatcher.GetComponent<Animator>();
		if(difficulty == 2)
			fakeCatchAnim = fakeCatcher.GetComponent<Animator>();
	}
	
	public override void GameUpdate () {
		if(!isWin){
			if(Input.GetKeyDown("space") && throwAnim.GetBool ("isThrow") == false){
				throwAnim.SetBool ("isThrow", true);
				myFootball = (GameObject) Instantiate(football);
				footballVel = (myReticule.transform.position - myFootball.transform.position);
				footballVel.Normalize ();
			}
			if(waitTime > 0)
				waitTime -= Time.deltaTime;
			else{
				if(difficulty != 2)
					myCatcher.transform.position = Vector3.Lerp (rightEnd, leftEnd, (transTime - totalTime)/transTime);
				else{
					myCatcher.transform.position = Vector3.Lerp (leftEnd, rightEnd, (transTime - totalTime)/transTime);
				}
			}
			if(difficulty == 2){
				if(totalTime <= 0.75f*fakeTransTime)
					fakeCatchAnim.SetBool ("isTrip", true);
				if(!fakeCatchAnim.GetBool ("isTrip"))
					fakeCatcher.transform.position = Vector3.Lerp (rightEnd, leftEnd, (fakeTransTime - totalTime)/fakeTransTime);
			}

			if (myFootball != null){
				myFootball.transform.position += footballVel/12f;
			}
			if(myCatcher.GetComponent<CatcherCollider>().isCollide){
				isWin = true;
				Destroy (myReticule);
				catchAnim.SetBool ("isCatch", true);
				throwAnim.SetBool ("isWin", true);
			}
		}
		totalTime -= Time.deltaTime;
		if(totalTime <= 0)
			Terminate ();
	}

	public override void Terminate(){
		Destroy(transform.root.gameObject);
		Destroy(myCatcher);
		Destroy(myBackground);
		Destroy(myFootball);
		Destroy(myReticule);
		Destroy(fakeCatcher);
		Destroy(myThrower);
		Destroy (mySong);
	}
}