using UnityEngine;

public class tankmove : MonoBehaviour
{
    public int player = 1;
    public float speed = 5f;
    public float turn = 180f;
    private Rigidbody rb;
    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private float movevalueinput;
    private float turnvalueinput;
    public Joystick js;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        

    }
    void Start()
    {
        m_MovementAxisName = "Vertical" + player;
        m_TurnAxisName = "Horizontal" + player;
    }

    // Update is called once per frame
    void Update()
    {
        //movevalueinput = Input.GetAxis(m_MovementAxisName);
        //turnvalueinput = Input.GetAxis(m_TurnAxisName);
        //movevalueinput = js.Vertical;
        //turnvalueinput = js.Horizontal;

    }
    void FixedUpdate()
    {
        Move();
        Turn();
    }
    private void Move()
    {
        // Vector3 movement = transform.forward * speed * Time.deltaTime*movevalueinput;
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        //rb.MovePosition(rb.position + movement);
        if (js.Vertical > 0.3f)
        {

            rb.MovePosition(rb.position + movement);
        }
        if (js.Vertical < -0.3f)
        {

            rb.MovePosition(rb.position - movement);
        }
    }
    
    private void Turn()
    {
        //turn = 130 * Time.deltaTime*turnvalueinput;
        turn = 130 * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0f, turn, 0f);
        //rb.MoveRotation(rb.rotation * rotation);
        if (js.Horizontal > 0.3f)
        {
            turn = 100 * Time.deltaTime;
            Quaternion rot = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * rot);
        }

        if (js.Horizontal < -0.3f)
        {
            turn = -100 * Time.deltaTime;
            Quaternion rot = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * rot);
        }
    }
}
