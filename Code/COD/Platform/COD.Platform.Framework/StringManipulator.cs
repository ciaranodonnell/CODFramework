using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Framework
{
    public class StringManipulator
    {


        int version = 0;
        Stats myStats = new Stats();
        LinkedList<Section> sections = new LinkedList<Section>();

        public int Length
        {
            get
            {
                var savedLength = GetOrMakeStat((s) => s.Length, MakeAllStats).Value;

                return savedLength;

            }
        }

        void MakeAllStats(Stats stats)
        {
            StringBuilder sb = new StringBuilder(this.Length);
            foreach (Section section in sections)
            {
                sb.Append(section.Base, section.Start, section.Length);
            }

            stats.Value = sb.ToString();
            stats.version = this.version;
            stats.Length = stats.Value.Length;
        }

        public void Add(string value)
        {
            lock (this)
            {
                sections.AddLast(new Section(value));
                version++;
            }
        }

        public void Insert(string value, int position)
        {
            lock (this)
            {
                int positionsSoFar = 0;
                var node = this.sections.First;
                do
                {
                    var section = node.Value;
                    positionsSoFar += section.Length;

                    if (positionsSoFar >= position) break;

                } while (node.Next != null);

                if (positionsSoFar > position)
                {

                    // qwerty|asdfg|
                    var toSplit = node.Value;


                    int lenToSplitPoint = positionsSoFar - position;


                    toSplit.Length -= lenToSplitPoint;
                    var newSplitBit = new Section(toSplit.Base, toSplit.Start + toSplit.Length, lenToSplitPoint);
                    sections.AddAfter(node, newSplitBit);

                    var insertedSection = new Section(value);
                    sections.AddAfter(node, insertedSection);


                }
                else if (positionsSoFar < position)
                {
                    //insert a new section at the end
                    this.Add(value);
                }
                else
                {
                    //cleanly fits between 2 sections
                    var insertedSection = new Section(value);
                    sections.AddAfter(node, insertedSection);


                }
            }

        }

        public override string ToString()
        {
            return GetOrMakeStat<string>((s) => s.Value, MakeAllStats);
        }

        private T GetOrMakeStat<T>(Func<Stats, T> getStat, Action<Stats> makeStat)
        {
            lock (this)
            {
                if (myStats.version != version)
                {
                    myStats = new Stats() { version = version };
                }
                T stat = getStat(myStats);
                if (stat == null)
                {
                    makeStat(myStats);
                    stat = getStat(myStats);
                }
                return stat;


            }
        }




        class Stats
        {
            public int? version = 0;
            public int? Length = 0;
            public string Value = null;
        }




        class Section
        {


            public Section(string theBase, int start = 0, int? length = null)
            {
                this.Base = theBase;
                this.Start = start;
                this.Length = length.HasValue ? length.Value : theBase.Length - start;
            }

            public int Length { get; set; }

            public String Base { get; set; }
            public int Start { get; private set; }

            public override string ToString()
            {
                return new string(Base.ToCharArray(), Start, Length);
            }
        }



    }
}
