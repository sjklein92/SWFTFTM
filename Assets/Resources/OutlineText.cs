using UnityEngine;
using System.Collections;

public class OutlineText : MonoBehaviour {

	//EGUI.js	
	//draw text of style color, with a specified outline color	
	public void DrawOutline(Rect position, string text, GUIStyle style, Color outColor){	
		var backupStyle = style;
		var oldColor = style.normal.textColor;
		style.normal.textColor = outColor;
		position.x--;
		GUI.Label(position, text, style);
		position.x +=2;
		GUI.Label(position, text, style);
		position.x--;
		position.y--;
		GUI.Label(position, text, style);
		position.y +=2;
		GUI.Label(position, text, style);
		position.y--;
		style.normal.textColor = oldColor;
		GUI.Label(position, text, style);
		style = backupStyle;    
	}

	//draw text of a specified color, with a specified outline color
	public void DrawOutline(Rect position, string text, GUIStyle style, Color outColor, Color inColor){	
		var backupStyle = style;
		style.normal.textColor = outColor;
		position.x--;
		GUI.Label(position, text, style);
		position.x +=2;
		GUI.Label(position, text, style);
		position.x--;
		position.y--;
		GUI.Label(position, text, style);
		position.y +=2;
		GUI.Label(position, text, style);
		position.y--;
		style.normal.textColor = inColor;
		GUI.Label(position, text, style);
		style = backupStyle;
	}
	
	//Example (Call from MasterScript)
	//draws the word 'laser' at 5 pixels in, 103 pixels up from bottom. Text area is 50 pixels wide, 25 pixels tall
	//in white, with a black outline
	
	//textScript.GetComponent(OutlineText).DrawOutline(Rect (5, Screen.Height - 103, 50, 25), "Laser", skin.GetStyle("medium"), Color.black, Color.white);
}
