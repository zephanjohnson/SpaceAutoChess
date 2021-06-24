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

    private GamestateManager _gamestateManager;

    void Awake()
    {
        _gamestateManager = FindObjectOfType<GamestateManager>();
     
        GridPosition = new Vector2(0,0);

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
        nextFire= 1.0f / (float)BulletFireRate;
        Debug.Log("Bullet Fire Rate:" +BulletFireRate);
        Debug.Log("Initial fire rate: " + nextFire);
    }

    /*
    * executed at fixed time steps (0.02 default). Use this for Unity physics
    */
    void FixedUpdate()
    {
        if (_gamestateManager.State == GameState.Autoplay) {
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
        CurrentHealth -= 5;
    }


    /*
    * Shoots bullets at given rate and frequency
    */
    void Shoot()
    {
        nextFire -= Time.deltaTime;
        
        if (nextFire <= 0)
        {
            Debug.Log("Fired bullet");
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

        
        float maxRangeUp = startPosition.y + (1- 1/MovementRange); /// (MovementRange * 2f) ;
        float maxRangeDown = startPosition.y - (1-1/MovementRange); /// (MovementRange * 2f) ;
     
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
        if(collision.gameObject.name.Contains("Board"))
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Board"))
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}

