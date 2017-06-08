using UnityEngine;
using System.Collections;

public class RockMovement : MonoBehaviour {
    float z, speed_h, time, y, aceleration,yo,zo;
    float speed_v = 2.5f;

    public ParticleSystem particleSystem;

    float triggerTime;
    float timeAfterHit;
    bool comPlayer;

    /*float angle, speed, radius;
    Vector3 origen;*/

    // Use this for initialization
    void Start () {
        aceleration = -60;
        speed_h = -32;
        speed_v = 60;
        time = 0f;
        zo = gameObject.GetComponent<Rigidbody>().position.z;
        yo = gameObject.GetComponent<Rigidbody>().position.y;
        triggerTime = 20;
        timeAfterHit = 20;
        comPlayer = transform.parent.GetComponentInChildren<G8PlayerController>().comPlayer;

        /*origen = new Vector3(0f, 2f, 16f);
        angle = Mathf.PI/2;
        radius = 16f;
        speed = Mathf.PI / 2;*/
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        z = zo + speed_h * time;
        //Debug.Log("z: " + z + ", v: " + speed_h + ", t: " + time);
        //speed_v = speed_v + aceleration * time;
        y = yo + (speed_v * time) + (0.5f * aceleration * Mathf.Pow(time, 2));
       // Debug.Log("y: " + y + ", v: " + speed_v + ", t: " + time);
        rigidbody.MovePosition(new Vector3(rigidbody.position.x, y, z));
        if(y<0f)
            Destroy(gameObject,1);
        /*Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        angle -= speed * Time.deltaTime;
        float y = (Mathf.Cos(angle) * radius) + origen.y;
        float z = (Mathf.Sin(angle) * radius) + origen.z;
        //transform.Translate(new Vector3(x - transform.position.x, 0f, z - transform.position.z));
        rigidbody.MovePosition(new Vector3(rigidbody.position.x, y, z));*/
        if (!comPlayer)
            return;
        triggerTime -= Time.deltaTime;
        timeAfterHit -= Time.deltaTime;
        if (triggerTime <= 0)
        {
            transform.parent.GetComponentInChildren<G8PlayerController>().Harden(true);
            triggerTime = 20f;
        }
        if (timeAfterHit <= 0)
        {
            transform.parent.GetComponentInChildren<G8PlayerController>().Harden(false);
            timeAfterHit = 20f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Equals("Character"))
        {
            if (collision.gameObject.GetComponent<G8PlayerController>().harden)
            {
                Destroy(gameObject, 1);
                Destroy(gameObject.GetComponent<MeshRenderer>());
                ParticleSystem ps = Instantiate(particleSystem, collision.transform.position, particleSystem.transform.localRotation) as ParticleSystem;
                ps.loop = false;
                Destroy(ps, 1);
            }
            else
            {
                /*angle = Mathf.PI / 2;
                origen = new Vector3(origen.x, origen.y, -2);
                radius = 2;
                speed = Mathf.PI / 0.25f;*/
                collision.gameObject.GetComponent<Animation>().Play("Impact");
                collision.gameObject.GetComponent<G8PlayerController>().TakeDamage(10f);
            }
            timeAfterHit = Random.Range(0.05f, 0.2f);
        }
        if (collision.transform.name.Equals("Plane"))
        {
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (comPlayer)
            triggerTime = Random.Range(0.0f, 0.35f);
    }
}
