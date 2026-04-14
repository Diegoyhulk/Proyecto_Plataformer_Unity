using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    protected PlayerMain1 main;
    protected virtual void Awake()
    {
        main = transform.root.GetComponent<PlayerMain1>();
    }
}
