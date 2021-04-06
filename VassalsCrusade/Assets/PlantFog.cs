using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFog : MonoBehaviour
{
    PlayerController playerController;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (playerController == null)
                playerController = other.GetComponent<PlayerController>();

            playerController?.TakeDamage(0.1f);
        }
    }

    public void Dispel(Transform objectTransform, Vector2 pointerDirection, float smoothTime, Vector3 velocity)
    {
        StartCoroutine(PushBack(objectTransform, pointerDirection, smoothTime, velocity));
    }


    public IEnumerator PushBack(Transform objectTransform, Vector2 pointerDirection, float smoothTime, Vector3 velocity)
    {
        var renderer = GetComponent<Renderer>() as Renderer;
        float alpha = renderer.material.GetFloat("_Alpha");
        // Contrary vector
        Vector2 v = -pointerDirection;
        Vector3 direction = new Vector2(v.x, v.y);

        for (int i = 0; i < 100; i++)
        {
            Vector3 targetPosition = objectTransform.position - direction * 1.5f;// power
            objectTransform.position = Vector3.SmoothDamp(objectTransform.position, targetPosition, ref velocity, smoothTime);
            alpha -= 0.006f; //fog's alpha is 0.6
            renderer.material.SetFloat("_Alpha", alpha);

            yield return new WaitForSeconds(0.01f);
        }

        Destroy(gameObject);

    }


}
