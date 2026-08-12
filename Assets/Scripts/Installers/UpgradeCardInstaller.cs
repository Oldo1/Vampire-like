using Assets.Scripts.Configs.Items;
using Assets.Scripts.Configs.Items.Weapons;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Boosters;
using Assets.Scripts.Items.Weapons;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class UpgradeCardInstaller : MonoInstaller
    {
        [SerializeField] private ItemInfo _itemInfo;
        public override void InstallBindings()
        {
            switch (_itemInfo)
            {
                case CooldownReducerConfig config:
                    BindItem<CooldownReducer>(config);
                    break;
                case DamageBoosterConfig config:
                    BindItem<DamageBooster>(config);
                    break;
                case RepeaterConfig config:
                    BindItem<Repeater>(config);
                    break;
                case SpeedBoosterConfig config:
                    BindItem<SpeedBooster>(config);
                    break;
                case FireBallShooterConfig config:
                    BindItem<FireBallShooter>(config);
                    break;
                case LightningShooterConfig config:
                    BindItem<LightningShooter>(config);
                    break;
                case DamageAuraConfig config:
                    BindItem<DamageAuraSpawner>(config);
                    break;
                case StoneThrowerConfig config:
                    BindItem<StoneThrower>(config);
                    break;
                default:
                    throw new ArgumentException($"Unsupported item info type: {_itemInfo?.GetType().Name ?? "null"}");
            }
        }

        private void BindItem<TItem>(ItemInfo itemInfo) where TItem : Item
        {
            Container.Bind<Item>()
                .To<TItem>()
                .AsCached()
                .WithArguments(itemInfo);
        }
    }
}
