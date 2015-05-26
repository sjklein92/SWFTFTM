using UnityEngine;
using System.Collections;

namespace Assets.Resources.GameAssets.Games.TrampolineMan__Game6_.Scripts
{
    class Trampoline : MonoBehaviour
    {
        private Animator animator;
        private byte pause;
        private bool down;
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            animator.SetBool("IsTouching", false);
            down = false;
            pause = 0;
        }
        void Update()
        {
            if (pause == 6)
            {
                animator.SetBool("IsTouching", false);
                down = false;
                pause = 0;
            }
            else if (pause > 0)
                pause++;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (!down)
            {
                pause = 1;
                down = true;
                animator.SetBool("IsTouching", true);
            }
        }
    }
}
