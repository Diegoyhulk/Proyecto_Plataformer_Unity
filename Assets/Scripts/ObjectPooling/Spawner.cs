using Intefaces;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private LavaBubble lavaPrefab;
    private ObjectPool<LavaBubble> lavabubblePool;
    private bool LavaActiva = true;
    private float rand;
    private float timer;
    public bool Done { get; set; }
    void Awake()
    {
        lavabubblePool = new ObjectPool<LavaBubble>(CreatenewLavabubble, GetLavaBubble, ReleaseLavaBubble);
        rand = Random.Range(0f, 3f);
    }
    private LavaBubble CreatenewLavabubble()
    {
        //Creo la bala
        LavaBubble copy = Instantiate<LavaBubble>(lavaPrefab, transform.position, transform.rotation);
        copy.MyPool = lavabubblePool;
        copy.SetSpawner(this);
        return copy;
    }

    private void GetLavaBubble(LavaBubble lavaToGet)
    {
        lavaToGet.gameObject.SetActive(true);
        lavaToGet.transform.position = transform.position;
    }
    private void ReleaseLavaBubble(LavaBubble bulletToRelease)
    {
        bulletToRelease.gameObject.SetActive(false);
        //Desactivo la gota
    }
    void Update()
    {
        if (!Done)
        {
            timer += Time.deltaTime;
            if (timer >= rand)
            {
                LavaActiva = false;
                Done = true;
            }
        }

        if (!LavaActiva)
        {
            lavabubblePool.Get();
            LavaActiva = true;
        }
    }

    public void BubleRelease()
    {
        LavaActiva = false;
    }
}
