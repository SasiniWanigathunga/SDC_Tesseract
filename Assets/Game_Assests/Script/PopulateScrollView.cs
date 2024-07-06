using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScrollView : MonoBehaviour
{
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject userPrefab;

    public void ScrollView()
    {
        // clear the content container
        foreach (Transform child in m_ContentContainer)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < 30; i++)
        {
            var player = Instantiate(userPrefab, new Vector2(7, 500 - i * 300), Quaternion.identity);

            player.transform.SetParent(m_ContentContainer);

            // check if the user views list is not empty
            if (GlobalManager_.Instance.UserViews.Count > i)
            {
                player.transform.Find("Rank").GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                player.transform.Find("Username").GetComponent<TMPro.TextMeshProUGUI>().text = GlobalManager_.Instance.UserViews[i].Username;
                player.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = GlobalManager_.Instance.UserViews[i].Score.ToString();
            }
            //reset the item's scale -- this can get munged with UI prefabs
            player.transform.localScale = Vector2.one;
        }
    }
}
