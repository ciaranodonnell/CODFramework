using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Framework
{
    public class Rope
    {

        int LeftLengthCache = 0;
        int RightLengthCache = 0;
        private Rope _Right;
        Rope _Left = null;


        public Rope()
        {

        }

        public Rope(string value)
        {

            Leaf = new LeafSection(value, 0, value.Length);
        }

        private Rope Left
        {
            get { return _Left; }
            set
            {
                _Left = value;
                if (value != null)
                {
                    Leaf = null;
                    value.LengthChangedAction = this.LengthChangedHandler;
                    this.LeftLengthCache = value?.Length ?? 0;
                }
                else
                {
                    LeftLengthCache = 0;
                }

                LengthChangedAction?.Invoke(this, Length);
            }
        }
        private Rope Right
        {
            get { return _Right; }
            set
            {
                this._Right = value;
                if (value != null)
                {
                    value.LengthChangedAction = this.LengthChangedHandler;
                    this.RightLengthCache = value.Length;
                }
                else
                {
                    RightLengthCache = 0;
                }

                LengthChangedAction?.Invoke(this, Length);
            }
        }

        private LeafSection Leaf { get; set; }


        public bool IsLeaf { get { return Left == null; } }

        Action<Rope, int> __LengthChangedAction;
        private Action<Rope, int> LengthChangedAction
        {
            get { return __LengthChangedAction; }
            set
            {
                __LengthChangedAction = value;
                value?.Invoke(this, Length);
            }
        }



        public char this[int index]
        {
            get
            {
                if (IsLeaf)
                    return Leaf[index];
                else
                {
                    var leftLen = LeftLength();
                    var totalLen = leftLen + RightLength();
                    if (index < totalLen)
                    {
                        if (index < leftLen)
                            return Left[index];
                        else
                        {
                            if (Right == null) throw new IndexOutOfRangeException();
                            return Right[index - leftLen];
                        }
                    }
                    else
                    {
                        throw new IndexOutOfRangeException($"The index is longer than the rope length ({totalLen})");
                    }
                }
            }
        }

        public (Rope, Rope) Split(int indexToSplitAt)
        {
            Rope other = new Rope();




            return (this, other);
        }





        private int RightLength()
        {
            return RightLengthCache;
            //return Right == null ? 0 : Right.Length;
        }
        private int LeftLength()
        {
            return LeftLengthCache;
            //return Left == null ? 0 : Left.Length;
        }

        public int Length
        {
            get
            {
                if (IsLeaf)
                {
                    return Leaf?.Length ?? 0;
                }
                else
                {
                    return LeftLength() + RightLength();

                }
            }
        }


        public Rope Append(string value)
        {
            Append(new Rope(value));
            return this;
        }



        private void LengthChangedHandler(Rope sender, int newLength)
        {
            if (Object.Equals(sender, Left)) LeftLengthCache = newLength;
            if (Object.Equals(sender, Right)) RightLengthCache = newLength;
            LengthChangedAction?.Invoke(this, LeftLengthCache + RightLengthCache);
        }

        public Rope Append(Rope value)
        {
            if (IsLeaf)
            {
                Left = new Rope { Leaf = Leaf, LengthChangedAction = LengthChangedHandler };
            }
            if (Right == null)
            {

                Right = value;
            }
            else
            {
                Rope newTop = new Rope { Left = Left, Right = Right };
                newTop.Left.LengthChangedAction = newTop.LengthChangedHandler;
                newTop.Right.LengthChangedAction = newTop.LengthChangedHandler;
                newTop.LengthChangedAction = LengthChangedHandler;
                Left = newTop;
                value.LengthChangedAction = LengthChangedHandler;
                Right = value;
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
                    Right = new Rope { Leaf = Leaf, LengthChangedAction = LengthChangedHandler };
                    Left = new Rope(value) { LengthChangedAction = LengthChangedHandler };
                }
                else if (position == Length)
                {
                    //Insert after this
                    Left = new Rope { Leaf = Leaf, LengthChangedAction = LengthChangedHandler };
                    Right = new Rope(value) { LengthChangedAction = LengthChangedHandler };
                }
                else
                {
                    //its in the middle of this bit
                    var newLeft = new Rope();
                    var newLeftLen = position;
                    newLeft.Left = new Rope { Leaf = new LeafSection(Leaf.Base, Leaf.Start, newLeftLen), LengthChangedAction = LengthChangedHandler };
                    newLeft.Right = new Rope(value) { LengthChangedAction = LengthChangedHandler };
                    var newRightStart = Leaf.Start + newLeftLen;
                    Right = new Rope { Leaf = new LeafSection(Leaf.Base, newRightStart, Leaf.Length - newRightStart), LengthChangedAction = LengthChangedHandler };
                    Left = newLeft;

                }
            }
            else
            {
                var leftLen = LeftLength();
                if (position < leftLen)
                {
                    Left.Insert(value, position);
                }
                else
                {
                    if (Right == null)
                    {
                        Right = new Rope(value) { LengthChangedAction = LengthChangedHandler };
                    }
                    else
                    {
                        Right.Insert(value, position - leftLen);
                    }
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
                    var newRight = new Rope { Leaf = new LeafSection(Leaf.Base, Leaf.Start + start + length, Leaf.Length - (start + length)), LengthChangedAction = LengthChangedHandler };
                    var newLeft = new Rope { Leaf = new LeafSection(Leaf.Base, Leaf.Start, start), LengthChangedAction = LengthChangedHandler };
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
                LengthChangedAction?.Invoke(this, Length);
            }
            else
            {
                var leftLen = LeftLength();
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

        public Rope Replace(string valueToRemove, string replacementValue, int startIndex = 0)
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
                    //the string is now shorter so the last find position comes down
                    lastSearchIndex -= valueLength;

                    if (replacementValue != null)
                    {
                        Insert(replacementValue, currentSearchIndex);
                        //need to start searching after what we inserted incase it matches the valueToRemove, we'd replace infinitely
                        currentSearchIndex += skipReplacementIncrement;
                        //also we made the string longer so the last search position moves out again
                        lastSearchIndex += replacementValue.Length;

                    }

                    //we removed the letter we were on, therefore we need to rewind a character to handle two consecutive matches
                    currentSearchIndex--;
                }

            }

            return this;
        }

        public string Substring(int start, int length)
        {
            StringBuilder sb = new StringBuilder(length);
            WriteToStringBuilder(sb, start, length);

            return sb.ToString();
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

        private void WriteToStringBuilder(StringBuilder sb, int start = 0)
        {
            if (IsLeaf) sb.Append(Leaf?.ToString());
            else
            {
                Left.WriteToStringBuilder(sb);
                Right?.WriteToStringBuilder(sb);
            }

        }

        private void WriteToStringBuilder(StringBuilder sb, int start, int length)
        {
            if (IsLeaf)
            {
                sb.Append(Leaf.Base.Substring(Leaf.Start + start, length));
            }
            else
            {
                var leftLen = LeftLength();
                if (start < leftLen)
                {
                    var lenInLeft = Math.Min(leftLen - start, length);
                    Left.WriteToStringBuilder(sb, start, lenInLeft);
                    if (lenInLeft < length)
                        Right.WriteToStringBuilder(sb, 0, length - lenInLeft);
                }
                else
                {
                    Right.WriteToStringBuilder(sb, start - leftLen, length);

                }
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
                    if (index > Length || index < 0) throw new IndexOutOfRangeException($"Index ({index}) must be between 0 and Length ({Length})");
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
