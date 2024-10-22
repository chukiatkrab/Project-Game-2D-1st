using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerClimbing : MonoBehaviour
{
    public float climbSpeed = 5f; // ความเร็วในการปีนบันได
    private bool isClimbing = false; // ตรวจสอบว่าผู้เล่นอยู่ในระยะบันไดหรือไม่
    private Rigidbody2D rb; // ใช้สำหรับควบคุมฟิสิกส์
    private float originalGravity; // เก็บค่าแรงโน้มถ่วงเริ่มต้น

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ดึง component Rigidbody2D ของตัวละคร
        originalGravity = rb.gravityScale; // เก็บค่าแรงโน้มถ่วงเริ่มต้น
    }

    void Update()
    {
        // ถ้าผู้เล่นอยู่ในระยะบันได
        if (isClimbing)
        {
            // ถ้าผู้เล่นกดปุ่ม W จะปีนขึ้น
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(rb.velocity.x, climbSpeed); // ปีนขึ้น
                Debug.Log("Climbing Up!");
            }
            // ถ้าผู้เล่นกดปุ่ม S จะปีนลง
            else if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(rb.velocity.x, -climbSpeed); // ปีนลง
                Debug.Log("Climbing Down!");
            }
            else
            {
                // หยุดเคลื่อนที่เมื่อไม่กด W หรือ S
                rb.velocity = new Vector2(rb.velocity.x, 0); // หยุด
                rb.gravityScale = originalGravity; // เปิดแรงโน้มถ่วงเมื่อไม่ได้ปีน
            }

            // ปิดแรงโน้มถ่วงระหว่างปีนบันได
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                rb.gravityScale = 0; // ปิดแรงโน้มถ่วงเมื่อปีน
            }
        }
        else
        {
            // คืนค่าแรงโน้มถ่วงเมื่อไม่ได้ปีนบันได
            rb.gravityScale = originalGravity;
        }
    }

    // เมื่อผู้เล่นเข้าสู่ระยะบันได
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder")) // ตรวจสอบว่าชนบันไดหรือไม่
        {
            Debug.Log("Touching Ladder");
            isClimbing = true; // เปิดการปีนบันได
        }
    }

    // เมื่อผู้เล่นออกจากระยะบันได
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder")) // ตรวจสอบว่าผู้เล่นออกจากบันไดหรือไม่
        {
            Debug.Log("Left Ladder");
            isClimbing = false; // ปิดการปีนบันได
            rb.gravityScale = originalGravity; // เปิดแรงโน้มถ่วงเมื่อออกจากบันได
        }
    }
}
