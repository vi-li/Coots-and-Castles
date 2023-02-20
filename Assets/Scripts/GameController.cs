using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverScreen GameOverObject;
    public void GameOver(){
        GameOverObject.Setup();
    }
    
}
