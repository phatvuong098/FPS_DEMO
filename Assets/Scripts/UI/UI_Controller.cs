using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] TMP_Text txtClipSize, txtPlayerHealth, txtEnemycount;
    [SerializeField] Image imgGun;
    [SerializeField] Sprite spRife, spHand;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] RectTransform crossHair;
    [SerializeField] float baseCrossHairSize;

    private bool isRife;
    private int enemyCount = 5;
    private int currentEnemyCount = 0;

    public static Action EnemyDead;

    private void Start()
    {
        isRife = true;
        imgGun.sprite = spRife;
        endGamePanel.SetActive(false);
        txtEnemycount.text = "Remain enemy: " + (enemyCount - currentEnemyCount).ToString();
    }

    private void OnEnable()
    {
        PlayerInput.EventChangeGun += OnChangeGun;
        EnemyDead += EnemyDeadHandler;
    }

    private void OnChangeGun()
    {
        isRife = !isRife;
        imgGun.sprite = isRife ? spRife : spHand;
        imgGun.SetNativeSize();
    }

    private void EnemyDeadHandler()
    {
        ++currentEnemyCount;
        txtEnemycount.text = "Remain enemy: " + (enemyCount - currentEnemyCount).ToString();

        if (currentEnemyCount >= enemyCount)
        {
            StartCoroutine("WaitCoro");
        }
    }

    IEnumerator WaitCoro()
    {
        yield return new WaitForSeconds(5);
        Time.timeScale = 0;
        endGamePanel.SetActive(true);
    }

    private void OnDisable()
    {
        EnemyDead -= EnemyDeadHandler;
        PlayerInput.EventChangeGun -= OnChangeGun;
    }

    public void UpdateBullet(int current, int total)
    {
        txtClipSize.text = current.ToString() + "/" + total.ToString();
    }

    public void BTN_FireDown()
    {
        PlayerInput.EventFire(true);
    }

    public void BTN_FireUp()
    {
        PlayerInput.EventFire(false);
    }

    public void BTN_Aim()
    {
        PlayerInput.EventAim();
    }

    public void BTN_Reload()
    {
        PlayerInput.EventReload();
    }

    public void BTN_ChangeGun()
    {
        PlayerInput.EventChangeGun();
    }

    public void UpdatePlayerHealth(int currentHealth)
    {
        txtPlayerHealth.text = "Health:" + currentHealth.ToString();
    }

    public void EndGame()
    {
        endGamePanel.gameObject.SetActive(true);
    }

    public void BTN_ReloadGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    //TODO: CrossHair chưa làm
    //public void UpdateCrossHair(float accuracy)
    //{
    //    crossHair.sizeDelta += accuracy * Vector2.one;
    //}
}
