using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PlayerCanvas playerCanvas;
    [SerializeField] private GameObject[] skinsByLvl;
    [SerializeField] private Animator animator;
    private GameObject _currentSkin;
    
    private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;
    private bool _isTurnPlayer;
    private int _currentLvl;
    private List<LevelUpInfo> _levelUpInfos;
    private bool _isMove;

    private Vector3 target2Position;
    private Vector3 target3Position;

    private void Start()
    {
        _levelUpInfos = Resources.LoadAll<LevelUpInfo>("ScriptableObject/PlayerLVL").ToList();
        _currentSkin = skinsByLvl[0];
        _currentLvl = 0;
        OnLvlUp();
        playerCanvas.OnNeedLvlUp += OnLvlUp;
        playerCanvas.OnNeedLvlDown += OnLvlDown;
    }

    private void OnLvlDown(float value)
    {        
        _currentLvl--;
        if (_currentLvl <= 0)
        {
            _currentLvl = 0;
            OnLvlUp();
            return;
        }
        
        var levelsInfo = _levelUpInfos.Find(obj => _currentLvl == obj.Lvl);
        if (levelsInfo)
        {
            playerCanvas.Initialized(levelsInfo, Mathf.Abs(levelsInfo.ValueToNextLvl + value));
            ChangeSkin();
        }
    }

    private void OnLvlUp()
    {
        _currentLvl++;
        var levelsInfo = _levelUpInfos.Find(obj => _currentLvl == obj.Lvl);
        if (levelsInfo)
        {
            playerCanvas.Initialized(levelsInfo);
            ChangeSkin();
        }
    }
    
    private void ChangeSkin()
    {
        _currentSkin.SetActive(false);
        _currentSkin = skinsByLvl[_currentLvl - 1];
        _currentSkin.SetActive(true);
        
    }
    
    public void SetWaypoints(List<Transform> waypoints)
    {
        _waypoints = waypoints.ToArray();
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            _isMove = true;
            animator.StopPlayback();
            animator.SetBool("Sad Walk", true);
        }
        
        if(_isMove)
        {
            MoveToNextWaypoint();
            if (_isTurnPlayer)
            {
                TurnPlayer();
            }
        }
    }
    
    private void MoveToNextWaypoint()
    {
        if(_waypoints == null || _waypoints.Length <=0) return;
        
        
        if (_currentWaypointIndex < _waypoints.Length)
        {
            Transform target = _waypoints[_currentWaypointIndex];
            float turnThreshold = 2.5f;
                
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (transform.position == target.position)
            {
                _currentWaypointIndex++;
                return;
            }
            
            if (_currentWaypointIndex < _waypoints.Length - 1)
            {

                if (distanceToTarget <= turnThreshold && !_isTurnPlayer)
                {
                    _isTurnPlayer = true;
                    Transform nextTarget = _waypoints[_currentWaypointIndex + 1];
                    Vector3 directionToNext = (nextTarget.position - transform.position).normalized;
                    target2Position = target.position;
                    target3Position = target.position + directionToNext * 2.5f;
                }
                else if(!_isTurnPlayer)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

        }
    }
    
    private void TurnPlayer()
    {
        Transform nextTarget = _waypoints[_currentWaypointIndex + 1];
        
        target2Position = Vector3.MoveTowards(target2Position, target3Position, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, target2Position, speed * Time.deltaTime);
        
        Vector3 directionToNext = (nextTarget.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToNext);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        
        float distanceToTarget = Vector3.Distance(transform.position, target3Position);
        
        if (distanceToTarget < 0.1)
        {
            _isTurnPlayer = false;
            _currentWaypointIndex++;
        }
    }

    public void AddValue(float value)
    {
        playerCanvas.AddValue(value);
    }

    private void OnDestroy()
    {
        playerCanvas.OnNeedLvlUp -= OnLvlUp;
        playerCanvas.OnNeedLvlDown -= OnLvlDown;
    }
}
