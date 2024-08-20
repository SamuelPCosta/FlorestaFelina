using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour{
    public enum PORTAL_ALIGNMENT { FRONT, RIGHT, BACK, LEFT }
    [SerializeField] public PORTAL_ALIGNMENT portalAlignment;

    public enum PORTAL_DESTINY {HOME, FOREST}
    [SerializeField] public PORTAL_DESTINY portalType;

    private string level1 = "Level1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void usePortal(){
        GameController gameController = FindObjectOfType<GameController>();
        InteractionsController interactionsController = FindObjectOfType<InteractionsController>();

        if (portalType == PORTAL_DESTINY.HOME && interactionsController.isCatInBag() && !SceneManager.GetActiveScene().name.Equals(level1)){
            Transform exitPosition = gameObject.transform.GetChild(0); //primeiro filho
            FindObjectOfType<CatsStatesController>().setMissionStateByPortal();
            MissionController missions = FindObjectOfType<MissionController>();
            missions.setOldsIngredients();
            missions.completeMission();
            AudioController.playAction(INTERACTIONS.Portal);
            FindObjectOfType<FeedbackController>().Vibrate(Power.Mid, Duration.Mid);
            gameController.savePlayerPortalPosition(exitPosition, getOrientation());
            gameController.setPlayerInHome();
        }
        else if(portalType == PORTAL_DESTINY.FOREST && SceneManager.GetActiveScene().name.Equals(level1)){
            AudioController.playAction(INTERACTIONS.Portal);
            FindObjectOfType<FeedbackController>().Vibrate(Power.Mid, Duration.Mid);
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
