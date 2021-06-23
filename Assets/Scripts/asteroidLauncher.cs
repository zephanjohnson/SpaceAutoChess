using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles {
    public class asteroidLauncher : MonoBehaviour {
        public GameObject asteroidPrefab;
        public bool didGameEnd = false;
        public float spawnTime = 1f;
        private Vector2 screenBounds;

        // Start is called before the first frame update
        void Start() {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            StartCoroutine(spawner());
        }

        private void spawn() {
            GameObject a = Instantiate(asteroidPrefab) as GameObject;
            a.transform.position = new Vector2(screenBounds.x * 2, Random.Range(screenBounds.y, -screenBounds.y));
            float randomScale = Random.Range(50, 200);
            a.transform.localScale = new Vector2(randomScale/100, randomScale/100);
        }

        IEnumerator spawner() {
            while(!didGameEnd) {
                yield return new WaitForSeconds(spawnTime);
                spawn();
            }
        } 
    }
}
