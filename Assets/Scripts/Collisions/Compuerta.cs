using System;
using Intefaces;
using UnityEngine;

public class Compuerta : MonoBehaviour, IEnterExit
{
    [SerializeField] private InputReaderSO inputreader;
    private GameObject door;
    private GameObject Rueda1;
    private GameObject Rueda2;
    bool start = false;
    [SerializeField] public float timer = 4f;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float Upspeed = 1f;
    [SerializeField] public float Downspeed = 0.5f;
    private float time;
    private bool isin;
    [SerializeField] private Vector3 dirction;

    private void OnEnable()
    {
        inputreader.OnJumpStarted += OpenDoor;
        inputreader.OnJumpCanceled += CloseDoor;
    }
    private void OnDisable()
    {
        inputreader.OnJumpStarted -= OpenDoor;
        inputreader.OnJumpCanceled -= CloseDoor;
    }
    void Awake()
    {
        door = transform.Find("Puerta").gameObject;
        Rueda1 = transform.Find("Rueda1").gameObject;
        Rueda2 = transform.Find("Rueda2").gameObject;
    }
    void Update()
    {
        if (start)
        {
            if (timer >= time)
            { 
                door.transform.position += dirction * (Time.deltaTime * Upspeed); 
                time += (Time.deltaTime * Upspeed);
                Rueda1.transform.Rotate(new Vector3(0f,0f,1f) * (speed * Time.deltaTime));
                Rueda2.transform.Rotate(new Vector3(0f,0f,1f) * (speed * Time.deltaTime));
            }
        }
        if (!start)
        {
            if (time >= 0)
            {
                door.transform.position -= dirction * (Time.deltaTime * Downspeed); 
                time -= (Time.deltaTime * Downspeed);
            }
        }
    }

    public void Onenter()
    {
        isin = true;
    }

    public void Onexit()
    {
        isin = false;
    }

    public void OpenDoor()
    {
        if (isin)
        {
            start = true;
        }
    }
    public void CloseDoor()
    {
        start = false;
    }
}
