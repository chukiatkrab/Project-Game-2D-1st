using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageTriggerLevel2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ตรวจสอบว่า GameObject ที่ชนคือ Player
        {
            // เปลี่ยนไปยัง Level 3
            SceneManager.LoadScene("BossFight");
        }
    }
}
