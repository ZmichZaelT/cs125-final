using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exit : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(exitGame);
    }
    void Start()
    {
        GameObject.Find("exit").GetComponentInChildren<Text>().text = "exit";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void exitGame()
    {
        Application.Quit();
    }
}
