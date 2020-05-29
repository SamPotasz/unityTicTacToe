using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EYE_STATE {
    LOOKING,
    CLOSED,
    CELEBRATING,    //was going to make "happy" eyes for winning team but I couldn't draw them!
};

public class EyeController : MonoBehaviour
{
    [SerializeField]
    private GameObject pupil;

    [SerializeField]
    private SpriteRenderer background;

    [SerializeField]
    private Sprite openSprite;

    [SerializeField]
    private Sprite closedSprite;

    private float pupilStartZ;

    // how far to move the pupil within the eye
    static float PUPIL_RADIUS = 0.1f;
    private EYE_STATE state = EYE_STATE.LOOKING;
    // Start is called before the first frame update
    void Start()
    {
        pupilStartZ = pupil.transform.localPosition.z;
    }

    void Update()
    {
        if( state.Equals(EYE_STATE.LOOKING) ) {
            Vector3 mousePos = Input.mousePosition;
            Vector3 pupilPos = Camera.main.WorldToScreenPoint(pupil.transform.position);

            float xDist = mousePos.x - pupilPos.x;
            float yDist = mousePos.y - pupilPos.y;

            Vector2 target = new Vector2(xDist, yDist);
            target.Normalize();
            target = target * PUPIL_RADIUS;

            pupil.transform.localPosition = Vector3.Lerp(
                pupil.transform.localPosition, 
                new Vector3(target.x, target.y, pupilStartZ),
                5 * Time.deltaTime );
        }
    }

    public void Open() {
        Debug.Log("OPening eyes");
        state = EYE_STATE.LOOKING;
        
        background.sprite = openSprite;
        pupil.SetActive(true);
    }

    public void Close() {
        Debug.Log("Closing eyes");

        state = EYE_STATE.CLOSED;

        background.sprite = closedSprite;
        pupil.SetActive(false);
    }
}
