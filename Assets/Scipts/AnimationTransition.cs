using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTransition : MonoBehaviour
{
    public Canvas canvas;
    public Vector2 initPosition;
    public Vector2 endPosition;

    private RectTransform rect;
    private Vector2 actualSizeCanvas;
    private bool inAnimation;

    // Start is called before the first frame update
    void Start()
    {
        inAnimation = true;
        rect = GetComponent<RectTransform>();
        transform.position = new Vector2(canvas.gameObject.transform.position.x + (canvas.pixelRect.width * initPosition.x), 
                                         canvas.gameObject.transform.position.y + (canvas.pixelRect.height * initPosition.y));
        
        actualSizeCanvas = canvas.pixelRect.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (inAnimation)
        {
            
            if (canvas.pixelRect.height - transform.position.y > 0)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 100);
            }
            if ((transform.position.x == canvas.gameObject.transform.position.x + (canvas.pixelRect.width * endPosition.x)) &&
                (transform.position.y == canvas.gameObject.transform.position.y + (canvas.pixelRect.width * endPosition.y)))
            {
                Debug.Log("HOLA");
                inAnimation = false;
            }
        }
        else
        {
            if (actualSizeCanvas.x != canvas.pixelRect.x)
            {
                transform.position = new Vector2(canvas.gameObject.transform.position.x + canvas.pixelRect.width, 
                    canvas.gameObject.transform.position.y);
            }
            if (actualSizeCanvas.y != canvas.pixelRect.y)
            {
                transform.position = new Vector2(canvas.gameObject.transform.position.x, 
                    canvas.gameObject.transform.position.y + canvas.pixelRect.height);
            }
        
            if (actualSizeCanvas.x != canvas.pixelRect.x || actualSizeCanvas.y != canvas.pixelRect.y)
            {
                actualSizeCanvas = canvas.pixelRect.size;
            }
        }
    }

    public void play(float duration)
    {
        inAnimation = true;
    }

    public bool isInAnimation()
    {
        return inAnimation;
    }
}
