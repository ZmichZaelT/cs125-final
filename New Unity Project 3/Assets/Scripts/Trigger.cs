using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Transform trans1;
    public Transform trans2;
    public int n;
    public GameObject level = GameObject.Find("Level");
    public trigger_ctrl ctrl = new trigger_ctrl();
    
    void OnEnable()
    {
        //ctrl = level.GetComponent<trigger_ctrl>();

    }

    // Update is called once per frame
    void Update()
    {
        
        

        Vector3 pos1 = trans1.position;
        Vector3 pos2 = trans2.position;
        var fraction = (Mathf.Sin(Time.time*2f) + 1.0f) / 2.0f;
        transform.position = Vector3.Lerp(pos1, pos2, fraction);

    }
    private void OnTriggerEnter(Collider coll)
    {
        
        //ctrl.make_new_trigger(Mathf.Sin(Time.time)*5, Mathf.Sin(Time.time) * 3);
        Debug.Log(coll);
        tankshooting tankshooting = coll.gameObject.GetComponent<tankshooting>();
        tankshooting.ammo = 3;
        if(Mathf.Sin(Time.time)>0)
        {
            tankshooting._tankState = tankshooting.TankState.DOUBLE;

        }
        else
        {
            tankshooting._tankState = tankshooting.TankState.MISSILE ;
        }
        
        
        
        tankshooting.fired = false;
        gameObject.SetActive(false); 


    }
    
}
