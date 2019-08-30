using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    /* Referencia al objeto de audio */
    AudioSource audioData;

    /* Se busca la referencia */
    private void Awake()
    {
        audioData = this.gameObject.GetComponent<AudioSource>();
    }

    /* Cuando el objeto al que está añadido el script se activa,
     * este reproduce el sonido */
    private void OnEnable()
    {

        audioData.Play(0);
    }
}

