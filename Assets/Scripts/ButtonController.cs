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

    ConductorScript conductorScript;

    GameObject currNote= null;

    int didPressButton = 0; // -1 indicated wrong button pressed, 0 indicates no button pressed , 1 indicated correct button pressed

    string notesTag = "Notes";
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        conductorScript = FindAnyObjectByType<ConductorScript>();

    }

    void Update()
    {


        if(Input.GetKeyDown(keyPressed) && canBePressed)
        {
            //corr press
            theSR.color = Color.green;
            canBePressed = false;
            if(currNote!=null) {Destroy(currNote);}

            didPressButton = 1;

            //set plaeyr sprite for corr press
            conductorScript.SetPlayerSprite(currId);

        }
        else if( (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.RightArrow) ) && canBePressed)
        {
            //wrong press
            theSR.color = Color.red;
             //Log($"correct id: {currId} , pressed id {}");
            canBePressed = false;
            didPressButton = -1;

            //set playersprite for wrong press
            conductorScript.SetPlayerSprite( Input.GetKeyDown(KeyCode.LeftArrow) ? 2 : (Input.GetKeyDown(KeyCode.DownArrow) ? 3 : (Input.GetKeyDown(KeyCode.UpArrow) ? 1 : (Input.GetKeyDown(KeyCode.RightArrow) ? 4 : -1))) );

            Invoke("ResetColor" , 0.2f);
            
        }

        if (Input.GetKeyUp(keyPressed))
        {
            ResetColor();
            
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == notesTag)
        {
            currNote = collision.gameObject;
            noteScript =  collision.gameObject.GetComponent<NoteController>();
            if(noteScript!=null)  currId = noteScript.GetId(); 
                //Debug.Log("script not null");


            canBePressed = true;
            
            //change npc sprite
            conductorScript.SetNpcSprite(currId);

        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == notesTag)
        {
            canBePressed = false;
            currNote = null;

            if(didPressButton==0)
            {
                theSR.color = Color.red;
                Invoke("ResetColor" , 0.05f);   

            }

            didPressButton = 0;
            conductorScript.InitSprites();
        }
    }

    void ResetColor()
    {
        theSR.color = Color.black;
    }

}
