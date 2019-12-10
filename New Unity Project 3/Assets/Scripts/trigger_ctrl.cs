using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_ctrl : MonoBehaviour
{
    public GameObject trigger;
    int i = 0;
    GameObject[] triggers = new GameObject[10];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = -1; i<1; i++)
        {
            for(int j = -1; j< 1; j++)
            {
                make_new_trigger(20*i+10, 50*j+25);

            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void make_new_trigger(float x, float z)
    {
        Vector3 pos = new Vector3(x, 1.2f, z);

        Instantiate(trigger, pos, Quaternion.identity);

    }
}
