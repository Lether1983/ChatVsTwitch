using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    float Timer = 0;
    void Start()
    {

    }

    void Update()
    {
        Timer += Time.deltaTime;

        if(Timer > 3)
        {
            this.gameObject.SetActive(false);
            Timer = 0;
        }
        transform.Translate(new Vector3(0.1f, 0, 0));
    }
}
