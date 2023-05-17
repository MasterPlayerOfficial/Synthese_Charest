using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private void Awake()
    {
        int nbrBackgroundMusic = FindObjectsOfType<BackgroundMusic>().Length;
        if (nbrBackgroundMusic > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
