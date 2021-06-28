// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/ScriptableObjects/Controles/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""RTS"",
            ""id"": ""fdeefe5c-75fa-4495-a1b3-2e0186bc42e6"",
            ""actions"": [
                {
                    ""name"": ""Seleccionar Item"",
                    ""type"": ""Button"",
                    ""id"": ""9413ce71-1a21-49c4-a6c1-a95c0aabfb34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Borrar Edificio/Cancelar"",
                    ""type"": ""Button"",
                    ""id"": ""10ef03d0-dc8e-4cc8-bb02-bd28f7eb639a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SaltarContenido"",
                    ""type"": ""Button"",
                    ""id"": ""999c5556-da51-4dcd-af8b-d9c1b45a447b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PosicionCursor"",
                    ""type"": ""Value"",
                    ""id"": ""ed1c7fd0-53f0-4cf1-b7e7-81a7ce2e7733"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoverCamara"",
                    ""type"": ""Button"",
                    ""id"": ""d8c62898-d5bc-4118-99cd-007020b9f552"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a61d9b15-aa34-4ab8-9a77-82b0f72e9f90"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Seleccionar Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25de8aea-a721-4967-bc93-75e611ba9c7e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Borrar Edificio/Cancelar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7aafa5e2-15d0-4f5a-912d-29a17d6aeaa4"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaltarContenido"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15d93c80-07d4-48e3-b103-a9ad257b98e8"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaltarContenido"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ecf6132f-92f6-4c4a-b665-1ee1cc81491e"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaltarContenido"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2156e519-4a72-449d-82f4-ec8f02fb562a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PosicionCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3254b24c-3f7d-4457-b553-5cd26df115f1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoverCamara"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8f04e964-687d-4fde-8591-ab93a862960a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoverCamara"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5fcd04cc-411c-4896-8490-5d67c99eb1a5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoverCamara"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a2819262-3105-47c9-8196-7658b4bbba6a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoverCamara"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""371ebe1b-761b-4bcc-babd-4e3b1cb15101"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoverCamara"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Castillo"",
            ""id"": ""8b27a99b-e3ef-4f59-acc8-c869b5773ca9"",
            ""actions"": [
                {
                    ""name"": ""Caminar"",
                    ""type"": ""Button"",
                    ""id"": ""bbd082a3-44f6-47b5-a159-414242b09ae4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hablar"",
                    ""type"": ""Button"",
                    ""id"": ""2d4bf6a7-6f20-4fee-ad33-7b1c7f2f0b8c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pausa"",
                    ""type"": ""Button"",
                    ""id"": ""244f89bd-01e1-4a1f-8e53-e3b6284aaabe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""6caed11d-1a1e-4163-a31d-f35d58686208"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c9a980ab-2734-4c29-b731-2fe1bd57e661"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""99e78c1b-b981-4f69-adca-6d256c88e8b2"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dfd92a99-caaf-4ffb-95f4-740d98625ff7"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5105665c-bb27-419f-9ccd-6faefc6655c8"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""FlechasTeclado"",
                    ""id"": ""f6ebb558-ac82-4096-a67a-de457b901434"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""07d52e81-cd9e-414b-9a22-50598cc21181"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2183969a-3a0f-47c4-bb0d-591764ff4410"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8899fef6-f979-4323-a843-7735fafbef49"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bc3fc8c9-5ac7-4cf6-bda0-7c1393b8ce12"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9a5d4993-7e1e-4574-967d-4eb6de8b6bc0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hablar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c813692-6b3a-4e53-a048-d552aefe40de"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hablar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9764094-3440-4072-a8ad-7a92661af9f2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pausa"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menus"",
            ""id"": ""080ca057-c055-4308-890a-b06e0cb2e185"",
            ""actions"": [
                {
                    ""name"": ""DesplazarCursor"",
                    ""type"": ""Button"",
                    ""id"": ""a15ba524-de9d-4be3-8cbc-8ae49a5b3e25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancelar"",
                    ""type"": ""Button"",
                    ""id"": ""d5b49d95-b19e-4e5a-b9d0-2fcc76e06786"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SeleccionarClic"",
                    ""type"": ""Button"",
                    ""id"": ""f88e3667-d49e-4f57-be05-72a3d7298f9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SeleccionarTeclado"",
                    ""type"": ""Button"",
                    ""id"": ""4a5b14f5-b713-42d0-9c10-e3f289d61eb4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c2b0583d-1511-4845-a462-99006ca6d5ae"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancelar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e63f5f46-a647-4fd6-9710-df9e71026274"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancelar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bb4cd0d-ee4a-4446-806b-e5ee66c831d8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SeleccionarClic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""FlechasTeclado"",
                    ""id"": ""e39f5409-933f-42ec-b30e-2f1d0eb60b8e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DesplazarCursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c5ca9214-658f-410e-9256-6046d64d56e2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DesplazarCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7e287e33-20ef-4efd-87d2-22f6b618b209"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DesplazarCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""dbca482c-b44e-4bac-af5a-903fbc15b235"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DesplazarCursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""196ddaa2-64c1-4773-b4be-52b3729b2964"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DesplazarCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9c6d299b-b7c0-4482-aa17-d2cb8ac47a9d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DesplazarCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bdf7f3e8-0797-4599-8e88-0028ea36b0c6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SeleccionarTeclado"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1c4322e-16c9-43ea-9b3b-818eedbfd4d4"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SeleccionarTeclado"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // RTS
        m_RTS = asset.FindActionMap("RTS", throwIfNotFound: true);
        m_RTS_SeleccionarItem = m_RTS.FindAction("Seleccionar Item", throwIfNotFound: true);
        m_RTS_BorrarEdificioCancelar = m_RTS.FindAction("Borrar Edificio/Cancelar", throwIfNotFound: true);
        m_RTS_SaltarContenido = m_RTS.FindAction("SaltarContenido", throwIfNotFound: true);
        m_RTS_PosicionCursor = m_RTS.FindAction("PosicionCursor", throwIfNotFound: true);
        m_RTS_MoverCamara = m_RTS.FindAction("MoverCamara", throwIfNotFound: true);
        // Castillo
        m_Castillo = asset.FindActionMap("Castillo", throwIfNotFound: true);
        m_Castillo_Caminar = m_Castillo.FindAction("Caminar", throwIfNotFound: true);
        m_Castillo_Hablar = m_Castillo.FindAction("Hablar", throwIfNotFound: true);
        m_Castillo_Pausa = m_Castillo.FindAction("Pausa", throwIfNotFound: true);
        // Menus
        m_Menus = asset.FindActionMap("Menus", throwIfNotFound: true);
        m_Menus_DesplazarCursor = m_Menus.FindAction("DesplazarCursor", throwIfNotFound: true);
        m_Menus_Cancelar = m_Menus.FindAction("Cancelar", throwIfNotFound: true);
        m_Menus_SeleccionarClic = m_Menus.FindAction("SeleccionarClic", throwIfNotFound: true);
        m_Menus_SeleccionarTeclado = m_Menus.FindAction("SeleccionarTeclado", throwIfNotFound: true);
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

    // RTS
    private readonly InputActionMap m_RTS;
    private IRTSActions m_RTSActionsCallbackInterface;
    private readonly InputAction m_RTS_SeleccionarItem;
    private readonly InputAction m_RTS_BorrarEdificioCancelar;
    private readonly InputAction m_RTS_SaltarContenido;
    private readonly InputAction m_RTS_PosicionCursor;
    private readonly InputAction m_RTS_MoverCamara;
    public struct RTSActions
    {
        private @PlayerControls m_Wrapper;
        public RTSActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SeleccionarItem => m_Wrapper.m_RTS_SeleccionarItem;
        public InputAction @BorrarEdificioCancelar => m_Wrapper.m_RTS_BorrarEdificioCancelar;
        public InputAction @SaltarContenido => m_Wrapper.m_RTS_SaltarContenido;
        public InputAction @PosicionCursor => m_Wrapper.m_RTS_PosicionCursor;
        public InputAction @MoverCamara => m_Wrapper.m_RTS_MoverCamara;
        public InputActionMap Get() { return m_Wrapper.m_RTS; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RTSActions set) { return set.Get(); }
        public void SetCallbacks(IRTSActions instance)
        {
            if (m_Wrapper.m_RTSActionsCallbackInterface != null)
            {
                @SeleccionarItem.started -= m_Wrapper.m_RTSActionsCallbackInterface.OnSeleccionarItem;
                @SeleccionarItem.performed -= m_Wrapper.m_RTSActionsCallbackInterface.OnSeleccionarItem;
                @SeleccionarItem.canceled -= m_Wrapper.m_RTSActionsCallbackInterface.OnSeleccionarItem;
                @BorrarEdificioCancelar.started -= m_Wrapper.m_RTSActionsCallbackInterface.OnBorrarEdificioCancelar;
                @BorrarEdificioCancelar.performed -= m_Wrapper.m_RTSActionsCallbackInterface.OnBorrarEdificioCancelar;
                @BorrarEdificioCancelar.canceled -= m_Wrapper.m_RTSActionsCallbackInterface.OnBorrarEdificioCancelar;
                @SaltarContenido.started -= m_Wrapper.m_RTSActionsCallbackInterface.OnSaltarContenido;
                @SaltarContenido.performed -= m_Wrapper.m_RTSActionsCallbackInterface.OnSaltarContenido;
                @SaltarContenido.canceled -= m_Wrapper.m_RTSActionsCallbackInterface.OnSaltarContenido;
                @PosicionCursor.started -= m_Wrapper.m_RTSActionsCallbackInterface.OnPosicionCursor;
                @PosicionCursor.performed -= m_Wrapper.m_RTSActionsCallbackInterface.OnPosicionCursor;
                @PosicionCursor.canceled -= m_Wrapper.m_RTSActionsCallbackInterface.OnPosicionCursor;
                @MoverCamara.started -= m_Wrapper.m_RTSActionsCallbackInterface.OnMoverCamara;
                @MoverCamara.performed -= m_Wrapper.m_RTSActionsCallbackInterface.OnMoverCamara;
                @MoverCamara.canceled -= m_Wrapper.m_RTSActionsCallbackInterface.OnMoverCamara;
            }
            m_Wrapper.m_RTSActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SeleccionarItem.started += instance.OnSeleccionarItem;
                @SeleccionarItem.performed += instance.OnSeleccionarItem;
                @SeleccionarItem.canceled += instance.OnSeleccionarItem;
                @BorrarEdificioCancelar.started += instance.OnBorrarEdificioCancelar;
                @BorrarEdificioCancelar.performed += instance.OnBorrarEdificioCancelar;
                @BorrarEdificioCancelar.canceled += instance.OnBorrarEdificioCancelar;
                @SaltarContenido.started += instance.OnSaltarContenido;
                @SaltarContenido.performed += instance.OnSaltarContenido;
                @SaltarContenido.canceled += instance.OnSaltarContenido;
                @PosicionCursor.started += instance.OnPosicionCursor;
                @PosicionCursor.performed += instance.OnPosicionCursor;
                @PosicionCursor.canceled += instance.OnPosicionCursor;
                @MoverCamara.started += instance.OnMoverCamara;
                @MoverCamara.performed += instance.OnMoverCamara;
                @MoverCamara.canceled += instance.OnMoverCamara;
            }
        }
    }
    public RTSActions @RTS => new RTSActions(this);

    // Castillo
    private readonly InputActionMap m_Castillo;
    private ICastilloActions m_CastilloActionsCallbackInterface;
    private readonly InputAction m_Castillo_Caminar;
    private readonly InputAction m_Castillo_Hablar;
    private readonly InputAction m_Castillo_Pausa;
    public struct CastilloActions
    {
        private @PlayerControls m_Wrapper;
        public CastilloActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Caminar => m_Wrapper.m_Castillo_Caminar;
        public InputAction @Hablar => m_Wrapper.m_Castillo_Hablar;
        public InputAction @Pausa => m_Wrapper.m_Castillo_Pausa;
        public InputActionMap Get() { return m_Wrapper.m_Castillo; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CastilloActions set) { return set.Get(); }
        public void SetCallbacks(ICastilloActions instance)
        {
            if (m_Wrapper.m_CastilloActionsCallbackInterface != null)
            {
                @Caminar.started -= m_Wrapper.m_CastilloActionsCallbackInterface.OnCaminar;
                @Caminar.performed -= m_Wrapper.m_CastilloActionsCallbackInterface.OnCaminar;
                @Caminar.canceled -= m_Wrapper.m_CastilloActionsCallbackInterface.OnCaminar;
                @Hablar.started -= m_Wrapper.m_CastilloActionsCallbackInterface.OnHablar;
                @Hablar.performed -= m_Wrapper.m_CastilloActionsCallbackInterface.OnHablar;
                @Hablar.canceled -= m_Wrapper.m_CastilloActionsCallbackInterface.OnHablar;
                @Pausa.started -= m_Wrapper.m_CastilloActionsCallbackInterface.OnPausa;
                @Pausa.performed -= m_Wrapper.m_CastilloActionsCallbackInterface.OnPausa;
                @Pausa.canceled -= m_Wrapper.m_CastilloActionsCallbackInterface.OnPausa;
            }
            m_Wrapper.m_CastilloActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Caminar.started += instance.OnCaminar;
                @Caminar.performed += instance.OnCaminar;
                @Caminar.canceled += instance.OnCaminar;
                @Hablar.started += instance.OnHablar;
                @Hablar.performed += instance.OnHablar;
                @Hablar.canceled += instance.OnHablar;
                @Pausa.started += instance.OnPausa;
                @Pausa.performed += instance.OnPausa;
                @Pausa.canceled += instance.OnPausa;
            }
        }
    }
    public CastilloActions @Castillo => new CastilloActions(this);

    // Menus
    private readonly InputActionMap m_Menus;
    private IMenusActions m_MenusActionsCallbackInterface;
    private readonly InputAction m_Menus_DesplazarCursor;
    private readonly InputAction m_Menus_Cancelar;
    private readonly InputAction m_Menus_SeleccionarClic;
    private readonly InputAction m_Menus_SeleccionarTeclado;
    public struct MenusActions
    {
        private @PlayerControls m_Wrapper;
        public MenusActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DesplazarCursor => m_Wrapper.m_Menus_DesplazarCursor;
        public InputAction @Cancelar => m_Wrapper.m_Menus_Cancelar;
        public InputAction @SeleccionarClic => m_Wrapper.m_Menus_SeleccionarClic;
        public InputAction @SeleccionarTeclado => m_Wrapper.m_Menus_SeleccionarTeclado;
        public InputActionMap Get() { return m_Wrapper.m_Menus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenusActions set) { return set.Get(); }
        public void SetCallbacks(IMenusActions instance)
        {
            if (m_Wrapper.m_MenusActionsCallbackInterface != null)
            {
                @DesplazarCursor.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnDesplazarCursor;
                @DesplazarCursor.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnDesplazarCursor;
                @DesplazarCursor.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnDesplazarCursor;
                @Cancelar.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancelar;
                @Cancelar.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancelar;
                @Cancelar.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancelar;
                @SeleccionarClic.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSeleccionarClic;
                @SeleccionarClic.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSeleccionarClic;
                @SeleccionarClic.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSeleccionarClic;
                @SeleccionarTeclado.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSeleccionarTeclado;
                @SeleccionarTeclado.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSeleccionarTeclado;
                @SeleccionarTeclado.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSeleccionarTeclado;
            }
            m_Wrapper.m_MenusActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DesplazarCursor.started += instance.OnDesplazarCursor;
                @DesplazarCursor.performed += instance.OnDesplazarCursor;
                @DesplazarCursor.canceled += instance.OnDesplazarCursor;
                @Cancelar.started += instance.OnCancelar;
                @Cancelar.performed += instance.OnCancelar;
                @Cancelar.canceled += instance.OnCancelar;
                @SeleccionarClic.started += instance.OnSeleccionarClic;
                @SeleccionarClic.performed += instance.OnSeleccionarClic;
                @SeleccionarClic.canceled += instance.OnSeleccionarClic;
                @SeleccionarTeclado.started += instance.OnSeleccionarTeclado;
                @SeleccionarTeclado.performed += instance.OnSeleccionarTeclado;
                @SeleccionarTeclado.canceled += instance.OnSeleccionarTeclado;
            }
        }
    }
    public MenusActions @Menus => new MenusActions(this);
    public interface IRTSActions
    {
        void OnSeleccionarItem(InputAction.CallbackContext context);
        void OnBorrarEdificioCancelar(InputAction.CallbackContext context);
        void OnSaltarContenido(InputAction.CallbackContext context);
        void OnPosicionCursor(InputAction.CallbackContext context);
        void OnMoverCamara(InputAction.CallbackContext context);
    }
    public interface ICastilloActions
    {
        void OnCaminar(InputAction.CallbackContext context);
        void OnHablar(InputAction.CallbackContext context);
        void OnPausa(InputAction.CallbackContext context);
    }
    public interface IMenusActions
    {
        void OnDesplazarCursor(InputAction.CallbackContext context);
        void OnCancelar(InputAction.CallbackContext context);
        void OnSeleccionarClic(InputAction.CallbackContext context);
        void OnSeleccionarTeclado(InputAction.CallbackContext context);
    }
}
