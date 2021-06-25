using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles {
    public class asteroidLauncher : MonoBehaviour {
        public GameObject asteroidPrefab;
        public GameObject crystalPrefab;
        public bool didGameEnd = false;
        public float spawnTime = 0.8f;
        public float scaleMultiplier = 1;
        public int loop = 0;
        public float AsteroidSpeedMultiplier = 2f;
        public float CrystalSpeedMultiplier = 1f;
        private int counter;
        private int crystalCounter = 0;
        private Vector2 screenBounds;

        private List<GameObject> spawnAsteroids;
        private float spawnLocationY;

        // Start is called before the first frame update
        void Start() {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            Random.InitState(1111);
            spawnAsteroids = new List<GameObject>();
            counter = loop;
            StartCoroutine(spawner());
        }

        private void spawn() {
            crystalCounter++;
            if (crystalCounter % 10 != 0) spawnAsteroid();
            else spawnCrystal();
        }

        private void spawnAsteroid(){
            if(counter > 1) {
                counter -= 1;
            } else if (counter > 0) {
                Random.InitState(1111);
                counter = loop;
            } else if ( loop != -1 && counter == -1 ) {
                counter = loop;
            };
            GameObject a = Instantiate(asteroidPrefab) as GameObject;
            var asteroids = a.GetComponent<Asteroids>();
            asteroids.speedMultiplier = AsteroidSpeedMultiplier;
            do
            {
                spawnLocationY = Random.Range(screenBounds.y, -screenBounds.y);
            } while(reRoll(spawnLocationY));
            a.transform.position = new Vector2(screenBounds.x * 2, spawnLocationY);
            float randomScale = Random.Range(50, 200) * scaleMultiplier;
            a.transform.localScale = new Vector2(randomScale/100, randomScale/100);
            spawnAsteroids.Add(a);
        }

        private void spawnCrystal(){
            Random.InitState((int) System.DateTime.Now.Ticks);
            GameObject c = Instantiate(crystalPrefab) as GameObject;
            var crystal = c.GetComponent<Crystal>();
            crystal.speed = CrystalSpeedMultiplier;
            spawnLocationY = Random.Range(screenBounds.y, -screenBounds.y);
            c.transform.position = new Vector2(screenBounds.x * 2, spawnLocationY);
            c.transform.localScale = new Vector2(scaleMultiplier, scaleMultiplier);
        }

        private bool reRoll(float current_y) {
            foreach (GameObject asteroid in spawnAsteroids)
            {
                if( asteroid != null 
                    && asteroid.transform.position.x >= (screenBounds.x * 5 / 3) 
                    && Mathf.Abs(asteroid.transform.position.y - current_y) <= screenBounds.y/4) return true;
            }
            return false;
        }

        IEnumerator spawner() {
            while(!didGameEnd) {
                yield return new WaitForSeconds(spawnTime);
                spawn();
            }
        } 
    }
}
