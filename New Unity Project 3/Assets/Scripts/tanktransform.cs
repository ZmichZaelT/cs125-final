using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class tanktransform : MonoBehaviour
{
    public GameObject guns;
    public GameObject muzzles;
    
    public GameObject shield;
    public GameObject turret;
    public Transform position2;
    public Transform muzzle_pos2;
    public float t = 0f;
    public Transform position1;
    public Transform muzzle_pos1;
    public tankshooting _tkshooting;





    
    // Start is called before the first frame update
    void Start()
    {
        guns.SetActive(false);
        shield.SetActive(false);
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
        if (_tkshooting._tankState ==tankshooting.TankState.DOUBLE)
        {
            Debug.Log("double");

            guns.SetActive(true);
            shield.SetActive(true);
            StartCoroutine(Transform());


        }
        if (_tkshooting.tankState == tankshooting.TankState.NORMAL)
        {
            TransformBack();
        }
    }*/
    public void Transformtank()
    {
        guns.SetActive(true);
            shield.SetActive(true);
            StartCoroutine(Transform());

    }
    
    IEnumerator Transform()
    {
        t = 0f;
        
        while (t< 1)
        {
            muzzles.transform.position = Vector3.Lerp(muzzles.transform.position, muzzle_pos2.position, t);
            turret.transform.position = Vector3.Lerp(turret.transform.position,position2.position,t);
            t += Time.deltaTime;
            yield return null;

        }

        

    }
    
    public void TransformBack()
    {



        
        muzzles.transform.position = muzzle_pos1.position;
        turret.transform.position = position1.position;
            

     
        guns.SetActive(false);
        shield.SetActive(false);


    }



}

