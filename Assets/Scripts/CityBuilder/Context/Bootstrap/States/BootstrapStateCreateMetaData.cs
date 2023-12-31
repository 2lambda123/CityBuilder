﻿using PG.CityBuilder.Model.Context;
using PG.CityBuilder.Model;
using PG.CityBuilder.Model.Data;
using UnityEngine;

namespace PG.CityBuilder.Context.Bootstrap
{
    public partial class BootstrapMediator
    {
        public class BootstrapStateCreateMetaData : BootstrapState
        {
            private readonly StaticDataModel _staticDataModel;

            public BootstrapStateCreateMetaData(Bootstrap.BootstrapMediator mediator) : base(mediator)
            {
                _staticDataModel = mediator._staticDataModel;
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
                
                MetaData MetaData = GameSettings.MetaDataAsset.Meta;

                CreateMetaDataSignal.CreateMetaData(SignalBus, MetaData).Then(
                    () => {
                        _staticDataModel.SeedMetaData(MetaData);
                        BootstrapModel.LoadingProgress.Value = Model.Context.BootstrapModel.ELoadingProgress.LoadUserData;
                    }
                ).Catch(e =>
                {
                    Debug.LogError("Exception Creating new Meta: " + e);
                });
            }
        }
    }
}