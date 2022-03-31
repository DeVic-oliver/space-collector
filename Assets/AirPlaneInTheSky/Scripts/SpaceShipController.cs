using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{   
    Vector2 lookInput;
    Vector2 mouseDistance;
    Vector2 screenCenter;

    float rollInput;

    [SerializeField] float fowardSpeed = 130f;
    [SerializeField] float turbo = 260f;
    [SerializeField] float turboCountDown = 1.5f;

    [SerializeField] ParticleSystem shockWave;

    public bool isTurboActive = false;
    
    public float lookRotateSpeed = 70f;
    public float rollSpeed = 1f;
    public float rollAcceleration = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        shockWave.Stop();

        MouseControl();

        RollControl();

        if (Input.GetKeyDown(KeyCode.Space) && !isTurboActive)
        {
            Turbo();
        }
    }


    void MouseControl()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
    }

    void RollControl()
    {
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(mouseDistance.y * lookRotateSpeed * Time.deltaTime,
            mouseDistance.x * lookRotateSpeed * Time.deltaTime,
            rollInput * rollSpeed, Space.Self);

        transform.Translate(Vector3.forward * fowardSpeed * Time.deltaTime);
    }

    void Turbo()
    {
        shockWave.Play();
        isTurboActive = true;
        float initialSpeed = fowardSpeed;
        fowardSpeed += turbo;
        StartCoroutine(TurboTime(initialSpeed));
    }

    IEnumerator TurboTime(float initialSpeed)
    {
        yield return new WaitForSeconds(turboCountDown);
        fowardSpeed = initialSpeed;
        isTurboActive = false;
    }
}
