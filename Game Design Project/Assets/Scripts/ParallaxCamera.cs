using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
 
[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
 
    private float oldPosition;
    private bool isTransitioning = true;
    public Transform player;              // Reference to the player
    public Vector3 startOffset;           // Initial offset for the camera start position           // Reference to the player
    public Vector3 startPos;
    public float transitionDuration = 2f;
    private Vector3 targetPosition;

    void Start()
    {
        transform.position = new Vector3 (player.position.x, transform.position.y, player.position.z);
        oldPosition = transform.position.x;
        transform.position = transform.position + startOffset;
        targetPosition = new Vector3(0f, .3f, -18f);
        isTransitioning = true;
    }
 
    void Update()
    {
        //if (transform.position.x != oldPosition)
        //{
        //    if (onCameraTranslate != null)
        //    {
        //        float delta = oldPosition - transform.position.x;
        //        onCameraTranslate(delta);
        //    }

        //    oldPosition = transform.position.x;
        //}
        if (isTransitioning && transform.position.x > player.position.x)
        {
            // Smoothly transition the camera to follow the player
            transform.position = new Vector3(transform.position.x - .01f, targetPosition.y, targetPosition.z);

            // End the transition when close enough
            if ((transform.position.x - player.position.x) < 0.1f)
            {
                UnityEngine.Debug.Log("FALSE");

                isTransitioning = false;
            }
        }
        else isTransitioning = false;
        if (!isTransitioning)
        {
            // Continue your existing camera movement logic
            if (transform.position.x != oldPosition)
            {
                if (onCameraTranslate != null)
                {
                    float delta = oldPosition - transform.position.x;
                    onCameraTranslate(delta);
                }

                oldPosition = transform.position.x;
            }
        }
    }

    private void EndTransition()
    {
        isTransitioning = false;
    }
}