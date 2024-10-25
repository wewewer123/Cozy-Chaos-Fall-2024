using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Gold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.position += Vector3.left;
        }
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.position += Vector3.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Crop")
        {
            if(collision.GetComponent<CropData>().CheckCollectCrop())
            {
                collision.GetComponent<CropData>().ResetCrop();
                Gold++;
            }
        }
    }
}
