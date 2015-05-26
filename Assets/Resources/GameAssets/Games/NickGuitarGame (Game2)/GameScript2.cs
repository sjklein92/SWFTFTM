using UnityEngine;
using System.Collections;

public class GameScript2 : GameScript {
	
	//public float pitchAdjust;
	//public float totalTime;
	//public int difficulty;
	
	//public string instruction;	
	//public bool isWin;

	public GameObject background, guitar, cheer;

	GameObject myBackground, myGuitar, myCheer;
	Animator anim;
	bool strumUp;
	int numStrums, curStrums;
	float pitch;

	public override void GameLoad(){
		instruction = "Rock out!";
		numStrums = 10 + 5*difficulty;
		curStrums = 0;
		strumUp = true;
	}
	
	public override void GameStart () {
		myBackground = (GameObject)Instantiate(background);
		anim = myBackground.GetComponent<Animator>();
		myGuitar = (GameObject)Instantiate (guitar);
		myCheer = (GameObject) Instantiate (cheer);
	}
	
	public override void GameUpdate () {

		if(Input.GetKeyDown("up") && strumUp){
			strumUp = ! strumUp;
			curStrums++;
			anim.SetBool ("strumUp", strumUp);
			myGuitar.GetComponent<AudioSource>().pitch = Random.Range (0.5f, 1.5f);
			myGuitar.GetComponent<AudioSource>().Play ();

		}
		
		else if (Input.GetKeyDown("down") && !strumUp){
			strumUp = ! strumUp;
			curStrums++;
			anim.SetBool ("strumUp", strumUp);
			myGuitar.GetComponent<AudioSource>().pitch = Random.Range (0.5f, 1.5f);
			myGuitar.GetComponent<AudioSource>().Play ();
		}
		
		if(curStrums >= numStrums && !isWin){
			isWin = true;
			anim.SetBool ("isWin", true);
			myCheer.GetComponent<AudioSource>().pitch = pitchAdjust;
			myCheer.GetComponent<AudioSource>().Play ();
		}

		totalTime -= Time.deltaTime;
		if(totalTime <= 0)
			Terminate ();
	}
	
	public override void Terminate(){
		Destroy (transform.root.gameObject);
		Destroy (myBackground);
		Destroy (myGuitar);
		Destroy (myCheer);
	}
}

