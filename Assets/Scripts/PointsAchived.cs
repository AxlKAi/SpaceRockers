using UnityEngine;
using UnityEngine.UI;

public class PointsAchived : MonoBehaviour
{
    [SerializeField]
    private Text _uiText;
    [SerializeField]
    private float _liftSpeed = 1f;
    [SerializeField]
    private float _dissapiaranceDelay = 1f;
    [SerializeField]
    private float _dissapiaranceTime = 1f;

    private bool _isInDissapearanceMode = false;
    private float _dissapearanceElapsetTime;

    public void SetValue(int value)
    {
        if (value > 100)
        {
            value = 99;

        }

        if (value < -100)
        {
            value = -99;

        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _dissapearanceElapsetTime = _dissapiaranceTime;
    }

    // Update is called once per frame
    private void Update()
    {

        Vector3 newPosition = new Vector3(
            transform.position.x,
            transform.position.y + _liftSpeed * Time.deltaTime,
            transform.position.z
            );

        transform.position = newPosition;

        if (_isInDissapearanceMode)
        {
            _dissapearanceElapsetTime -= Time.deltaTime;

            if (_dissapearanceElapsetTime > 0)
            {
                Color newColor = new Color(
                    _uiText.color.r,
                    _uiText.color.g,
                    _uiText.color.b,
                    _dissapearanceElapsetTime / _dissapiaranceTime
                    );

                _uiText.color = newColor;
            }
            else
            {
                Destroy(gameObject); 
            }
        }
        else
        {
            _dissapiaranceDelay -= Time.deltaTime;

            if (_dissapiaranceDelay < 0)
                _isInDissapearanceMode = true;
        }
    }
}
