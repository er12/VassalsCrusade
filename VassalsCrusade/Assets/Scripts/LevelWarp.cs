using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelWarp : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;

    public string levelToLoad;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Stop illusion
            collision.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<PlayerController>().enabled = false;

            LoadNextLevel();
        }
    }


    public void LoadNextLevel()
    {
        //SceneManager.LoadScene(levelToLoad);

        StartCoroutine(TransitionToLevel(levelToLoad));

    }

    IEnumerator TransitionToLevel(string levelToLoad)
    {
        // Play animation
        transition.SetTrigger("Start");
        
        // Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelToLoad);


    }

}
