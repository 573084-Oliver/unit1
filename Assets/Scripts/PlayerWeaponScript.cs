using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    public GameObject player;
    public GameObject self;
    HelperScript helper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        helper = gameObject.GetComponent<HelperScript>();
        Invoke("Destroy", 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void Destroy()
    {
      Destroy(self.gameObject);   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Wall")
        {
            Destroy(self.gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(self.gameObject);
        }
    }
}
