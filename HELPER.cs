using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace HELPER
{
    public static class COLOR
    {
        public static List<Color> GenerateUniqueColors(int count)
        {
            List<Color> colors = new List<Color>
            {
                RandomColor()
            };

            for (int i = 0; i < count - 1; i++)
            {
                colors.Add(ComplementaryColor(colors[i], 1f / count));
            }

            return colors;
        }

        public static Color RandomColor()
        {
            float h = UnityEngine.Random.value; // Hue between 0 and 1
            float s = UnityEngine.Random.Range(0.6f, 0.9f); // Saturation between 0.5 and 1
            float v = UnityEngine.Random.Range(0.7f, 0.9f); // Value between 0.7 and 0.9
            return Color.HSVToRGB(h, s, v);
        }

        public static Color RandomBackgroundColor()
        {
            float h = UnityEngine.Random.Range(0.25f, 0.8f);
            float s = UnityEngine.Random.Range(0.55f, 0.7f);
            float v = UnityEngine.Random.Range(0.55f, 0.7f);

            float a = UnityEngine.Random.Range(0.75f, 0.9f);
            Color color = Color.HSVToRGB(h, s, v);
            color.a = a;

            return color;
        }

        public static Color AdjustColor(Color color, float hueShift, float valueShift)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);

            h += hueShift;
            if (h > 1f) h -= 1f;
            if (h < 0f) h += 1f;
            
            v += valueShift;
            if (v > 1f) h -= 1f;
            if (v < 0f) h += 1f;

            Color newColor = Color.HSVToRGB(h, s, v);
            newColor.a = color.a;

            return newColor;
        }

        public static Color ComplementaryColor(Color color, float position)
        {
            position = Mathf.Clamp01(position);
            Color.RGBToHSV(color, out float h, out float s, out float v);
            h = (h + position) % 1.0f; // Complementary hue
            return Color.HSVToRGB(h, s, v);
        }

        public static string ToHex(Color color)
        {
            // Convert Color's r, g, b, a components to integers (0-255 range)
            int r = Mathf.RoundToInt(color.r * 255f);
            int g = Mathf.RoundToInt(color.g * 255f);
            int b = Mathf.RoundToInt(color.b * 255f);
            int a = Mathf.RoundToInt(color.a * 255f);

            // Return the color as a hex string (e.g., "#RRGGBB" or "#RRGGBBAA")
            return $"#{r:X2}{g:X2}{b:X2}{(a < 255 ? a.ToString("X2") : "")}";
        }
    }

    public static class COLLECTION
    {
        public static List<T> GetUniqueRandomItems<T>(List<T> items, int count)
        {
            List<T> result = new List<T>();
            List<int> indices = new List<int>();

            while (indices.Count < count)
            {
                int randomIndex = UnityEngine.Random.Range(0, items.Count);
                if (!indices.Contains(randomIndex))
                {
                    indices.Add(randomIndex);
                    result.Add(items[randomIndex]);
                }
            }

            return result;
        }

        public static List<T> ShuffleList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T temp = list[i];
                int randomIndex = UnityEngine.Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
            return list;
        }

        public static bool AreSequencesEqual<T>(List<T> sequence1, List<T> sequence2)
        {
            if (sequence1.Count != sequence2.Count)
            {
                return false;
            }

            for (int i = 0; i < sequence1.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(sequence1[i], sequence2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static T[,] ConvertListTo2DArray<T>(List<T> list, int columns)
        {
            int rows = Mathf.CeilToInt(list.Count / (float)columns);

            T[,] array2D = new T[rows, columns];

            for (int i = 0; i < list.Count; i++)
            {
                int row = i / columns;
                int col = i % columns;
                array2D[row, col] = list[i];
            }

            return array2D;
        }

        public static List<T> Convert2DArrayToList<T>(T[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            List<T> result = new List<T>(rows * cols);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result.Add(array[row, col]);
                }
            }

            return result;
        }

        public static T[,] FlipHorizontal<T>(T[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            T[,] result = new T[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = array[row, cols - 1 - col];
                }
            }
            return result;
        }

        public static T[,] FlipVertical<T>(T[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            T[,] result = new T[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = array[rows - 1 - row, col];
                }
            }
            return result;
        }

        public static T[,] Rotate90ToLeft<T>(T[,] array)
        {
            int n = array.GetLength(0);
            if (n != array.GetLength(1)) throw new InvalidOperationException("Array must be square");

            T[,] result = new T[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[n - j - 1, i] = array[i, j];
                }
            }
            return result;
        }

        public static T[,] Rotate90ToRight<T>(T[,] array)
        {
            int n = array.GetLength(0);
            if (n != array.GetLength(1)) throw new InvalidOperationException("Array must be square");

            T[,] result = new T[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[j, n - i - 1] = array[i, j];
                }
            }
            return result;
        }

        public static T[,] Rotate180<T>(T[,] array)
        {
            int n = array.GetLength(0);
            int m = array.GetLength(1);
            T[,] result = new T[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = array[n - 1 - i, m - 1 - j];
                }
            }

            return result;
        }

        public static T[,] FlipDiagonal<T>(T[,] array)
        {
            int n = array.GetLength(0);
            if (n != array.GetLength(1)) throw new InvalidOperationException("Array must be square");

            T[,] result = new T[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[j, i] = array[i, j];
                }
            }
            return result;
        }

        public static T[,] FlipAntiDiagonal<T>(T[,] array)
        {
            int n = array.GetLength(0);
            if (n != array.GetLength(1)) throw new InvalidOperationException("Array must be square");

            T[,] result = new T[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[j, i] = array[n - 1 - i, n - 1 - j];
                }
            }
            return result;
        }

        public static T[,] RandomTransform<T>(T[,] array)
        {
            int n = array.GetLength(0);
            int m = array.GetLength(1);

            if (n == m)
            {
                // Square array transformations
                int transformation = UnityEngine.Random.Range(0, 8); // 0 to 7
                switch (transformation)
                {
                    case 0: return FlipHorizontal(array);
                    case 1: return FlipVertical(array);
                    case 2: return Rotate180(array);
                    case 3: return Rotate90ToRight(array);
                    case 4: return Rotate90ToLeft(array);
                    case 5: return FlipDiagonal(array);
                    case 6: return FlipAntiDiagonal(array);
                    case 7: return array; // No transformation
                    default: return array; // No transformation
                }
            }
            else

            {
                // Non-square array transformations
                int transformation = UnityEngine.Random.Range(0, 4); // 0 to 3
                switch (transformation)
                {
                    case 0: return FlipHorizontal(array);
                    case 1: return FlipVertical(array);
                    case 2: return Rotate180(array);
                    case 3: return array; // No transformation
                    default: return array; // No transformation
                }
            }
        }

        public static string GenerateConfigUniqueId()
        {
            string _configId = Guid.NewGuid().ToString();
            return _configId;
        }

        public static void LogLargeData<T>(T data , string keyBase)
        {
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);
            int chunkSize = 8 * 1024; // 8KB per log entry
            int totalChunks = (dataToStore.Length + chunkSize - 1) / chunkSize;

            for (int i = 0; i < totalChunks; i++)
            {
                int startIndex = i * chunkSize;
                int length = Math.Min(chunkSize, dataToStore.Length - startIndex);
                string chunk = dataToStore.Substring(startIndex, length);
                Debug.Log($"{keyBase}_chunk_{i}:\n{chunk}");
            }
        }

        public static void SaveLargeDataToPlayerPrefs<T>(T data, string keyBase)
        {
            try
            {
                string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);
                int chunkSize = 8 * 1024; // 8KB per chunk
                int totalChunks = (dataToStore.Length + chunkSize - 1) / chunkSize;

                for (int i = 0; i < totalChunks; i++)
                {
                    int startIndex = i * chunkSize;
                    int length = Math.Min(chunkSize, dataToStore.Length - startIndex);
                    string chunk = dataToStore.Substring(startIndex, length);
                    PlayerPrefs.SetString($"{keyBase}_chunk_{i}", chunk);
                }
                PlayerPrefs.SetInt($"{keyBase}_totalChunks", totalChunks);
                PlayerPrefs.Save();
                //LogLargeData(LoadFromPlayerPrefs<T>(keyBase), keyBase);
            }
            catch (Exception e)
            {
                Debug.LogError("Couldn't save data to PlayerPrefs with key " + keyBase + "\n" + e);
            }
        }

        public static T LoadLargeDataFromPlayerPrefs<T>(string keyBase)
        {
            T loadedData = default;
            try
            {
                if (PlayerPrefs.HasKey($"{keyBase}_totalChunks"))
                {
                    int totalChunks = PlayerPrefs.GetInt($"{keyBase}_totalChunks");
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < totalChunks; i++)
                    {
                        string chunk = PlayerPrefs.GetString($"{keyBase}_chunk_{i}");
                        sb.Append(chunk);
                    }

                    string dataToLoad = sb.ToString();
                    loadedData = JsonConvert.DeserializeObject<T>(dataToLoad);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Couldn't load data from PlayerPrefs with key " + keyBase + "\n" + e);
            }
            return loadedData;
        }

        public static string ToString<T>(T[,] array)
        {
            StringBuilder sb = new StringBuilder();
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(array[i, j] + "\t");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    public static class ROTATION
    {
        public static Quaternion GetNearestFaceRotation(Quaternion quaternion)
        {
            // Get the current rotation of the cube in Euler angles
            Vector3 currentRotation = quaternion.eulerAngles;

            // Normalize the rotation to be within the range of -180 to 180 degrees
            currentRotation.x = NormalizeAngle(currentRotation.x);
            currentRotation.y = NormalizeAngle(currentRotation.y);
            currentRotation.z = NormalizeAngle(currentRotation.z);

            // Find the nearest standard rotation (-180, -90, 0, 90, 180) for each axis
            currentRotation.x = FindNearestStandardAngle(currentRotation.x);
            currentRotation.y = FindNearestStandardAngle(currentRotation.y);
            currentRotation.z = FindNearestStandardAngle(currentRotation.z);

            // Return the adjusted rotation as a Quaternion
            return Quaternion.Euler(currentRotation);
        }

        // Normalize angle to be within the range of -180 to 180 degrees
        public static float NormalizeAngle(float angle)
        {
            while (angle > 180)
            {
                angle -= 360;
            }
            while (angle < -180)
            {
                angle += 360;
            }
            return angle;
        }

        // Find the nearest angle from the set {-180, -90, 0, 90, 180}
        public static float FindNearestStandardAngle(float angle)
        {
            float[] standardAngles = { -180f, -90f, 0f, 90f, 180f };
            float nearestAngle = standardAngles[0];
            float minDifference = Mathf.Abs(angle - nearestAngle);

            foreach (float standardAngle in standardAngles)
            {
                float difference = Mathf.Abs(angle - standardAngle);
                if (difference < minDifference)
                {
                    nearestAngle = standardAngle;
                    minDifference = difference;
                }
            }

            return nearestAngle;
        }

        public static Quaternion GetRandomFaceRotation()
        {
            // Define the set of possible face rotation angles
            float[] faceAngles = { -180f, -90f, 0f, 90f, 180f };

            // Randomly select an angle for each axis
            float randomX = faceAngles[UnityEngine.Random.Range(0, faceAngles.Length)];
            float randomY = faceAngles[UnityEngine.Random.Range(0, faceAngles.Length)];
            float randomZ = faceAngles[UnityEngine.Random.Range(0, faceAngles.Length)];

            // Create and return the Quaternion with the random face rotation
            return Quaternion.Euler(randomX, randomY, randomZ);
        }

        public static Quaternion GetRandomRotation()
        {
            // Generate random values for the Quaternion components
            float x = UnityEngine.Random.Range(-1f, 1f);
            float y = UnityEngine.Random.Range(-1f, 1f);
            float z = UnityEngine.Random.Range(-1f, 1f);
            float w = UnityEngine.Random.Range(-1f, 1f);

            // Normalize the Quaternion to ensure it represents a valid rotation
            Quaternion randomRotation = new Quaternion(x, y, z, w);
            randomRotation.Normalize();

            return randomRotation;
        }

        public static void RotateCube(Transform cube, Vector2 delta)
        {
            float rotationSpeedX = 0.4f;
            float rotationSpeedY = 0.4f;

            cube.Rotate(Vector3.up, -delta.x * rotationSpeedX, Space.World);
            cube.Rotate(Vector3.right, delta.y * rotationSpeedY, Space.World);
        }

        public static IEnumerator ApplyInertia(Transform _cube, Vector2 initialVelocity, float inertiaDuration)
        {
            float time = 0f;
            Vector2 currentVelocity;

            while (time < inertiaDuration)
            {
                // Reduce the velocity over time to simulate friction/inertia
                currentVelocity = Vector2.Lerp(initialVelocity, Vector2.zero, time / inertiaDuration);

                // Rotate the cube based on the current velocity
                RotateCube(_cube, currentVelocity * Time.deltaTime * 0.4f);

                time += Time.deltaTime;
                yield return null;
            }

            // After inertia, rotate to the nearest face
            //StartCoroutine(RotateToNearestFace(_cube));
        }

        public static IEnumerator RotateToNearestFace(Transform _cube, float delay = 0)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            Quaternion targetRotation = GetNearestFaceRotation(_cube.rotation);
            float rotationSpeed = 10f;

            while (Quaternion.Angle(_cube.rotation, targetRotation) > 1f)
            {
                _cube.rotation = Quaternion.Slerp(_cube.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                yield return null;
            }

            _cube.rotation = targetRotation;
        }

        public static IEnumerator RotateToTargetRotation(Transform _cube, Quaternion targetRotation, float delay = 0)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            float rotationSpeed = 10f;

            while (Quaternion.Angle(_cube.rotation, targetRotation) > 1f)
            {
                _cube.rotation = Quaternion.Slerp(_cube.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                yield return null;
            }

            _cube.rotation = targetRotation;
        }

        public static IEnumerator SyncImitatorRotationWithCube(Transform cube, Transform imitator)
        {
            while (true && cube != null && imitator != null)
            {
                imitator.rotation = cube.rotation;
                yield return null;
            }
        }

        public static float CalculateInertiaDuration(Vector2 _dragVelocity)
        {
            return Mathf.Clamp01(Vector2.Distance(_dragVelocity, Vector2.zero) / 3000f);
        }

        public static Vector2 GetRandomVector2(float _size)
        {
            // Generate a random angle in radians
            float randomAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);

            // Calculate the x and y components using the angle
            float x = Mathf.Cos(randomAngle);
            float y = Mathf.Sin(randomAngle);

            // Create the Vector2 with the calculated direction and scale it to the desired size
            Vector2 randomVector = new Vector2(x, y) * _size;

            return randomVector;
        }

        public static Vector2 RotateVector(Vector2 vector, float angleDegrees)
        {
            float angleRadians = angleDegrees * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angleRadians);
            float sin = Mathf.Sin(angleRadians);
            return new Vector2(
                vector.x * cos - vector.y * sin,
                vector.x * sin + vector.y * cos
            );
        }
    }

    public static class TIME
    {
        public static readonly string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public static string Now()
        {
            var gregorianCalendar = new GregorianCalendar();
            var dateTimeFormat = new DateTimeFormatInfo
            {
                Calendar = gregorianCalendar,
                FullDateTimePattern = DateTimeFormat
            };

            return DateTime.Now.ToString(DateTimeFormat, dateTimeFormat);
        }

        public static float CalculateTotalTime(string startTime, string endTime)
        {
            DateTime startDateTime = DateTime.ParseExact(startTime, DateTimeFormat, CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.ParseExact(endTime, DateTimeFormat, CultureInfo.InvariantCulture);

            TimeSpan timeDifference = endDateTime - startDateTime;

            return (float)timeDifference.TotalSeconds * 1000;

            // Optionally, you could return it in minutes, hours, etc.:
            // totalTime = (float)timeDifference.TotalMinutes;  // Total minutes
            // totalTime = (float)timeDifference.TotalHours;    // Total hours
        }
    }
}
