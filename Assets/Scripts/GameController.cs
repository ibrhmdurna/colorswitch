using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController GameStart;

    private void Awake()
    {
        if (!GameStart)
        {
            GameStart = this;
            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(GetComponent<AudioSource>());
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
