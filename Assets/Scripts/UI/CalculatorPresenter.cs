using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Linq;

public class CalculatorPresenter : MonoBehaviour
{
    private Calculator _calculator;
    private BinarySaver Saver;

    [SerializeField] private GameObject _sorryDialogueCanvas;
    [SerializeField] private GameObject _inputOutputCanvas;

    [SerializeField] private Button _newEquation;
    [SerializeField] private Button _quit;

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _result;


    private void Awake()
    {
        _calculator = new Calculator();
        Saver = new BinarySaver();
        LoadEquation();
    }

    private void OnEnable() 
    {
        _inputField.onValueChanged.AddListener(OnValueChanged);
        _result.onClick.AddListener(Calculate);

        _newEquation.onClick.AddListener(NewEquation);
        _quit.onClick.AddListener(Quit);
    }

    private void OnDisable() 
    {
        _inputField.onValueChanged.RemoveListener(OnValueChanged);
        _result.onClick.RemoveListener(Calculate);

        _newEquation.onClick.RemoveListener(NewEquation);
        _quit.onClick.RemoveListener(Quit);
    }

    public void OnValueChanged(string arg0)
    {
        if(arg0.Length == 1 && arg0 == "/")
        {
            _inputField.text = "0/";
            _inputField.selectionStringAnchorPosition = 0;
            return;
        }

        if(arg0.Length > 1)
        {
            char lastChar = arg0[arg0.Length - 1];
            char secondLastChar = arg0[arg0.Length - 2];
        
            if(lastChar == '/' && secondLastChar == '/')
            {
                _inputField.text = arg0.Remove(arg0.Length - 1);
            }
        }

        int count = arg0.ToCharArray().Where(i => i == '/').Count();
        if(count >= 2)
        {
            _inputField.text = arg0.Remove(arg0.Length - 1);

            count = arg0.ToCharArray().Where(i => i == '/').Count();
            if(count == 1)
            {
                Calculate();
            }           
        }

        char[] chars = arg0.ToCharArray();
        
        for(int i = 0; i < chars.Length; i++)
        {
            if(!char.IsDigit(chars[i]) && chars[i] != '/')
            {
                _inputField.text = arg0.Remove(arg0.Length - 1);
                _inputOutputCanvas.SetActive(false);
                _sorryDialogueCanvas.SetActive(true);
            }

            if(char.IsWhiteSpace(chars[i]))
            {
                _inputField.text = _inputField.text.Replace(" ", "");
            }
        }
    }

    private void Calculate()
    {
        var result = _calculator.Calculate(new ArithmeticExpression(_inputField.text));
        _inputField.text = result.Expression;
    }

    private void NewEquation()
    {
        Saver.DeleteSave();
        LoadEquation();

        _sorryDialogueCanvas.SetActive(false);
        _inputOutputCanvas.SetActive(true);
    }

    private void LoadEquation()
    {
        _calculator.Data = Saver.Load();
        _inputField.text = _calculator.Data.Expression;
    }

    private void Quit()
    {
        Saver.DeleteSave();
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Saver.Save(new ArithmeticExpression(_inputField.text));
    }
}
