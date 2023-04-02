using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameObject gmController;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        DontDestroyOnLoad(this.transform.root.gameObject);

        if(gmController == null)
        {
            gmController = this.transform.root.gameObject;
        }
        else
        {
            Destroy(this.transform.root.gameObject);
        }
    }

    
}
