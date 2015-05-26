using UnityEngine;
using System.Collections;

namespace Assets.Resources.GameAssets.Games.TrampolineMan__Game6_.Scripts
{
    public class TrampolineMan : MonoBehaviour
    {
        public float totalTime;
        public Transform gunPos;
        private Animator animator;
        private bool isFalling;
        private byte pause;
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            animator.SetBool("onTrampoline", false);
            isFalling = false;
            pause = 0;
        }
        void Update()
        {
            //+3 to move y pos to a 0-6 scale, 6-(half size of sprite)
            if (transform.position.y+3 > (6-(gameObject.renderer.bounds.size.y/2)))
                isFalling = true;
            if (pause == 5)
            {
                animator.SetBool("onTrampoline", false);
                pause = 0;
            }
            else if (pause > 0)
                pause++;
            else
            {
                if (isFalling)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                        gameObject.transform.position.y - (((totalTime / 3) - gameObject.transform.position.y + 3) * Time.deltaTime)
                        , gameObject.transform.position.z);
                }
                else
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                        gameObject.transform.position.y + (((totalTime / 3) - gameObject.transform.position.y + 3) * Time.deltaTime)
                        , gameObject.transform.position.z);
                }
            }

        }
        void OnCollisionEnter2D(Collision2D other)
        {
            if (isFalling)
            {
                pause = 1;
                isFalling = false;
                animator.SetBool("onTrampoline", true);
            }
        }
    }
}
