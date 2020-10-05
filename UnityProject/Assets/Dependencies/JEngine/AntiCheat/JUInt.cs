//
// JUInt.cs
//
// Author:
//       JasonXuDeveloper（傑） <jasonxudeveloper@gmail.com>
//
// Copyright (c) 2020 JEngine
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
namespace JEngine.AntiCheat
{
    public struct JUInt
    {
        private uint _obscuredUInt;
        private int _obscuredKey;

        private uint Value
        {
            get => (uint)(_obscuredUInt-_obscuredKey);
            
            set
            {
                unchecked
                {
                    _obscuredKey = (int) JRandom.RandomNum((int)(int.MaxValue - value));
                    _obscuredUInt = (uint) (value + _obscuredKey);
                }
            }
        }

        public JUInt(uint val = 0)
        {
            _obscuredUInt = 0;
            _obscuredKey = 0;
            Value = val;
        }
        public JUInt(int val = 0)
        {
            _obscuredUInt = 0;
            _obscuredKey = 0;
            Value = val > uint.MaxValue ? uint.MaxValue : (uint) val;
        }
        
        public JUInt(string val = "0")
        {
            _obscuredUInt = 0;
            _obscuredKey = 0;
            var result = uint.TryParse(val,out var _value);
            if (!result)
            {
                JEngine.Core.Log.PrintError($"无法将{val}变为{Value.GetType()},已改为0");
                Value = 0;
            }
            else
            {
                Value = _value;
            }
        }

        public static implicit operator JUInt(uint val) => new JUInt(val);
        public static implicit operator uint(JUInt val) => val.Value;
        public static bool operator ==(JUInt a, JUInt b) => a.Value == b.Value;
        public static bool operator !=(JUInt a, JUInt b) => a.Value != b.Value;

        public static JUInt operator ++(JUInt a)
        {
            a.Value++;
            return a;
        }

        public static JUInt operator --(JUInt a)
        {
            a.Value--;
            return a;
        }
        
        public static JUInt operator + (JUInt a, JUInt b) => new JUInt(a.Value + b.Value);
        public static JUInt operator + (JUInt a, uint b) => new JUInt(a.Value + b);
        
        public static JUInt operator - (JUInt a, JUInt b) => new JUInt(a.Value - b.Value);
        public static JUInt operator - (JUInt a, uint b) => new JUInt(a.Value - b);

        public static JUInt operator * (JUInt a, JUInt b) => new JUInt(a.Value * b.Value);
        public static JUInt operator * (JUInt a, uint b) => new JUInt(a.Value * b);

        public static JUInt operator / (JUInt a, JUInt b) => new JUInt(a.Value / b.Value);
        public static JUInt operator / (JUInt a, uint b) => new JUInt(a.Value / b);
        
        public static JUInt operator % (JUInt a, JUInt b) => new JUInt(a.Value % b.Value);
        public static JUInt operator % (JUInt a, uint b) => new JUInt(a.Value % b);

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => Value.Equals((obj is JUInt ? (JUInt) obj : default).Value);
    }
}