using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
   
 public void PlayGame(){                        //function for starting game
        SceneManager.LoadSceneAsync(1);         //usees the scene manager to load selected scene based on scene ranking in project settings
    }

public void MapSelect(){                        //function for map selecting menu
        SceneManager.LoadSceneAsync(2);          //usees the scene manager to load selected scene based on scene ranking in project settings
    }


public void ShipSelect(){                        //function for ships menu
        SceneManager.LoadSceneAsync(3);          //usees the scene manager to load selected scene based on scene ranking in project settings
    }

public void Exit() {                             //function for exit
            Application.Quit();                 // access the application to quit game
    }

  
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
