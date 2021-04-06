using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Slash : MonoBehaviour
{
    public float damage = 5f;

    //for fading effect
    SpriteShapeRenderer spriteShapeRenderer;

    void Start()
    {
        spriteShapeRenderer = transform.Find("SlashSprite").GetComponent<SpriteShapeRenderer>();
        StartCoroutine(FadeSlash());
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();

            if (enemyController != null)
                enemyController.TakeDamage(damage);
        }
    }

    IEnumerator FadeSlash()
    {

        //gradual transparency

        for (float t = 1.0f; t > 0.0f; t -= 0.1f)
        {
            spriteShapeRenderer.color = new Color(
                spriteShapeRenderer.color.r,
                spriteShapeRenderer.color.g,
                spriteShapeRenderer.color.b,
                t);
            yield return new WaitForSeconds(0.05f);
            // Maybe if t<0.5 -> disable collider
        }

        Destroy(gameObject);

    }
}
