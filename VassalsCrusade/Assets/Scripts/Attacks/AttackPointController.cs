using UnityEngine;

public class AttackPointController : MonoBehaviour
{
    Transform player;
    Vector3 mousePosition;

    float angle;
    float distance = 2;


    void Start()
    {
        player = transform.parent.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        mousePosition -= player.position;

        //mousePosition.z = (player.transform.position.z - Camera.main.transform.position.z);

        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if (angle < 0.0f) angle += 360f;

        transform.localEulerAngles = new Vector3(0, 0, angle);

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;

        transform.localPosition = new Vector3( xPos, yPos , 0);
    }
}
