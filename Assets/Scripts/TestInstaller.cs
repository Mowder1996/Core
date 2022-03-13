using System.Collections;
using System.Collections.Generic;
using ContentLoader.Bootstrap;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Install<ContentLoaderInstaller>();
    }
}
