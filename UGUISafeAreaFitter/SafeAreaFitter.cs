using UnityEngine;

namespace Venus.SafeAreaFitter
{
    public enum RefreshType
    {
        Enable,
        Update
    }
    
    public enum SafeAreaType
    {
        Top,
        Bottom,
        Left,
        Right,
    }
    
    [RequireComponent(typeof(RectTransform))]
    [ExecuteAlways]
    public class SafeAreaFitter : MonoBehaviour
    {
        [SerializeField] private SafeAreaType _fitType = SafeAreaType.Top;
        [SerializeField] private RefreshType _refreshType = RefreshType.Enable;
        [SerializeField] private float _padding = 0;
        
        private RectTransform _rectTransform;
        private DrivenRectTransformTracker _tracker;
    
        void Awake() 
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    
        void OnEnable()
        {
            Fitter();
        }
    
        void Update() 
        {
            if (Application.isPlaying == false)
            {
                Fitter();
            }
            else if (_refreshType == RefreshType.Update)
            {
                Fitter();
            }
        }
    
        private void Fitter()
        {
            _tracker.Clear();
            Rect safeArea = Screen.safeArea;
            float screenHeight = Screen.height;
            float screenWidth = Screen.width;
            Vector3 rectPos = Vector3.zero;
            switch (_fitType)
            {
                case SafeAreaType.Top:
                    _rectTransform.anchorMin = new Vector2(0, 1);
                    _rectTransform.anchorMax = Vector2.one;
                    rectPos.y = -(screenHeight - safeArea.yMax + _padding);
                    break;
                case SafeAreaType.Bottom:
                    _rectTransform.anchorMin = Vector2.zero;
                    _rectTransform.anchorMax = new Vector2(1, 0);
                    rectPos.y = safeArea.y + _padding;
                    break;
                case SafeAreaType.Left:
                    _rectTransform.anchorMin = Vector2.zero;
                    _rectTransform.anchorMax = new Vector2(0, 1);
                    rectPos.x = safeArea.x / 2 + _padding;
                    break;
                case SafeAreaType.Right:
                    _rectTransform.anchorMin = new Vector2(1, 0);
                    _rectTransform.anchorMax = Vector2.one;
                    rectPos.x = -((screenWidth - safeArea.xMax)/2 + _padding);
                    break;
            }
            
            _tracker.Add(this, _rectTransform, DrivenTransformProperties.AnchorMax | DrivenTransformProperties.AnchorMax);
            _rectTransform.anchoredPosition = rectPos;
        }
    }
}
