using System.Collections.Generic;
using Zenject;
using System.Linq;

namespace Assets.Scripts.Gameplay
{
    public class UpgradeCardsContainer : IInitializable
    {
        private Dictionary<int, UpgradeCard> _availableCards;
        public IEnumerable<UpgradeCard> Values => _availableCards.Values;
        public bool HasCards => _availableCards.Any();
        public int Count => _availableCards.Count;
        private readonly UpgradeCard[] _upgradeCards;

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

        public bool Remove(int id)
        {
            return _availableCards.Remove(id);
        }
    }
}
