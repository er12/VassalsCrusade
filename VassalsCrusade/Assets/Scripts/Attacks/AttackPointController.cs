using UnityEngine;

public class AttackPointController : MonoBehaviour
{
    Transform playerTransform;
    Vector3 mousePosition;

    float angle;
    float distance = 2;

    // Update is called once per frame
    void FixedUpdate()
    {
    //     playerTransform = transform.parent.transform;

    //     mousePosition = Input.mousePosition;
    //     // Ignore ray
    //     RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero, 100);

    //     if(hit.collider != null)
    //     {
    //         mousePosition = hit.collider.gameObject.transform.position;
    //     }

    //     //mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(mousePosition.x, mousePosition.y, -Camera.main.transform.position.z)) ;
    //     //mousePosition.z = Camera.main.nearClipPlane;

    //     //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //     Debug.Log("Player: " + playerTransform.position + " - " + "Mouse: " + mousePosition + " - " + (mousePosition - playerTransform.position));

    //     mousePosition -= playerTransform.position;

    //     //mousePosition.z = (player.transform.position.z - Camera.main.transform.position.z);

    //     angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

    //     if (angle < 0.0f) angle += 360f;

    //     //transform.localEulerAngles = new Vector3(0, 0, angle);

    //     float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
    //     float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;

    //     transform.localPosition = new Vector3( xPos, yPos , 0);
    // }
    }
}
