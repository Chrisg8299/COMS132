using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;
    void Start()
    {

    }
void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    
    void Update()
    
        {
            if (bndCheck.offUp)
            {
                Destroy(gameObject);
            }
        }
    }
}

