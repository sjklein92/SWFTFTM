using UnityEngine;
using System.Collections;

public class MasterScript : MonoBehaviour {

	enum GameState {Menu, SinglePlayer};
	GameState gameState;
	enum SinglePlayerState {Init, Game, Win, Lose, Trans, Faster, Harder, Boss, GameOver};
	SinglePlayerState singlePlayerState;

	float curUpdateTime, timer, textFlashTimer, pitchAdjust;
	int lives, level, cycleCounter, difficulty, curLevel;
	bool gameWin, textVisible, isTest;
	public GameObject menuHolder, singlePlayerHolder;
	public ArrayList gamesHolder;
	public AudioSource InitSound, WinSound, LoseSound, TransSound, FasterSound;
	GameObject curHolder, gameHolder;
	string instruction;

	// Use this for initialization
	void Start () {
		gameState = GameState.Menu;
		menuInit();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameState == GameState.Menu)
			menuUpdate();
		else if(gameState == GameState.SinglePlayer)
			singlePlayerUpdate();
	}

	void OnGUI(){
		GUIStyle style = new GUIStyle();
		if(gameState == GameState.Menu){
			style.fontSize = 50* Screen.width/800;
			GetComponent<OutlineText>().DrawOutline(new Rect (Screen.width*0.2f, Screen.height*0.8f, 0, 0),
			                                        "Press Space to begin", style, Color.black, Color.white);
		}
		else if(gameState == GameState.SinglePlayer){
			style.fontSize = 20* Screen.width/800;
			if(singlePlayerState != SinglePlayerState.Game)
			GetComponent<OutlineText>().DrawOutline(new Rect (5, 300, 0, 0),
			                                        "Lives: " + lives, style, Color.black, Color.white);
			/*else
				GetComponent<OutlineText>().DrawOutline(new Rect (5, 300, 0, 0),
				                                        "Time: " + timer, style, Color.black, Color.white);*/
			if((singlePlayerState == SinglePlayerState.Trans && timer <= curUpdateTime/8f) ||
			   (singlePlayerState == SinglePlayerState.Game && timer >= curUpdateTime*(7/8f))){
					style.fontSize = 100* Screen.width/800;
					style.alignment = TextAnchor.UpperCenter;
					GetComponent<OutlineText>().DrawOutline(new Rect (Screen.width*0.5f, Screen.height*0.5f, 0, 0),
					                                        instruction, style, Color.black, Color.white);
				style.alignment = TextAnchor.UpperLeft;

			}
			if(singlePlayerState != SinglePlayerState.Game){
				style.fontSize = 180* Screen.width/800;
				GetComponent<OutlineText>().DrawOutline(new Rect (Screen.width*0.3f, Screen.height*0.1f, 0, 0),
				                                        level.ToString ("000"), style, Color.black, Color.white);
			}

			if(singlePlayerState == SinglePlayerState.Faster && textVisible){
				style.fontSize = 120* Screen.width/800;
				GetComponent<OutlineText>().DrawOutline(new Rect (Screen.width*0.25f, Screen.height*0.6f, 0, 0),
				                                        "Faster!", style, Color.black, Color.white);
			}

			else if(singlePlayerState == SinglePlayerState.Harder && textVisible){
				style.fontSize = 120* Screen.width/800;
				GetComponent<OutlineText>().DrawOutline(new Rect (Screen.width*0.25f, Screen.height*0.6f, 0, 0),
				                                        "Harder!", style, Color.black, Color.white);
			}
		}
	}

	void menuInit(){
		curHolder = (GameObject)Instantiate(menuHolder, new Vector3(0, 0, 0), Quaternion.identity);
		isTest = false;
	}

	void menuUpdate(){
		if(Input.GetKeyDown("space")){
			gameState = GameState.SinglePlayer;
			Destroy(curHolder);
			singlePlayerInit();
		}

		else if(Input.GetKeyDown ("t")){
			isTest = true;
			gameState = GameState.SinglePlayer;
			Destroy(curHolder);
			singlePlayerInit();
		}
	}

	void singlePlayerInit(){
		curHolder = (GameObject)Instantiate(singlePlayerHolder, new Vector3(0, 0, 0), Quaternion.identity);
		curUpdateTime = 3.47826086952f;
		textVisible = true;
		timer = curUpdateTime;
		singlePlayerState = SinglePlayerState.Init;
		lives = 4;
		difficulty = 0;
		cycleCounter = 1;
		Time.timeScale = 1;
		pitchAdjust = 1;
		curLevel = 0;
		gameWin = true;
		InitSound.Play();
	}

	void FixedUpdate(){
		if(singlePlayerState == SinglePlayerState.Game){
			gameHolder.GetComponent<GameScript>().GameFixedUpdate();
		}
	}

	void singlePlayerUpdate(){
		//Debug.Log (curLevel);
		if(singlePlayerState == SinglePlayerState.Game){
			gameHolder.GetComponent<GameScript>().GameUpdate();
		}
		else if(singlePlayerState == SinglePlayerState.Faster){
			textFlashTimer -= Time.deltaTime;
			if(textFlashTimer <= 0){
				textFlashTimer = (curUpdateTime/8);
				textVisible = !textVisible;
			}
		}

		else if(singlePlayerState == SinglePlayerState.Harder){
			textFlashTimer -= Time.deltaTime;
			if(textFlashTimer <= 0){
				textFlashTimer = (curUpdateTime/8);
				textVisible = !textVisible;
			}
		}
		
		timer -= Time.deltaTime;

		if(timer <= 0){
			updateSinglePlayerState();//Changes states and resets timer
		}
		//Debug.Log ("TimeScale: " + Time.timeScale + " Pitch :" + pitchAdjust);
	}

	void updateSinglePlayerState(){
		if(singlePlayerState == SinglePlayerState.Init){
			singlePlayerState = SinglePlayerState.Trans;
			TransSound.Play();
			timer = (curUpdateTime/2.0f);
			level++;
			InitGame();
		}
		else if (singlePlayerState == SinglePlayerState.Trans){
			singlePlayerState = SinglePlayerState.Game;
			timer = curUpdateTime;
			gameHolder.GetComponent<GameScript>().GameStart();
		}
		else if (singlePlayerState == SinglePlayerState.Game){
			gameWin = gameHolder.GetComponent<GameScript>().isWin;
			gameHolder.GetComponent<GameScript>().Terminate ();
			if(gameWin == true){
				singlePlayerState = SinglePlayerState.Win;
				WinSound.Play();
			}
			else{
				singlePlayerState = SinglePlayerState.Lose;
				lives--;
				LoseSound.Play ();
			}

			gameHolder = null;
			timer = (curUpdateTime/2.0f);
			gameWin = true;
			cycleCounter++;
		}
		else if(singlePlayerState == SinglePlayerState.Win || (singlePlayerState == SinglePlayerState.Lose && lives > 0)){
			curLevel = (curLevel+1)%6;
			if(((isTest && (cycleCounter == 4))|| (cycleCounter == 7)) && difficulty < 2){
				difficulty++;
				Time.timeScale /= 1.21f;
				pitchAdjust /= 1.21f;
				textFlashTimer = (curUpdateTime/8);
				textVisible = true;
				singlePlayerState = SinglePlayerState.Harder;
				timer = curUpdateTime;
				AdjustSound();
				FasterSound.Play ();
				cycleCounter = 1;
			}

			else if((isTest && (cycleCounter >= 2)) || (cycleCounter == 3 || cycleCounter == 5 || cycleCounter == 10)){
				Time.timeScale *= 1.1f;
				pitchAdjust *= 1.1f;
				textFlashTimer = (curUpdateTime/8);
				textVisible = true;
				singlePlayerState = SinglePlayerState.Faster;
				AdjustSound();
				timer = curUpdateTime;
				FasterSound.Play ();
				if(cycleCounter == 10 || isTest && cycleCounter == 4)
					cycleCounter = 1;
			}

			else{
				singlePlayerState = SinglePlayerState.Trans;
				level++;
				timer = (curUpdateTime/2.0f);
				TransSound.Play ();
				InitGame();
			}
		}
		else if(singlePlayerState == SinglePlayerState.Lose){
			if(lives <= 0){
				singlePlayerState = SinglePlayerState.GameOver;
			}
				
			else{
					singlePlayerState = SinglePlayerState.Trans;
					level++;
					timer = (curUpdateTime/2.0f);
					TransSound.Play ();
			}
		}
		else if(singlePlayerState == SinglePlayerState.Faster){
			singlePlayerState = SinglePlayerState.Trans;
			level++;
			timer = (curUpdateTime/2.0f);
			TransSound.Play ();
			InitGame();
		}

		else if(singlePlayerState == SinglePlayerState.Harder){
			singlePlayerState = SinglePlayerState.Trans;
			level++;
			timer = (curUpdateTime/2.0f);
			TransSound.Play ();
			InitGame();
		}
	}

	void InitGame(){
		if(isTest)
			gameHolder = (GameObject)Instantiate(Resources.Load("GameAssets/Games/GamePrefabs/TestGame") as GameObject, new Vector3(0, 0, 0), Quaternion.identity);
		else
			gameHolder = (GameObject)Instantiate(Resources.Load("GameAssets/Games/GamePrefabs/Game" + curLevel) as GameObject, new Vector3(0, 0, 0), Quaternion.identity);
		gameHolder.GetComponent<GameScript>().difficulty = difficulty;
		gameHolder.GetComponent<GameScript>().totalTime = curUpdateTime;
		gameHolder.GetComponent<GameScript>().pitchAdjust = pitchAdjust;
		gameHolder.GetComponent<GameScript>().GameLoad();
		instruction = gameHolder.GetComponent<GameScript>().instruction;
	}

	void AdjustSound(){
		WinSound.pitch = pitchAdjust;
		LoseSound.pitch = pitchAdjust;
		TransSound.pitch = pitchAdjust;
		FasterSound.pitch = pitchAdjust;
	}
}