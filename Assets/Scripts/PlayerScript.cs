using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    public Vector2 speed = new Vector2(5, 5);

    // 2 - Store the movement
    private Vector2 movement;
    private Rigidbody2D Rigidbody;
    private Vector2 shipCenter;

    //focus movement: hold shift for slow movement. not used because PC. 
    private bool isFocus = false;
    float inputX = 0;
    float inputY = 0;

    void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    void Update()
    {
        //instantiate variables
        shipCenter = GetComponent<Rigidbody2D>().position;
        Vector2 deltaTouchPosition = new Vector2(0, 0);
        
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {

                deltaTouchPosition = Input.GetTouch(i).deltaPosition;
                inputX = deltaTouchPosition.x / 10;
                inputY = deltaTouchPosition.y / 10;

            }

            //move sprite towards touch point.  
            else if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Stationary)
            {
                Vector2 touchPosition = Input.GetTouch(i).position;
                Vector2 target = Camera.main.ScreenToWorldPoint(touchPosition);

                inputX = (target.x - shipCenter.x) / 5;
                inputY = (target.y - shipCenter.y) / 5;
                
                //speed limiter
                if (inputX > 5)
                {
                    inputX = 5;
                }

                if (inputY > 5)
                {
                    inputY = 5;
                }

            }
            //clean up speed when ended. 
            else if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                inputX = 0;
                inputY = 0;
            }
        }

        //debug
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))//(Input.GetTouch(i).phase == TouchPhase.Stationary)
        {
            Vector2 touchPosition = Input.mousePosition;
            Vector2 target = Camera.main.ScreenToWorldPoint(touchPosition);

            //Debug.Log("touchPosition" + touchPosition);
            //Debug.Log("world touchposition" + target);
            //Debug.Log(shipCenter);

            inputX = (target.x - shipCenter.x) * 5; 
            inputY = (target.y - shipCenter.y) * 5;
            Debug.Log(inputX);
            Debug.Log(inputY);
            

        }
        #endif

        //disable focus.  This is only for PC... 
        /*if (Input.GetKeyDown("left shift"))
        {
            isFocus = true;
        }
        else if (Input.GetKeyUp("left shift"))
        {
            isFocus = false;
        }*/


        // 4 - Movement per direction
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);

        if (isFocus)
        {
            movement.x = 2 * inputX;
            movement.y = 2 * inputY;
        }
    }

    void FixedUpdate()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        // 5 - Move the game object
        Rigidbody.velocity = movement;

    }
}