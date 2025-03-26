using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movements : MonoBehaviour
{
    [SerializeField] public float thrustForce = 10f;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] ParticleSystem rocketThrustEffect; // Roket için partikül sistemi
    [SerializeField] ParticleSystem rightThrustEffect;
    [SerializeField] ParticleSystem leftThrustEffect;

    public float rotationSpreed = 100f;
    public Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        //StartEffect();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        ProcessTrusht();
        ProcessRotation();
    }

    void ProcessTrusht()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartEffect();
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                StopEffect();
                audioSource.Stop();
            }
        }

    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LeftRotationEffect();
            ApplyRotation(rotationSpreed);

        }
        else
        {
            leftThrustEffect.Pause();

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RightRotationEffect();
            ApplyRotation(-rotationSpreed);
        }
        else
        {
            rightThrustEffect.Pause();

        }

    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;

    }
    void StartEffect()
    {
        rocketThrustEffect.Play(); // Partikül sistemini başlat

    }
    void StopEffect()
    {
        if (rocketThrustEffect.isPlaying) // Eğer partikül sistemi çalışıyorsa
        {
            rocketThrustEffect.Pause(); // Partikül sistemini durdur
        }

    }
    void RightRotationEffect()
    {
        rightThrustEffect.Play();

    }
    void LeftRotationEffect()
    {
        leftThrustEffect.Play();
        Debug.Log("sol itiş");

    }

}
