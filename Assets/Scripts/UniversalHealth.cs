using UnityEngine;

public class UniversalHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int minHealth;
    [SerializeField] private int currHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        currHealth -= damageAmount;
        if (currHealth < minHealth)
        {
            Destroy(gameObject);
        }
    }
}
