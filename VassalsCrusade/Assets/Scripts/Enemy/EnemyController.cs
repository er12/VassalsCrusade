using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float maxHealth = 30;
    public float currentHealth;

    private Animator anim;
    private SpriteRenderer spriteRenderer ;
    private Transform target;

    private bool animatingHit;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(!animatingHit)
        {
            StartCoroutine(animateDamage());

        }


        if (currentHealth <= 0)
        {
            //Die();
        }
    }

    void Die()
    {
        // TODO: Death Effect
        // Disable enemy

        Destroy(gameObject);

    }

    IEnumerator animateDamage()
    {
        animatingHit = true;

        Color spriteColor = spriteRenderer.color;

        for(int i = 0 ; i < 2 ; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, 0, 0);
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = new Color(spriteRenderer.color.r,spriteColor.g, spriteColor.b);
            yield return new WaitForSeconds(0.15f);
        }

        animatingHit = false;

    }
    
}
