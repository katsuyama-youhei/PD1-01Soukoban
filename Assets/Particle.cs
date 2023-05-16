using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public static Particle particle;
    private float lifeTimer;
    private float leftLifeTime;
    private Vector3 velocity;
    private Vector3 defaultScale;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = 0.3f;
        leftLifeTime = lifeTimer;
        defaultScale = transform.localScale;
        float maxVelocity = 5;
        velocity = new Vector3
            (
            Random.Range(-maxVelocity, maxVelocity),
            Random.Range(-maxVelocity, maxVelocity),
            0
            );
    }

    // Update is called once per frame
    public void Update()
    {
        leftLifeTime -= Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        transform.localScale = Vector3.Lerp
            (
            new Vector3(0, 0, 0),
            defaultScale,
            leftLifeTime / lifeTimer
            );
        if (leftLifeTime <= 0) { Destroy(gameObject); }
    }
}
