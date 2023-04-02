using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdRigidbody;
    public float velocityAngleMultiplier = 3;

    public AudioSource flapAudio;
    public AudioSource gameOverAudio;
    private bool fadingAudio = false;

    private float gameOverSpinsPerSecond;

    public GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ListenMouse0Input();
        DoAudioCheck();
        AdjustAngle();
        CheckOutOfBounds();
    }

    private void CheckOutOfBounds()
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (!(GeometryUtility.TestPlanesAABB(frustumPlanes, GetComponent<CapsuleCollider2D>().bounds) || gameController.gameOver))
        {
            gameController.GameOver();
        }
    }

    private void ListenMouse0Input()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameController.gameOver)
        {
            birdRigidbody.velocity = Vector2.up * gameController.birdFlapStrength;
            PlayFlapAudio();
        }
    }

    private void AdjustAngle()
    {
        if (!gameController.gameOver)
        {
            float angle = birdRigidbody.velocity.y * velocityAngleMultiplier;
            birdRigidbody.rotation = angle > 90 ? 90 : (angle < -90 ? -90 : angle);
        }
        else
        {
            float degreesFullSpin = 360;

            transform.Rotate(new Vector3(0, 0, (degreesFullSpin * gameOverSpinsPerSecond) * Time.deltaTime));
        }
    }

    private void PlayFlapAudio()
    {
        if (!flapAudio.isPlaying)
        {
            flapAudio.Play();
        } else
        {
            fadingAudio = true;
        }
    }

    private void DoAudioCheck()
    {
        if (fadingAudio)
        {
            flapAudio.volume -= 1 * Time.deltaTime / 0.05f;
        }

        if (flapAudio.volume <= 0)
        {
            fadingAudio = false;
            flapAudio.Stop();
            flapAudio.volume = 1;
            flapAudio.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameOverAudio.Play();
        gameOverSpinsPerSecond = gameController.pipeMoveSpeed / 10;
        gameController.GameOver();
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
