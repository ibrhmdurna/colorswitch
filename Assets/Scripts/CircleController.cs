using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public void PlayStarAudio()
    {
        GetComponent<AudioSource>().Play();
    }
}
