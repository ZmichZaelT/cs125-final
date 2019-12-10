using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
        
        button.onClick.AddListener(LoadScene);
    }
    void Start()
    {
        GameObject.Find("startgame").GetComponentInChildren<Text>().text = "start";
        //SceneManager.loadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
