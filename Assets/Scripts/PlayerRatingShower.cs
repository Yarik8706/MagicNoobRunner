using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

namespace Flatformer.GameData.UIManager
{
    public class PlayerRatingShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text playerRatingText;
        [SerializeField] private Image ratingImage;

        private int playerRank;

        private void Awake()
        {
            YandexGame.onGetLeaderboard += OnUpdateLB;
        }

        private void OnUpdateLB(LBData lb)
        {
            int playerRank;
            foreach (var player in lb.players)
            {
                if (player.uniqueID == YandexGame.playerId)
                {
                    playerRank = player.rank;
                    UpdateRating(playerRank);
                    return;
                }
            }

            playerRank = lb.players.Length + 1;
            UpdateRating(playerRank);
        }

        private void UpdateRating(int newPlayerRating)
        {
            // switch (newPlayerRating)
            // {
            //     case 1:
            //         ratingImage.color = Color.yellow;
            //         break;
            //     case 2:
            //         ratingImage.color = Color.gray;
            //         break;
            //     case 3:
            //         ratingImage.color = new Color(0.59f, 0.29f, 0f);
            //         break;
            //     default:
            //         ratingImage.color = new Color(0.92f, 0.42f, 0f);
            //         break;
            // }
            
            if(playerRank == newPlayerRating) return;

            playerRank = newPlayerRating;
            playerRatingText.transform.DOScale(playerRatingText.transform.localScale * 1.2f, 0.5f).OnComplete(() =>
            {
                playerRatingText.text = "" + newPlayerRating;
                playerRatingText.transform.DOScale(playerRatingText.transform.localScale * 0.8f, 0.5f);
            }).SetDelay(0.4f);
        }
    }
}