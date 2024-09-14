# Unity Helper Library

This repository contains a utility library for Unity, providing helper functions for working with colors, collections, rotations, and more. The aim is to simplify common tasks and improve code reuse across Unity projects.

## Features
- **Color Utilities**: Generate random or complementary colors, adjust hues, and convert to hex.
- **Collection Utilities**: Shuffle, rotate, and transform collections (lists, arrays) with various helper methods.
- **Rotation Utilities**: Handle quaternion rotations and alignments in 3D space.
- **Large Data Handling**: Serialize large data to `PlayerPrefs` or logs.
- **Time Utilities**: Work with time and date formats.

---

## Installation

1. Download the `HELPER` script.
2. Import the `Helper` namespace into your Unity project:
   ```csharp
   using HELPER;
   ```

---

## Usage

### 1. Color Utilities

#### **Generate Random Colors**
Generate a list of unique random colors with ease:

```csharp
int colorCount = 5;
List<Color> uniqueColors = COLOR.GenerateUniqueColors(colorCount);
```

#### **Get a Random Color**
Generate a single random color:

```csharp
Color randomColor = COLOR.RandomColor();
```

#### **Adjust a Color**
Modify the hue and value of a given color:

```csharp
Color adjustedColor = COLOR.AdjustColor(Color.red, 0.1f, -0.2f);
```

#### **Convert Color to Hex**
Convert any `Color` to a hex string for use in UIs or serialization:

```csharp
string hexColor = COLOR.ToHex(Color.blue);
// Returns "#0000FF"
```

### 2. Collection Utilities

#### **Shuffle a List**
Shuffle any list of elements randomly:

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
List<int> shuffledNumbers = COLLECTION.ShuffleList(numbers);
```

#### **Get Unique Random Items from a List**
Select a unique subset of items from a list:

```csharp
List<int> selectedItems = COLLECTION.GetUniqueRandomItems(numbers, 3);
```

#### **Rotate a 2D Array**
Perform various transformations on a 2D array:

```csharp
int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

// Flip horizontally
int[,] flippedHorizontal = COLLECTION.FlipHorizontal(matrix);

// Rotate 90 degrees to the right
int[,] rotatedRight = COLLECTION.Rotate90ToRight(matrix);

// Log results
Debug.Log($"Original: {COLLECTION.ToString(matrix)}");
Debug.Log($"Flipped Horizontal: {COLLECTION.ToString(flippedHorizontal)}");
Debug.Log($"Rotated Right: {COLLECTION.ToString(rotatedRight)}");
```

### 3. Rotation Utilities

#### **Get Nearest Face Rotation**
Snap an object’s rotation to the nearest face of a cube:

```csharp
Quaternion currentRotation = someTransform.rotation;
Quaternion nearestFaceRotation = ROTATION.GetNearestFaceRotation(currentRotation);
```

#### **Rotate an Object with Inertia**
Apply a smooth rotation effect with inertia:

```csharp
StartCoroutine(ROTATION.ApplyInertia(cubeTransform, new Vector2(1f, 2f), 1f));
```

#### **Sync Rotation with Imitator**
Keep an imitator object's rotation in sync with another object:

```csharp
StartCoroutine(ROTATION.SyncImitatorRotationWithCube(cubeTransform, imitatorTransform));
```

### 4. Large Data Utilities

#### **Save and Load Large Data to PlayerPrefs**
Efficiently save large JSON data by splitting it into chunks and storing it in `PlayerPrefs`:

```csharp
// Save
COLLECTION.SaveLargeDataToPlayerPrefs(largeDataObject, "unique_key");

// Load
var loadedData = COLLECTION.LoadLargeDataFromPlayerPrefs<MyDataClass>("unique_key");
```

#### **Log Large Data**
Log large amounts of data to the console in chunks:

```csharp
COLLECTION.LogLargeData(largeDataObject, "data_key");
```

### 5. Time Utilities

#### **Get the Current Date and Time**
Fetch the current date and time in a standard format:

```csharp
string currentTime = TIME.Now();
// Returns "2024-09-14 12:34:56.789"
```

#### **Calculate Total Time**
Calculate the total time elapsed between two timestamps:

```csharp
string startTime = TIME.Now();
// Simulate some delay
yield return new WaitForSeconds(2);
string endTime = TIME.Now();

float elapsedTime = TIME.CalculateTotalTime(startTime, endTime);
Debug.Log($"Elapsed Time: {elapsedTime} ms");
```

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
