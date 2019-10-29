using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= 5f)
        {
            GetComponent<Animator>().SetTrigger("attack");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.001f);
        }
    }
}
