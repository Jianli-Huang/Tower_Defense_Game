using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy;

    // Use this for initialization
    void Start()
    {
        GetComponent<Slider>().maxValue = enemy.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy)
        {
            GetComponent<Slider>().value = enemy.health;
            UtilityMethods.MoveUiElementToWorldPosition(GetComponent<RectTransform>(), enemy.transform.position + new Vector3(0, 0, 1));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
