using UnityEngine;
using System.Collections;
namespace Assets.Resources.GameAssets.Games.TrampolineMan__Game6_.Scripts
{
    public class GameScript6: GameScript 
    {
	    //public float pitchAdjust;//Set the pitch of any sounds/music you play to this value
	    //public float totalTime;//The amount of time your game will last in seconds. Terminate is called when this runs out. Use this to time any movement.
	    //public int difficulty;//How hard your game should be. Will be either 0 (easy), 1 (medium), or 2 (hard).

        public GameObject TrampolineMan, Trampoline, Target, Bullet, Background, BulletIcon;
        private GameObject trampolineMan, trampoline, background;
        private GameObject[] targets, bulletIcons;
        private byte bulletCount;
	    public override void GameLoad()
        {
            instruction = "Shoot the targets!!!";
	    }
	
	    public override void GameStart() 
        {
            bulletCount = 0;
            background = (GameObject)Instantiate(Background);
            trampolineMan = (GameObject)Instantiate(TrampolineMan);
            trampolineMan.GetComponent<TrampolineMan>().totalTime = totalTime;
            trampolineMan.transform.position = new Vector3(-1.778f, 0.7007136f, 0f);
            trampoline = (GameObject)Instantiate(Trampoline);
            trampoline.transform.position = new Vector3(-1.817836f, -2.189563f, 0f);
            targets=new GameObject[difficulty+1];
            bulletIcons = new GameObject[difficulty + 1];
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i] = (GameObject)Instantiate(Target);
                targets[i].transform.position = new Vector3(2.93926f, 2.289769f-(1.67f*i), 1f);
                bulletIcons[i] = (GameObject)Instantiate(BulletIcon);
                bulletIcons[i].transform.position = new Vector3(1.5f+i, -2.3f, 0f);
            }
	    }
	
	    public override void GameUpdate() 
        {
            if (Input.GetKeyDown("space")&&bulletCount<=difficulty)
            {
                GameObject bullet=(GameObject) Instantiate(Bullet, trampolineMan.GetComponent<TrampolineMan>().gunPos.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().totalTime = totalTime;
                Destroy(bulletIcons[difficulty-bulletCount]);
                bulletCount++;
            }
            if (!isWin)
            {
                int count = 0;
                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i].GetComponent<Target>().isBroken)
                        count++;
                }
                if (count > difficulty)
                    isWin = true;
            }
		    totalTime -= Time.deltaTime;
		    if(totalTime <= 0)
			    Terminate ();
	    }

	    public override void Terminate()
        {
            Destroy(trampoline);
            Destroy(trampolineMan);
            Destroy(background);
            for (int i = 0; i < targets.Length; i++)
                Destroy(targets[i]);
            for(int i=difficulty-bulletCount;i>=0;i--)
                Destroy(bulletIcons[i]);
            Destroy(this.gameObject);
	    }
    }
}
