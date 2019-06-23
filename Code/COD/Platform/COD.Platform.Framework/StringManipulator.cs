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

                } while ((node = node.Next) != null);

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

        public void Remove(string value, string replaceWith = "")
        {
            int foundLetters = 0;
            bool isFinding = false;
            int globalIndexOf = -1;
            int sectionsToGoBack = 0;

            var node = this.sections.First;
            while (node != null)
            {
                var section = node.Value;

                var foundAt = section.Start;
                var sbase = section.Base;

                if (!isFinding)
                {
                    foundLetters = 0;
                    foundAt = section.Start;

                    if ((foundAt = sbase.IndexOf(value[0], foundAt, section.Length)) > -1)
                    {
                        //found the first character so starting looping through to find the rest
                        isFinding = true;
                        foundLetters++;
                        while (isFinding
                            && foundLetters < value.Length
                            && (foundAt + foundLetters) < section.Start + section.Length)
                        {
                            if (sbase[foundAt + foundLetters] != value[foundLetters])
                            {
                                //swing and a miss. 
                                isFinding = false;
                                foundLetters = 0;
                                foundAt++;
                                break;
                            }

                            foundLetters++;
                        }
                        if (isFinding)
                        {
                            if (foundLetters == value.Length)
                            {
                                var newStart = (foundAt + foundLetters);
                                sections.AddAfter(node, new Section(section.Base, newStart, section.Length - newStart));
                                section.Length = foundAt - section.Start;
                            }
                            else
                            {
                                //must be crossing a section boundary
                                sectionsToGoBack = 1;
                            }
                        }

                    }
                }
                else
                {
                    int pointerInThisSection = section.Start;
                    while (isFinding
                            && foundLetters < value.Length
                            && (pointerInThisSection) < section.Length )
                    {
                        if (sbase[section.Start + pointerInThisSection] != value[foundLetters])
                        {
                            //swing and a miss. 
                            isFinding = false;
                            foundLetters = 0;
                            foundAt++;
                            break;
                        }
                        pointerInThisSection++;
                        foundLetters++;
                    }
                    if (isFinding)
                    {
                        if (foundLetters == value.Length)
                        {
                            //trim the beginning of this sections
                            section.Start += pointerInThisSection;
                            section.Length -= pointerInThisSection;

                            int charsInFirstSection = foundLetters - pointerInThisSection;
                            for (int x = 1; x < sectionsToGoBack; x++)
                            {
                                charsInFirstSection -= node.Previous.Value.Length;
                                sections.Remove(node.Previous);
                            }

                            //trim end of previous node
                            node.Previous.Value.Length -= charsInFirstSection;

                        }
                        else
                        {
                            //must be crossing a section boundary
                            sectionsToGoBack += 1;
                        }
                    }
                }
                node = node.Next;
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
            public int Start { get; set; }

            public override string ToString()
            {
                return new string(Base.ToCharArray(), Start, Length);
            }
        }



    }
}
