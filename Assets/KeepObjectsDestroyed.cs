using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//Makes sure that if a dialog has been destroyed it stays gone on the scene's reload, so that the player doesn't have to keep repeating the same dialog, it also destroys any sparks the player has already collected
public class KeepObjectsDestroyed : MonoBehaviour
{
    public List<string> objects;
    ActivateDialog dialog;

    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene != null)
        {
            GameObject[] sparks = GameObject.FindGameObjectsWithTag("Spark");
            ActivateDialog[] dialogs = GameObject.FindObjectsOfType<ActivateDialog>();
            if (dialogs.Length > 0)
            {
                
                for (int i = 0; i < dialogs.Length; i++)
                {
                    for (int j = 0; j < objects.Count; j++)
                    {
                        if (dialogs[i].name == objects[j])
                        {
                            dialog = dialogs[i].GetComponent<ActivateDialog>();

                            if (dialog.destroyAfterSpeech)
                            {
                                Destroy(dialog);
                            }
                            
                        }
                    }
                }
            }
            for (int i = 0; i < sparks.Length; i++)
            {
                for (int j = 0; j < objects.Count; j++)
                {
                    if (sparks[i].name == objects[j])
                    {
                        Destroy(sparks[i]);
                    }
                }
            }
        }

    }
}
