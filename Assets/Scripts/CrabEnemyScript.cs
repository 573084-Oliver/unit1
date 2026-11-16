using System;
using Unity.Mathematics;
using UnityEngine;

public class CrabEnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    HelperScript helper;
    public GameObject player;
    public GameObject self;
    public GameObject exp;
    public GameObject heal;
    int dir = 1;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        helper = gameObject.AddComponent<HelperScript>();
        InvokeRepeating("Left", 0f, 2f);
        InvokeRepeating("Right", 1f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player Weapon")
        {
            helper.DestroyObject(true);
            GameObject clone;
            clone = Instantiate(exp, transform.position, transform.rotation);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y
            , transform.position.z + 1);
            int potion = UnityEngine.Random.Range(1,10);
            if (potion >= 4)
            {
                clone = Instantiate(heal, transform.position, transform.rotation);
                rb.transform.position = new Vector3(transform.position.x+0.5f, transform.position.y+0.5f
                , transform.position.z + 1);
            }
        }
    }
    void Right()
    {
        rb.linearVelocityX = dir;
    }

    void Left()
    {
        rb.linearVelocityX = -dir;
    }
}
