using UnityEngine;
using System.Collections;

public class ControlPersonaje : MonoBehaviour {

    public float velocidad = 10f;
    public float fuerzaSalto = 350f;
    private bool enElSuelo = true;
    private bool saltando = false;
    private Vector3 direccion;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
        direccion = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && saltando == false && enElSuelo == true)
        {
            saltando = true;
            enElSuelo = false;
        }
	}

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(direccion) * velocidad * Time.deltaTime);

        if (saltando == true && enElSuelo == false)
        {
            rb.AddForce(transform.up * fuerzaSalto);
            saltando = false;
        }
    }

    void OnCollisionEnter(Collision objeto)
    {
        if (objeto.gameObject.CompareTag("Planeta"))
        {
            enElSuelo = true;
        }
    }
}
