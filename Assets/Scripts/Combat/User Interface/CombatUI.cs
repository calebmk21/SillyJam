using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class CombatUI : MonoBehaviour
{
    public static CombatUI Instance { get; private set; }

    private enum SelectorType {Attack, Ability}
    private SelectorType _selectorType;

    [SerializeField] Button _attackButton;
    [SerializeField] Button _defendButton;
    [SerializeField] Button _abilitiesButton;
    [SerializeField] Button _itemsButton;
    [SerializeField] GameObject _actionMenu;
    [SerializeField] GameObject _subMenu;
    // [SerializeField] List<PlayerUIController> _playerInfoControllers;
    [SerializeField] GameObject _selector;
    [SerializeField] float _selectorOffsetX = 2f;
    //[SerializeField] float _selectorOffsetY = -1f;
    [SerializeField] GameObject _subMenuButtonPrefab;
    [SerializeField] private int _startingButtonPoolSize;
    [Tooltip("Offset relative to active Hero's position.")]
    [SerializeField] private Vector2 _actionMenuOffset;

    // private Camera _camera;
    private bool _isSelectingEnemy = false;
    private bool _isSelectingAlly = false;
    private bool _isInMenu = true;
    private bool _isInSubMenu = false;
    private int _selectorIndex;
    private Ability _selectedAbility;
    // private UISoundHandler _soundHandler;
    private readonly List<Button> _subMenuButtonPool = new List<Button>();

    public delegate void AttackSelectEnemyEventHandler(EnemyUnit enemy);
    public static event AttackSelectEnemyEventHandler OnSelectEnemyAttack;

    public delegate void AbilitySelectEnemyEventHandler(EnemyUnit enemy, Ability ability);
    public static event AbilitySelectEnemyEventHandler OnSelectEnemyAbility;
    
    // Input Actions for Combat UI Navigation
    private InputAction m_navigateUI;
    private InputAction m_Cancel;
    private InputAction m_Submit;
    private Vector2 m_navigationDirection;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // _soundHandler = GetComponent<UISoundHandler>();
        // _camera = Camera.main;

        // Input action control scheme
        // InputSystem.actions.FindActionMap("Combat").Disable();
        InputSystem.actions.FindActionMap("UI").Enable();

        m_navigateUI = InputSystem.actions.FindAction("Navigate");
        m_Cancel = InputSystem.actions.FindAction("Cancel");
        m_Submit = InputSystem.actions.FindAction("Submit");
    }
    
    private void Start()
    {
        if(CombatManager.Instance != null)
        {
            _attackButton.onClick.AddListener(delegate { StartSelectEnemy(SelectorType.Attack); });
            // _defendButton.onClick.AddListener(CombatManager.Instance.ChoseDefend);
            _abilitiesButton.onClick.AddListener(DisplayAbilitiesMenu);
            // _itemsButton.onClick.AddListener(DisplayItemsMenu);
            // InitializePlayerUI();
            InitializeButtonPool(_startingButtonPoolSize);
        }
        else
        {
            Debug.LogError("Battle Manager Instance was not found!");
        }
    }
    
    private void Update()
    {
        //TODO: Use coroutines for all below.      
        if (!_isInMenu)
        {
            MoveEnemySelector();
        }
        //If in the sub menu, pressing escape will take you back to the action menu.
        if(_isInSubMenu && m_Cancel.WasPressedThisFrame())
        {
            _subMenu.SetActive(false);
            _actionMenu.SetActive(true);
            _isInSubMenu = false;
        }

        m_navigationDirection = m_navigateUI.ReadValue<Vector2>();
        // Debug.Log(m_navigationDirection);

    }
    
    
    //Clears button for reuse.
    private void ResetSubButtons()
    {
        foreach(Button button in _subMenuButtonPool)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(true);
        }
    }
    
       private void DisplayAbilitiesMenu()
    {
        _isInSubMenu = true;
        _subMenu.gameObject.SetActive(true);
        PlayerUnit currentHero = CombatManager.Instance.GetCurrentCombatant() as PlayerUnit;
        
        ResetSubButtons();
        
        //Creates more buttons if there's not enough button in the pool.
        // if (PartyManager.Instance.Inventory.Items.Count > currentHero.Abilities.Count)
        // {
        //     int difference = PartyManager.Instance.Inventory.Items.Count - currentHero.Abilities.Count;
        //     for (int i = 0; i < difference; i++)
        //     {
        //         CreateSubMenuButton();
        //     }
        // }

        for(int i = 0; i < _subMenuButtonPool.Count; i++)
        {
            //Buttons that are not assigned an ability are hidden.
            if(i >= currentHero.Abilities.Count)
            {
                _subMenuButtonPool[i].gameObject.SetActive(false);
                continue;
            }

            Ability ability = currentHero.Abilities[i];
            string abilityName = ability.name;

            //Set text on the button.
            var texts = _subMenuButtonPool[i].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in texts)
            {
                if(text.gameObject.CompareTag("UI_Name"))
                {
                    text.SetText(abilityName);
                }
            }

            // use this if we want mana / cooldowns
            // if (manaCost > currentHero.CurrentMana)
            // {
            //     _subMenuButtonPool[i].interactable = false;
            // }

            //Sort out abilities into buttons based on their ability script subclass type.
            if (ability.GetType() == typeof(AttackAbility))
            {
                _subMenuButtonPool[i].onClick.AddListener(delegate { StartSelectEnemy(SelectorType.Ability, ability); });
            }
            // else if(ability.GetType() == typeof(SupportAbility))
            // {
            //     _subMenuButtonPool[i].onClick.AddListener(delegate { BattleManager.Instance.ChoseAbility(ability); _subMenu.SetActive(false); _actionMenu.SetActive(false); });
            // }
            else
            {
                Debug.LogError("Unable to set an ability to a skill button.");
            }
        }
    }
    

    // public void ToggleActionMenu(bool value)
    // {
    //     var currentHero = CombatManager.Instance.GetCurrentCombatant();
    //     Vector3 menuPosition = currentHero.transform.position + new Vector3(-currentHero.MoveDistance,0,0) + (Vector3)_actionMenuOffset;
    //     _actionMenu.transform.position = _camera.WorldToScreenPoint(menuPosition);
    //     _actionMenu.gameObject.SetActive(value);
    // }
    
    // private void InitializePlayerUI()
    // {
    //     // If there is any leftover Hero UIs that don't need to be assigned, they will not be active and displayed.
    //     for (int i = 0; i < _playerInfoControllers.Count; i++)
    //     {
    //         if(i >= CombatManager.Instance.Party.Count)
    //         {
    //             _playerInfoControllers[i].gameObject.SetActive(false);
    //             continue;
    //         }
    //         PlayerUnit unit = CombatManager.Instance.Party[i];
    //         _playerInfoControllers[i].Initialize(unit);
    //     }
    // }
    
    private void InitializeButtonPool(int startingSize)
    {
        for (int i = 0; i < startingSize; i++)
        {
            CreateSubMenuButton();
        }
    }

    private void CreateSubMenuButton()
    {
        var button = Instantiate(_subMenuButtonPrefab, _subMenu.transform, false);
        _subMenuButtonPool.Add(button.GetComponent<Button>());
    }
    
    private void StartSelectEnemy(SelectorType type)
    {
        _selectorType = type;
        _actionMenu.SetActive(false);
        _selector.gameObject.SetActive(true);
        _isSelectingEnemy = true;
        _selector.transform.position = CombatManager.Instance.Enemies[0].gameObject.transform.position + new Vector3(_selectorOffsetX, 0, 0);
        _selectorIndex = 0;
    }
    //Overload for abilities that require targeting enemies.
    private void StartSelectEnemy(SelectorType type, Ability ability)
    {
        _subMenu.gameObject.SetActive(false);
        _selectedAbility = ability;
        StartSelectEnemy(type);
    }
    
    
    private void MoveEnemySelector()
    {
        // Moves selector between enemies; left or right.
        if (_isSelectingEnemy)
        {
            List<EnemyUnit> enemies = CombatManager.Instance.Enemies;
            if (m_navigationDirection == new Vector2(1f, 0f))
            {
                _selectorIndex++;
                // _soundHandler.PlayHighBeep();
                if (_selectorIndex >= enemies.Count)
                {
                    _selectorIndex = 0;
                }
                _selector.transform.position = enemies[_selectorIndex].gameObject.transform.position + new Vector3(_selectorOffsetX, 0, 0);
            }
            else if (m_navigationDirection == new Vector2(-1f, 0f))
            {
                _selectorIndex--;
                // _soundHandler.PlayLowBeep();
                if (_selectorIndex < 0)
                {
                    _selectorIndex = enemies.Count - 1;
                }
                _selector.transform.position = enemies[_selectorIndex].gameObject.transform.position + new Vector3(_selectorOffsetX, 0 , 0);
            }
            else if (m_Submit.WasPressedThisFrame()) //Confirm select current enemy to attack.
            {
                _selector.gameObject.SetActive(false);
                _isSelectingEnemy = false;
                switch (_selectorType)
                {
                    case SelectorType.Attack:
                        if(OnSelectEnemyAttack != null)
                            OnSelectEnemyAttack.Invoke(enemies[_selectorIndex]);
                        break;
                    case SelectorType.Ability:
                        if (OnSelectEnemyAbility != null)
                            OnSelectEnemyAbility.Invoke(enemies[_selectorIndex], _selectedAbility);
                        break;
                    default:
                        Debug.LogError("In unknown selector in Battle UI Handler!");
                        break;
                }
            }
            else if (m_Cancel.WasPressedThisFrame()) //Returns player back to action menu if canceled.
            {
                _actionMenu.gameObject.SetActive(true);
                _selector.gameObject.SetActive(false);
                _isSelectingEnemy = false;
            }
        }
    }
    
}
