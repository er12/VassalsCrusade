using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnemy : MonoBehaviour
{
    public GameObject PlantFog;
    bool hasPlantFog;
    public float plantFogRate = 20f;
    private float nextPlantFog = 20f;

    GameObject plantGFX;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        hasPlantFog = false;
        plantGFX = transform.Find("FlowerGFX").gameObject;
        animator = plantGFX.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO:
        // If alert start... make random start with fog,

        // Check every 20 seconds if has plant fog to spawn
        if (Time.time > nextPlantFog)
        {
            nextPlantFog = Time.time + plantFogRate;

            //If no plant fog and is alert -> spawn
            if (!hasPlantFog && animator.GetCurrentAnimatorStateInfo(0).IsName("Alert"))
            {
                SpawnFog();
                hasPlantFog = true;
                // TODO: send attack to spawn at event
                animator.SetTrigger("Attack");
            }
        }
    }

    void SpawnFog()
    {
        GameObject fog = Instantiate(PlantFog, transform.position, Quaternion.identity);
        fog.transform.SetParent(transform);

    }

}
