using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InputManager inputManager;

    void Awake()
    {
        if (instance != null){
            Destroy(this.gameObject);
        }
        else{
            instance = this;
        }

        inputManager = new InputManager();
    }
}
