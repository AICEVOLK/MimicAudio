using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEclip : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public bool on_SE;
    // public bool on_color;
    // Start is called before the first frame update
    void Start() {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SE() {
        if (on_SE == false) {
            on_SE = true;
            audioSource.PlayOneShot(clip);
        } else {
            on_SE = false;
        }
    }

    // public void Color() {
    //     if (on_color == false) {
    //         on_color = true;
    //     } else {
    //         on_color = false;
    //     }
    // }
}
