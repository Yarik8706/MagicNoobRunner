using DG.Tweening;
using Flatformer.GameData;
using System.Collections;
using blocks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



namespace ShopMechanics
{
    public enum CharacterSkinType
    {
        Target,
        PlayerHero
    }
    public enum StateCharacterItem
    {
        Lock,
        Unlock,
        AlphaLock
    }
    public class CharacterItem : MonoBehaviour
    {
        public static readonly UnityEvent<int> BuySkinEvent = new UnityEvent<int>();
        public static readonly UnityEvent<int> SelectSkinEvent = new UnityEvent<int>();
        
        [Header("Information Character Item")]
        [SerializeField] private Image _characterStateBG;
        [SerializeField] private Image _characterImage;
        [SerializeField] private GameObject _characterSelect;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _headerText;

        [Space(20f)]
        [Header("Event Character Item")]
        [SerializeField] private Button _characterPurchaseButton;
        [SerializeField] private Button _selectItemButton;

        [Header("State Character Image")]
        [SerializeField] private Sprite _backGroundLock;
        [SerializeField] private Sprite _backGroundUnlock;
        [SerializeField] private Sprite _backGroundAlphaLock;

        private int _unlockLevelRequired;
        private int _index = -1;

        public void SetCharacterIndex(int index) => this._index = index;

        public void SetCharacterImage(Sprite sprite) => _characterImage.sprite = sprite;

        public void SetPurchaseEqualAds()
        {
            _characterPurchaseButton.transform.GetChild(1).gameObject.SetActive(false);
            _characterPurchaseButton.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void SetPurchaseEqualActiveLevel(string header)
        {
            _characterPurchaseButton.gameObject.SetActive(false);
            _headerText.gameObject.SetActive(true);
            _headerText.text = header;
        }

        public void SetPurchaseEqualActiveDays(string header)
        {
            _characterPurchaseButton.gameObject.SetActive(false);
            _headerText.gameObject.SetActive(true);
            _headerText.text = header;
        }

        public void SetPurchaseEqualCoin(int price)
        {
            _characterPurchaseButton.transform.GetChild(0).gameObject.SetActive(false);
            _characterPurchaseButton.transform.GetChild(1).gameObject.SetActive(true);
            
            _priceText.text = price.ToString();
        }

        public void SetCharacterStateBG(StateCharacterItem stateItem)
        {
            switch (stateItem)
            {
                case StateCharacterItem.Lock:
                    _headerText.gameObject.SetActive(false);
                    _characterStateBG.sprite = _backGroundLock;
                    break;
                case StateCharacterItem.Unlock:
                    _headerText.gameObject.SetActive(false);
                    _characterStateBG.sprite = _backGroundUnlock;
                    break;
                case StateCharacterItem.AlphaLock:
                    _characterStateBG.sprite = _backGroundAlphaLock;
                    break;
            }
        }
		
		private void OnEnable()
		{
            if(_index == -1) return;
			if (_unlockLevelRequired != 0 && GameDataManager.GetLevel() > _unlockLevelRequired) 
			{
				SetPurchaseAsCharacter();
				_headerText.gameObject.SetActive(false);
				_selectItemButton.interactable = true;
				OnSelectItem();
			} 
            else if (GameDataManager.GetPurchaseAsCharacter(_index))
            {
                Debug.Log("unlock charaster: " + _index);
                SetPurchaseAsCharacter();
                _headerText.gameObject.SetActive(false);
                _selectItemButton.interactable = true;
                OnSelectItem();
            }
		}
		
        public void OnPurchaseItem()
        {
            _characterPurchaseButton.onClick.RemoveAllListeners();
            _characterPurchaseButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayAudioSound(SoundManager.Instance.buttonAudio); 
                BuySkinEvent.Invoke(_index);
            });
        }

        public void SetPurchaseAsCharacter()
        {
            _characterPurchaseButton.gameObject.SetActive(false);
            _selectItemButton.interactable = true;
            _characterStateBG.sprite = _backGroundUnlock;
        }

        public void OnSelectItem()
        {
            _selectItemButton.onClick.RemoveAllListeners();
            _selectItemButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayAudioSound(SoundManager.Instance.buttonAudio);
                SelectSkinEvent.Invoke(_index);
            });
        }

        public void SelectItem()
        {
            _characterSelect.SetActive(true);
            _selectItemButton.interactable = false;
        }

        public void DeSelectItem()
        {
            _characterSelect.gameObject.SetActive(false);
            _selectItemButton.interactable = true;
        }

        public void OnCompleteAds()
        {
            StartCoroutine(DelayCompleteAds(5));
        }
        
        private IEnumerator DelayCompleteAds(float delay)
        {
            var t = delay;
            while(t > 0)
            {
                yield return new WaitForEndOfFrame();
                t--;
               
            }
            GameDataManager.AddPurchaseCharacter(_index);
            SetPurchaseAsCharacter();
            OnSelectItem();
        }

        public void SetCharacter(Character character, int index)
        {
            SetCharacterIndex(index);
            SetCharacterImage(character.image);
            if (GameDataManager.GetPurchaseAsCharacter(index))
            {
                SetPurchaseAsCharacter();
                OnSelectItem();
            }
            else
            {
                SetCharacterStateBG(StateCharacterItem.Lock);
                if (character.isNeedAds)
                {
                    OnPurchaseItem();
                    SetPurchaseEqualAds();
                }
                else if (character.levelRequired != 0 && GameDataManager.GetLevel() < character.levelRequired)
                {
                    _unlockLevelRequired = character.levelRequired;
                    SetCharacterStateBG(StateCharacterItem.AlphaLock);
                    SetPurchaseEqualActiveLevel(ShopManager.UnlockLevelText.GetText() + character.levelRequired);
                }
                else if (character.price != 0)
                {
                    OnPurchaseItem();
                    SetPurchaseEqualCoin(character.price);
                }
                else if (character.entryDaysRequired != 0) 
                {
                    SetCharacterStateBG(StateCharacterItem.AlphaLock);
                    SetPurchaseEqualActiveDays(ShopManager.OpenOnText.GetText() + character.entryDaysRequired);
                }
            }
        }
    }
}
