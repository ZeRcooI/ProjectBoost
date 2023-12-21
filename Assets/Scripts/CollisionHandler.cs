using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private AudioClip _success;
    [SerializeField] private AudioClip _crash;

    [SerializeField] private ParticleSystem _successParticle;
    [SerializeField] private ParticleSystem _crashParticle;

    private AudioSource _audioSource;

    private float _levelLoadDelay = 2f;

    private bool _isTransitioning = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTransitioning)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":

                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();

        _audioSource.PlayOneShot(_success);

        _successParticle.Play();

        GetComponent<Movement>().enabled = false;

        Invoke(nameof(LoadNextScene), _levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();

        _audioSource.PlayOneShot(_crash);

        _crashParticle.Play();

        GetComponent<Movement>().enabled = false;

        Invoke(nameof(ReloadLevel), _levelLoadDelay);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}