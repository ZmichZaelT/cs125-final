using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    public LayerMask tankmask;
    private float dam = 100f;
    private float expforce = 500f;
    private float lifetime = 4f;
    private float r = 2f;
    public Rigidbody rb;
    private float power = 10f;
    public float timer = 0;
    public GameObject particle;
    public int player;
    private float turnvalueinput;
    private string m_TurnAxisName;
    public Joystick js;
    public GameObject launcher;
    public GameObject enemy;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    
    
        
        
    
    /*void Update()
    {
        turnvalueinput = Input.GetAxis(m_TurnAxisName);
        Turn();
    }*/
    void OnEnable()
    {
        var rotation = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.transform.position.z);
        transform.LookAt(rotation);
        js = launcher.GetComponent<FixedJoystick>();
        //m_TurnAxisName = "Horizontal" + player;
        timer = 0;
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }
    void FixedUpdate()
    {
        Turn();
        timer += Time.deltaTime;
        //power += power * Time.deltaTime*2f;
        rb.AddForce(transform.forward*50.0f); //50f
        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
        
    }
    
    private void Turn()
    {
        //transform.forward = Quaternion.Euler(enemy.transform.position - transform.position) * Vector3.forward;
        var rotation = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.transform.position.z);
        transform.LookAt(rotation);
        //float turn = -200 * Time.deltaTime * turnvalueinput;
        //Quaternion rotation = Quaternion.Euler(0f, 0f, turn);
        //rb.MoveRotation(rb.rotation * rotation);

        /*if (js.Horizontal < -0.3f)
        {
            float turn = -200 * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0f,0f,turn);
            rb.MoveRotation(rb.rotation * rotation);
        }

        if (js.Horizontal > 0.3f)
        {
            float turn = 200 * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0f, 0f,turn);
            rb.MoveRotation(rb.rotation * rotation);
        }*/
    }
    private void OnTriggerEnter(Collider coll)
    {

        //Instantiate(particle, transform.position, particle.transform.rotation);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Instantiate(particle, transform.position, particle.transform.rotation);
        //Debug.Log("destroyed");
        Collider[] colliders = Physics.OverlapSphere(transform.position, r, tankmask);
        for (int i = 0; i < colliders.Length; i++)
        {

            Rigidbody b = colliders[i].GetComponent<Rigidbody>();
            if (!b)
            {
                continue;
            }
            b.AddExplosionForce(expforce, transform.position, r);
            TankHealth tkhealth = b.GetComponent<TankHealth>();
            if (!tkhealth)
            {
                continue;
            }
            //float damage = CalculateDamage(b.position);
            tkhealth.TakeDamage(30);
            //Debug.Log(damage);
        }
    }
    /*private float CalculateDamage(Vector3 position)
    {
        Vector3 explosion = position - transform.position;
        float expdistance = explosion.magnitude;
        float relativedistance = (r - expdistance) / r;
        float damage = relativedistance * dam;
        damage = Mathf.Max(0f, damage);
        return damage;
    }*/



}
