using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // ต้องเพิ่มบรรทัดนี้ถ้าใช้ TextMeshPro

public class PlayerScore : MonoBehaviour
{
    public int score = 0; // ตัวแปรเก็บคะแนนเริ่มต้น
    public TextMeshProUGUI scoreText; // เปลี่ยนประเภทเป็น TextMeshProUGUI

    void Start()
    {
        UpdateScoreText(); // เรียกใช้เพื่ออัปเดตคะแนนตอนเริ่มเกม
        scoreText.text = "Score: " + Scoring.tatalScore;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าผู้เล่นชนกับไอเท็ม (Gem)
        if (collision.CompareTag("Gem"))
        {
            Scoring.tatalScore += 10;
            scoreText.text = "Score: " + Scoring.tatalScore;
            Debug.Log(score);
            Destroy(collision.gameObject); // ทำลายไอเท็มที่เก็บแล้ว
        }
    }

    void UpdateScoreText()
    {
        // อัปเดตคะแนนใน UI TextMeshPro
        scoreText.text = "Score: " + score.ToString();
    }
}
