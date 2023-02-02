using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
    [SerializeField]
    LayerMask layer; //Hangi nesnenin üstündeyken zıplayayım?
    [SerializeField]
    bool isG = true;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float jumpSpeed = 8f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (PlayerController.isStartGame == false)
        {
            return;
        }
        hit();
        Jump();
        
    }
    bool hit()
    {
        RaycastHit2D isgrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, layer);//Raycast2D = Ayaktan yere ışın çizmeye yarar.
        if (isgrounded.collider != null)//Bir şeye çarpıyorsa
        {
            isG = true;
        }
        else
        {
            isG = false;
        }
        return isG;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isG == true)//(Input.GetMouseButtonDown(0) && isG == true) sol click = 0 sağ = 1  Kaydıraç=2 
        {
            rb.velocity = new Vector2(0, jumpSpeed);//(rb.velocity.x, jumpSpeed * Time.deltaTime)
        }
    }
}
