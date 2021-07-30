using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CAH.Game.Utility
{ 
    /// <summary>
    /// BigInteger의 단위를 표현할 수 있는 클래스
    /// </summary>
    public static class BigIntegerManager
    {      
        private static readonly BigInteger _unitSize = 1000;  
        private static Dictionary<string, BigInteger> _unitsMap = new Dictionary<string, BigInteger>();
        private static Dictionary<string, int> _idxMap = new Dictionary<string, int>();

        private static readonly List<string> _units = new List<string>();
        private static int _unitCapacity = 5; 
        private static readonly int _asciiA = 65;
        private static readonly int _asciiZ = 90;
        private static bool isInitialize = false;
        private static void UnitInitialize(int capacity)
        {
            _unitCapacity += capacity;
            
            //Initialize 0~999
            _units.Clear();  
            _unitsMap.Clear();
            _idxMap.Clear(); 
            _units.Add("");
            _unitsMap.Add("", 0);
            _idxMap.Add("", 0);   
            
            
            for (int n = 0; n <= _unitCapacity; n++)
            {
                for (int i = _asciiA; i <= _asciiZ; i++)
                {
                    string unit = null;
                    if (n == 0) 
                        unit = ((char) i).ToString();
                    else
                    {
                        var nCount = (float)n / 26; 
                        var nextChar = _asciiA + n - 1;  
                        var fAscii = (char) nextChar;
                        var tAscii = (char) i;
                        unit = $"{fAscii}{tAscii}"; 
                    }  
                    _units.Add(unit); 
                    _unitsMap.Add(unit, BigInteger.Pow(_unitSize, _units.Count)); 
                    _idxMap.Add(unit, _units.Count-1);
                }
            }    
            isInitialize = true;
        }

        private static (int value, int idx) GetSize(BigInteger value)
        { 
            //단위를 구하기 위한 값으로 복사
            var currentValue = value; 
            var current = (value / _unitSize) % _unitSize;
            var idx = 0;
            while (currentValue > _unitSize -1)
            {
                currentValue /= _unitSize;
                idx += 1;
            }
                
            //유닛 단위가 idx보다 적으면 새로운 단위를 새롭게 추가 
            //단위가 무한대로 늘어나기 위한 작업
            while (_units.Count <= idx) 
                UnitInitialize(5); 
            
            return ((int)currentValue, idx);
        }

        /// <summary>
        /// 숫자를 단위로 리턴
        /// </summary>
        /// <param name="value">값</param>
        /// <returns></returns>
        public static string GetUnit(BigInteger value)
        {
            if (isInitialize == false) 
                UnitInitialize(5);
            
            var sizeStruct = GetSize(value);
            return  string.Format("{0}{1}", sizeStruct.value, _units[sizeStruct.idx]); 
        }  
        
        /// <summary>
        /// 단위를 숫자로 변경
        /// 10A = 10000으로 리턴
        /// </summary>
        /// <param name="unit">단위</param>
        /// <returns></returns>
        public static BigInteger UnitToValue(string unit)
        {

            var value = BigInteger.Parse((Regex.Replace(unit, "[^0-9]", "")));
            var unitStr = Regex.Replace(unit, "[^A-Z]", ""); 
            while (_unitsMap.ContainsKey(unitStr) == false) 
                UnitInitialize(5); 
            return _unitsMap[unitStr] * value;
        }
    }
}
