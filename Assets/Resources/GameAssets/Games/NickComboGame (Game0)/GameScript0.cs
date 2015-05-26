using UnityEngine;
using System.Collections;
/**
 * ComboGame GameScript
 **/
public class GameScript0 : GameScript {

	//Given by MasterScript
	//public float pitchAdjust, totalTime;
	//public int difficulty;

	//Local
	//public string instruction;//Needed by master script	
	public GameObject up, down, left, right, punch, initBackground, winBackground, gameSong, punchSound;
	int[] combo;
	GameObject[] sprites;
	GameObject curBackground, actualSong, actualPunchSound;
	int count;
	//public bool isWin;
	bool isLose;//isWin checked by MasterScript at end of microgame

	// Use this for loading assets, but not instantiating them

	public override void GameLoad(){
		instruction = "Combo!";
		isWin = false;
		isLose = false;
		if(difficulty == 0){
			combo = new int[4];
			sprites = new GameObject[4];
		}
		else if (difficulty == 1){
			combo = new int[5];
			sprites = new GameObject[5];
		}
		
		else{
			combo = new int[6];
			sprites = new GameObject[6];
		}

	}

	// Use this for initialization

	public override void GameStart () {
		actualSong = (GameObject)Instantiate (gameSong);
		actualSong.GetComponent<AudioSource>().pitch = pitchAdjust;
		actualPunchSound = (GameObject)Instantiate (punchSound);
		actualPunchSound.GetComponent<AudioSource>().pitch = pitchAdjust;
		actualSong.GetComponent<AudioSource>().Play ();
		for(int i = 0; i < combo.Length-1; i++){
			combo[i] = Random.Range (0, 4);
			if(combo[i] == 0)
				sprites[i] = (GameObject)Instantiate (up, new Vector3(i*1 - 2.5f, 2, 0), Quaternion.identity);
			else if (combo[i] == 1)
				sprites[i] = (GameObject)Instantiate (right, new Vector3(i*1 - 2.5f, 2, 0), Quaternion.identity);
			else if (combo[i] == 2)
				sprites[i] = (GameObject)Instantiate (down, new Vector3(i*1 - 2.5f, 2, 0), Quaternion.identity);
			else if (combo[i] == 3)
				sprites[i] = (GameObject)Instantiate (left, new Vector3(i*1 - 2.5f, 2, 0), Quaternion.identity);
		}

		combo[combo.Length-1] = 4;
		sprites[sprites.Length-1] = (GameObject)Instantiate (punch, new Vector3(sprites.Length - 3.5f, 2, 0), Quaternion.identity);

		count = 0;

		curBackground = (GameObject)Instantiate (initBackground);
	}
	
	// Update is called once per frame
	public override void GameUpdate () {
		if(!isWin && !isLose){
			if(Input.GetKeyDown("space")){
				if(combo[count] == 4){
					for(int i = 0; i < sprites.Length; i++)
						Destroy (sprites[i]);
					Destroy (curBackground);
					curBackground = (GameObject)Instantiate (winBackground);
					actualPunchSound.GetComponent<AudioSource>().Play ();
					isWin = true;
				}
				else{
					sprites[count].GetComponent <SpriteRenderer>().color = Color.red;
					isLose = true;
				}
			}	

			else if(Input.GetKeyDown("left")){
				if(combo[count] == 3){
					sprites[count].GetComponent <SpriteRenderer>().color = Color.green;
					count++;
				}
				else{
					sprites[count].GetComponent <SpriteRenderer>().color = Color.red;
					isLose = true;
				}
			}

			else if(Input.GetKeyDown("right")){
				if(combo[count] == 1){
					sprites[count].GetComponent <SpriteRenderer>().color = Color.green;
					count++;
				}
				else{
					sprites[count].GetComponent <SpriteRenderer>().color = Color.red;
					isLose = true;
				}
			}

			else if(Input.GetKeyDown("up")){
				if(combo[count] == 0){
					sprites[count].GetComponent <SpriteRenderer>().color = Color.green;
					count++;
				}
				else{
					sprites[count].GetComponent <SpriteRenderer>().color = Color.red;
					isLose = true;
				}
			}

			else if(Input.GetKeyDown("down")){
				if(combo[count] == 2){
					sprites[count].GetComponent <SpriteRenderer>().color = Color.green;
					count++;
				}
				else{
					sprites[count].GetComponent <SpriteRenderer>().color = Color.red;
					isLose = true;
				}
			}
		}
		totalTime -= Time.deltaTime;
		if(totalTime <= 0)
			Terminate ();
	}

	public override void Terminate(){
		for(int i = 0; i < sprites.Length; i++)
			Destroy (sprites[i]);
		Destroy (curBackground);
		Destroy (actualSong);
		Destroy (actualPunchSound);
		Destroy (transform.root.gameObject);
	}
}
