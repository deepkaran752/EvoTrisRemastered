using System.Collections;
using UnityEngine;
using babbarversestudios;

public class TableFlipRedesigned: MonoBehaviour, IInteractable{

	public Animator FlipL;
	public bool isLefty;

	private InteractableState currentState;

	void Start () => 
		currentState = InteractableState.Closed;

	public IEnumerator WaitForCertainDuration(InteractableState setState)
	{
		yield return new WaitForSeconds(.5f);
		currentState = setState;
	}

    public void Interact()
    {
		Debug.Log("[DK LOG] -> trying to interact with this object");
		switch (currentState)
		{
			case InteractableState.Closed:
                FlipL.Play(DecideAnimation(toClose: false));
				currentState = InteractableState.Opening;
				StartCoroutine(WaitForCertainDuration(InteractableState.Open));
				break;

			case InteractableState.Open:
                FlipL.Play(DecideAnimation(toClose: true));
				currentState = InteractableState.Closing;
                StartCoroutine(WaitForCertainDuration(InteractableState.Closed));
                break;

			case InteractableState.Opening:
			case InteractableState.Closing:
				Debug.Log("[DK LOG] -> cant do anything in this state");
				break;
		}
    }

	private string DecideAnimation(bool toClose)
	{
		if (isLefty && toClose)
			return "Ldown";
		else if (isLefty && !toClose)
			return "Lup";
		else if (!isLefty && toClose)
			return "Rdown";
		else
			return "Rup";
	}
}

