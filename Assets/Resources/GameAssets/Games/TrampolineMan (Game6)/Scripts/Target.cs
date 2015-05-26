using UnityEngine;
using System.Collections;

namespace Assets.Resources.GameAssets.Games.TrampolineMan__Game6_.Scripts
{
    class Target : MonoBehaviour
    {
        private Animator animator;
        public bool isBroken;
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            animator.SetBool("isHit", false);
            isBroken = false;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            animator.SetBool("isHit", true);
            isBroken = true;
        }
    }
}
