using UnityEngine;

public class DoorScript : MonoBehaviour
{
    HelperScript helper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        helper = gameObject.AddComponent<HelperScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Key")
        {
            helper.DestroyObject(true);
        }
    }
}
