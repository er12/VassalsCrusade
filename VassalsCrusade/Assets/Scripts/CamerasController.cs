using UnityEngine;

public class CamerasController : MonoBehaviour
{
    private static bool camerasExists;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    private void Start()
    {
        if (!camerasExists)
        {
            camerasExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = target;
    }

    void OnEnable()
    {
        //Changing 2d boundaries between worlds
        SceneStartController.OnNewAreaStart += SetConfinerCollider;
    }


    void OnDisable()
    {
        SceneStartController.OnNewAreaStart -= SetConfinerCollider;
    }

    void SetConfinerCollider()
    {
        var confiner = virtualCamera.GetComponent<Cinemachine.CinemachineConfiner>();

        confiner.InvalidatePathCache();
        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("World").GetComponent<Collider2D>();
    }
}
