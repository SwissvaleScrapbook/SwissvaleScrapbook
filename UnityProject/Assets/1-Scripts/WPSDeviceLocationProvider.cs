namespace Mapbox.Unity.Location
{
	using System.Collections;
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.CheapRulerCs;
	using System;

	using NianticSpatial.NSDK.AR.Utilities;
	using UnityEngine.XR.ARFoundation;
	using NianticSpatial.NSDK.AR.Utilities.Logging;
	using NianticSpatial.NSDK.AR.XRSubsystems;

	public class WPSDeviceLocationProvider : AbstractLocationProvider
	{
		[SerializeField] public NianticSpatial.NSDK.AR.WorldPositioning.ARWorldPositioningManager positioningManager;
		private NianticSpatial.NSDK.AR.WorldPositioning.ARWorldPositioningCameraHelper cameraHelper;

		/// <summary>
		/// Using higher value like 500 usually does not require to turn GPS chip on and thus saves battery power. 
		/// Values like 5-10 could be used for getting best accuracy.
		/// </summary>
		[SerializeField]
		[Tooltip("Using higher value like 500 usually does not require to turn GPS chip on and thus saves battery power. Values like 5-10 could be used for getting best accuracy.")]
		public float _desiredAccuracyInMeters = 1.0f;


		/// <summary>
		/// The minimum distance (measured in meters) a device must move laterally before Input.location property is updated. 
		/// Higher values like 500 imply less overhead.
		/// </summary>
		[SerializeField]
		[Tooltip("The minimum distance (measured in meters) a device must move laterally before Input.location property is updated. Higher values like 500 imply less overhead.")]
		public float _updateDistanceInMeters = 0.0f;


		[SerializeField]
		[Tooltip("The minimum time interval between location updates, in milliseconds. It's reasonable to not go below 500ms.")]
		public long _updateTimeInMilliSeconds = 500;


		[SerializeField]
		[Tooltip("Smoothing strategy to be applied to the UserHeading.")]
		public AngleSmoothingAbstractBase _userHeadingSmoothing;


		[SerializeField]
		[Tooltip("Smoothing strategy to applied to the DeviceOrientation.")]
		public AngleSmoothingAbstractBase _deviceOrientationSmoothing;


		[Serializable]
		public struct DebuggingInEditor
		{
			[Header("Set 'EditorLocationProvider' to 'DeviceLocationProvider' and connect device with UnityRemote.")]
			[SerializeField]
			[Tooltip("Mock Unity's 'Input.Location' to route location log files through this class (eg fresh calculation of 'UserHeading') instead of just replaying them. To use set 'Editor Location Provider' in 'Location Factory' to 'Device Location Provider' and select a location log file below.")]
			public bool _mockUnityInputLocation;

			[SerializeField]
			[Tooltip("Also see above. Location log file to mock Unity's 'Input.Location'.")]
			public TextAsset _locationLogFile;
		}

		[Space(20)]
		public DebuggingInEditor _editorDebuggingOnly;

		private Coroutine _pollRoutine;
		private double _lastLocationTimestamp;
		private WaitForSeconds _wait1sec;
		private WaitForSeconds _waitUpdateTime;
		/// <summary>list of positions to keep for calculations</summary>
		private CircularBuffer<Vector2d> _lastPositions;
		/// <summary>number of last positons to keep</summary>
		private int _maxLastPositions = 5;
		/// <summary>minimum needed distance between oldest and newest position before UserHeading is calculated</summary>
		private double _minDistanceOldestNewestPosition = 1.5;

		private IMapboxLocationService _mockLocationService;

		private NianticSpatial.NSDK.AR.LocationService _locationService;

		//using usingEditor as an alternative to conditional if
		//this is because I(Nickhil) always want the WPS code in this file to be compiled so I can check the editor for compilation errors (I have an android phone, which this project doesn't support yet)
		private bool usingEditor = false;

		// Android 6+ permissions have to be granted during runtime
		// these are the callbacks for requesting location permission
		// TODO: show message to users in case they accidentallly denied permission
#if UNITY_ANDROID
			private bool _gotPermissionRequestResponse = false;

			private void OnAllow() { _gotPermissionRequestResponse = true; }
			private void OnDeny() { _gotPermissionRequestResponse = true; }
			private void OnDenyAndNeverAskAgain() { _gotPermissionRequestResponse = true; }
#endif


		protected virtual void Awake()
		{
#if UNITY_EDITOR
			usingEditor = true;
			if (_editorDebuggingOnly._mockUnityInputLocation)
			{
				if (null == _editorDebuggingOnly._locationLogFile || null == _editorDebuggingOnly._locationLogFile.bytes)
				{
					throw new ArgumentNullException("Location Log File");
				}

				_mockLocationService = new MapboxLocationServiceMock(_editorDebuggingOnly._locationLogFile.bytes);
			}
			else
			{
#endif
				_mockLocationService = new MapboxLocationServiceUnityWrapper();
#if UNITY_EDITOR
			}
#endif
			if (usingEditor == false)
			{
				_locationService.Start(_desiredAccuracyInMeters, _updateDistanceInMeters);
			}

			_currentLocation.Provider = "unity";
			_wait1sec = new WaitForSeconds(1f);
			_waitUpdateTime = _updateTimeInMilliSeconds < 500 ? new WaitForSeconds(0.5f) : new WaitForSeconds((float)_updateTimeInMilliSeconds / 1000.0f);

			if (null == _userHeadingSmoothing) { _userHeadingSmoothing = transform.gameObject.AddComponent<AngleSmoothingNoOp>(); }
			if (null == _deviceOrientationSmoothing) { _deviceOrientationSmoothing = transform.gameObject.AddComponent<AngleSmoothingNoOp>(); }

			_lastPositions = new CircularBuffer<Vector2d>(_maxLastPositions);

			if (_pollRoutine == null)
			{
				// TODO: come back to this and implement PollLocationRoutine
				//_pollRoutine = StartCoroutine(PollLocationRoutine());
			}
		}

		protected virtual void Start()
        {
			cameraHelper = positioningManager.DefaultCameraHelper;
        }
	}
}
