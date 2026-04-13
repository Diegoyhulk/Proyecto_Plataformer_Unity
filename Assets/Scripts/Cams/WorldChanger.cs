using Intefaces;
using Unity.Cinemachine;
using UnityEngine;

public class WorldChanger : MonoBehaviour,IEnterExit
{
    [SerializeField] private CinemachineCamera camera;
    [SerializeField] private Collider2D ConfinerDrop;
    [SerializeField] private Collider2D Confinerwold2;
    
    public void Onenter()
    {
        var confiner = camera.GetComponent<CinemachineConfiner2D>();
        confiner.BoundingShape2D = ConfinerDrop;
        confiner.InvalidateCache(); // IMPORTANTE
    }

    public void Onexit()
    {
        var confiner = camera.GetComponent<CinemachineConfiner2D>();
        confiner.BoundingShape2D = Confinerwold2;
        confiner.InvalidateCache();
    }
}
