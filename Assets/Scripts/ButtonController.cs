using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{

    [SerializeField] KeyCode keyPressed;
    [SerializeField] int keyId;
    
    [SerializeField] bool canBePressed = false;

    int currId = -1;
    SpriteRenderer theSR;

    NoteController noteScript;

    GameObject currNote= null;
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {


        if(Input.GetKeyDown(keyPressed) && canBePressed)
        {
            //corr press
            theSR.color = Color.green;
            canBePressed = false;
            if(currNote!=null) {Destroy(currNote);}
    
        }
        else if( (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.RightArrow) ) && canBePressed)
        {
            //wrong press
            theSR.color = Color.red;
            Debug.Log($"key pressed {keyPressed}");
            canBePressed = false;

            Invoke("ResetColor" , 0.2f);
            
        }


        if (Input.GetKeyUp(keyPressed))
        {
            ResetColor();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            currNote = collision.gameObject;
            noteScript =  collision.gameObject.GetComponent<NoteController>();
            if(noteScript!=null)  currId = noteScript.GetId(); 
                //Debug.Log("script not null");
            

           canBePressed = true;
            //Debug.Log("Expected id ---> " + currId + "\n");

        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canBePressed = false;
            currNote = null;
        }
    }

    void ResetColor()
    {
        theSR.color = Color.black;
    }

}
