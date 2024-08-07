using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour{
    public enum PORTAL_ALIGNMENT { FRONT, RIGHT, BACK, LEFT }
    [SerializeField] public PORTAL_ALIGNMENT portalAlignment;

    public enum PORTAL_TYPE {HOME, FOREST}
    [SerializeField] public PORTAL_TYPE portalType;

    private string level1 = "Level1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void usePortal(){
        GameController gameController = FindObjectOfType<GameController>();
        InteractionsController interactionsController = FindObjectOfType<InteractionsController>();
        if (portalType == PORTAL_TYPE.HOME && interactionsController.isCatInBag() && !SceneManager.GetActiveScene().name.Equals(level1)){
            Transform exitPosition = gameObject.transform.GetChild(0); //primeiro filho
            gameController.savePlayerPosition(exitPosition, getOrientation());
            gameController.setPlayerInHome();
        }
        else if(portalType == PORTAL_TYPE.FOREST && SceneManager.GetActiveScene().name.Equals(level1)){
            gameController.setPlayerInForest();
        } 
    }

    private int getOrientation(){
        switch (portalAlignment){
            case PORTAL_ALIGNMENT.FRONT: return 180;
            case PORTAL_ALIGNMENT.RIGHT: return -90;
            case PORTAL_ALIGNMENT.BACK: return 0;
            case PORTAL_ALIGNMENT.LEFT: return 90;
            default: return 180;
        }
    }
}
