using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource audioData;
    private void Awake()
    {
        audioData = this.gameObject.GetComponent<AudioSource>();
    }
    private void OnEnable()
    {

        audioData.Play(0);
    }
}

