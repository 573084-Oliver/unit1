using UnityEngine;

public class ParrotScript : MonoBehaviour
{
    public GameObject self;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Talk();
    }

    void Talk()
    {
          if (player.transform.position.y < self.transform.position.y + 3 && player.transform.position.y > self.transform.position.y - 3 && player.transform.position.x < self.transform.position.x + 3 && player.transform.position.x > self.transform.position.x - 3)
          {

          }
    }
}
