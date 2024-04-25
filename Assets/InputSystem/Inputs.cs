//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputSystem/Inputs.inputactions
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

public partial class @Inputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f62a4b92-ef5e-4175-8f4c-c9075429d32c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6bc1aaf4-b110-4ff7-891e-5b9fe6f32c4d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""2690c379-f54d-45be-a724-414123833eb4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""980e881e-182c-404c-8cbf-3d09fdb48fef"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveFast"",
                    ""type"": ""Value"",
                    ""id"": ""c1177de8-9a69-4cb8-96a1-4fdf903693a2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Workbench"",
                    ""type"": ""Button"",
                    ""id"": ""fc641341-a948-4ebd-bbf5-2df0ce5091da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Collet"",
                    ""type"": ""Button"",
                    ""id"": ""4449344b-e854-47f8-b187-d9939bbb82a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cat"",
                    ""type"": ""Button"",
                    ""id"": ""c257a71b-fd13-4a6e-8482-3d0b64bce9b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Craft"",
                    ""type"": ""Button"",
                    ""id"": ""0da1a330-396c-4ef2-b329-2e64c490dbe0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MakeWay"",
                    ""type"": ""Button"",
                    ""id"": ""ec2608f4-b808-4ae1-8aa6-827140f86b95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextLevel"",
                    ""type"": ""Button"",
                    ""id"": ""00770e6d-4a59-4582-9337-7c1a0c4b87f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dialog"",
                    ""type"": ""Button"",
                    ""id"": ""e1e63f3a-0d51-4cc8-a85c-542d6e26d749"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""b7594ddb-26c9-4ba2-bd5a-901468929edc"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2063a8b5-6a45-43de-851b-65f3d46e7b58"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""64e4d037-32e1-4fb9-80e4-fc7330404dfe"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0fce8b11-5eab-4e4e-a741-b732e7b20873"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7bdda0d6-57a8-47c8-8238-8aecf3110e47"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bb94b405-58d3-4998-8535-d705c1218a98"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""929d9071-7dd0-4368-9743-6793bb98087e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""28abadba-06ff-4d37-bb70-af2f1e35a3b9"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""45f115b6-9b4f-4ba8-b500-b94c93bf7d7e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e2f9aa65-db06-4c5b-a2e9-41bc8acb9517"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed66cbff-2900-4a62-8896-696503cfcd31"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),ScaleVector2(x=0.05,y=0.05)"",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1d171b6-19d8-47a6-ba3a-71b6a8e7b3c0"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),StickDeadzone,ScaleVector2(x=300,y=300)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc65b89f-9bd3-43fb-92af-d0d87ba5faa4"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8fcd86e-dcfd-4f88-8e93-b638cdbf3320"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d9c59c3-2221-4ae2-a704-c088998e5a1b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Workbench"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78ee407c-9621-41c7-a9a8-b0f429c34327"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Workbench"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9dd1c640-c82c-4c2e-a859-6e8134302747"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Collet"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c97c9bd-ab43-4eb8-862b-956d71679250"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Collet"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccac2999-4e3b-4ea3-aa53-0f8042d8a652"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Craft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""893e0e73-3b2e-471a-b7b5-cda6314cfdbc"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Craft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6904e42a-83a7-48db-a1e4-9c587d0e4991"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MakeWay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40d0a8f5-371f-43ae-9f53-ff7ef54cd8ec"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MakeWay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9e4ad72-ce83-41c1-8f7c-fbe16b3df06a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35bf7369-2c78-4985-b0ba-11325a908014"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a519f22a-b6bb-4928-8972-f0062680ed80"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""918b48f1-d1af-42c4-bb99-8694dd913040"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ff0c25b-62ad-49f1-a1fd-f697fd21e210"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveFast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f3a8963-07f8-4f0e-a422-ef5ca59bcf16"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveFast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a96ab2e-9065-4e55-9986-f8d36dd9cdb6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""699be067-ece2-49a7-8629-856df1d64966"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0f3f8cf-f1e3-4aab-8a50-6feaa9174808"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9b5222e-e6b4-4f86-bdae-7dbc5e0e1478"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Godmode"",
            ""id"": ""cde3d0aa-2705-411e-adfc-02c2fb1c702c"",
            ""actions"": [
                {
                    ""name"": ""extraW"",
                    ""type"": ""Button"",
                    ""id"": ""067057ed-b447-43e1-9550-d94b7385f1ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""extraP1"",
                    ""type"": ""Button"",
                    ""id"": ""ad76ae8b-d4c1-493f-b142-05ff70a26c1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""extraP2"",
                    ""type"": ""Button"",
                    ""id"": ""28457737-ede4-4263-a9c0-60eac3993115"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""25cb1210-5df5-44e3-b6bd-2ab54f4d9bd2"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""extraW"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf868e00-e07d-490d-8e58-ddfe76f082ca"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""extraP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84031ee4-0b2a-4e86-98bf-38998e74458d"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""extraP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Xbox Controller"",
            ""bindingGroup"": ""Xbox Controller"",
            ""devices"": []
        },
        {
            ""name"": ""PS4 Controller"",
            ""bindingGroup"": ""PS4 Controller"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_MoveFast = m_Player.FindAction("MoveFast", throwIfNotFound: true);
        m_Player_Workbench = m_Player.FindAction("Workbench", throwIfNotFound: true);
        m_Player_Collet = m_Player.FindAction("Collet", throwIfNotFound: true);
        m_Player_Cat = m_Player.FindAction("Cat", throwIfNotFound: true);
        m_Player_Craft = m_Player.FindAction("Craft", throwIfNotFound: true);
        m_Player_MakeWay = m_Player.FindAction("MakeWay", throwIfNotFound: true);
        m_Player_NextLevel = m_Player.FindAction("NextLevel", throwIfNotFound: true);
        m_Player_Dialog = m_Player.FindAction("Dialog", throwIfNotFound: true);
        // Godmode
        m_Godmode = asset.FindActionMap("Godmode", throwIfNotFound: true);
        m_Godmode_extraW = m_Godmode.FindAction("extraW", throwIfNotFound: true);
        m_Godmode_extraP1 = m_Godmode.FindAction("extraP1", throwIfNotFound: true);
        m_Godmode_extraP2 = m_Godmode.FindAction("extraP2", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_MoveFast;
    private readonly InputAction m_Player_Workbench;
    private readonly InputAction m_Player_Collet;
    private readonly InputAction m_Player_Cat;
    private readonly InputAction m_Player_Craft;
    private readonly InputAction m_Player_MakeWay;
    private readonly InputAction m_Player_NextLevel;
    private readonly InputAction m_Player_Dialog;
    public struct PlayerActions
    {
        private @Inputs m_Wrapper;
        public PlayerActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @MoveFast => m_Wrapper.m_Player_MoveFast;
        public InputAction @Workbench => m_Wrapper.m_Player_Workbench;
        public InputAction @Collet => m_Wrapper.m_Player_Collet;
        public InputAction @Cat => m_Wrapper.m_Player_Cat;
        public InputAction @Craft => m_Wrapper.m_Player_Craft;
        public InputAction @MakeWay => m_Wrapper.m_Player_MakeWay;
        public InputAction @NextLevel => m_Wrapper.m_Player_NextLevel;
        public InputAction @Dialog => m_Wrapper.m_Player_Dialog;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @MoveFast.started += instance.OnMoveFast;
            @MoveFast.performed += instance.OnMoveFast;
            @MoveFast.canceled += instance.OnMoveFast;
            @Workbench.started += instance.OnWorkbench;
            @Workbench.performed += instance.OnWorkbench;
            @Workbench.canceled += instance.OnWorkbench;
            @Collet.started += instance.OnCollet;
            @Collet.performed += instance.OnCollet;
            @Collet.canceled += instance.OnCollet;
            @Cat.started += instance.OnCat;
            @Cat.performed += instance.OnCat;
            @Cat.canceled += instance.OnCat;
            @Craft.started += instance.OnCraft;
            @Craft.performed += instance.OnCraft;
            @Craft.canceled += instance.OnCraft;
            @MakeWay.started += instance.OnMakeWay;
            @MakeWay.performed += instance.OnMakeWay;
            @MakeWay.canceled += instance.OnMakeWay;
            @NextLevel.started += instance.OnNextLevel;
            @NextLevel.performed += instance.OnNextLevel;
            @NextLevel.canceled += instance.OnNextLevel;
            @Dialog.started += instance.OnDialog;
            @Dialog.performed += instance.OnDialog;
            @Dialog.canceled += instance.OnDialog;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @MoveFast.started -= instance.OnMoveFast;
            @MoveFast.performed -= instance.OnMoveFast;
            @MoveFast.canceled -= instance.OnMoveFast;
            @Workbench.started -= instance.OnWorkbench;
            @Workbench.performed -= instance.OnWorkbench;
            @Workbench.canceled -= instance.OnWorkbench;
            @Collet.started -= instance.OnCollet;
            @Collet.performed -= instance.OnCollet;
            @Collet.canceled -= instance.OnCollet;
            @Cat.started -= instance.OnCat;
            @Cat.performed -= instance.OnCat;
            @Cat.canceled -= instance.OnCat;
            @Craft.started -= instance.OnCraft;
            @Craft.performed -= instance.OnCraft;
            @Craft.canceled -= instance.OnCraft;
            @MakeWay.started -= instance.OnMakeWay;
            @MakeWay.performed -= instance.OnMakeWay;
            @MakeWay.canceled -= instance.OnMakeWay;
            @NextLevel.started -= instance.OnNextLevel;
            @NextLevel.performed -= instance.OnNextLevel;
            @NextLevel.canceled -= instance.OnNextLevel;
            @Dialog.started -= instance.OnDialog;
            @Dialog.performed -= instance.OnDialog;
            @Dialog.canceled -= instance.OnDialog;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Godmode
    private readonly InputActionMap m_Godmode;
    private List<IGodmodeActions> m_GodmodeActionsCallbackInterfaces = new List<IGodmodeActions>();
    private readonly InputAction m_Godmode_extraW;
    private readonly InputAction m_Godmode_extraP1;
    private readonly InputAction m_Godmode_extraP2;
    public struct GodmodeActions
    {
        private @Inputs m_Wrapper;
        public GodmodeActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @extraW => m_Wrapper.m_Godmode_extraW;
        public InputAction @extraP1 => m_Wrapper.m_Godmode_extraP1;
        public InputAction @extraP2 => m_Wrapper.m_Godmode_extraP2;
        public InputActionMap Get() { return m_Wrapper.m_Godmode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GodmodeActions set) { return set.Get(); }
        public void AddCallbacks(IGodmodeActions instance)
        {
            if (instance == null || m_Wrapper.m_GodmodeActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GodmodeActionsCallbackInterfaces.Add(instance);
            @extraW.started += instance.OnExtraW;
            @extraW.performed += instance.OnExtraW;
            @extraW.canceled += instance.OnExtraW;
            @extraP1.started += instance.OnExtraP1;
            @extraP1.performed += instance.OnExtraP1;
            @extraP1.canceled += instance.OnExtraP1;
            @extraP2.started += instance.OnExtraP2;
            @extraP2.performed += instance.OnExtraP2;
            @extraP2.canceled += instance.OnExtraP2;
        }

        private void UnregisterCallbacks(IGodmodeActions instance)
        {
            @extraW.started -= instance.OnExtraW;
            @extraW.performed -= instance.OnExtraW;
            @extraW.canceled -= instance.OnExtraW;
            @extraP1.started -= instance.OnExtraP1;
            @extraP1.performed -= instance.OnExtraP1;
            @extraP1.canceled -= instance.OnExtraP1;
            @extraP2.started -= instance.OnExtraP2;
            @extraP2.performed -= instance.OnExtraP2;
            @extraP2.canceled -= instance.OnExtraP2;
        }

        public void RemoveCallbacks(IGodmodeActions instance)
        {
            if (m_Wrapper.m_GodmodeActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGodmodeActions instance)
        {
            foreach (var item in m_Wrapper.m_GodmodeActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GodmodeActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GodmodeActions @Godmode => new GodmodeActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("Xbox Controller");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    private int m_PS4ControllerSchemeIndex = -1;
    public InputControlScheme PS4ControllerScheme
    {
        get
        {
            if (m_PS4ControllerSchemeIndex == -1) m_PS4ControllerSchemeIndex = asset.FindControlSchemeIndex("PS4 Controller");
            return asset.controlSchemes[m_PS4ControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnMoveFast(InputAction.CallbackContext context);
        void OnWorkbench(InputAction.CallbackContext context);
        void OnCollet(InputAction.CallbackContext context);
        void OnCat(InputAction.CallbackContext context);
        void OnCraft(InputAction.CallbackContext context);
        void OnMakeWay(InputAction.CallbackContext context);
        void OnNextLevel(InputAction.CallbackContext context);
        void OnDialog(InputAction.CallbackContext context);
    }
    public interface IGodmodeActions
    {
        void OnExtraW(InputAction.CallbackContext context);
        void OnExtraP1(InputAction.CallbackContext context);
        void OnExtraP2(InputAction.CallbackContext context);
    }
}
