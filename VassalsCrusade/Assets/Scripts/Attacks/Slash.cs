using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public  float damage = 5f;

    Vector2 areaSize = new Vector2(1.9f, 0.497f);
    public LayerMask enemyLayer;

    //for fading effect
    SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeSlash());
        Attack();
    }

    void Attack()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.position, areaSize, 0, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            //In case enemyLAyer Object doesn't have script
            if (enemyController  != null)
                enemyController.TakeDamage(damage);
        }

        
    }

    IEnumerator FadeSlash()
    {
        yield return new WaitForSeconds(1);

        //gradual transparency
        /*

        float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 1.0f; t > 0.0f; t -= 0.001f)
        {
            alpha = t;
        yield return null;
        }
        */
        Destroy(gameObject);

    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, areaSize);
    }
}
