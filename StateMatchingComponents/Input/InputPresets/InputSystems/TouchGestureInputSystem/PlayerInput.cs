//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Script/State_Matching_System/InputSystem/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""cdc025e6-0e9f-4ce6-ae9c-ed5c4803c31a"",
            ""actions"": [
                {
                    ""name"": ""Location0"",
                    ""type"": ""Value"",
                    ""id"": ""b518524d-b14a-4d85-a417-848df03d24e8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Location1"",
                    ""type"": ""Value"",
                    ""id"": ""91d55c4f-a092-44e2-9bd3-58cc941f996f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Tap0"",
                    ""type"": ""Button"",
                    ""id"": ""ec70bc1a-b7f1-4b30-b3f7-aeb0b73fd253"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tap1"",
                    ""type"": ""Button"",
                    ""id"": ""db194a9a-e783-40a0-9330-7a5872b283bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Finger0"",
                    ""type"": ""Button"",
                    ""id"": ""aa10eafc-ec51-4a9a-81c2-b1c7e41e2f7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Finger1"",
                    ""type"": ""Button"",
                    ""id"": ""b4c1078f-e209-4aa8-bf6e-09b339f999c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6fe925e2-fdc7-4694-acb6-b467277e54f3"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Location0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34b2bbf0-9959-4f54-9eb8-3c96925e4620"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Location1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff269c8a-5eee-4e46-ae16-6b66e413982f"",
                    ""path"": ""<Touchscreen>/touch0/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Tap0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e1d47df-69ef-48e1-a475-ceecab607575"",
                    ""path"": ""<Touchscreen>/touch1/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Tap1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7edba57-8044-4cc2-94be-6993ee2465fe"",
                    ""path"": ""<Touchscreen>/touch0/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Finger0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce789812-7f01-40ee-adff-38051315bee5"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Finger1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_Location0 = m_Touch.FindAction("Location0", throwIfNotFound: true);
        m_Touch_Location1 = m_Touch.FindAction("Location1", throwIfNotFound: true);
        m_Touch_Tap0 = m_Touch.FindAction("Tap0", throwIfNotFound: true);
        m_Touch_Tap1 = m_Touch.FindAction("Tap1", throwIfNotFound: true);
        m_Touch_Finger0 = m_Touch.FindAction("Finger0", throwIfNotFound: true);
        m_Touch_Finger1 = m_Touch.FindAction("Finger1", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_Location0;
    private readonly InputAction m_Touch_Location1;
    private readonly InputAction m_Touch_Tap0;
    private readonly InputAction m_Touch_Tap1;
    private readonly InputAction m_Touch_Finger0;
    private readonly InputAction m_Touch_Finger1;
    public struct TouchActions
    {
        private @PlayerInput m_Wrapper;
        public TouchActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Location0 => m_Wrapper.m_Touch_Location0;
        public InputAction @Location1 => m_Wrapper.m_Touch_Location1;
        public InputAction @Tap0 => m_Wrapper.m_Touch_Tap0;
        public InputAction @Tap1 => m_Wrapper.m_Touch_Tap1;
        public InputAction @Finger0 => m_Wrapper.m_Touch_Finger0;
        public InputAction @Finger1 => m_Wrapper.m_Touch_Finger1;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @Location0.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnLocation0;
                @Location0.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnLocation0;
                @Location0.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnLocation0;
                @Location1.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnLocation1;
                @Location1.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnLocation1;
                @Location1.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnLocation1;
                @Tap0.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTap0;
                @Tap0.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTap0;
                @Tap0.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTap0;
                @Tap1.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTap1;
                @Tap1.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTap1;
                @Tap1.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTap1;
                @Finger0.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnFinger0;
                @Finger0.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnFinger0;
                @Finger0.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnFinger0;
                @Finger1.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnFinger1;
                @Finger1.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnFinger1;
                @Finger1.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnFinger1;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Location0.started += instance.OnLocation0;
                @Location0.performed += instance.OnLocation0;
                @Location0.canceled += instance.OnLocation0;
                @Location1.started += instance.OnLocation1;
                @Location1.performed += instance.OnLocation1;
                @Location1.canceled += instance.OnLocation1;
                @Tap0.started += instance.OnTap0;
                @Tap0.performed += instance.OnTap0;
                @Tap0.canceled += instance.OnTap0;
                @Tap1.started += instance.OnTap1;
                @Tap1.performed += instance.OnTap1;
                @Tap1.canceled += instance.OnTap1;
                @Finger0.started += instance.OnFinger0;
                @Finger0.performed += instance.OnFinger0;
                @Finger0.canceled += instance.OnFinger0;
                @Finger1.started += instance.OnFinger1;
                @Finger1.performed += instance.OnFinger1;
                @Finger1.canceled += instance.OnFinger1;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface ITouchActions
    {
        void OnLocation0(InputAction.CallbackContext context);
        void OnLocation1(InputAction.CallbackContext context);
        void OnTap0(InputAction.CallbackContext context);
        void OnTap1(InputAction.CallbackContext context);
        void OnFinger0(InputAction.CallbackContext context);
        void OnFinger1(InputAction.CallbackContext context);
    }
}
