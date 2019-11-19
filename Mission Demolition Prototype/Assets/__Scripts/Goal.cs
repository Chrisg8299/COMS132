using System.Collections;
using UnityEngine;

public class Goal : MonoBehaviour {
    public static bool goalMet = false;

    public GameObject explosionVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;

            if (explosionVFX)
            {
                GameObject vfx = Instantiate(explosionVFX);
                vfx.transform.position = other.gameObject.transform.position;
            }

            AudioSource afx = GetComponent<AudioSource>();
            if (afx)
                afx.Play();
        }
    }
}
