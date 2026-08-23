using System.Collections.Generic;
using Zenject;
using System.Linq;

namespace Assets.Scripts.Gameplay
{
    public class UpgradeCardsContainer : IInitializable
    {
        public IReadOnlyList<UpgradeCard> AllCards => _upgradeCards;
        public IEnumerable<UpgradeCard> AvailableCards => _availableCards.Values;
        public bool HasCards => _availableCards.Count > 0;
        public int Count => _availableCards.Count;

        private readonly UpgradeCard[] _upgradeCards;
        private Dictionary<int, UpgradeCard> _availableCards;

        public UpgradeCardsContainer(UpgradeCard[] upgradeCards)
        {
            _upgradeCards = upgradeCards;
        }

        public void Initialize()
        {
            _availableCards = _upgradeCards.ToDictionary(x => x.ItemInfo.Id);
        }

        public UpgradeCard GetUpgradeCard(int id)
        {
            if (_availableCards.TryGetValue(id, out var upgradeCard))
            {
                return upgradeCard;
            }
            throw new System.ArgumentOutOfRangeException("id");
        }

        public bool TryGetUpgradeCard(int id, out UpgradeCard upgradeCard)
        {
            return _availableCards.TryGetValue(id, out upgradeCard);
        }

        public bool Remove(int id)
        {
            return _availableCards.Remove(id);
        }
    }
}
