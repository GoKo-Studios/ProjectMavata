using UnityEngine;

public class UIManager : MonoBehaviour
{
    private HealthbarController m_healthbarController;
    private StaminabarController m_staminabarController;
    private TimerController m_timerController;
    private ScoreIndicatorController m_scoreController;
    private RoundIndicatorController m_roundController;
    private InteractionIndicatorController m_interactionController;

    private void Awake(){
        m_healthbarController = FindObjectOfType<HealthbarController>();
        m_staminabarController = FindObjectOfType<StaminabarController>();
        m_timerController = FindObjectOfType<TimerController>();
        m_scoreController = FindObjectOfType<ScoreIndicatorController>();
        m_roundController = FindObjectOfType<RoundIndicatorController>();
        m_interactionController = FindObjectOfType<InteractionIndicatorController>();
    }

    private void Start(){
        EventManager.Instance.HealthChanged_P1 += OnHealthChanged_P1;
        EventManager.Instance.HealthChanged_P2 += OnHealthChanged_P2;
        EventManager.Instance.StaminaChanged_P1 += OnStaminaChanged_P1;
        EventManager.Instance.StaminaChanged_P2 += OnStaminaChanged_P2;
        EventManager.Instance.BlockChanged_P1 += OnBlockChanged_P1;
        EventManager.Instance.BlockChanged_P2 += OnBlockChanged_P2;
        EventManager.Instance.Interaction_P1 += OnInteraction_P1;
        EventManager.Instance.Interaction_P2 += OnInteraction_P2;
        EventManager.Instance.TimeChanged += OnTimeChanged;
        EventManager.Instance.ScoreChanged += OnScoreChanged;
        EventManager.Instance.RoundChanged += OnRoundChanged;
    }

    private void OnDisable(){
        EventManager.Instance.HealthChanged_P1 -= OnHealthChanged_P1;
        EventManager.Instance.HealthChanged_P2 -= OnHealthChanged_P2;
        EventManager.Instance.StaminaChanged_P1 -= OnStaminaChanged_P1;
        EventManager.Instance.StaminaChanged_P2 -= OnStaminaChanged_P2;
        EventManager.Instance.BlockChanged_P1 -= OnBlockChanged_P1;
        EventManager.Instance.BlockChanged_P2 -= OnBlockChanged_P2;
        EventManager.Instance.Interaction_P1 -= OnInteraction_P1;
        EventManager.Instance.Interaction_P2 -= OnInteraction_P2;
        EventManager.Instance.TimeChanged -= OnTimeChanged;
        EventManager.Instance.ScoreChanged -= OnScoreChanged;
        EventManager.Instance.RoundChanged -= OnRoundChanged;
    }

    #region Healthbar Controller

    private void OnHealthChanged_P1(float value, float maxValue){
        if (!m_healthbarController) return;
        value = value / maxValue;
        m_healthbarController.UpdateHealthbar_P1(value);
    }

    private void OnHealthChanged_P2(float value, float maxValue){
        if (!m_healthbarController) return;
        value = value / maxValue;
        m_healthbarController.UpdateHealthbar_P2(value);
    }

    #endregion

    #region Staminabar Controller

    private void OnStaminaChanged_P1(float value, float maxValue){
        if(!m_staminabarController) return;
        value = value / maxValue;
        m_staminabarController.UpdateStaminabar_P1(value);
    }

    private void OnBlockChanged_P1(float value, float maxValue){
        if(!m_staminabarController) return;
        value = value / maxValue;
        m_staminabarController.UpdateBlock_P1(value);
    }

    private void OnStaminaChanged_P2(float value, float maxValue){
        if(!m_staminabarController) return;
        value = value / maxValue;
        m_staminabarController.UpdateStaminabar_P2(value);
    }

    private void OnBlockChanged_P2(float value, float maxValue){
        if(!m_staminabarController) return;
        value = value / maxValue;
        m_staminabarController.UpdateBlock_P2(value);
    }

    #endregion

    private void OnTimeChanged(int value){
        if (!m_timerController) return;
        m_timerController.UpdateTimer(value);
    }

    private void OnScoreChanged(Player player, int value){
        if (!m_scoreController) return;
        m_scoreController.UpdateScore(player, value);
    }

    private void OnRoundChanged(int value, int maxValue){
        if (!m_roundController) return;
        m_roundController.UpdateText(value, maxValue);
    }

    private void OnInteraction_P1(Interactions value){
        if (!m_interactionController) return;
        m_interactionController.UpdateText_P1(value);
    }

    private void OnInteraction_P2(Interactions value){
        if (!m_interactionController) return;
        m_interactionController.UpdateText_P2(value);
    }
}
