using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterFewSec : MonoBehaviour
{
    // Start is called before the first frame update

    private float interval = 5;
    public AudioClip explosionSound;
    //private AudioSource audioSource;
    void Start()
    {
        Destroy(gameObject, interval);
        //audioSource = GetComponent<AudioSource>();
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
