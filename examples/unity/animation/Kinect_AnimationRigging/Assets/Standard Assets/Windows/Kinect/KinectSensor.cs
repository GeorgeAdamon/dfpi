using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static System.Runtime.InteropServices.CallingConvention;


namespace Windows.Kinect
{
    //
    // Windows.Kinect.KinectSensor
    //
    [InitializeOnLoad]
    public sealed partial class KinectSensor : Helper.INativeWrapper
    {
        
        static KinectSensor() // static Constructor
        {
            var currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        #if UNITY_EDITOR_32
        var dllPath = Application.dataPath
        + Path.DirectorySeparatorChar + "SomePath"
        + Path.DirectorySeparatorChar + "Plugins"
        + Path.DirectorySeparatorChar + "x86";
        #elif UNITY_EDITOR_64
            var dllPath = Application.dataPath
                          + Path.DirectorySeparatorChar + "Plugins"
                          + Path.DirectorySeparatorChar + "x86_64";
        #else // Player
        var dllPath = Application.dataPath
        + Path.DirectorySeparatorChar + "Plugins";
        #endif
            if (currentPath != null && currentPath.Contains(dllPath) == false)
                Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + dllPath, EnvironmentVariableTarget.Process);
            
        }
        
        internal IntPtr _pNative;
       
        IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal KinectSensor(IntPtr pNative)
        {
            _pNative = pNative;
            Windows_Kinect_KinectSensor_AddRefObject(ref _pNative);
        }

