using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllySpaceObject : SpaceObject
{
    public int BulletVelocity;
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

    /*
     * For shooting
     */
    public GameObject Bullet;
    protected Vector2 bulletPos;
    protected float nextFire;
    protected float screenFactor;

    private GamestateManager _gamestateManager;

    void Awake()
    {
        _gamestateManager = FindObjectOfType<GamestateManager>();

        GridPosition = new Vector2(0, 0);

        OnCurrentHealthChange += (int oldValue, int current) =>
        {
            if (current <= 0)
            {
                Destroy(this.gameObject);
            }
        };
    }

    void Start()
    {
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
        Debug.Log("Number of SpaceShips " + numberOfSpaceShips + " Screen Height " + Screen.height + "size of ship " + sizeOfSpaceShip + "screenFactor " + screenFactor);
    }

    /*
    * executed at fixed time steps (0.02 default). Use this for Unity physics
    */
    void FixedUpdate()
    {
        if (_gamestateManager.State == GameState.Autoplay)
        {
            //change velocity to calculated moving vector
            rb.velocity = new Vector2(moveV.x, moveV.y) * (MovementVelocity * .25f);
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
        Vector3 position = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z);

        float maxRangeUp = startPosition.y +  MovementRange - screenFactor;
        float maxRangeDown = startPosition.y - MovementRange + screenFactor;

        float clampedMaxRangeUp = Mathf.Clamp(maxRangeUp, -4f, 4f);
        float clampedMaxRangeDown = Mathf.Clamp(maxRangeDown, -4f, 4f);
        bool bChanged = false;

        if (bFirstTimeOscillating)
        {
            position.y = clampedMaxRangeUp;
            bChanged = true;
            bFirstTimeOscillating = false;
        }
        else if (clampedMaxRangeUp == requestedPosition.y && transform.position.y >= requestedPosition.y)
        {
            position.y = clampedMaxRangeDown;
            bChanged = true;
        }
        else if (clampedMaxRangeDown == requestedPosition.y && transform.position.y <= requestedPosition.y)
        {
            position.y = clampedMaxRangeUp;
            bChanged = true;

        }
        if (bChanged)
        {
            moveV = position - transform.position;
            requestedPosition = position;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CurrentHealth " + CurrentHealth);
        if (collision.gameObject.name.Contains("Board"))
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        else if (collision.gameObject.name.Contains("asteroid"))
        {
            CurrentHealth--;
        }
        //TODO: Call api to get the collectable.
    }
}

