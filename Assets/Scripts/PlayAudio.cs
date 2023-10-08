using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip soundClip;
    public GameObject soundLocation;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(playSoundClip);
    }

    public void playSoundClip()
    {
        AudioSource.PlayClipAtPoint(soundClip, soundLocation.transform.position, 0.4f);
    }
}