        ~KinectSensor()
        {
            Dispose(false);
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern void Windows_Kinect_KinectSensor_ReleaseObject(ref IntPtr pNative);
     
        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern void Windows_Kinect_KinectSensor_AddRefObject(ref IntPtr pNative);

        // Public Properties
        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_AudioSource(IntPtr pNative);
      
        public  Windows.Kinect.AudioSource AudioSource
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_AudioSource(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.AudioSource>(objectPointer, n => new Windows.Kinect.AudioSource(n));
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_BodyFrameSource(IntPtr pNative);
    
        public  Windows.Kinect.BodyFrameSource BodyFrameSource
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_BodyFrameSource(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.BodyFrameSource>(objectPointer, n => new Windows.Kinect.BodyFrameSource(n));
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_BodyIndexFrameSource(IntPtr pNative);
    
        public  Windows.Kinect.BodyIndexFrameSource BodyIndexFrameSource
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_BodyIndexFrameSource(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.BodyIndexFrameSource>(objectPointer, n => new Windows.Kinect.BodyIndexFrameSource(n));
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_ColorFrameSource(IntPtr pNative);
  
        public  Windows.Kinect.ColorFrameSource ColorFrameSource
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_ColorFrameSource(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.ColorFrameSource>(objectPointer, n => new Windows.Kinect.ColorFrameSource(n));
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_CoordinateMapper(IntPtr pNative);
      
        public  Windows.Kinect.CoordinateMapper CoordinateMapper
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_CoordinateMapper(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.CoordinateMapper>(objectPointer, n => new Windows.Kinect.CoordinateMapper(n));
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_DepthFrameSource(IntPtr pNative);
    
        public  Windows.Kinect.DepthFrameSource DepthFrameSource
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_DepthFrameSource(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.DepthFrameSource>(objectPointer, n => new Windows.Kinect.DepthFrameSource(n));
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_InfraredFrameSource(IntPtr pNative);
      
        public  Windows.Kinect.InfraredFrameSource InfraredFrameSource
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_InfraredFrameSource(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.InfraredFrameSource>(objectPointer, n => new Windows.Kinect.InfraredFrameSource(n));
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern bool Windows_Kinect_KinectSensor_get_IsAvailable(IntPtr pNative);
       
        public  bool IsAvailable
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                return Windows_Kinect_KinectSensor_get_IsAvailable(_pNative);
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern bool Windows_Kinect_KinectSensor_get_IsOpen(IntPtr pNative);
    
        public  bool IsOpen
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                return Windows_Kinect_KinectSensor_get_IsOpen(_pNative);
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern Windows.Kinect.KinectCapabilities Windows_Kinect_KinectSensor_get_KinectCapabilities(IntPtr pNative);
        public  Windows.Kinect.KinectCapabilities KinectCapabilities
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                return Windows_Kinect_KinectSensor_get_KinectCapabilities(_pNative);
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_LongExposureInfraredFrameSource(IntPtr pNative);
        public  Windows.Kinect.LongExposureInfraredFrameSource LongExposureInfraredFrameSource
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_LongExposureInfraredFrameSource(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.LongExposureInfraredFrameSource>(objectPointer, n => new Windows.Kinect.LongExposureInfraredFrameSource(n));
            }
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_get_UniqueKinectId(IntPtr pNative);
        public  string UniqueKinectId
        {
            get
            {
                if (_pNative == IntPtr.Zero)
                {
                    throw new ObjectDisposedException("KinectSensor");
                }

                IntPtr objectPointer = Windows_Kinect_KinectSensor_get_UniqueKinectId(_pNative);
                Helper.ExceptionHelper.CheckLastError();

                var managedString = Marshal.PtrToStringUni(objectPointer);
                Marshal.FreeCoTaskMem(objectPointer);
                return managedString;
            }
        }


        // Events
        private static GCHandle _Windows_Kinect_IsAvailableChangedEventArgs_Delegate_Handle;
     
        [UnmanagedFunctionPointer(Cdecl)]
        private delegate void _Windows_Kinect_IsAvailableChangedEventArgs_Delegate(IntPtr args, IntPtr pNative);
        private static Helper.CollectionMap<IntPtr, List<EventHandler<Windows.Kinect.IsAvailableChangedEventArgs>>> Windows_Kinect_IsAvailableChangedEventArgs_Delegate_callbacks = new Helper.CollectionMap<IntPtr, List<EventHandler<Windows.Kinect.IsAvailableChangedEventArgs>>>();
     
        [AOT.MonoPInvokeCallbackAttribute(typeof(_Windows_Kinect_IsAvailableChangedEventArgs_Delegate))]
        private static void Windows_Kinect_IsAvailableChangedEventArgs_Delegate_Handler(IntPtr result, IntPtr pNative)
        {
            List<EventHandler<Windows.Kinect.IsAvailableChangedEventArgs>> callbackList = null;
            Windows_Kinect_IsAvailableChangedEventArgs_Delegate_callbacks.TryGetValue(pNative, out callbackList);
            lock(callbackList)
            {
                var objThis = Helper.NativeObjectCache.GetObject<KinectSensor>(pNative);
                var args = new Windows.Kinect.IsAvailableChangedEventArgs(result);
                foreach(var func in callbackList)
                {
                    Helper.EventPump.Instance.Enqueue(() => { try { func(objThis, args); } catch { } });
                }
            }
        }
     
        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern void Windows_Kinect_KinectSensor_add_IsAvailableChanged(IntPtr pNative, _Windows_Kinect_IsAvailableChangedEventArgs_Delegate eventCallback, bool unsubscribe);
        public  event EventHandler<Windows.Kinect.IsAvailableChangedEventArgs> IsAvailableChanged
        {
            add
            {
                Helper.EventPump.EnsureInitialized();

                Windows_Kinect_IsAvailableChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Kinect_IsAvailableChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Add(value);
                    if(callbackList.Count == 1)
                    {
                        var del = new _Windows_Kinect_IsAvailableChangedEventArgs_Delegate(Windows_Kinect_IsAvailableChangedEventArgs_Delegate_Handler);
                        _Windows_Kinect_IsAvailableChangedEventArgs_Delegate_Handle = GCHandle.Alloc(del);
                        Windows_Kinect_KinectSensor_add_IsAvailableChanged(_pNative, del, false);
                    }
                }
            }
            remove
            {
                if (_pNative == IntPtr.Zero)
                {
                    return;
                }

                Windows_Kinect_IsAvailableChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Kinect_IsAvailableChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Remove(value);
                    if(callbackList.Count == 0)
                    {
                        Windows_Kinect_KinectSensor_add_IsAvailableChanged(_pNative, Windows_Kinect_IsAvailableChangedEventArgs_Delegate_Handler, true);
                        _Windows_Kinect_IsAvailableChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
        }

        private static GCHandle _Windows_Data_PropertyChangedEventArgs_Delegate_Handle;
     
        [UnmanagedFunctionPointer(Cdecl)]
        private delegate void _Windows_Data_PropertyChangedEventArgs_Delegate(IntPtr args, IntPtr pNative);
        private static Helper.CollectionMap<IntPtr, List<EventHandler<Windows.Data.PropertyChangedEventArgs>>> Windows_Data_PropertyChangedEventArgs_Delegate_callbacks = new Helper.CollectionMap<IntPtr, List<EventHandler<Windows.Data.PropertyChangedEventArgs>>>();
       
        [AOT.MonoPInvokeCallbackAttribute(typeof(_Windows_Data_PropertyChangedEventArgs_Delegate))]
        private static void Windows_Data_PropertyChangedEventArgs_Delegate_Handler(IntPtr result, IntPtr pNative)
        {
            List<EventHandler<Windows.Data.PropertyChangedEventArgs>> callbackList = null;
            Windows_Data_PropertyChangedEventArgs_Delegate_callbacks.TryGetValue(pNative, out callbackList);
            lock(callbackList)
            {
                var objThis = Helper.NativeObjectCache.GetObject<KinectSensor>(pNative);
                var args = new Windows.Data.PropertyChangedEventArgs(result);
                foreach(var func in callbackList)
                {
                    Helper.EventPump.Instance.Enqueue(() => { try { func(objThis, args); } catch { } });
                }
            }
        }
        
        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern void Windows_Kinect_KinectSensor_add_PropertyChanged(IntPtr pNative, _Windows_Data_PropertyChangedEventArgs_Delegate eventCallback, bool unsubscribe);
        public  event EventHandler<Windows.Data.PropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                Helper.EventPump.EnsureInitialized();

                Windows_Data_PropertyChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Data_PropertyChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Add(value);
                    if(callbackList.Count == 1)
                    {
                        var del = new _Windows_Data_PropertyChangedEventArgs_Delegate(Windows_Data_PropertyChangedEventArgs_Delegate_Handler);
                        _Windows_Data_PropertyChangedEventArgs_Delegate_Handle = GCHandle.Alloc(del);
                        Windows_Kinect_KinectSensor_add_PropertyChanged(_pNative, del, false);
                    }
                }
            }
            remove
            {
                if (_pNative == IntPtr.Zero)
                {
                    return;
                }

                Windows_Data_PropertyChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Data_PropertyChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Remove(value);
                    if(callbackList.Count == 0)
                    {
                        Windows_Kinect_KinectSensor_add_PropertyChanged(_pNative, Windows_Data_PropertyChangedEventArgs_Delegate_Handler, true);
                        _Windows_Data_PropertyChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
        }


        // Static Methods
        [DllImport("KinectUnityAddin", CallingConvention=Cdecl)]
        private static extern IntPtr Windows_Kinect_KinectSensor_GetDefault();
       
        public static KinectSensor GetDefault()
        {
            IntPtr objectPointer = Windows_Kinect_KinectSensor_GetDefault();
            Helper.ExceptionHelper.CheckLastError();
            if (objectPointer == IntPtr.Zero)
            {
                return null;
            }

            return Helper.NativeObjectCache.CreateOrGetObject(objectPointer, n => new KinectSensor(n));
        }


        // Public Methods
        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern void Windows_Kinect_KinectSensor_Open(IntPtr pNative);
        public void Open()
        {
            if (_pNative == IntPtr.Zero)
            {
                throw new ObjectDisposedException("KinectSensor");
            }

            Windows_Kinect_KinectSensor_Open(_pNative);
            Helper.ExceptionHelper.CheckLastError();
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern void Windows_Kinect_KinectSensor_Close(IntPtr pNative);
        public void Close()
        {
            if (_pNative == IntPtr.Zero)
            {
                throw new ObjectDisposedException("KinectSensor");
            }

            Windows_Kinect_KinectSensor_Close(_pNative);
            Helper.ExceptionHelper.CheckLastError();
        }

        [DllImport("KinectUnityAddin", CallingConvention=Cdecl, SetLastError=true)]
        private static extern IntPtr Windows_Kinect_KinectSensor_OpenMultiSourceFrameReader(IntPtr pNative, FrameSourceTypes enabledFrameSourceTypes);
       
        public MultiSourceFrameReader OpenMultiSourceFrameReader(FrameSourceTypes enabledFrameSourceTypes)
        {
            if (_pNative == IntPtr.Zero)
            {
                throw new ObjectDisposedException("KinectSensor");
            }

            IntPtr objectPointer = Windows_Kinect_KinectSensor_OpenMultiSourceFrameReader(_pNative, enabledFrameSourceTypes);
            Helper.ExceptionHelper.CheckLastError();
            if (objectPointer == IntPtr.Zero)
            {
                return null;
            }

            return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.MultiSourceFrameReader>(objectPointer, n => new Windows.Kinect.MultiSourceFrameReader(n));
        }

        private void __EventCleanup()
        {
            {
                Windows_Kinect_IsAvailableChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Kinect_IsAvailableChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    if (callbackList.Count > 0)
                    {
                        callbackList.Clear();
                        if (_pNative != IntPtr.Zero)
                        {
                            Windows_Kinect_KinectSensor_add_IsAvailableChanged(_pNative, Windows_Kinect_IsAvailableChangedEventArgs_Delegate_Handler, true);
                        }
                        _Windows_Kinect_IsAvailableChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
            {
                Windows_Data_PropertyChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Data_PropertyChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    if (callbackList.Count > 0)
                    {
                        callbackList.Clear();
                        if (_pNative != IntPtr.Zero)
                        {
                            Windows_Kinect_KinectSensor_add_PropertyChanged(_pNative, Windows_Data_PropertyChangedEventArgs_Delegate_Handler, true);
                        }
                        _Windows_Data_PropertyChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
        }
    }

}
