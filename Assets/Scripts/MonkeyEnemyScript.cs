using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MonkeyEnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    HelperScript helper;
    public GameObject player;
    public GameObject self;
    public GameObject exp;
    public GameObject nut;
    public GameObject heal;
    int dir = 1;
    int health = 3;
    bool bullet = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        helper = gameObject.AddComponent<HelperScript>();
        InvokeRepeating("Reload", 2, 2);
        InvokeRepeating("Up", 0, 3);
        InvokeRepeating("Down", 1.5f, 3);
    }

    // Update is called once per frame
    void Update()
    {
        Throw();
        Death();
    }

    void Throw()
    {
        if ((player.transform.position.y < self.transform.position.y + 0.2f) && (player.transform.position.y > self.transform.position.y - 0.2f) && (player.transform.position.x <= self.transform.position.x + 2) && (bullet == true))
        {
            GameObject clone;
            clone = Instantiate(nut, transform.position, transform.rotation);

            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(-5, 0);

            rb.transform.position = new Vector3(transform.position.x + -0.8f, transform.position.y 
            , transform.position.z + 1);
            bullet = false;
        }
        if ((player.transform.position.y < self.transform.position.y + 0.2f) && (player.transform.position.y > self.transform.position.y - 0.2f) && (player.transform.position.x >= self.transform.position.x - 2) && (bullet == true))
        {
            GameObject clone;
            clone = Instantiate(nut, transform.position, transform.rotation);

            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(5, 0);

            rb.transform.position = new Vector3(transform.position.x + 0.8f, transform.position.y
            , transform.position.z + 1);
            bullet = false;
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
            health = health - 1;
            helper.DestroyObject(true);
            GameObject clone;
            clone = Instantiate(exp, transform.position, transform.rotation);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y
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

    void Reload()
    {
        bullet = true;
    }

    void Up()
    {
        rb.linearVelocityY = dir;
    }

    void Down()
    {
        rb.linearVelocityY = -dir;
    }
}
