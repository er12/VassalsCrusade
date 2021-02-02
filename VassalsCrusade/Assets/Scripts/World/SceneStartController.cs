using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartController : MonoBehaviour
{
    private PlayerController player;


    //Event when changing area
    public delegate void ChangeArea();
    public static event ChangeArea OnNewAreaStart;


    // Start is called before the first frame update
    void Start()
    {
        // Start new Area to set up camera
        OnNewAreaStart?.Invoke();

        player = FindObjectOfType<PlayerController>();
        player.transform.position = transform.position;

        //Stop illusion
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;



        //player.animator.SetFloat("Horizontal", transform.position.x);
        //player.animator.SetFloat("Vertical", transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
