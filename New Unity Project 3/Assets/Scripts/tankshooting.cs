using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class tankshooting : MonoBehaviour
{
    
    public Rigidbody[] projectile = new Rigidbody[2];
    public firebutton fb;
    
    public Transform[] firetransform = new Transform[2];
   
    public Slider aimslider;
    public float minforce = 15f;
    public float maxforce = 30f;
    public float chargetime = 0.75f;
    private string firebutton;
    private float currentforce;
    private float chargespeed;
    public bool fired;
    public Slider Reloadslider;
    public float timer = 0f;
    public int player;
    private string m_FireButton;
    public int ammo;
    private bool pressed;
    private TankState prev = TankState.NORMAL;
    public GameObject enemy;


    public enum TankState
    {
        NORMAL,
        MISSILE,
        DOUBLE
    }
    public TankState _tankState = TankState.NORMAL;
    public TankState tankState
    {
        get { return _tankState; }
        set { _tankState = value; }
    }

    public missile missilescript;

    public GameObject guns;
    public GameObject muzzles;

    public GameObject shield;
    public GameObject turret;
    public Transform position2;
    public Transform muzzle_pos2;
    public float t = 0f;
    public Transform position1;
    public Transform muzzle_pos1;

    // Use this for initialization
    private void OnEnable()
    {
        currentforce = minforce;
        aimslider.value = minforce;
        guns.SetActive(false);
        shield.SetActive(false);
        //Reloadslider.value = 0f;
        pressed = false;
    }
	void Start ()
    {
        //fb = FindObjectOfType<firebutton>();
        Reloadslider.value = 100f;
        chargespeed = (maxforce - minforce) / chargetime;
        m_FireButton = "Fire" + player;
        pressed = false;

    }

    // Update is called once per frame
    void Update () {
        
        timer += Time.deltaTime;
        /*if (Input.GetKeyDown(KeyCode.C))
        {
            _tankState = TankState.NORMAL;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            _tankState = TankState.MISSILE;
            fired = false;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            _tankState = TankState.DOUBLE;
            fired = false;
        }*/

        aimslider.value = minforce;
        if(ammo ==0)
        {
            _tankState = TankState.NORMAL;
        }
        if(prev != TankState.DOUBLE&& _tankState ==TankState.DOUBLE)
        {
            Transformtank();
            
        }
        if (prev == TankState.DOUBLE && _tankState != TankState.DOUBLE)
        {
            TransformBack();
            
        }

        //(Input.GetButtonDown(m_FireButton)
        switch (_tankState)
        {
            case TankState.NORMAL:
               
                Reloadslider.gameObject.SetActive(false);
                aimslider.gameObject.SetActive(true);
                if (currentforce >= maxforce && !fired)
                {
                    currentforce = maxforce;
                    Fire(0);
                    pressed = false;
                    break;
                }
                else if ((fb.p || Input.GetKeyDown(KeyCode.Space)) && !pressed)
                {
                    Debug.Log("getkey");
                    fired = false;
                    currentforce = minforce;
                    pressed = true;
                }
                else if ((!fb.p || Input.GetKeyUp(KeyCode.Space)) && pressed && !fired)
                {
                    pressed = false;
                    Fire(0);
                   
                    break;
                }
                else if (pressed && !fired)
                {
                    currentforce += chargespeed * Time.deltaTime;
                    aimslider.value = currentforce;
                }
                break;
            case TankState.DOUBLE:
                //Debug.Log("double in ");
                aimslider.gameObject.SetActive(false);
                Reloadslider.gameObject.SetActive(true);
                Reloadslider.value = timer*50;
                if ((fb.p) && fired == false && ammo>=1)
                {
                    Debug.Log("pressed");
                    currentforce = maxforce * 2;
                    Fire(2);
                    timer = 0f;
                    
                    StartCoroutine(Recoil());
                    
                    StartCoroutine(Reload());
                    

                    break;
                }
                break;


            case TankState.MISSILE:
                Reloadslider.gameObject.SetActive(false);
                aimslider.gameObject.SetActive(false);
                if ((fb.p|| Input.GetKeyDown(KeyCode.Space)) && ammo >= 1 && fired == false)
                {
                    ammo -= 1;
                    missilescript.player = player;
                    missilescript.enemy = enemy;
                    missilescript.launcher = gameObject;
                    Fire(1);
                    StartCoroutine(ReloadMissile());
                    break;
                }
                break;
            
                
            default:
                break;

        }
        prev = _tankState;




    }
    IEnumerator ReloadMissile()
    {

        fired = true;

        yield return new WaitForSeconds(1);

        fired = false;
    }
    IEnumerator Reload()
    {

        fired = true;

        yield return new WaitForSeconds(2);
        
        fired = false;
    }
    
    private void Fire(int i)
    {
        fired = true;
        Rigidbody projectileinstance = Instantiate(projectile[i], firetransform[i].position, firetransform[i].rotation) as Rigidbody;
        if(i !=1)
        {
            projectileinstance.velocity = currentforce * firetransform[i].forward ;

        }
        

        
        currentforce = minforce;
    }


    public void Transformtank()
    {
        guns.SetActive(true);
        shield.SetActive(true);
        StartCoroutine(Transform());

    }

    IEnumerator Transform()
    {
        t = 0f;

        while (t < 1)
        {
            muzzles.transform.position = Vector3.Lerp(muzzles.transform.position, muzzle_pos2.position, t);
            turret.transform.position = Vector3.Lerp(turret.transform.position, position2.position, t);
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
    IEnumerator Recoil()
    {
        t = 0f;

        while (t < 0.25)
        {
            muzzles.transform.position = Vector3.Lerp(muzzles.transform.position, muzzle_pos1.position, t*2);
            t += Time.deltaTime;
            yield return null;

        }
        t = 0f;
        while (t < 0.5f)
        {
            muzzles.transform.position = Vector3.Lerp(muzzles.transform.position, muzzle_pos2.position, t*2);
            t += Time.deltaTime;
            yield return null;

        }
        ammo -= 1;
    }

}
