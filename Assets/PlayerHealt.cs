using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour   
{
    public int maxHealth = 100; // จำนวน HP สูงสุด
    private int currentHealth; // จำนวน HP ปัจจุบัน

    void Start()
    {
        currentHealth = maxHealth; // ตั้งค่า HP ปัจจุบัน
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
