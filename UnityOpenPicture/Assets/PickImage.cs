namespace JDGame.Framework
{
    using UnityEngine;
    using System;
    using System.Runtime.InteropServices;

    public class PickImage
    {

        #region iOS Native

        #if UNITY_IOS
        delegate void PhotoCallBack(IntPtr param);

        /// <summary>
        /// Gets the photo control plus.
        /// </summary>
        /// <param name="mode">模式： 0：打开摄像机； 1：打开相册 </param>
        /// <param name="editWidth">可编辑的宽度.</param>
        /// <param name="editHeight">可编辑的高度.</param>
        /// <param name="editCompress">压缩比［0～1.0］之间.</param>
        /// <param name="isEdit">是否可以编辑</param>
        [DllImport("__Internal")]
        private static extern void _GetPhotoControlPlus(int mode, float editWidth, float editHeight, float editCompress, bool isEdit);

        /// <summary>
        /// Sets the photo success callback.
        /// </summary>
        /// <param name="cb">Cb.</param>
        [DllImport("__Internal")]
        private static extern void _SetPhotoSuccessCallback(PhotoCallBack cb);

        /// <summary>
        /// Sets the photo cancel callback.
        /// </summary>
        /// <param name="cb">Cb.</param>
        [DllImport("__Internal")]
        private static extern void _SetPhotoCancelCallback(PhotoCallBack cb);

        [AOT.MonoPInvokeCallback(typeof(PhotoCallBack))]
        static void SuccessCallback(IntPtr param)
        {
            var base64 = Marshal.PtrToStringAuto(param);
            var binary = Convert.FromBase64String(base64);

            if (_SuccessAction != null)
            {
                _SuccessAction.Invoke(binary);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(PhotoCallBack))]
        static void CancelCallback(IntPtr param)
        {
            var p = (string)Marshal.PtrToStringAuto(param);

            if (_CancelAction != null)
            {
                _CancelAction.Invoke(p);
            }
        }


        private static System.Action<byte[]> _SuccessAction;
        private static System.Action<string> _CancelAction;

        public enum EOpenPhotoSource
        {
            /// <summary>
            /// 相机
            /// </summary>
            Camera = 0,

            /// <summary>
            /// 相册
            /// </summary>
            PhotoLibrary = 1,
        }

        public static void Init(System.Action<byte[]> succeeCallback, System.Action<string> cancelCallback)
        {
            _SuccessAction = succeeCallback;
            _CancelAction = cancelCallback;

            _SetPhotoSuccessCallback(SuccessCallback);
            _SetPhotoCancelCallback(CancelCallback);

        }

        public static void OpenPhotoLib(EOpenPhotoSource source, float editWidth, float editHeight)
        {
            _GetPhotoControlPlus((int)source, editWidth, editHeight, 0, true);
        }

        #endif

        #endregion

    }
}