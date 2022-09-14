using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRigid = null;

    [SerializeField]
    float speed = 6f;

    public Animator playerAnim;
    int floorMask;

    float camRayLength = 100f;

    public VariableJoystick moveJoystick = null, rotateJoystick = null;

    public Rigidbody rb = null;

    PlayerShooting playerShooting = null;

    public GameObject mainManager = null;

    float rotateVertical;
    float rotateHorizontal;

    [SerializeField]
    Transform arrowObject = null;

    public float timeBetweenBullet = 0.15f;

    float timer;

    // Start is called before the first frame update
    private void Start()
    {
        timeBetweenBullet = GameManager.data.dataWeapons[GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexWeapon].rateOfFire;
        playerRigid = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
        //arrowObject = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<Transform>();
        playerAnim = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<Animator>();
        playerShooting = GameObject.Find("FirePoint").GetComponent<PlayerShooting>();
    }
    // Update is called once per frame

    private void Update()
    {
        timer += Time.deltaTime;

        Vector3 moveDirection = Vector3.forward * moveJoystick.Vertical + Vector3.right * moveJoystick.Horizontal;
        Vector3 direction = Vector3.forward * rotateJoystick.Vertical + Vector3.right * rotateJoystick.Horizontal;

        transform.Translate(moveDirection * speed * Time.deltaTime);

        rotateHorizontal = direction.x * 100f;
        rotateVertical = direction.z * 100f;

        Animating(moveDirection.x, moveDirection.y);

        if (direction.x > 0 && direction.z > 0)
        {
            arrowObject.rotation = Quaternion.Euler(0, (rotateHorizontal - 9), 0);

        }
        else if (direction.x > 0 && direction.z < 0)
        {
            arrowObject.rotation = Quaternion.Euler(0, (81f + (-rotateVertical)), 0);

        }
        else if (direction.x < 0 && direction.z < 0)
        {
            arrowObject.rotation = Quaternion.Euler(0, (171f + (-rotateHorizontal)), 0);

        }
        else if (direction.x < 0 && direction.z > 0)
        {
            arrowObject.rotation = Quaternion.Euler(0, (261f + rotateVertical), 0);

        }

        if (direction.x != 0 || direction.y != 0)
        {
            if (timer >= timeBetweenBullet)
            {
                timer = playerShooting.Shoot();
            }
        }
    }
    void FixedUpdate()
    {

        
    }
    /*
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigid.MovePosition(transform.position + movement);

        
    }
    */
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigid.MoveRotation(newRotation);
        }
    }
    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        playerAnim.SetBool("IsWalking", walking);
    }
}
