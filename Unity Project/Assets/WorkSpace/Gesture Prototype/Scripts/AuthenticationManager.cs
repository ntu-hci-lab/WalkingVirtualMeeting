using Unity.Services.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Unity.Services.Core;
using UnityEngine;

public class AuthenticationManager : MonoBehaviour
{
    private List<string> keys;

    public bool isLogin = false;
    public string playerId = string.Empty;

    internal async Task Awake()
    {
        await UnityServices.InitializeAsync();
        await SignInAnonymously();
    }

    private async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            // Generate a new UUID
            Guid uuid = Guid.NewGuid();

            // Convert the UUID to a string
            string uuidString = uuid.ToString();

            playerId = uuidString;
            isLogin = true;

            // TODO: �h�@�� gameobject ���ϥΪ̪��D�O�_���\�n�J

            Debug.Log("Signed in as: " + playerId);
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            // Take some action here...
            Debug.Log(s);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }


}