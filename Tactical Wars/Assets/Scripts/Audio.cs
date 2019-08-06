using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource audioData;
    private void OnEnable()
    {
        audioData = this.gameObject.GetComponent<AudioSource>();
        audioData.Play(0);
    }
}
