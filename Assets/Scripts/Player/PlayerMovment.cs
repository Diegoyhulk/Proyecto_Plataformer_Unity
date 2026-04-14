using UnityEngine;

public class PlayerMain : PlayerSystem
{
    [Header("Player Movment")]
    [field: SerializeField] public float movmentforce;
    public float jumpangle = 0;
    private float maxjumpangle = 50f;
    private float hinput;
    private static Vector2 Swiming;
    private float direction;
    [SerializeField] private float speedrotation;
    [Header("Particle")]
    private SpriteRenderer ball;
    [SerializeField] private ParticleSystem ps;
    private bool startpin;
    private bool startparticles = true;
    private float spinamount => jumpangle / maxjumpangle;
    private void OnEnable()
    {
        main.inputreader.Moving += UpdateMovment;
        main.inputreader.WMoving += Swim;
        main.inputreader.OnWMoving += SwimPlay;
        main.inputreader.OffWMoving += SwimStop;
    }

    private void OnDisable()
    {
        main.inputreader.Moving -= UpdateMovment;
        main.inputreader.WMoving -= Swim;
        main.inputreader.OnWMoving -= SwimPlay;
        main.inputreader.OffWMoving -= SwimStop;
    }

    protected override void Awake()
    {
        base.Awake();
        ball = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (main.Stop && !main.WMode)
        {
            Create_Spin();  
        }
    }
    private void FixedUpdate()
    {
        if (main.Stop && !startpin && !main.WMode) 
        {
            ball.transform.eulerAngles = new Vector3(0, 0, -main.rb.linearVelocity.x * speedrotation);
            main.rb.AddForce(new Vector2(hinput, 0) * movmentforce, ForceMode2D.Force);
        }
        if (main.WMode)
        {
            main.rb.AddForce(Swiming * 50f, ForceMode2D.Force);
        }
    }
    private void UpdateMovment(Vector2 ctx)
    {
        if (!main.WMode)
        {
            hinput = ctx.x;
        }
    }

    private void Swim(Vector2 ctx)
    {
        if (main.WMode)
        {
            Swiming = ctx;
            WRotate();
        }
    }

    private void SwimPlay()
    {
        if (main.WMode)
        {
            main.anim.SetBool("Spinning", true);
            foreach (var bub in main.bb)
            {
                bub.Play();
            }
        }
    }

    private void SwimStop()
    {
        if (main.WMode)
        {
            main.anim.SetBool("Spinning", false);
            foreach (var bub in main.bb)
            {
                bub.Stop();
            }
        }
    }
    private void WRotate()
    {
        Vector2 dir = Swiming;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Create_Spin()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (startparticles)
            {
                ball.transform.eulerAngles = Vector3.zero;
                ps.Play(); 
                main.anim.SetBool("Spinning", true);
            }
            startparticles = false;
            startpin = true;
            if (jumpangle <= maxjumpangle)
            {
                jumpangle += (Time.deltaTime * 20);
                float t = spinamount;
                float lifetime = Mathf.Lerp(1, 2, t);
                var main = ps.main;
                main.startLifetime = lifetime;
                Rotate(); 
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ps.Stop();
            main.rb.AddForce(new Vector2((spinamount) * hinput, 1f) * 20, ForceMode2D.Impulse);
            main.rb.AddTorque(-100f * hinput * (spinamount), ForceMode2D.Force);
            jumpangle = 0;
            main.rb.constraints = RigidbodyConstraints2D.None;
            main.Stop = false;
            startpin = false;
            startparticles = true;
        }
    }
    private void Rotate()
    {
        if (hinput > 0)//Dch
        {
            ps.transform.eulerAngles = new Vector3(0, -90, 87.615f);
        }
        else if (hinput < 0) //Izq
        {
            ps.transform.eulerAngles = new Vector3(0, 90, 87.615f);
        }
    }
}
