using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPrefab : MonoBehaviour
{
    void Update()
    {

        Vector3 mousePos2D = Input.mousePosition;


        mousePos2D.z = -
        Camera.main.transform.position.z;

        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
    void OnCollisionEnter(Collision coll)
    {

        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);

        }


    }
}

