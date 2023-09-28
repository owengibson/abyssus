/*
    ----------------------- oezyowen --------------------------

    This is a collections of useful methods for use in your projects.
    Enjoy!

    -----------------------------------------------------------
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace oezyowen
{
    /// <summary>
    /// A collection of useful methods for use in Unity project.
    /// </summary>

    public static class Utils
    {
        /// <summary>
        /// Takes in two Vector3s and returns a random Vector3 between them.
        /// </summary>
        /// <param name="min">The minimium inclusive Vector3.</param>
        /// <param name="max">The maximum inclusive Vector3.</param>
        /// <returns>Returns a random Vector3 within the given range.</returns>
        public static Vector3 RandomRangeVector3(Vector3 min, Vector3 max)
        {
            return new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
        }

        /// <summary>
        /// Takes in min and max XYZ values and returns a random Vector3 between them. (Range is inclusive).
        /// </summary>
        /// <param name="xMin">The minimum inclusive X value</param>
        /// <param name="xMax">The maximum inlusive X value </param>
        /// <param name="yMin">The minimum inclusive Y value</param>
        /// <param name="yMax">The maximum inclusive Y value</param>
        /// <param name="zMin">The minimum inclusive Z value</param>
        /// <param name="zMax">The maximum inclusive Z value</param>
        /// <returns>Returns a random Vector3 within the given range.</returns>
        public static Vector3 RandomRangeVector3(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
        {
            return new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax), UnityEngine.Random.Range(zMin, zMax));
        }

        /// <summary>
        /// Takes in two Vector3Ints and returns a random Vector3Int between them.
        /// </summary>
        /// <param name="minInclusive">The minimum inclusive Vector3Int.</param>
        /// <param name="maxExclusive">The maximum exclusive Vector3Int.</param>
        /// <returns>Returns a random Vector3Int within the given range.</returns>
        public static Vector3Int RandomRangeVector3Int(Vector3Int minInclusive, Vector3Int maxExclusive)
        {
            return new Vector3Int(UnityEngine.Random.Range(minInclusive.x, maxExclusive.x), UnityEngine.Random.Range(minInclusive.y, maxExclusive.y), UnityEngine.Random.Range(minInclusive.z, maxExclusive.z));
        }

        /// <summary>
        /// Takes in min and max XYZ values and returns a random Vector3Int between them. (All max values are exclusive).
        /// </summary>
        /// <param name="xMin">The minimum inclusive X value.</param>
        /// <param name="xMax">The maximum exclusive X value.</param>
        /// <param name="yMin">The minimum inclusive Y value.</param>
        /// <param name="yMax">The maximum exclusive Y value.</param>
        /// <param name="zMin">The minimum inclusive Z value.</param>
        /// <param name="zMax">The maximum exclusive Z value.</param>
        /// <returns>Returns a random Vector3Int within the given range.</returns>
        public static Vector3Int RandomRangeVector3Int(int xMin, int xMax, int yMin, int yMax,  int zMin, int zMax)
        {
            return new Vector3Int(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax), UnityEngine.Random.Range(zMin, zMax));
        }

        /// <summary>
        /// Takes in two Vector2s and returns a random Vector2 between them.
        /// </summary>
        /// <param name="min">The minimum inclusive Vector2.</param>
        /// <param name="max">The maximum inclusive Vector2.</param>
        /// <returns>Returns a random Vector2 within the given range.</returns>
        public static Vector2 RandomRangeVector2(Vector2 min, Vector2 max)
        {
            return new Vector2(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y));
        }

        /// <summary>
        /// Take in min and max XY values and returns a random Vector2 between them. (Range is inclusive).
        /// </summary>
        /// <param name="xMin">The minimum inclusive X value.</param>
        /// <param name="xMax">The maximum inclusive X value.</param>
        /// <param name="yMin">The minimum inclusive Y value.</param>
        /// <param name="yMax">The maximum inclusive Y value.</param>
        /// <returns>Returns a random Vector2 within the given range.</returns>
        public static Vector2 RandomRangeVector2(float xMin, float xMax, float yMin, float yMax)
        {
            return new Vector2(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax));
        }

        /// <summary>
        /// Takes in two Vector2Ints and returns a random Vector2Int between them.
        /// </summary>
        /// <param name="minInclusive">The minimum inclusive Vector2Int.</param>
        /// <param name="maxExclusive">The maximum exclusive Vector2Int.</param>
        /// <returns>Returns a random Vector2Int within the given range.</returns>
        public static Vector2Int RandomRangeVector2Int(Vector2Int minInclusive, Vector2Int maxExclusive)
        {
            return new Vector2Int(UnityEngine.Random.Range(minInclusive.x, maxExclusive.x), UnityEngine.Random.Range(minInclusive.y, maxExclusive.y));
        }

        /// <summary>
        /// Takes in min and max XY values and returns a random Vector2Int between them. (All max values are exclusive).
        /// </summary>
        /// <param name="xMin">The minimum inclusive X value.</param>
        /// <param name="xMax">The maximum exclusive X value.</param>
        /// <param name="yMin">The minimum inclusive Y value.</param>
        /// <param name="yMax">The maximum exclusive Y value.</param>
        /// <returns>Returns a random Vector2Int within the given range.</returns>
        public static Vector2Int RandomRangeVector2Int(int xMin, int xMax, int yMin, int yMax)
        {
            return new Vector2Int(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax));
        }

        /// <summary>
        /// Generates a random normalized direction on all axes.
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetRandomDirection()
        {
            return new Vector3(UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

        /// <summary>
        /// Generates a random normalized direction on the X and Y axes.
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetRandomDirectionXY()
        {
            return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

        /// <summary>
        /// Generates a random normalized direction on the X and Z axes.
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetRandomDirectionXZ()
        {
            return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

        /// <summary>
        /// Generates a vector based on a given angle, aligned along the X and Y axes.
        /// </summary>
        /// <param name="angle">An angle in degrees between 0 and 360.</param>
        /// <returns></returns>
        public static Vector2 GetDirectionFromAngleXY(float angle)
        {
            float angleRadians = angle * (Mathf.PI / 180f);
            return new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
        }

        /// <summary>
        /// Generates a vector based on a given angle, aligned along the X and Z axes.
        /// </summary>
        /// <param name="angle">An angle in degrees between 0 and 360</param>
        /// <returns></returns>
        public static Vector3 GetDirectionFromAngleXZ(float angle)
        {
            float angleRadians = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRadians), 0, Mathf.Sin(angleRadians));
        }

        /// <summary>
        /// Generates an angle based on a given Vector2 (aligned on the X and Y axes).
        /// </summary>
        /// <param name="dir">An XY vector or direction. Normalization does not matter.</param>
        /// <returns></returns>
        public static float GetAngleFromVectorXY(Vector2 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360f;
            return n;
        }

        /// <summary>
        /// Generates an angle based on a given Vector3 (aligned on the X and Z axes).
        /// </summary>
        /// <param name="dir">An XZ vector or direction. Normalization does not matter.</param>
        /// <returns></returns>
        public static float GetAngleFromVectorXZ(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360f;
            return n;
        }

        /// <summary>
        /// Generates a random color.
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomColor()
        {
            return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        }

        /// <summary>
        /// Generates a random string of letters and numbers than can be used as a unique ID.
        /// </summary>
        /// <param name="chars">Length of ID string.</param>
        /// <returns></returns>
        public static string GetIDString(int chars = 8)
        {
            string alphabet = "0123456789abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ";
            string ret = "";
            for (int i = 0; i < chars; i++)
            {
                ret += alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
            }
            return ret;
        }

        /// <summary>
        /// Calls a private or public method given the method name and the object it belongs to.
        /// </summary>
        /// <param name="obj">The object that the method belongs to.</param>
        /// <param name="methodName">Name of the method to call.</param>
        /// <returns></returns>
        public static object CallMethod(object obj, string methodName)
        {
            return obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj, null);
        }

        /// <summary>
        /// Gets the value of a private or public field given its name and object.
        /// </summary>
        /// <param name="obj">The object that the field belongs to.</param>
        /// <param name="fieldName">The name of the field to get.</param>
        /// <returns></returns>
        public static object GetField(object obj, string fieldName)
        {
            return obj.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
        }
    }
}
