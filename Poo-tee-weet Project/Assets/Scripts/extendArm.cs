using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extendArm : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.size = new Vector2(spriteRenderer.size.x + 0.1f, spriteRenderer.size.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.size = new Vector2(spriteRenderer.size.x - 0.1f, spriteRenderer.size.y);
        }
    }
}
