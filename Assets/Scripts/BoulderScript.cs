using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    public GameObject player;
    public GameObject self;
    public GameObject door;
    public GameObject skeleton;
    Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && player.transform.position.y > self.transform.position.y)
        {
            rb.linearVelocityY = -1;
            
        }
        if (other.gameObject.tag == "Player" && player.transform.position.y < self.transform.position.y)
        {
            rb.linearVelocityY = 1;
            
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Correct")
        {
            Destroy(door);
            rb.linearVelocityY = 0;
        }

        if (other.gameObject.tag == "Incorrect")
        {
            rb.linearVelocityY = 0;
            GameObject clone;
            clone = Instantiate(skeleton, transform.position, transform.rotation);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z + 1);
        }
    }
}
