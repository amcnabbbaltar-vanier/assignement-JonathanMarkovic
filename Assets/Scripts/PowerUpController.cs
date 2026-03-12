using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public float respawnTime = 30f;

    private Collider pickupCollider;
    private MeshRenderer pickupRenderer;


    // Start is called before the first frame update
    void Start()
    {
        pickupCollider = GetComponent<Collider>();
        pickupRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        pickupCollider.enabled = false;
        pickupRenderer.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        pickupCollider.enabled = true;
        pickupRenderer.enabled = true;
    }
}
