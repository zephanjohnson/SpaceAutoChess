using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllySpaceObject : SpaceObject
{
    public float BulletVelocity;
    public int BulletFireRate;
    public int BulletDamage;

    public void SetData(AllyData allyData)
    {
        MaxHealth = allyData.BaseHealth;
        CurrentHealth = MaxHealth;
        MovementVelocity = allyData.MovementVelocity;
        MovementRange = allyData.MovementRange;
        BulletVelocity = allyData.BulletVelocity;
        BulletFireRate = allyData.BulletFireRatePerSecond;
        BulletDamage = allyData.BulletDamage;
    }

    /*
     * Variables needed for Oscillations.
     */
    protected Vector3 startPosition;
    protected Rigidbody2D rb;
    protected bool bFirstTimeOscillating = true;
    protected Vector3 requestedPosition;
    protected Vector3 moveV;
    private bool dirRight = true;


    /*
     * For shooting
     */
    public GameObject Bullet;
    protected Vector2 bulletPos;
    protected float nextFire;
    protected float screenFactor;

    public UnityAction<AllySpaceObject> OnAllySpaceObjectDestroyed;

    private GamestateManager _gamestateManager;
    public HealthBar HealthBar;

    void Awake()
    {
        _gamestateManager = FindObjectOfType<GamestateManager>();
        HealthBar = GetComponentInChildren<HealthBar>();

        GridPosition = new Vector2(0, 0);
        OnCurrentHealthChange += UpdateHealthBar;
    }

    void Start()
    {
        dirRight = Random.Range(0,2) == 0;
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.transform.position;
        rb.isKinematic = true;
        nextFire = 1.0f / (float)BulletFireRate;

        //Number of ship rows
        int numberOfSpaceShips = 6; //Hardcoded for now
        float sizeOfSpaceShip = (float)(GetComponent<BoxCollider2D>().bounds.size.y);

        //Units per pixel
        Vector3 p1 = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 p2 = Camera.main.ScreenToWorldPoint(Vector3.up);
        float unitsPerPixel = Vector3.Distance(p1, p2);
        //Total Units
        //Somehow below fuzzy logic works 
        float totalUnits = Screen.height * unitsPerPixel;

        //Hardcode ship size
        sizeOfSpaceShip = .8f;

        screenFactor = totalUnits / (sizeOfSpaceShip * numberOfSpaceShips);
        //Debug.Log("Number of SpaceShips " + numberOfSpaceShips + " Screen Height " + Screen.height + "size of ship " + sizeOfSpaceShip + "screenFactor " + screenFactor);
    }

    /*
    * executed at fixed time steps (0.02 default). Use this for Unity physics
    */
    void FixedUpdate()
    {
        if (_gamestateManager.State == GameState.Autoplay)
        {
            //change velocity to calculated moving vector
            //rb.velocity = new Vector2(moveV.x, moveV.y) * (MovementVelocity * .25f);
        }
    }

    /*
     * Method executed every frame
    */
    void Update()
    {
        if (_gamestateManager.State == GameState.Autoplay)
        {

            Oscillate();
            Shoot();
        }
    }

    private void OnMouseDown()
    {
        //_gamestateManager.SelectCollectible(Collectible);
    }


    /*
    * Shoots bullets at given rate and frequency
    */
    void Shoot()
    {
        nextFire -= Time.deltaTime;

        if (nextFire <= 0)
        {
            nextFire = 1.0f / (float)BulletFireRate;
            bulletPos = transform.position;
            bulletPos += new Vector2(+1f, -.43f);

            var bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletBehavior>().speed = BulletVelocity;
        }
    }


    void Oscillate()
    {
        if (dirRight)
            transform.Translate(Vector3.down * MovementVelocity * Time.deltaTime);
        else
            transform.Translate(Vector3.up * MovementVelocity * Time.deltaTime);

        if (transform.position.y >= startPosition.y + MovementRange / 2)
        {
            dirRight = true;
        }

        if (transform.position.y <= startPosition.y - MovementRange / 2)
        {
            dirRight = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Board"))
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        else if (collision.gameObject.name.Contains("asteroid"))
        {
            CurrentHealth--;
        }
        //TODO: Call api to get the collectable.
    }

    private void UpdateHealthBar(int oldHealth, int currentHealth)
    {
        Debug.Log("CurrentHealth " + currentHealth);
        float healthRatio = (float) currentHealth / (float) MaxHealth;
        HealthBar.SetSize(healthRatio);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        OnAllySpaceObjectDestroyed?.Invoke(this);
    }
}

