using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [SerializeField] KeyCode keyPressed;
    [SerializeField] int keyId;
    
    bool canBePressed = false;

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
                theSR.color = Color.white;
                canBePressed = false;
                if(currNote!=null) {Destroy(currNote);}
    
        }
        
        if (Input.GetKeyUp(keyPressed))
        {
            theSR.color = Color.black;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            currNote = collision.gameObject;
            noteScript =  collision.gameObject.GetComponent<NoteController>();
            if(noteScript!=null) { currId = noteScript.GetId(); 
                //Debug.Log("script not null");
            }

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
}
