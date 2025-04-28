using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace PDollarGestureRecognizer
{
    public class PROTOToileMagique : MonoBehaviour
    {
        public Transform gestureOnScreenPrefab;

        private List<Gesture> trainingSet = new List<Gesture>();

        private List<Point> points = new List<Point>();
        private int strokeId = -1;

        private Vector3 virtualKeyPosition = Vector2.zero;
        private Rect drawArea;

        private RuntimePlatform platform;
        private int vertexCount = 0;

        private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
        private LineRenderer currentGestureLineRenderer;

        [SerializeField] private Canvas toileMagiqueCanva;
        private CanvasGroup toileMagiqueCanvasGroup;

        [SerializeField] private GameObject gestureSocket;

        public bool toileEnable = false;

        //GUI
        private bool recognized;

        void Start()
        {
            platform = Application.platform;
            drawArea = toileMagiqueCanva.pixelRect;

            toileMagiqueCanvasGroup = toileMagiqueCanva.GetComponent<CanvasGroup>();

            //Load pre-made gestures
            TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/LostColors/");
            foreach (TextAsset gestureXml in gesturesXml)
                trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
        }

        void OpenAndCloseToileMagique()
        {
            if (toileEnable == false)
            {
                toileMagiqueCanvasGroup.alpha = 1f;
                toileEnable = true;
            }
            else
            {
                toileMagiqueCanvasGroup.alpha = 0f;
                toileEnable = false;
            }
 
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                OpenAndCloseToileMagique();

            if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount > 0)
                {
                    virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
                }
            }
            else
            {
                if (Input.GetMouseButton(0) && toileEnable)
                {
                    virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
                }
            }

            if (drawArea.Contains(virtualKeyPosition))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (recognized)
                    {
                        recognized = false;
                        strokeId = -1;

                        points.Clear();

                        foreach (LineRenderer lineRenderer in gestureLinesRenderer)
                        {

                            lineRenderer.positionCount = 0;
                            Destroy(lineRenderer.gameObject);
                        }

                        gestureLinesRenderer.Clear();
                    }

                    ++strokeId;

                    Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation, gestureSocket.transform) as Transform;
                    currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                    gestureLinesRenderer.Add(currentGestureLineRenderer);

                    vertexCount = 0;
                }

                if (Input.GetMouseButton(0))
                {
                    points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

                    currentGestureLineRenderer.positionCount = ++vertexCount;
                    currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
                }
            }
        }
    }
}

