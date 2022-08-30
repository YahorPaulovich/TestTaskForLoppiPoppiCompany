using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    [Header("Input Settings: ")]
    [SerializeField] private LayerMask _boxesLayerMask;
    [SerializeField] private float _touchRadius;
    private PlayerInput _playerInput;
    private PlayerInputActions _playerInputActions;

    [Header("Mark Sprites: ")]
    [SerializeField] private Sprite _spriteX;
    [SerializeField] private Sprite _spriteO;

    [Header("Mark Colors: ")]
    [SerializeField] private Color _colorX;
    [SerializeField] private Color _colorO;

    [Header("Sounds of the game of tic-tac-toe: ")]
    public AudioSource AudioSource;
    [SerializeField] private AudioClip _clickSound;
    public AudioClip _soundOfPressButton;
    [SerializeField] private AudioClip _soundOfWinning;

    public UnityAction<Mark, Color> OnWinAction;

    [SerializeField]  private Camera _camera;

    public Mark[] Marks;
    private Mark _currentMark;
    private int _marksCount = 0;

    private bool _canPlay;
    private LineRenderer _lineRenderer;
    

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Click.performed += Click;

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
    }
    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        SetTransparencyForSprites();
        _currentMark = Mark.X;
        Marks = new Mark[9];
        _canPlay = true;
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (_canPlay)
        {
            Vector2 touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapCircle(touchPosition, _touchRadius, _boxesLayerMask);

            if (hit)
            {
                HitBox(hit.GetComponent<Box_>());
            }
        }     
    } 

    private void HitBox(Box_ box)
    {
        if (!box.IsMarked)
        {
            Marks[box.Index] = _currentMark;

            box.SetAsMarked(GetSprite(), _currentMark, GetColor());
            _marksCount++;
            AudioSource.PlayOneShot(_clickSound);

            bool won = CheckIfWin();
            if (won)
            {
                if (OnWinAction != null)
                {
                    OnWinAction.Invoke(_currentMark, GetColor());
                }
                Debug.Log(_currentMark.ToString() + " Wins.");

                _canPlay = false;
                return;
            }

            if (_marksCount == 9)
            {
                if (OnWinAction != null)
                {
                    OnWinAction.Invoke(Mark.None, Color.white);
                }
                Debug.Log("Nobody Wins.");

                _canPlay = false;
                return;
            }

            SwitchPlayer();
        }
    }

    private bool CheckIfWin()
    {
        return
            AreaBoxesMatched(0, 1, 2) ||
            AreaBoxesMatched(3, 4, 5) ||
            AreaBoxesMatched(6, 7, 8) ||
            AreaBoxesMatched(0, 3, 6) ||
            AreaBoxesMatched(1, 4, 7) ||
            AreaBoxesMatched(2, 5, 8) ||
            AreaBoxesMatched(0, 4, 8) ||
            AreaBoxesMatched(2, 4, 6);
    }

    private bool AreaBoxesMatched(int i, int j, int k)
    {
        Mark mark = _currentMark;
        bool matched = Marks[i] == mark && 
            Marks[j] == mark && 
            Marks[k] == mark;

        if (matched)
        {
            DrawLine(i, k);
        }

        return matched;
    }

    private void DrawLine(int i, int k)
    {
        _lineRenderer.SetPosition(0, transform.GetChild(i).position);
        _lineRenderer.SetPosition(1, transform.GetChild(k).position);

        Color color = GetColor();
        color.a = .3f;
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;

        _lineRenderer.enabled = true;
        AudioSource.PlayOneShot(_soundOfWinning);
    }

    private void SwitchPlayer()
    {
        _currentMark = (_currentMark == Mark.X) ? Mark.O : Mark.X;
    }

    private Color GetColor()
    {
        return (_currentMark == Mark.X) ? _colorX : _colorO;
    }

    private Sprite GetSprite()
    {
        return (_currentMark == Mark.X) ? _spriteX : _spriteO;
    }

    private void SetTransparencyForSprites()
    {
        _colorX.a = 255;
        _colorO.a = 255;
    }
}
