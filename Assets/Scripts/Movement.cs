using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower = 250f;
    [SerializeField] float rotationPower = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThursterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    

    new Rigidbody rigidbody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else 
        {
            StopThrust();
        }
    }

        void Rotate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void StartThrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
        rigidbody.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void StartRightRotation()
    {
        if (!leftThursterParticles.isPlaying)
        {
            rightThrusterParticles.Stop();
            leftThursterParticles.Play();
        }
        ApplyRotation(-rotationPower);
    }

    private void StartLeftRotation()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            leftThursterParticles.Stop();
            rightThrusterParticles.Play();
        }
        ApplyRotation(rotationPower);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidbody.freezeRotation = false;
    }

    private void StopRotation()
    {
        leftThursterParticles.Stop();
        rightThrusterParticles.Stop();
    }
}
