using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    public void SpawnCustomers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject customer = Instantiate(customerPrefab, transform);
            Customer controller = customer.GetComponent<Customer>();
            if (controller != null)
            {
                controller.Visuals.SetSortingOrder(i);
            }
        }
    }
    
    #if UNITY_EDITOR_WIN
    [ContextMenu("Test Spawn")]
    public void Test()
    {
        SpawnCustomers(10);
    }
    #endif
}