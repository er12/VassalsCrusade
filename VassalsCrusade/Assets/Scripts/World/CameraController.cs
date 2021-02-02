using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Assertions;


public class CameraController : MonoBehaviour
{
    public Transform target; //later this should be changed to target
    private static bool cameraExists;
    

    private void Start()
    {
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneStartController.OnNewAreaStart += SetConfinerCollider;
    }


    void OnDisable()
    {
        SceneStartController.OnNewAreaStart -= SetConfinerCollider;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            transform.position = targetPosition;
        }
    }

    void SetConfinerCollider()
    {
        var confiner = GetComponent<Cinemachine.CinemachineConfiner>();

        confiner.InvalidatePathCache();
        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("World").GetComponent<Collider2D>();
    }
}
