using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private Rigidbody playerRb;
    private float playerSpeed = 5f;
    private GameObject focalPoint;
    private bool hasPowerup;
    private float powerupStrength = 10f;
    public GameObject powerupIndicator;
    private Vector3 poerupOffset = new Vector3(0f,-0.59f,0f);
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * playerSpeed);
        powerupIndicator.transform.position = transform.position + poerupOffset;

        if(transform.position.y < 0.09)
        {
            gameOver = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        Debug.Log("hasPowerup" + hasPowerup);
        powerupIndicator.SetActive(false);
    }
}
