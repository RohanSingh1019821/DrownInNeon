using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.01f;
    Material myMaterial;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        //offset = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        offset = new Vector2(moveX, moveY).normalized;

        myMaterial.mainTextureOffset += offset * backgroundScrollSpeed * Time.deltaTime;

    }
}




