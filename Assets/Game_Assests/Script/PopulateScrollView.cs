using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
                // reset the color of the text fields
                player.transform.Find("Username").GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
                player.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;

                // Display the leaderboard
                player.transform.Find("Rank").GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                player.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = GlobalManager_.Instance.UserViews[i].Score.ToString();

                TMP_Text username = player.transform.Find("Username").GetComponent<TMPro.TextMeshProUGUI>();
                username.text = GlobalManager_.Instance.UserViews[i].Username;
                string our_username = GlobalManager_.Instance.Username;
                if (username.text == our_username)
                {
                    username.color = Color.red;
                    player.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
                }
            }
            //reset the item's scale -- this can get munged with UI prefabs
            player.transform.localScale = Vector2.one;
        }
    }
}
