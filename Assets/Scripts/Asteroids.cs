using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour {
    public float speedMultiplier = 10.0f;
    private float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    
    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        speed = Random.Range(speedMultiplier, speedMultiplier * 3);
        rb.velocity = new Vector2(-speed, 0);
    }

    // Update is called once per frame
    void Update() {
        transform.RotateAround(transform.position, Vector3.forward, Random.Range(50, 150) * Time.deltaTime);
        if(transform.position.x < - screenBounds.x * 1.2) {
            Destroy(this.gameObject);
        }
    }
}
