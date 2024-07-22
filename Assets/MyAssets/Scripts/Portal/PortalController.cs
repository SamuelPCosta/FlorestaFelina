using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour{
    public enum PORTAL_TYPE {HOME, FOREST}
    [SerializeField] public PORTAL_TYPE portalType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void usePortal(){
        if(portalType == PORTAL_TYPE.HOME && !SceneManager.GetActiveScene().name.Equals("Level1")){
            SceneManager.LoadScene("Level1");
        }
        else{
            //SceneManager.LoadScene("Level1");
        } 
    }
}
