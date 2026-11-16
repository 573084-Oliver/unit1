using UnityEngine;

public class SkeletonEnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    HelperScript helper;
    public GameObject player;
    public GameObject self;
    public GameObject exp;
    public GameObject heal;
    int dir = 1;
    int health = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        helper = gameObject.AddComponent<HelperScript>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Death();
    }

    void Move()
    {

        if (player.transform.position.y < self.transform.position.y + 3 && player.transform.position.y > self.transform.position.y - 3 && player.transform.position.x < self.transform.position.x + 3 && player.transform.position.x > self.transform.position.x - 3)
        {
           if (player.transform.position.y < self.transform.position.y)
            {
                rb.linearVelocityY = -dir;
            }
           if (player.transform.position.y > self.transform.position.y)
            {
                rb.linearVelocityY = dir;
            }
           if (player.transform.position.x < self.transform.position.x)
            {
                rb.linearVelocityX = -dir;
            }
           if (player.transform.position.x > self.transform.position.x)
            {
                rb.linearVelocityX = dir;
            }
        }
        else
        {
            rb.linearVelocityY = 0;
            rb.linearVelocityX = 0;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player Weapon")
        {
            health = health - 1;
        }
    }

    void Death()
    {
       if (health == 0)
        {
            helper.DestroyObject(true);
            GameObject clone;
            clone = Instantiate(exp, transform.position, transform.rotation);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y + 1f
            , transform.position.z + 1);
            clone = Instantiate(exp, transform.position, transform.rotation);
            rb.transform.position = new Vector3(transform.position.x + 1f, transform.position.y
            , transform.position.z + 1);
            clone = Instantiate(exp, transform.position, transform.rotation);
            rb.transform.position = new Vector3(transform.position.x  - 1f, transform.position.y
            , transform.position.z + 1);
            int potion = UnityEngine.Random.Range(1, 10);
            if (potion >= 4)
            {
                clone = Instantiate(heal, transform.position, transform.rotation);
                rb.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f
                , transform.position.z + 1);
            }
        }
    }
}
