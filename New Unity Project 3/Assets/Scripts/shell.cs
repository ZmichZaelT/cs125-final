using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour {
    public LayerMask  tankmask;
    private float dam = 100f;
    private float expforce = 1000f;
    private float lifetime = 2f;
    private float r = 5f;
    public GameObject particle;

	// Use this for initialization
	void Start () {
        //Destroy(gameObject, lifetime);
		
	}
	
	// Update is called once per frame
    private void OnTriggerEnter(Collider coll)
    {
        
            Instantiate(particle, transform.position, particle.transform.rotation);
            Destroy(gameObject);

        
        
    }
    private float CalculateDamage(Vector3 position)
    {
        Vector3 explosion = position - transform.position;
        float expdistance = explosion.magnitude;
        float relativedistance = (r - expdistance) / r;
        float damage = relativedistance * dam;
        damage = Mathf.Max(0f, damage); //prevent negative damage, which is weird
        return damage;
    }
    
    private void OnDestroy()
    {
        Debug.Log(transform.position);
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
            float damage = CalculateDamage(b.position);
            tkhealth.TakeDamage(damage);
            Debug.Log(damage);
        }
    }
    
    void Update()
    {
		
	}
}
