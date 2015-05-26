using UnityEngine;
using System.Collections;

namespace Assets.Resources.GameAssets.Games.TrampolineMan__Game6_.Scripts
{
    class Bullet : MonoBehaviour
    {
        public float totalTime;
        void Start()
        {

        }
        void Update()
        {
            if (gameObject.transform.position.x > 5||totalTime<=0)
                Destroy(this.gameObject);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x+(6f)*Time.deltaTime,
                        gameObject.transform.position.y, gameObject.transform.position.z);
            totalTime -= Time.deltaTime;
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(this.gameObject);
        }
    }
}
