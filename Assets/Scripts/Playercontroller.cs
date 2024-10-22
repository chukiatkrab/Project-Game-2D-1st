using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpspeed = 8f;
    private float direction = 0f;

    public float fallMultiplier = 2.5f;  // ค่านี้ใช้เพื่อเพิ่มความเร็วในการตกหลังจากกระโดด
    public float lowJumpMultiplier = 2f; // ค่านี้ใช้เพื่อเพิ่มการควบคุมความสูงของการกระโดด

    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator playerAnimation;
    public string sceneName;


    public int maxHealth = 100; // จำนวน HP สูงสุด
    private int currentHealth; // จำนวน HP ปัจจุบัน
    
    //------------------------------------------------------------------------
     private bool canDoubleJump; // ตัวแปรใหม่สำหรับ double jump
    //------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck. position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if(direction > 0f ) 
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y );
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (direction < 0f) 
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y );
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y );
        }

        if(Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpspeed);
        }
        
        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);

        //---------------------------------------------------------------------------
        if (player.velocity.y < 0)
        {
        // ทำให้ตกลงมาเร็วขึ้นถ้าไม่ได้กดปุ่มกระโดดค้าง
        player.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (player.velocity.y > 0 && !Input.GetButton("Jump"))
        {
        // ลดความเร็วขณะกระโดดขึ้นถ้าปล่อยปุ่มกระโดด
        player.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        {
        // เรียกใช้ฟังก์ชัน HandleDoubleJump()
        HandleDoubleJump();
        }
        
    }
        //-----------------------------------------------------------------------------  
           
        // ตัวแปรใหม่สำหรับการกระโดดสองรอบ
    // ฟังก์ชันสำหรับการกระโดดสองรอบ
void HandleDoubleJump()
{
    // ถ้าตัวละครสัมผัสพื้น ให้รีเซ็ต double jump
    if (isTouchingGround)
    {
        canDoubleJump = true; // สามารถ double jump ได้อีกครั้งเมื่อแตะพื้น
    }

    // การกระโดดครั้งแรก
    if (Input.GetButtonDown("Jump") && isTouchingGround)
    {
        player.velocity = new Vector2(player.velocity.x, jumpspeed);
    }
    // การกระโดดครั้งที่สอง (double jump)
    else if (Input.GetButtonDown("Jump") && canDoubleJump && !isTouchingGround)
    {
        player.velocity = new Vector2(player.velocity.x, jumpspeed);
        canDoubleJump = false; // ใช้ double jump แล้วจะไม่สามารถกระโดดได้อีกจนกว่าจะลงพื้น
    }
}
public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ลด HP ปัจจุบัน

        Debug.Log("Player takes damage: " + damage + ". Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // ถ้า HP ต่ำกว่าหรือเท่ากับ 0 ให้เรียกใช้ฟังก์ชัน Die()
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // เพิ่มโค้ดเพิ่มเติมที่นี่ เช่น การเล่นอนิเมชั่นการตายหรือการรีเซ็ตเกม
    }
    
}