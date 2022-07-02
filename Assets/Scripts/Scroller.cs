using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField]
    float speed = 50f;

    bool crawling = false;
    float topStopPoint;

    // Start is called before the first frame update
    void Start()
    {
        crawling = true;
        topStopPoint = (Screen.height * 0.5f) - (Screen.height * 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!crawling)
        {
            return;
        }
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (gameObject.transform.position.y > topStopPoint)
        {
            crawling = false;
        }
    }
}
