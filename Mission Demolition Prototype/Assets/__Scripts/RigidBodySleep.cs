using UnityEngine;

public class RigidbodySleep : MonoBehaviour {
	void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.Sleep();
        }
	}

}
