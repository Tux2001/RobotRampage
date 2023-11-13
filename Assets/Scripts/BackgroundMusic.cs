using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Start()
    {
        //Won't delete music when changing scenes
        DontDestroyOnLoad(gameObject);  
    }
}
