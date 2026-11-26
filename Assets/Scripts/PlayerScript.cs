using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    InputAction moveAction;
    InputAction attackAction;
    InputAction healAction;
    Rigidbody2D rb;
    HelperScript helper;
    public GameObject weapon;
    public GameObject player;
    public GameObject bomb;
    public GameObject door;
    public GameObject dark;
    bool flip;
    bool key;
    bool axe;
    bool raft;
    bool stick;
    bool torch;
    int rotate;
    int health;
    int maxHealth;
    int potion;
    int exp;
    int expReq;
    int direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        healAction = InputSystem.actions.FindAction("Heal");
        rb = GetComponent<Rigidbody2D>();
        helper = gameObject.AddComponent<HelperScript>();
        maxHealth = 3;
        health = maxHealth;
        potion = 3;
        exp = 0;
        expReq = 3;
        direction = 1; 
        key = false;
        axe = false;
        raft = false;
        stick = false;
        torch = false;
        
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
    }

    void Move()
    {
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveVel.x * 3, moveVel.y * 3);
    }

    void Attack()
    {
        if (attackAction.WasPressedThisFrame() && (flip==false) && (rotate == 0) && (rb.linearVelocityX==0) && (rb.linearVelocityY==0))
        {
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(0, 2.5f);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y 
            , transform.position.z + 1);
           

        }
        if (attackAction.WasPressedThisFrame() && (flip==true) && (rotate == 0) && (rb.linearVelocityX==0) && (rb.linearVelocityY==0))
        {
           
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(0, -2.5f);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y 
            , transform.position.z + 1);
        }
        if (attackAction.WasPressedThisFrame() && (flip==false) && (rotate == 1) && (rb.linearVelocityX==0) && (rb.linearVelocityY==0))
        {
           
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(-2.5f,0);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y 
            , transform.position.z + 1);
        }
        if (attackAction.WasPressedThisFrame() && (flip==false) && (rotate == -1) && (rb.linearVelocityX==0) && (rb.linearVelocityY==0))
        {
           
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(2.5f,0);
            rb.transform.position = new Vector3(transform.position.x,  transform.position.y
            , transform.position.z + 1);
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
            //

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
        if (other.gameObject.tag == "Tree" && axe==true)
        {
           Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Water" && raft==true)
        {
           Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Fire" && stick==true)
        {
           torch = true;
           stick = false;
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
            exp = exp + 1;
        }
        if (other.gameObject.tag == "Key")
        {
            key = true;
        }
        if (other.gameObject.tag == "Raft")
        {
            raft = true;
        }
        if (other.gameObject.tag == "Axe")
        {
            axe = true;
        }
        if (other.gameObject.tag == "Stick")
        {
            stick = true;
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
         if (other.gameObject.tag == "Dark" && torch==true)
        {
           Destroy(other.gameObject);
        }
    }
    void LevelUp()
    {
        if (exp == expReq)
        {
            print("level up");
            maxHealth = maxHealth + 1;
            health = maxHealth;
            exp = 0;
            expReq = expReq + 1;
        }
    }
    

   
}
