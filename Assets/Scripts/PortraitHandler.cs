using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitHandler : MonoBehaviour {
	public enum Portraits {
		None, Talk, Surprised, Annoyed
	}

	[SerializeField]
	private Image sprite;

	private Animator animator;

	private void Awake() {
		animator = sprite.GetComponent<Animator>();
		SetPortrait(string.Empty);
	}

	public void SetPortrait(string portraitStr) {
		Portraits type = Portraits.None;
		switch (portraitStr.ToUpperInvariant()) {
			case "TALKING":
				type = Portraits.Talk;
				break;
			case "SURPRISED":
				type = Portraits.Surprised;
				break;
			case "ANNOYED":
				type = Portraits.Annoyed;
				break;
		}

		sprite.gameObject.SetActive(type != Portraits.None);
		if (type != Portraits.None) {
			switch (type) {
				case Portraits.Talk:
					animator.Play("Talking");
					break;
				case Portraits.Surprised:
					animator.Play("Surprised");
					break;
				case Portraits.Annoyed:
					animator.Play("Annoyed");
					break;
			}
		}
	}
}
