using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour{
    public enum PORTAL_TYPE {HOME, FOREST}
    [SerializeField] public PORTAL_TYPE portalType;

    private string level1 = "Level1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void usePortal(){
        GameController gameController = FindObjectOfType<GameController>();
        if (portalType == PORTAL_TYPE.HOME && !SceneManager.GetActiveScene().name.Equals(level1)){
            Transform exitPosition = gameObject.transform.GetChild(0); //primeiro filho
            gameController.savePlayerPosition(exitPosition);
            gameController.setPlayerInHome();
        }
        else if(portalType == PORTAL_TYPE.FOREST && SceneManager.GetActiveScene().name.Equals(level1)){
            gameController.setPlayerInForest();
        } 
    }
}
