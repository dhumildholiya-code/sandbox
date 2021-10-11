using UnityEngine;

namespace Mandelbrot_fractal_explorer
{
    public class Explorer : MonoBehaviour
    {
        public Material mat;
        public Vector2 position;
        public float scale, angle;

        private Vector2 _smoothPosition;
        private float _smoothScale, _smoothAngle;

        private static readonly int Area = Shader.PropertyToID("_Area");
        private static readonly int Angle = Shader.PropertyToID("_Angle");

        private void FixedUpdate()
        {
            HandleInputs();
            UpdateShader();
        }

        private void HandleInputs()
        {
            if (Input.GetKey(KeyCode.KeypadPlus))
                scale *= 0.99f;
            if (Input.GetKey(KeyCode.KeypadMinus))
                scale *= 1.01f;


            if (Input.GetKey(KeyCode.Q))
                angle += 0.01f;
            if (Input.GetKey(KeyCode.E))
                angle -= 0.01f;

            Vector2 dir = new Vector2(.01f * scale, 0);
            float s = Mathf.Sin(angle);
            float c = Mathf.Cos(angle);
            dir = new Vector2(dir.x * c, dir.x * s);

            if (Input.GetKey(KeyCode.A))
                position -= dir;
            if (Input.GetKey(KeyCode.D))
                position += dir;

            dir = new Vector2(-dir.y, dir.x);

            if (Input.GetKey(KeyCode.W))
                position += dir;
            if (Input.GetKey(KeyCode.S))
                position -= dir;
        }

        private void UpdateShader()
        {
            _smoothPosition = Vector2.Lerp(_smoothPosition, position, 0.03f);
            _smoothScale = Mathf.Lerp(_smoothScale, scale, 0.03f);
            _smoothAngle = Mathf.Lerp(_smoothAngle, angle, 0.03f);

            float aspect = (float) Screen.width / (float) Screen.height;
            float scaleX = _smoothScale;
            float scaleY = _smoothScale;
            if (aspect > 1f)
            {
                scaleY /= aspect;
            }
            else
            {
                scaleX *= aspect;
            }

            mat.SetVector(Area, new Vector4(_smoothPosition.x, _smoothPosition.y, scaleX, scaleY));
            mat.SetFloat(Angle, _smoothAngle);
        }
    }
}