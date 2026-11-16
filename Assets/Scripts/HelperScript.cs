using UnityEngine;

public class HelperScript : MonoBehaviour
{
    public void FlipSpriteX(bool flipX)
    {
        // Get the SpriteRenderer component
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        

        if (flipX == true)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
    public void FlipSpriteY(bool flipY)
    {
        // Get the SpriteRenderer component
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Transform tr = gameObject.GetComponent<Transform>();
        if (flipY == true)
        {
            sr.flipY = true;
            tr.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            sr.flipY = false;
            tr.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public void RotateLeft(bool rotateL)
    {
      Transform tr = gameObject.GetComponent<Transform>();
      SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (rotateL == true)
        {
            sr.flipY = false;
            tr.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }
    public void RotateRight(bool rotateR)
    {
      Transform tr = gameObject.GetComponent<Transform>();
      SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (rotateR == true)
        {
            sr.flipY = false;
            tr.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
    }
    public void DestroyObject(bool destroy)
    {
        if (destroy == true)
        {
            Destroy(gameObject);
        }
    }
}