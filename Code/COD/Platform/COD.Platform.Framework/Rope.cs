using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Framework
{
    public class Rope
    {
        public Rope()
        {

        }

        public Rope(string value)
        {

            Leaf = new LeafSection(value, 0, value.Length);
        }
        Rope _Left = null;
        private Rope Left { get { return _Left; } set { if (value != null) Leaf = null; _Left = value; } }
        private Rope Right { get; set; }


        private LeafSection Leaf { get; set; }


        public bool IsLeaf { get { return Left == null; } }





        public char this[int index]
        {
            get
            {
                if (IsLeaf)
                    return Leaf[index];
                else
                {
                    var leftLen = Left.Length;
                    if (index < leftLen)
                        return Left[index];
                    else
                        return Right[index - leftLen];
                }
            }
        }


        public int Length
        {
            get
            {
                if (Left == null)
                {
                    return Leaf?.Length ?? 0;
                }
                else
                {
                    return Left.Length + (Right?.Length ?? 0);

                }
            }
        }


        public Rope Append(string value)
        {
            if (IsLeaf)
            {
                if (Leaf != null)
                {
                    Left = new Rope { Leaf = Leaf };
                    Right = new Rope(value);
                }
                else
                {
                    this.Leaf = new LeafSection(value);
                }
            }
            else
            {
                if (this.Left == null)
                {
                    Left = new Rope { Leaf = Leaf };
                }
                if (Right == null)
                {
                    Right = new Rope(value);
                }
                else
                {
                    Right.Append(value);
                }
            }
            return this;
        }
        public Rope Append(Rope value)
        {
            if (this.Left == null)
            {
                Left = new Rope { Leaf = Leaf };
            }
            if (Right == null)
            {
                Right = value;
            }
            else
            {
                Right.Append(value);
            }
            return this;
        }
        public Rope Insert(string value, int position)
        {

            if (this.IsLeaf)
            {
                if (position == 0)
                {
                    //Insert before this
                    Right = new Rope { Leaf = Leaf };
                    Left = new Rope(value);
                }
                else if (position == Length)
                {
                    //Insert after this
                    Left = new Rope { Leaf = Leaf };
                    Right = new Rope(value);
                }
                else
                {
                    //its in the middle of this bit
                    var newLeft = new Rope();
                    var newLeftLen = position;
                    newLeft.Left = new Rope { Leaf = new LeafSection(Leaf.Base, Leaf.Start, newLeftLen) };
                    newLeft.Right = new Rope(value);
                    var newRightStart = Leaf.Start + newLeftLen;
                    Right = new Rope { Leaf = new LeafSection(Leaf.Base, newRightStart, Leaf.Length - newRightStart) };
                    Left = newLeft;

                }
            }
            else
            {
                var leftLen = Left.Length;
                if (position < leftLen)
                {
                    Left.Insert(value, position);
                }
                else
                {
                    Right.Insert(value, position - leftLen);
                }
            }

            return this;
        }


        public Rope Remove(string value)
        {
            return Replace(value, null);
        }


        public Rope Remove(int start, int length)
        {
            if (IsLeaf)
            {
                if (Leaf.Length < start) throw new ArgumentOutOfRangeException("Start is after the length of this string. Cant remove");
                if (start + length < Leaf.Length)
                {
                    //need to split the leaf into left and right
                    var newRight = new Rope { Leaf = new LeafSection(Leaf.Base, Leaf.Start + start + length, Leaf.Length - (start + length)) };
                    var newLeft = new Rope { Leaf = new LeafSection(Leaf.Base, Leaf.Start, start) };
                    this.Right = newRight;
                    this.Left = newLeft;


                }
                else if (start == 0)
                {
                    //need to remove the begining
                    Leaf.Start += length;
                    Leaf.Length -= length;
                }
                else
                {
                    //need to remove the end 
                    Leaf.Length = start;
                }
            }
            else
            {
                var leftLen = Left.Length;
                if (start < leftLen)
                {
                    var lenInLeft = Math.Min(leftLen - start, length);
                    Left.Remove(start, lenInLeft);
                    if (lenInLeft < length)
                        Right.Remove(0, length - lenInLeft);
                }
                else
                {
                    Right.Remove(start - leftLen, length);
                 
                }
                if (Right?.Length == 0)
                    Right = null;

                if (Left?.Length == 0)
                {
                    //if left is empty then move right to left
                    if (Right == null) Left = null;
                    else { Left = Right; Right = null; }
                }

            }


            return this;
        }

        public Rope Replace(string valueToRemove, string replacementValue, int startIndex=0)
        {

            if (string.IsNullOrEmpty(valueToRemove)) throw new ArgumentException("Value must be a string with non-zero length");
            if (string.IsNullOrEmpty(valueToRemove)) throw new ArgumentException("Value must be a string with non-zero length");
            if (startIndex > Length - 1) throw new ArgumentOutOfRangeException("startIndex must be a value inside the Rope");


            int currentSearchIndex = startIndex;
            int lastSearchIndex = Length - valueToRemove.Length;
            int valueLength = valueToRemove.Length;
            int skipReplacementIncrement = replacementValue == null ? 0 : replacementValue.Length - 1;
            for (; currentSearchIndex <= lastSearchIndex; currentSearchIndex++)
            {
                var seekAheadIndex = 0;
                while (seekAheadIndex < valueLength && valueToRemove[seekAheadIndex] == this[currentSearchIndex + seekAheadIndex])
                {
                    seekAheadIndex++;
                }

                if (seekAheadIndex == valueLength)
                {
                    Remove(currentSearchIndex, valueLength);
                    if (replacementValue != null)
                    {
                        Insert(replacementValue, currentSearchIndex);
                        currentSearchIndex += skipReplacementIncrement;
                    }
                }

            }
                       
            return this;
        }

        public string Substring(int start, int length)
        {

            return string.Empty;
        }


        public int IndexOf(string value, int startIndex = 0)
        {

            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value must be a string with non-zero length");
            if (startIndex > Length - 1) throw new ArgumentOutOfRangeException("startIndex must be a value inside the Rope");


            int currentSearchIndex = startIndex;
            int lastSearchIndex = Length - value.Length;
            int valueLength = value.Length;

            for (; currentSearchIndex <= lastSearchIndex; currentSearchIndex++)
            {
                var seekAheadIndex = 0;
                while (seekAheadIndex < valueLength && value[seekAheadIndex] == this[currentSearchIndex + seekAheadIndex])
                {
                    seekAheadIndex++;
                }

                if (seekAheadIndex == valueLength)
                    return currentSearchIndex;

            }

            return -1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Length);
            WriteToStringBuilder(sb);
            return sb.ToString();
        }

        private void WriteToStringBuilder(StringBuilder sb)
        {
            if (IsLeaf) sb.Append(Leaf?.ToString());
            else
            {
                Left.WriteToStringBuilder(sb);
                Right?.WriteToStringBuilder(sb);
            }

        }

        class LeafSection
        {
            public LeafSection(string theBase, int start = 0, int? length = null)
            {
                this.Base = theBase;
                this.Start = start;
                this.Length = length.HasValue ? length.Value : theBase.Length - start;
            }

            public int Length { get; set; }

            public String Base { get; set; }
            public int Start { get; set; }

            public char this[int index]
            {
                get
                {
                    if (index > Length || index < 0) throw new IndexOutOfRangeException($"Index must be between 0 and Length ({Length})");
                    return Base[Start + index];
                }
            }

            public override string ToString()
            {
                return Length != Base.Length ? new string(Base.ToCharArray(), Start, Length) : Base;
            }

        }
    }
}
