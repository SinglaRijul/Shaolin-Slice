using UnityEngine;

public class NoteController : MonoBehaviour
{
    [HideInInspector] public float speed ;

    [SerializeField] int identifier;

    void Update()
    {
        transform.position -= new Vector3(0f , speed * Time.deltaTime , 0f);

        if(transform.position.y < -5f) {Destroy(this.gameObject , 0.1f);}
    }


    public int GetId()
    {
        return identifier;
    }
    
}
