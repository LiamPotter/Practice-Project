using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GeneralUI
{
	public class GeneralUIController : MonoBehaviour
	{
		public GridLayoutGroup LayoutGroup;

		public RectTransform LayouGroupRect;

		public RectTransform ScrollViewRect;

		public Button PrefabButton;

		public Text DisplayText;

		private Button tempButton;

		private ColorBlock tempColorBlock;

		private Text tempText;

		private RectTransform buttonRect;

		private int maxRows, maxColumns;

		private int maxButtons;

		private void Start()
		{
			ScrollViewRect = LayoutGroup.GetComponent<RectTransform>();
			buttonRect = PrefabButton.GetComponent<RectTransform>();
		}

		public void CreateButton()
		{
			

			tempButton = Instantiate<Button>(PrefabButton);
			tempText = tempButton.GetComponentInChildren<Text>();
			tempColorBlock = new ColorBlock();

			tempColorBlock.normalColor = UnityEngine.Random.ColorHSV();
			tempColorBlock.highlightedColor = tempColorBlock.normalColor;
			tempColorBlock.pressedColor = tempColorBlock.normalColor;
			tempColorBlock.colorMultiplier = 1;
			tempButton.colors = tempColorBlock;

			string name = "Button " + LayoutGroup.transform.childCount;
			tempText.text = name;
			tempButton.name = name;

			tempButton.GetComponent<DummyButton>().DisplayText = DisplayText;

			tempButton.transform.SetParent(LayoutGroup.transform);
		}

	

		
	}
}
