using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _mainThrust = 1f;
    [SerializeField] private float _rotatingThrust = 1f;
    [SerializeField] private AudioClip _mainEngine;

    [SerializeField] private ParticleSystem _mainEngineParticle;
    [SerializeField] private ParticleSystem _leftThrusterParticle;
    [SerializeField] private ParticleSystem _rightThrusterParticle;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(_rotatingThrust);

            if (!_rightThrusterParticle.isPlaying)
            {
                _rightThrusterParticle.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-_rotatingThrust);

            if (!_leftThrusterParticle.isPlaying)
            {
                _leftThrusterParticle.Play();
            }
        }
        else
        {
            _leftThrusterParticle.Stop();
            _rightThrusterParticle.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true;

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        _rigidbody.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);

            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_mainEngine);
            }

            if (!_mainEngineParticle.isPlaying)
            {
                _mainEngineParticle.Play();
            }
        }
        else
        {
            _audioSource.Stop();
            _mainEngineParticle.Stop();
        }
    }
}