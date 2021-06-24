using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllySpaceObject : SpaceObject
{
    public static int BASE_HEALTH = 50;
    public static int BULLET_VELOCITY = 10;
    public static int BULLET_FIRE_RATE_PER_SECOND = 5;
    public static int BULLET_DAMAGE = 3;
    public static int MOVEMENT_VELOCITY = 1;
    public static int MOVEMENT_RANGE = 2;

    public int BulletVelocity = BULLET_VELOCITY;
    public int BulletFireRate = BULLET_FIRE_RATE_PER_SECOND;
    public int BulletDamage = BULLET_DAMAGE;

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

        MaxHealth = BASE_HEALTH;
        CurrentHealth = MaxHealth;
        GridPosition = new Vector2(0,0);
        MovementVelocity = MOVEMENT_VELOCITY;
        MovementRange = MOVEMENT_RANGE;

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
        float fireRate = 1/(float)BulletFireRate;
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            bulletPos = transform.position;
            bulletPos += new Vector2(+1f, -.43f);
            
            var bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletBehavior>().velX = BulletVelocity;
        }
    }


    void Oscillate()
    {
        Vector3 position = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z);

        float maxRangeUp = startPosition.y + MovementRange / (MovementRange * 2f) ;
        float maxRangeDown = startPosition.y - MovementRange/ (MovementRange * 2f) ;
     
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
}
