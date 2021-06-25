using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {
    public float speed = 20.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private bool outOfBounds = false;

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(-speed, 0);
    }

    // Update is called once per frame
    void Update() {
        if(transform.position.x < - screenBounds.x * 1.2) {
            outOfBounds = true;
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (outOfBounds) { return; }

        var gameBoardManager = FindObjectOfType<GameBoardManager>();
        var allyData = Collectible.GetRandomAllyData();
        gameBoardManager.AddToInventory(allyData.Data);
    }
}
