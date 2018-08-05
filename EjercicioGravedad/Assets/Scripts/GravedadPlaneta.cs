using UnityEngine;
using System.Collections;

public class GravedadPlaneta : MonoBehaviour {

    public float gravedad = 9.8f;
    public float radioExterior = 12f;
    public float radioInterior = 10f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioExterior);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioInterior);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] objetos = Physics.OverlapSphere(transform.position, radioExterior);
        foreach (Collider objeto in objetos)
        {
            if (Vector3.Distance(transform.position, objeto.transform.position) > radioInterior)
            {
                if (objeto.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = objeto.GetComponent<Rigidbody>();
                    rb.AddForce(gravedad * (transform.position - objeto.transform.position).normalized);

                    Quaternion rotacionDestino = Quaternion.FromToRotation(objeto.transform.up, (objeto.transform.position - transform.position).normalized) * objeto.transform.rotation;
                    objeto.transform.rotation = Quaternion.Slerp(objeto.transform.rotation, rotacionDestino, 10 * Time.deltaTime);
                }
            }
        }
	}
}
