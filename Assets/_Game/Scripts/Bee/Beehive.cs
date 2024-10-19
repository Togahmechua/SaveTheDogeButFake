using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour
{
    [Header("-------------------Bee List-------------------")]
    [SerializeField] private List<Bee> beeList = new List<Bee>();

    [Header("-------------------SpawnBeeSys-------------------")]
    [SerializeField] private Transform spawnPos;   
    [SerializeField] private Bee bee;              
    [SerializeField] private float amount;         
    [SerializeField] private float spawnDelay;    

    public bool isActive;

    [Header("-------------------Other-------------------")]
    [SerializeField] private PlayerCtrl playerCtrl; 
    [SerializeField] private float rangeToSpawn;    

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (isActive)
        {
            StartCoroutine(SpawnBeesWithDelay());
            isActive = false;
        }
    }

    private void OnInit()
    {
        beeList.Clear();
        isActive = false;
    }

    private void SpawnBeeInDirection(float angle)
    {
        // Tính toán hướng phát tán dựa trên góc (angle)
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

        // Tính toán vị trí spawn của ong dựa trên khoảng cách từ tổ ong
        Vector3 spawnPosition = spawnPos.position + direction * rangeToSpawn;

        // Tạo ong tại vị trí spawn
        Bee b = SimplePool.Spawn<Bee>(bee, spawnPosition, Quaternion.identity);
        beeList.Add(b);
    }

    public void DeleteBee()
    {
        foreach (Bee b in beeList)
        {
            SimplePool.Despawn(b);
        }
        beeList.Clear();
    }

    private IEnumerator SpawnBeesWithDelay()
    {
        float angleStep = 360f / amount;  // Chia vòng tròn thành các phần bằng nhau dựa trên số lượng ong
        float angle = 0f;

        for (int i = 0; i < amount; i++)
        {
            // Gọi hàm SpawnBeeInDirection với góc hiện tại
            SpawnBeeInDirection(angle * Mathf.Deg2Rad);  // Chuyển đổi góc từ độ sang radian

            // Cập nhật góc cho lần spawn tiếp theo
            angle += angleStep;

            // Đợi một thời gian trước khi spawn con ong tiếp theo
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnDrawGizmos()
    {
        // Vẽ hình cầu để hiển thị bán kính phát tán ong trong scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPos.position, rangeToSpawn);
    }
}
