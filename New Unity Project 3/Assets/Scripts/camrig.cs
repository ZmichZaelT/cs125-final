using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camrig:MonoBehaviour
{
    public float damptime = 0.2f;
    public float screenedge = 4f;
    public float minsize = 6.5f;
    public Transform[] targets;
    private Camera cam;
    private float zoomspeed;
    private Vector3 movev;
    private Vector3 desiredposition;

	// Use this for initialization
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }
	void Start () {
		
	}
	
    private void FixedUpdate()
    {
        Move();
        Zoom();
    }
    private void Move()
    {
        Findaverage();
        transform.position = Vector3.SmoothDamp(transform.position, desiredposition, ref movev, damptime);
    }
    private void Findaverage()
    {
        Vector3 avg = new Vector3();
        int numtargets = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            if (!targets[i].gameObject.activeSelf)
            {
                continue;
            }
            avg += targets[i].position;
            numtargets++;
        }
        if (numtargets > 0)
        {
            avg /= numtargets;
        }
        avg.y = transform.position.y;
        desiredposition = avg;
    }
    private void Zoom()
    {
        float requiredsize = Findsize();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredsize, ref zoomspeed, damptime);//if no ref?
    }
    private float Findsize()
    {
        Vector3  desiredlocalposition = transform.InverseTransformPoint(desiredposition);//if no?
        float size = 0f;
        for (int i = 0; i < targets.Length; i++)
        {
            if (!targets[i].gameObject.activeSelf)
            {
                continue;
            }
            Vector3 targetlocalpos = transform.InverseTransformPoint(targets[i].position);
            Vector3 desiredpostotarget = targetlocalpos - desiredlocalposition;
            size = Mathf.Max(size, Mathf.Abs(desiredpostotarget.y));//abs ensure always positive
            size = Mathf.Max(size, Mathf.Abs(desiredpostotarget.x) / cam.aspect);
        }
        size+=screenedge ;
        size = Mathf.Max(size, minsize);
        return size;
    }
    public void SetStartPosAndSize()
    {
        Findaverage();
        transform.position = desiredposition;
        cam.orthographicSize = Findsize();
    }



	void Update () {
		
	}
}
