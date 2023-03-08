using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance
    public static GameManager Instance => instance;
    private static GameManager instance;
    #endregion

    #region Properties
    public PlayerControls PlayerControls => playerControls;
    #endregion

    #region Attributes
    private PlayerControls playerControls;
    #endregion

    private void Awake()
    {
        if(instance == null)
            instance = this;

        playerControls = new PlayerControls();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
