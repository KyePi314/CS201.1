using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImport : MonoBehaviour
{

    public TextAsset textFile;
    public string[] speech;
    // Start is called before the first frame update
    void Start()
    {
        if (textFile != null)
        {
            //Adding text from the text file, with the new line splitting the text lines into seperate parts to be added in the array.
            speech = (textFile.text.Split('\n'));
        }
    }

    
}
