using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour {
    public float startinghealth = 100f;
    public Slider slider;
    public float currenthealth = 100f;
    public bool dead;
    public Color full = Color.green;
    public Color zero = Color.red;
    public Image image;
    public Vector3 spawnPoint;
    public GameObject shield;
    public GameObject level;


	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;
	}
    private void OnEnable()
    {
        tankshooting tkshooting = gameObject.GetComponent<tankshooting>();
        tkshooting._tankState = tankshooting.TankState.NORMAL;
        dead = false;
        StartCoroutine(enable());
        SetHealthUI();
    }

	
	// Update is called once per frame
	void Update () {
		
	}
    public void TakeDamage(float amount)
    {
        currenthealth -= amount;
        SetHealthUI();
        if(currenthealth <=0 &&!dead)
        {
            OnDeath();
            
        }
    }
    private void SetHealthUI()
    {
        slider.value = currenthealth;
        image .color  = Color.Lerp(zero, full, currenthealth / startinghealth);
    }
    private void OnDeath()
    {
        manager manag = level.GetComponent<manager>();
        trigger_ctrl ctrl = level.GetComponent<trigger_ctrl>();
        gameObject.SetActive (false);
        ctrl.make_new_trigger(transform.position.x, transform.position.z);
        gameObject.transform.position = spawnPoint;
        dead = true;
        manag.re(gameObject);
        
 

    }
    
    IEnumerator enable()
    {
        shield.SetActive(true);
        currenthealth = 100000000;
        
        yield return new WaitForSeconds(2);
        shield.SetActive(false);
        currenthealth = startinghealth;
    }
}
