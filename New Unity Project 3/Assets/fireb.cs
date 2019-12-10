using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class fireb : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    // Start is called before the first frame update
    public bool p;
    void Start()
    {
        p = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData e)
    {
        p = true;
    }
    public void OnPointerUp(PointerEventData e)
    {
        p = false;
    }
}
