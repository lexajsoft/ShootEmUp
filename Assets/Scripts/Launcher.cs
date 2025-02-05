using System;
using System.Collections;
using System.Collections.Generic;
using GameLoop;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[DefaultExecutionOrder(-1000)]
public class Launcher : MonoBehaviour
{
    [Header("В контексте должен быть выключен AUTO RUN")]
    [SerializeField] private RunnableContext _sceneContext;

    private void Awake()
    {
        Debug.Log($"[{this.GetType().Name}]:" + "Context.Run");
        _sceneContext.Run();
    }

    // private IEnumerator Start()
    // {
    //     // ожидание в 1 кадр нужно для того чтобы когда пойдет инициализация игрового цикла,
    //     // то все элементы уже будут подписаны в IGameListener и тем самым
    //     // потом у MainGameLoop можно будет вызвать инициализацию, если в MainGameLoop просто добавить интерфейс,
    //     // то он иницилизируется сразу, а там как бы надо первым делом сделать подписку на эвент в IGameListener,
    //     // подождать когда все уведомят кого нужно регистрировать, а кого нет и только потом начинать инициализацию 
    //     yield return null;
    //     
    //     Debug.Log($"[{this.GetType().Name}][PostResolve]:" + "MainGameLoop.Init");
    //     var mainGameLoop = _sceneContext.Container.Resolve<MainGameLoop>();
    //     mainGameLoop.InitGame();
    // }
}
