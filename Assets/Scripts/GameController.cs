using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
   // [SerializeField] Image heart1;
   // [SerializeField] Image heart2;
   // [SerializeField] Image heart3;

    public void GameOver(){
        SceneManager.LoadScene("Defeat");
    }
    public void Victory(){
        SceneManager.LoadScene("Victory");
    }
    /*public void setHealth(float hp){
        if(hp == 3){
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if(hp == 2){
            heart3.SetActive(false);
        }
        else if(hp == 1){
            heart2.SetActive(false);
        }
        else{
            heart1.SetActive(false);
            GameOver();
        }
    } */
}
