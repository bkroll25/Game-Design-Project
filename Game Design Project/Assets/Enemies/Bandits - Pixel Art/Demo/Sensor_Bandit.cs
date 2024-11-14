using UnityEngine;
using System.Collections;

public class Sensor_Bandit : MonoBehaviour {

    private int m_ColCount = 0;
    private BoxCollider2D m_box2d;
    private float m_DisableTimer;

    private void OnEnable()
    {
        m_ColCount = 0;
        m_box2d = GetComponent<BoxCollider2D>();
    }

    public bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Sensor") && !other.CompareTag("Player"))
        {
            m_ColCount++;
            UnityEngine.Debug.Log("ADD to cols: " + m_ColCount);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Sensor") && !other.CompareTag("Player"))
        {
            m_ColCount--;
            UnityEngine.Debug.Log("REMOVE to cols: " + m_ColCount);
        }
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }

    public void SetOffset(float x, float y)
    {
        m_box2d.offset = new Vector2(x, y);
    }

    public void Swap()
    {
        m_ColCount--;
    }
}
