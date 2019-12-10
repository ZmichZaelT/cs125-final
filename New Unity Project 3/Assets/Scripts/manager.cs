using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void re(GameObject obj)
    {
        StartCoroutine(reborn(obj));
    }
    public IEnumerator reborn(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(true);



    }
}
