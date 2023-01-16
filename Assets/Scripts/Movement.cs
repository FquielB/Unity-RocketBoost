using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    new Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float thrustPower = 250f;
    [SerializeField] float rotationPower = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationPower);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationPower);
        }
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            rigidbody.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        } else if (audioSource.isPlaying) {
            audioSource.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidbody.freezeRotation = false;
    }
}
