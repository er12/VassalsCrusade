using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyController>().TakeDamage(10);
        }
    }

    void TriggerHit()
    {
        StartCoroutine(ToogleCollider());
    }
    
    private IEnumerator ToogleCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<Collider2D>().enabled = false;

    }

    public void DestroyMagic()
    {
        Destroy(gameObject);
    } 
}
