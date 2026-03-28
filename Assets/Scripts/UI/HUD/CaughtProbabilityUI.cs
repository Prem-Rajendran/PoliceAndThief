using UnityEngine;
using TMPro;

public class CaughtProbabilityUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI probabilityText;

    [Header("Detection Settings")]
    [SerializeField] private float maxDetectionRange = 20f;
    [SerializeField] private float dangerRange = 5f;

    private Transform m_Follower;  // Police
    private Transform m_Target;    // Thief

    public void SetupCaughtProbabilityUI(Transform follower, Transform target)
    {
        m_Follower = follower;
        m_Target = target;
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (m_Follower == null || m_Target == null) return;

        float probability = CalculateProbability();
        UpdateUI(probability);
    }

    private float CalculateProbability()
    {
        float distance = Vector3.Distance(m_Follower.position, m_Target.position);

        if (distance >= maxDetectionRange) return 0f;
        if (distance <= dangerRange) return 1f;

        return 1f - ((distance - dangerRange) / (maxDetectionRange - dangerRange));
    }

    private void UpdateUI(float probability)
    {
        int percent = Mathf.RoundToInt(probability * 100);
        probabilityText.text = $"Caught: {percent}%";
        probabilityText.color = GetProbabilityColor(probability);
    }

    private Color GetProbabilityColor(float probability)
    {
        if (probability < 0.5f)
            return Color.Lerp(Color.green, Color.yellow, probability * 2f);
        else
            return Color.Lerp(Color.yellow, Color.red, (probability - 0.5f) * 2f);
    }

    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }
}