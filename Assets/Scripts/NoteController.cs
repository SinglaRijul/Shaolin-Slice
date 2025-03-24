using UnityEngine;

public class NoteController : MonoBehaviour
{
    [HideInInspector] public float speed ;

    [SerializeField] int identifier;

    Vector2 screenBounds;


    void Start()
    {
        screenBounds = new Vector2(Screen.width , Screen.height);
    }


    void Update()
    {
        //transform.position += Vector3.down * speed * Time.deltaTime;
        transform.position -= new Vector3(0f , speed * Time.deltaTime , 0f);

        if(transform.position.y <0) {Destroy(this.gameObject , 0.1f);}
    }


    public int GetId()
    {
        return identifier;
    }
    
}
