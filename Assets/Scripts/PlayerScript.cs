using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    InputAction moveAction;
    InputAction attackAction;
    InputAction healAction;
    InputAction throwAction;
    Rigidbody2D rb;
    HelperScript helper;
    public GameObject weapon;
    public GameObject player;
    public GameObject bomb;
    public GameObject door;
    bool flip;
    bool key;
    int rotate;
    int health;
    int maxHealth;
    int potion;
    int exp;
    int direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        healAction = InputSystem.actions.FindAction("Heal");
        throwAction = InputSystem.actions.FindAction("Throw");
        rb = GetComponent<Rigidbody2D>();
        helper = gameObject.AddComponent<HelperScript>();
        maxHealth = 3;
        health = maxHealth;
        potion = 3;
        exp = 0;
        key = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        FlipSpriteY();
        Death();
        Heal();
        LevelUp();
        Throw();
    }

    void Move()
    {
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveVel.x * 3, moveVel.y * 3);
    }

    void Attack()
    {
        if (attackAction.WasPressedThisFrame() && (flip==false) && (rotate == 0))
        {
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(0, 5);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y 
            , transform.position.z + 1);
        }
        if (attackAction.WasPressedThisFrame() && (flip==true) && (rotate == 0))
        {
           
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(0, -5);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y 
            , transform.position.z + 1);
        }
        if (attackAction.WasPressedThisFrame() && (flip==false) && (rotate == 1))
        {
           
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(-5,0);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y 
            , transform.position.z + 1);
        }
        if (attackAction.WasPressedThisFrame() && (flip==false) && (rotate == -1))
        {
           
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(5,0);
            rb.transform.position = new Vector3(transform.position.x,  transform.position.y
            , transform.position.z + 1);
        }
    }

    void Throw()
    {
        if (throwAction.WasPressedThisFrame() && (flip == false) && (rotate == 0) && (key==true))
        {
            
            GameObject clone;
            clone = Instantiate(bomb, transform.position, transform.rotation);

            rb.transform.position = new Vector3(transform.position.x, transform.position.y +
            -2.5f, transform.position.z + 1);
            key = false;
        }
        if (throwAction.WasPressedThisFrame() && (flip == true) && (rotate == 0) && (key==true))
        {
            
            GameObject clone;
            clone = Instantiate(bomb, transform.position, transform.rotation);

            rb.transform.position = new Vector3(transform.position.x, transform.position.y +
            2.5f, transform.position.z + 1);
            key = false;
        }
        if (throwAction.WasPressedThisFrame() && (flip == false) && (rotate == 1) && (key==true))
        {
           
            GameObject clone;
            clone = Instantiate(bomb, transform.position, transform.rotation);

            rb.transform.position = new Vector3(transform.position.x + 2.5f, transform.position.y
            , transform.position.z + 1);
            key = false;
        }
        if (throwAction.WasPressedThisFrame() && (flip == false) && (rotate == -1) && (key==true))
        {
           
            GameObject clone;
            clone = Instantiate(bomb, transform.position, transform.rotation);

            rb.transform.position = new Vector3(transform.position.x + -2.5f, transform.position.y
            , transform.position.z + 1);
            key = false;
        }
    }

    void FlipSpriteY()
    {
        if (rb.linearVelocityY > 0.1)
        {
            helper.FlipSpriteY(false);
            flip = false;
            rotate = 0;
            direction = 1;
        }
        if (rb.linearVelocityY < -0.1)
        {
            helper.FlipSpriteY(true);
            flip = true;
            rotate = 0;
            direction = 2;
        }
        if (rb.linearVelocityX > 0.1)
        {
            helper.RotateRight(true);
            flip = false;
            rotate = -1;
            direction = 3;
        }
        if (rb.linearVelocityX < -0.1)
        {
            helper.RotateLeft(true);
            flip = false;
            rotate = 1;
            direction = 4;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MEnemy")
        {
            health = health-1;
            print(health + "/" + maxHealth);
        }
        if (other.gameObject.tag == "CEnemy")
        {
            health = health-2;
            print(health + "/" + maxHealth);
        }
        if (other.gameObject.tag == "SEnemy")
        {
            health = health-3;
            print(health + "/" + maxHealth);
        }
        if (other.gameObject.tag == "Door" && key==true)
        {
           Destroy(other.gameObject);
        }
    }

    void Death()
    {
        if (health <= 0)
        {
            helper.DestroyObject(true);
        }
        
    }
    void Heal()
    {
        if (healAction.WasPressedThisFrame() && (potion > 0))
        {
            health = health + 3;
            potion = potion - 1;
            
        }

        if (health > maxHealth)
        {
            health = maxHealth;
            print(health + "/" + maxHealth);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Exp")
        {
            exp = exp + 5;
        }
        if (other.gameObject.tag == "Key")
        {
            key = true;
        }
        if (other.gameObject.tag == "Potion")
        {
            potion = potion + 1;
            if (potion > 3)
            {
                potion = 3;
            }
        }
        if (other.gameObject.tag == "EnemyWeapon")
        {
            health = health - 1;
            print(health + "/" + maxHealth);
        }
    }
    void LevelUp()
    {
        if (exp == 5)
        {
            print("level up");
            maxHealth = maxHealth + 1;
            health = maxHealth;
            exp = 0;
        }
    }
}
