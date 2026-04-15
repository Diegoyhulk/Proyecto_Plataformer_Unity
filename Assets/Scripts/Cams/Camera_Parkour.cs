using System;
using Intefaces;
using Unity.Cinemachine;
using UnityEngine;

public class Camera_Parkour : MonoBehaviour, IEnterExit
{
    [SerializeField] private Vector3 final_scale = new Vector3(4, 2.2249609f, 2.37448525f);
    private CinemachineCamera cam;
    [SerializeField] private GameObject sunset;
    private Vector3 susetinitscal;
    private float speed = 0f;
    private void Awake()
    {
        cam = GetComponent<CinemachineCamera>();
        susetinitscal =  sunset.transform.localScale;
    }

    void Update()
    {
        if (cam.enabled)
        {
            if (speed <= 1)
            {
               speed += (Time.deltaTime * 0.6f); 
            }
        }
        if (!cam.enabled)
        {
            if (speed >= 0)
            {
                speed -= Time.deltaTime * 0.6f;
            }
        }
        sunset.transform.localScale = Vector3.Lerp(susetinitscal, final_scale, speed);
    }

    public void Onenter()
    {
        cam.enabled = true;
    }

    public void Onexit()
    {
        cam.enabled = false;
    }
}
