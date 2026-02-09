using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private PlayerMouseFollower mouseFollowerScript;
    [SerializeField] private VisualEffect impactVFX;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mouseFollowerScript = FindObjectOfType<PlayerMouseFollower>();
        //transform.rotation = player.transform.rotation;
        //Vector3 newHeightCorPoint = new Vector3(mouseFollowerScript.heightCorrectedPoint2.x, mouseFollowerScript.heightCorrectedPoint2.y, mouseFollowerScript.heightCorrectedPoint2.z);
        //transform.LookAt(newHeightCorPoint);
        //transform.position = Vector3.MoveTowards(transform.position, newHeightCorPoint, Time.deltaTime);
        Destroy(gameObject, 3f);
        //Debug.Log(newHeightCorPoint);
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        impactVFX.Play();
        if (!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Dropped Weapon"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        impactVFX.Play();
        if (!collision.gameObject.CompareTag("Player") || !collision.gameObject.CompareTag("Dropped Weapon"))
        {
            Destroy(gameObject);
        }
    }
}
