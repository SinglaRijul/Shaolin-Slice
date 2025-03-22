using UnityEngine;

public class NoteController : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    [SerializeField] int identifier;
    void Start()
    {
        
    }


    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }


    public int GetId()
    {
        return identifier;
    }
    
}
